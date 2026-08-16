using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

public static class AutoLoginManager
{
    private static readonly string _filePath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "WarriorAndWealth", "autologin.dat");

    public class DatiAutoLogin  // ora public, non più private, serve accedervi da fuori
    {
        public bool AutoLogin { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string Lingua { get; set; }
        public string ServerId { get; set; }
    }

    public static void Salva(DatiAutoLogin dati)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);
        byte[] plainBytes = JsonSerializer.SerializeToUtf8Bytes(dati);
        byte[] encryptedBytes = ProtectedData.Protect(plainBytes, null, DataProtectionScope.CurrentUser);
        File.WriteAllBytes(_filePath, encryptedBytes);
    }

    public static bool TryCarica(out DatiAutoLogin dati)
    {
        dati = null;
        if (!File.Exists(_filePath)) return false;

        try
        {
            byte[] encryptedBytes = File.ReadAllBytes(_filePath);
            byte[] plainBytes = ProtectedData.Unprotect(encryptedBytes, null, DataProtectionScope.CurrentUser);
            dati = JsonSerializer.Deserialize<DatiAutoLogin>(plainBytes);
            return dati != null;
        }
        catch
        {
            return false;
        }
    }

    public static void Elimina()
    {
        if (File.Exists(_filePath)) File.Delete(_filePath);
    }
}