using Newtonsoft.Json;
using Server_Strategico.ServerData.Moduli.Battaglie;
using System.Globalization;
using System.Text;
using System.Text.Json;
using Warrior_and_Wealth;
using WatsonTcp;
using static Strategico_V2.Variabili_Client;
using static System.Runtime.InteropServices.JavaScript.JSType;
using JsonSerializer = System.Text.Json.JsonSerializer;
using String = System.String;

namespace Strategico_V2
{
    public class ClientConnection
    {
        public class QuestUpdatePacket
        {
            public string? Type { get; set; }
            public List<ClientQuestData>? Quests { get; set; }
        }
        public class ClientQuestData
        {
            public int Id { get; set; }
            public string? Quest_Description { get; set; }
            public int Experience { get; set; }
            public int Require { get; set; }
            public int Max_Complete { get; set; }
            public int Progress { get; set; }
            public int Completata { get; set; }
        }

        public static string argomento_Invio = "";
        public static string argomento_Ricevuto = "";
        public static bool client_Connesso = false;

        internal class TestClient
        {
            //public static string _ServerIp = "warriorandwealth.duckdns.org";
            public static string _ServerIp = "localhost";
            private static int _ServerPort = 8443;
            private static bool _Ssl = false;
            private static string _CertFile = "";
            private static string _CertPass = "Password1";
            private static bool _DebugMessages = true;
            private static bool _AcceptInvalidCerts = true;
            private static bool _MutualAuth = false;
            public static WatsonTcpClient? _Client = null;
            private static string? _PresharedKey = null;


            public static Task InitializeClient()
            {
                return Task.Run(async () =>
                {
                    Console.WriteLine("Client partito");
                    Console.WriteLine($"Use SSL: {_Ssl}");

                    if (_Ssl)
                    {
                        bool supplyCert = true;
                        Console.WriteLine($"Supply SSL certificate: {supplyCert}");

                        if (supplyCert)
                        {
                            _CertFile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + $@"/Documents/Client.pfx";
                            _CertPass = "Password1";
                        }

                        _AcceptInvalidCerts = true;
                        _MutualAuth = true;
                        Console.WriteLine($"Accept invalid certs: {_AcceptInvalidCerts}");
                        Console.WriteLine($"Mutually authenticate: {_MutualAuth}");
                    }
                    await ConnectClient();
                });
            }
            public static Task ConnectClient()
            {
                return Task.Run(() =>
                {
                    try
                    {
                        if (_Client != null) _Client.Dispose();
                        if (!_Ssl) _Client = new WatsonTcpClient(_ServerIp, _ServerPort);
                        else
                        {
                            _Client = new WatsonTcpClient(_ServerIp, _ServerPort, _CertFile, _CertPass);
                            _Client.Settings.AcceptInvalidCertificates = _AcceptInvalidCerts;
                            _Client.Settings.MutuallyAuthenticate = _MutualAuth;
                        }

                        _Client.Events.ServerConnected += ServerConnected;
                        _Client.Events.ServerDisconnected += ServerDisconnected;
                        _Client.Events.MessageReceived += MessageReceived;
                        _Client.Events.ExceptionEncountered += ExceptionEncountered;
                        _Client.Events.AuthenticationFailure += AuthenticationFailure;
                        _Client.Events.AuthenticationSucceeded += AuthenticationSucceeded;
                        _Client.Callbacks.AuthenticationRequested = AuthenticationRequested;
                        _Client.Settings.DebugMessages = _DebugMessages;
                        _Client.Settings.Logger = Logger;
                        _Client.Settings.NoDelay = true;
                        _Client.Keepalive.EnableTcpKeepAlives = true;
                        _Client.Keepalive.TcpKeepAliveInterval = 1;
                        _Client.Keepalive.TcpKeepAliveTime = 1;
                        _Client.Keepalive.TcpKeepAliveRetryCount = 3;

                        _Client.Connect();

                        client_Connesso = true;
                        Send("Connesso");
                    }
                    catch (Exception ex)
                    {
                        client_Connesso = false;
                        Console.WriteLine($"Connessione fallita: {ex.Message}");
                    }
                });
            }
            public static void Send(string messaggio)
            {
                _Client.SendAsync(messaggio);
            }
            private static void ExceptionEncountered(object sender, ExceptionEventArgs e)
            {
                Console.WriteLine("*** Exception ***");
                Console.WriteLine(e.ToString());
            }
            private static string AuthenticationRequested()
            {
                // return "0000000000000000";
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("Server requests authentication");
                Console.WriteLine("Press ENTER and THEN enter your preshared key");
                if (String.IsNullOrEmpty(_PresharedKey)) _PresharedKey = _CertPass;
                return _PresharedKey;
            }
            private static void ServerConnected(object sender, ConnectionEventArgs args) // Controlla se c'è una connessione col server
            {
                Console.WriteLine("Server connected");
                client_Connesso = true;
            }
            private static void ServerDisconnected(object sender, DisconnectionEventArgs args)
            {
                Console.WriteLine("Server disconnected: " + args.Reason.ToString());
                client_Connesso = false;
            }
            private static void Logger(Severity sev, string msg)
            {
                Console.WriteLine("[" + sev.ToString().PadRight(9) + "] " + msg);
            }

