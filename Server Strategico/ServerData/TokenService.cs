using System;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;

namespace WarriorAndWealth.Server.Auth
{
    /// <summary>
    /// Esito della validazione di un access token.
    /// </summary>
    public class TokenValidationResult
    {
        public bool EValido { get; set; }
        public bool Scaduto { get; set; }
        public string Username { get; set; }
    }

    /// <summary>
    /// Informazioni sul refresh token di un utente (stateful, revocabile).
    /// </summary>
    internal class RefreshTokenInfo
    {
        public string Token { get; set; }
        public DateTime Scadenza { get; set; }
    }

    /// <summary>
    /// Gestisce ciclo di vita di access token (HMAC-SHA256, stateless)
    /// e refresh token (random, stateful in memoria).
    /// </summary>
    public static class TokenService
    {
        // In produzione: leggere da variabile d'ambiente o secret manager,
        // MAI hardcoded nel repo. Se cambia, tutti i token esistenti diventano invalidi.
        private static readonly string SecretKey =
            Environment.GetEnvironmentVariable("WAW_TOKEN_SECRET") ?? "chiave-di-sviluppo-CAMBIAMI";

        private static readonly TimeSpan AccessTokenDurata = TimeSpan.FromMinutes(20);
        private static readonly TimeSpan RefreshTokenDurata = TimeSpan.FromDays(7);

        // username -> refresh token info
        private static readonly ConcurrentDictionary<string, RefreshTokenInfo> _refreshTokens = new();

        // ---------------------------------------------------------------
        // ACCESS TOKEN
        // ---------------------------------------------------------------

        /// <summary>
        /// Genera un nuovo access token per l'utente. Formato: base64(payload).base64(firma)
        /// </summary>
        public static string GeneraAccessToken(string username)
        {
            long scadenzaUnix = DateTimeOffset.UtcNow.Add(AccessTokenDurata).ToUnixTimeSeconds();
            string payload = $"{username}|{scadenzaUnix}";
            string firma = Firma(payload);

            string payloadB64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(payload));
            string firmaB64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(firma));

            return $"{payloadB64}.{firmaB64}";
        }

        /// <summary>
        /// Valida un access token: verifica firma e scadenza.
        /// Se scaduto, restituisce comunque lo username (utile per guidare il client verso il refresh).
        /// </summary>
        public static TokenValidationResult ValidaAccessToken(string token)
        {
            var risultato = new TokenValidationResult { EValido = false };

            if (string.IsNullOrWhiteSpace(token) || !token.Contains('.'))
                return risultato;

            var parti = token.Split('.');
            if (parti.Length != 2)
                return risultato;

            try
            {
                string payload = Encoding.UTF8.GetString(Convert.FromBase64String(parti[0]));
                string firmaRicevuta = Encoding.UTF8.GetString(Convert.FromBase64String(parti[1]));

                string firmaAttesa = Firma(payload);
                if (!FirmeUguali(firmaRicevuta, firmaAttesa))
                    return risultato; // token manomesso o firmato con altra chiave

                var campi = payload.Split('|');
                if (campi.Length != 2)
                    return risultato;

                string username = campi[0];
                long scadenzaUnix = long.Parse(campi[1]);
                DateTimeOffset scadenza = DateTimeOffset.FromUnixTimeSeconds(scadenzaUnix);

                if (DateTimeOffset.UtcNow > scadenza)
                {
                    risultato.Scaduto = true;
                    risultato.Username = username;
                    return risultato;
                }

                risultato.EValido = true;
                risultato.Username = username;
                return risultato;
            }
            catch
            {
                return risultato; // token malformato
            }
        }

        // ---------------------------------------------------------------
        // REFRESH TOKEN
        // ---------------------------------------------------------------

        /// <summary>
        /// Genera un nuovo refresh token per l'utente, sovrascrivendo eventuali precedenti
        /// (login su un nuovo dispositivo invalida quelli vecchi, se preferisci un solo dispositivo attivo).
        /// </summary>
        public static string GeneraRefreshToken(string username)
        {
            byte[] buffer = new byte[32];
            RandomNumberGenerator.Fill(buffer);
            string refreshToken = Convert.ToBase64String(buffer);

            _refreshTokens[username] = new RefreshTokenInfo
            {
                Token = refreshToken,
                Scadenza = DateTime.UtcNow.Add(RefreshTokenDurata)
            };

            return refreshToken;
        }

        /// <summary>
        /// Verifica che il refresh token fornito corrisponda a quello salvato e non sia scaduto.
        /// </summary>
        public static bool ValidaRefreshToken(string username, string refreshToken)
        {
            if (!_refreshTokens.TryGetValue(username, out var info))
                return false;

            if (info.Token != refreshToken)
                return false;

            if (DateTime.UtcNow > info.Scadenza)
            {
                _refreshTokens.TryRemove(username, out _);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Revoca il refresh token di un utente (logout, sospensione account, cambio password...).
        /// </summary>
        public static void RevocaRefreshToken(string username)
        {
            _refreshTokens.TryRemove(username, out _);
        }

        // ---------------------------------------------------------------
        // HELPER
        // ---------------------------------------------------------------

        private static string Firma(string payload)
        {
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(SecretKey));
            byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Confronto a tempo costante, per non esporre il servizio a timing attack sulla firma.
        /// </summary>
        private static bool FirmeUguali(string a, string b)
        {
            if (a.Length != b.Length) return false;
            int diff = 0;
            for (int i = 0; i < a.Length; i++)
                diff |= a[i] ^ b[i];
            return diff == 0;
        }
    }
}