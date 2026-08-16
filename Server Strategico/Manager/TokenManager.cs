using Server_Strategico.ServerData.Moduli;
using System.Text;
using System.Text.Json;

namespace Server_Strategico.Manager
{
    public static class TokenManager
    {
        private static readonly byte[] SecretKey = LoadSecretKey();
        private static readonly System.Collections.Concurrent.ConcurrentDictionary<string, (string Email, string Username, DateTimeOffset Expiry)> RefreshStore = new();
        private static readonly string RefreshStorePath = Path.Combine(GameSave.SavePath, "token.json");

        private class RefreshTokenRecord
        {
            public string Email { get; set; }
            public string Username { get; set; }
            public DateTimeOffset Expiry { get; set; }
        }

        private static byte[] LoadSecretKey()
        {
            string keyPath = OperatingSystem.IsLinux() ? "/opt/warriorandwealth/secret.key" : "secret.key";
            if (!File.Exists(keyPath))
            {
                var key = System.Security.Cryptography.RandomNumberGenerator.GetBytes(32);
                File.WriteAllBytes(keyPath, key);
                return key;
            }
            return File.ReadAllBytes(keyPath);
        }

        // ---- Access Token (stateless, HMAC, 6h) ----
        public static string GenerateAccessToken(string Email, string username, TimeSpan validity)
        {
            long expiry = DateTimeOffset.UtcNow.Add(validity).ToUnixTimeSeconds();
            string payload = $"{username}|{expiry}";
            byte[] payloadBytes = Encoding.UTF8.GetBytes(payload);

            using var hmac = new System.Security.Cryptography.HMACSHA256(SecretKey);
            byte[] signature = hmac.ComputeHash(payloadBytes);

            return $"{Convert.ToBase64String(payloadBytes)}.{Convert.ToBase64String(signature)}";
        }

        public static bool ValidateAccessToken(string token, out string username, out bool isExpired)
        {
            username = null;
            isExpired = false;

            if (string.IsNullOrWhiteSpace(token)) return false;
            var parts = token.Split('.');
            if (parts.Length != 2) return false;

            try
            {
                byte[] payloadBytes = Convert.FromBase64String(parts[0]);
                byte[] expectedSig = Convert.FromBase64String(parts[1]);

                using var hmac = new System.Security.Cryptography.HMACSHA256(SecretKey);
                byte[] actualSig = hmac.ComputeHash(payloadBytes);

                if (!System.Security.Cryptography.CryptographicOperations.FixedTimeEquals(expectedSig, actualSig))
                    return false; // firma errata → invalido

                var fields = Encoding.UTF8.GetString(payloadBytes).Split('|');
                if (fields.Length != 2 || !long.TryParse(fields[1], out long expiry))
                    return false; // payload corrotto → invalido

                // Controllo scadenza
                if (DateTimeOffset.UtcNow.ToUnixTimeSeconds() > expiry)
                {
                    username = fields[0]; // (opzionale) sappiamo chi è scaduto
                    isExpired = true;
                    return false; // scaduto
                }

                username = fields[0];
                return true; // valido
            }
            catch { return false; }
        }

        // ---- Refresh Token (stateful, random, 30gg, revocabile) ----
        public static string GenerateRefreshToken(string email, string username, TimeSpan validity)
        {
            string token = Convert.ToBase64String(System.Security.Cryptography.RandomNumberGenerator.GetBytes(32));
            RefreshStore[token] = (email, username, DateTimeOffset.UtcNow.Add(validity));
            return token;
        }

        public static bool ValidateRefreshToken(string refreshToken, out string username, out bool isExpired)
        {
            username = null;
            isExpired = false;

            if (string.IsNullOrWhiteSpace(refreshToken)) return false;

            if (!RefreshStore.TryGetValue(refreshToken, out var entry))
                return false; // token non trovato: mai esistito, già usato/ruotato, o revocato

            if (DateTimeOffset.UtcNow > entry.Expiry)
            {
                isExpired = true;
                RefreshStore.TryRemove(refreshToken, out _); // pulizia, non serve più
                return false;
            }

            username = entry.Username;
            return true;
        }

        public static void RevokeRefreshToken(string refreshToken) => RefreshStore.TryRemove(refreshToken, out _);

        // Utile per "logout da tutti i dispositivi" o ban immediato
        public static void RevokeAllRefreshTokensForUser(string email)
        {
            foreach (var kv in RefreshStore)
                if (kv.Value.Email == email)
                    RefreshStore.TryRemove(kv.Key, out _);
        }

        public static async Task SaveRefreshTokens()
        {
            try
            {
                var toSave = RefreshStore.ToDictionary(
                    kv => kv.Key,
                    kv => new RefreshTokenRecord
                    {
                        Email = kv.Value.Email,
                        Username = kv.Value.Username,
                        Expiry = kv.Value.Expiry
                    });

                // Scrittura atomica: prima su file temporaneo, poi replace
                string tempPath = RefreshStorePath + ".tmp";

                await using (var fs = new FileStream(tempPath, FileMode.Create, FileAccess.Write, FileShare.None, 65536, true))
                {
                    await JsonSerializer.SerializeAsync(fs, toSave, new JsonSerializerOptions { WriteIndented = true });
                }

                File.Move(tempPath, RefreshStorePath, overwrite: true);

                Console.WriteLine($"[TokenStore] Salvati {toSave.Count} refresh token");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TokenStore] Errore durante il salvataggio: {ex.Message}");
            }
        }
        public static async Task LoadRefreshTokens()
        {
            try
            {
                if (!File.Exists(RefreshStorePath))
                {
                    Console.WriteLine("[TokenStore] Nessun file refresh token trovato, parto vuoto");
                    return;
                }

                await using var fs = new FileStream(RefreshStorePath, FileMode.Open, FileAccess.Read, FileShare.Read, 65536, true);
                var loaded = await JsonSerializer.DeserializeAsync<Dictionary<string, RefreshTokenRecord>>(fs);

                if (loaded == null) return;

                int scartati = 0;
                foreach (var kv in loaded)
                {
                    // Scarta token già scaduti durante il downtime del server
                    if (DateTimeOffset.UtcNow > kv.Value.Expiry)
                    {
                        scartati++;
                        continue;
                    }

                    RefreshStore[kv.Key] = (kv.Value.Email, kv.Value.Username, kv.Value.Expiry);
                }

                Console.WriteLine($"[TokenStore] Caricati {RefreshStore.Count} refresh token ({scartati} scaduti scartati)");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TokenStore] Errore durante il caricamento: {ex.Message}");
            }
        }
    }
}