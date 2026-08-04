using System.Text;

namespace Server_Strategico.Manager
{
    public static class TokenManager
    {
        private static readonly byte[] SecretKey = LoadSecretKey();

        // refreshToken -> (username, scadenza). In RAM ora; volendo persistibile su file/DB.
        private static readonly System.Collections.Concurrent.ConcurrentDictionary<string, (string Email, string Username, DateTimeOffset Expiry)> RefreshStore
            = new();

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

        public static bool ValidateAccessToken(string token, out string username)
        {
            username = null;
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
                    return false;

                var fields = Encoding.UTF8.GetString(payloadBytes).Split('|');
                if (fields.Length != 2 || !long.TryParse(fields[1], out long expiry)) return false;
                if (DateTimeOffset.UtcNow.ToUnixTimeSeconds() > expiry) return false;

                username = fields[0];
                return true;
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

        public static bool TryUseRefreshToken(string refreshToken, out string username)
        {
            username = null;
            if (!RefreshStore.TryGetValue(refreshToken, out var entry)) return false;
            if (DateTimeOffset.UtcNow > entry.Expiry)
            {
                RefreshStore.TryRemove(refreshToken, out _);
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
    }
}