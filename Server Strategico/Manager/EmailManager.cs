using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Strategico_V2.Manager
{
    /// <summary>
    /// Gestisce l'invio di email tramite Resend API (https://resend.com).
    /// Nessuna dipendenza SMTP — usa HttpClient verso l'endpoint REST.
    /// </summary>
    public static class EmailManager
    {
        // ── Configurazione Resend ──────────────────────────────────────────────
        private const string API_KEY = "re_PZQ2pUVo_9gXESpMJjcP1H2HXLeMGVi6J"; // Dashboard → API Keys
        private const string SENDER_EMAIL = "onboarding@resend.dev";              // Dominio verificato su Resend
        private const string SENDER_NAME = "Warrior and Wealth";
        private const string RESEND_URL = "https://api.resend.com/emails";

        // HttpClient statico — riutilizzato per tutta la vita del server
        private static readonly HttpClient _http = CreateClient();

        private static HttpClient CreateClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", API_KEY);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        // ── Template base ──────────────────────────────────────────────────────
        // (identico a prima — nessuna modifica al template HTML)
        private const string HTML_TEMPLATE = @"
            <!DOCTYPE html>
            <html>
            <head>
              <meta charset='UTF-8'>
              <style>
                body      {{ font-family: Georgia, serif; background: #1a1208; margin: 0; padding: 20px; }}
                .container{{ max-width: 600px; margin: auto; background: #2d1f0e;
                             border: 2px solid #8b6914; border-radius: 8px; overflow: hidden; }}
                .header   {{ background: #3d2b0a; padding: 24px; text-align: center;
                             border-bottom: 2px solid #8b6914; }}
                .header h1{{ color: #d4a843; margin: 0; font-size: 26px; letter-spacing: 2px; }}
                .header p {{ color: #a07830; margin: 6px 0 0; font-size: 13px; }}
                .body     {{ padding: 30px 36px; color: #d8c9a0; line-height: 1.7; }}
                .body h2  {{ color: #d4a843; margin-top: 0; }}
                .code-box {{ background: #1a1208; border: 1px solid #8b6914; border-radius: 6px;
                             padding: 16px; text-align: center; margin: 24px 0; }}
                .code-box span {{ font-size: 32px; font-weight: bold; color: #f0c040;
                                  letter-spacing: 6px; font-family: monospace; }}
                .footer   {{ background: #1e1508; padding: 16px; text-align: center;
                             color: #6b5030; font-size: 12px; border-top: 1px solid #4a3510; }}
                .warning  {{ color: #c07020; font-size: 13px; margin-top: 16px; }}
              </style>
            </head>
            <body>
              <div class='container'>
                <div class='header'>
                  <h1>⚔️ Warrior and Wealth ⚔️</h1>
                  <p>Il gioco di strategia medievale</p>
                </div>
                <div class='body'>
                  {BODY_CONTENT}
                </div>
                <div class='footer'>
                  © Warrior and Wealth — Email automatica, non rispondere a questo messaggio.
                </div>
              </div>
            </body>
            </html>";

        // ══════════════════════════════════════════════════════════════════════
        // METODO PRINCIPALE — invio generico via Resend REST API
        // ══════════════════════════════════════════════════════════════════════

        public static async Task<bool> SendEmailAsync(
            string toEmail,
            string toName,
            string subject,
            string htmlBody)
        {
            try
            {
                var html = HTML_TEMPLATE.Replace("{BODY_CONTENT}", htmlBody);

                // Payload JSON per Resend
                var payload = new
                {
                    from = $"{SENDER_NAME} <{SENDER_EMAIL}>",
                    to = new[] { toEmail },
                    subject = subject,
                    html = html
                };

                var json = JsonSerializer.Serialize(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _http.PostAsync(RESEND_URL, content);
                var body = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    Log($"[EMAIL] Inviata a {toEmail} — {subject}");
                    return true;
                }
                else
                {
                    Log($"[EMAIL][ERRORE] HTTP {(int)response.StatusCode}: {body}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log($"[EMAIL][ERRORE] {ex.Message}");
                return false;
            }
        }

        // ══════════════════════════════════════════════════════════════════════
        // RECUPERO PASSWORD — OTP (identico a prima)
        // ══════════════════════════════════════════════════════════════════════

        public static async Task<string?> SendPasswordRecoveryAsync(
            string toEmail,
            string username,
            string code)
        {
            var subject = "⚔️ Warrior and Wealth — Recupero Password";

            var body = $@"
                <h2>Recupero Password</h2>
                <p>Ciao <strong>{EscapeHtml(username)}</strong>,</p>
                <p>Hai richiesto il recupero della tua password per accedere al regno di <em>Warrior and Wealth</em>.</p>
                <p>Usa il seguente codice per reimpostare la tua password:</p>
                <div class='code-box'>
                  <span>{code}</span>
                </div>
                <p>Il codice è valido per <strong>15 minuti</strong>.</p>
                <p class='warning'>⚠️ Se non hai richiesto tu questo recupero, ignora questa email.
                Il tuo account è al sicuro.</p>";

            var ok = await SendEmailAsync(toEmail, username, subject, body);
            return ok ? code : null;
        }

        // ══════════════════════════════════════════════════════════════════════
        // BENVENUTO
        // ══════════════════════════════════════════════════════════════════════

        public static Task<bool> SendWelcomeAsync(string toEmail, string username)
        {
            var subject = "⚔️ Benvenuto in Warrior and Wealth!";
            var body = $@"
                <h2>Benvenuto, {EscapeHtml(username)}!</h2>
                <p>Il tuo account è stato creato con successo.</p>
                <p>Preparati a costruire il tuo regno, reclutare eserciti e dominare le terre medievali.</p>
                <p>Buona fortuna, valoroso condottiero! ⚔️</p>";

            return SendEmailAsync(toEmail, username, subject, body);
        }

        // ══════════════════════════════════════════════════════════════════════
        // NOTIFICA LOGIN
        // ══════════════════════════════════════════════════════════════════════

        public static Task<bool> SendLoginAlertAsync(
            string toEmail,
            string username,
            string ipAddress)
        {
            var subject = "⚠️ Warrior and Wealth — Nuovo Accesso Rilevato";
            var body = $@"
                <h2>Accesso al tuo account</h2>
                <p>Ciao <strong>{EscapeHtml(username)}</strong>,</p>
                <p>Abbiamo rilevato un accesso al tuo account da:</p>
                <div class='code-box'>
                  <span style='font-size:20px'>{EscapeHtml(ipAddress)}</span>
                </div>
                <p>Data e ora: <strong>{DateTime.UtcNow:dd/MM/yyyy HH:mm} UTC</strong></p>
                <p class='warning'>⚠️ Se non sei stato tu, contatta immediatamente il supporto
                e cambia la tua password.</p>";

            return SendEmailAsync(toEmail, username, subject, body);
        }

        // ══════════════════════════════════════════════════════════════════════
        // UTILITÀ PRIVATE
        // ══════════════════════════════════════════════════════════════════════

        private static string EscapeHtml(string s) =>
            s.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;");

        private static void Log(string msg) =>
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {msg}");
    }
}