using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

public static class AutoLoginManager
{
    private static readonly string _filePath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "WarriorAndWealth", "autologin.dat");

    private class DatiAutoLogin
    {
        public bool AutoLogin { get; set; }
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
    }

    public static void Salva(bool AutoLogin, string accessToken, string refreshToken)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);

        var dati = new DatiAutoLogin { AutoLogin = AutoLogin, accessToken = accessToken, refreshToken = refreshToken };
        byte[] plainBytes = JsonSerializer.SerializeToUtf8Bytes(dati);

        byte[] encryptedBytes = ProtectedData.Protect(plainBytes, null, DataProtectionScope.CurrentUser);
        File.WriteAllBytes(_filePath, encryptedBytes);
    }

    public static bool TryCarica(out bool AutoLogin, out string accessToken, out string refreshToken)
    {
        accessToken = null;
        refreshToken = null;
        AutoLogin = false;

        if (!File.Exists(_filePath)) return false;

        try
        {
            byte[] encryptedBytes = File.ReadAllBytes(_filePath);
            byte[] plainBytes = ProtectedData.Unprotect(encryptedBytes, null, DataProtectionScope.CurrentUser);
            var dati = JsonSerializer.Deserialize<DatiAutoLogin>(plainBytes);

            AutoLogin = dati.AutoLogin;
            accessToken = dati.accessToken;
            refreshToken = dati.refreshToken;
            return true;
        }
        catch
        {
            // File corrotto o cifrato da un altro utente/macchina: lo elimino e forzo login manuale
            Elimina();
            return false;
        }
    }

    public static void Elimina()
    {
        if (File.Exists(_filePath)) File.Delete(_filePath);
    }
}