            private static void AuthenticationSucceeded(object sender, EventArgs args)
            {
                Console.WriteLine("Authentication succeeded");
            }
            private static void AuthenticationFailure(object sender, EventArgs args)
            {
                Console.WriteLine("Authentication failed");
            }
            private static void MessageReceived(object sender, MessageReceivedEventArgs args)
            {
                Console.Write("Message from server: ");
                if (args.Data == null)
                {
                    Console.WriteLine("[null]");
                    return;
                }
                string messaggio = Encoding.UTF8.GetString(args.Data);

                Console.WriteLine("Messaggio Ricevuto");
                Console.WriteLine("Ricevuto: " + messaggio);

                ClientMessageHandlers.Quest(messaggio);
                ClientMessageHandlers.AggiornaVillaggiDalServer(messaggio);
                Update_Report(messaggio);

                string[]? mess = null;
                if (messaggio.Contains('|'))
                {
                    mess = messaggio.Split('|');
                    switch (mess[0])
                    {
                        case "TOKEN_SCADUTO":
                            if (Variabili_Client.refresh_Token != null)
                                ComandiInvio.Refresh_AccessToken();
                            break;
                        case "Refresh_Access_Token":
                            Variabili_Client.access_Token = mess[1];
                            break;
                        case "Login":
                            if (mess[1] == "true")
                            {
                                Variabili_Client.Utente.User_Login = true;
                                Variabili_Client.access_Token = mess[2];
                                Variabili_Client.refresh_Token = mess[3];
                            } 
                            else
                            {
                                Variabili_Client.Utente.User_Login = false;
                                Login.login_data = mess[2]; ///Cos'è sta roba? è il messaggio di errore del login, se c'è
                            }
                            break;
                        case "Update_Data": ClientMessageHandlers.Update_Data(mess); break;
                        case "Log_Server": Update_Log(mess[1]); break;
                        case "Update_PVP_Player": ClientMessageHandlers.Update_PVP_List(mess); break;
                        case "Descrizione": ClientMessageHandlers.Update_Desc(mess[1], mess[2]); break;
                        case "Raduno": ClientMessageHandlers.Update_Lista_Raduni(mess); break;
                        case "Raduni_Player": ClientMessageHandlers.Update_Lista_Raduni_Player(mess); break;
                        case "RadunoPartecipo":
                            ClientMessageHandlers.Update_Raduni_Partecipazione(mess);
                            break;
                        case "Tutorial":
                            if (mess[1] == "Dati")
                            {
                                var dati = JsonSerializer.Deserialize<List<dati>>(mess[2]);
                                Variabili_Client.tutorial_dati = dati;
                            }
                            break;
                        case "Gamepass_Premi":
                            for (int i = 1; i < mess.Count(); i++)
                                Variabili_Client.GamePass_Premi[i-1] = mess[i];
                            break;
                        case "Gamepass_Premi_Ottenuti":
                            for (int i = 1; i < mess.Count(); i++)
                                Variabili_Client.GamePass_Premi_Completati[i - 1] = Convert.ToBoolean(mess[i]);
                            break;

                        default: Console.WriteLine($"[Errore] >> [{messaggio}] Comando non riconosciuto"); break;
                    }
                    Console.WriteLine("");
                    Console.WriteLine("-----------------------------");
                    Console.WriteLine($"Comando:        {mess[0]}");
                    Console.WriteLine("-----------------------------");
                    Console.WriteLine("");
                }

            }
            public static void Update_Report(string messaggio)
            {
                if (!messaggio.Contains("Report")) return;

                var mess = messaggio.Split('|');
                switch (mess[1])
                {
                    case "Report_Lista":   // al login, lista completa
                        Variabili_Client.Report = JsonConvert.DeserializeObject<List<Battaglia.Report>>(mess[2]);
                        break;

                    case "Report_Nuovo":   // durante il gioco, solo il nuovo
                        Variabili_Client.Report.Add(JsonConvert.DeserializeObject<Battaglia.Report>(mess[2]));
                        break;

                    case "Report_Aperto":  // solo aggiorna il flag
                        int index = int.Parse(mess[2]);
                        Variabili_Client.Report[index].Aperto = true;
                        break;

                }
            }
            static void Update_Log(string mes)
            {
                Gioco.Log_Update(mes);
            }
        }

    }
}
