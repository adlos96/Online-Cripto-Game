using Strategico_V2;
using Warrior_and_Wealth.Strumenti;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Warrior_and_Wealth
{
    public partial class Login : Form
    {
        public static string login_data = "";
        static bool avviso_Aggiornamento = false;
        string lingua_Selezionata = "ITA";
        public Login()
        {
            InitializeComponent();
        }

        private void Gioco_Load(object sender, EventArgs e)
        {
            GameAudio.PlayMenuMusic("Login");
            MusicManager.SetVolume(0.3f);

            this.ActiveControl = Btn_Login; // assegna il focus al bottone
            panel1.BackColor = Color.FromArgb(100, 229, 208, 181);
            banner_1.BackColor = Color.FromArgb(100, 229, 208, 181);
            banner_2.BackColor = Color.FromArgb(100, 229, 208, 181);
            banner_1.BringToFront();
            banner_2.BringToFront();


            lbl_Titolo.Font = new Font("Cinzel Decorative", 12, FontStyle.Bold);
            lbl_Username_Login.Font = new Font("Cinzel Decorative", 9, FontStyle.Regular);
            lbl_Password_Login.Font = new Font("Cinzel Decorative", 9, FontStyle.Regular);

            // btn_Login
            Btn_Login.BackColor = Color.FromArgb(100, 229, 208, 181);
            Btn_Login.Font = new Font("Old English Text MT", 9, FontStyle.Bold);

            // Btn_New_Game
            Btn_New_Game.Font = new Font("Cinzel Decorative", 9, FontStyle.Bold);
            Btn_New_Game.BackColor = Color.FromArgb(100, 229, 208, 181);

            // txt_Email
            txt_Email.BackColor = Color.FromArgb(229, 208, 181);
            txt_Email.Text = "Inserisci Email";
            txt_Email.Font = new Font("Cinzel Decorative", 9, FontStyle.Regular);

            // txt_Username
            txt_Username_Login.BackColor = Color.FromArgb(229, 208, 181);
            txt_Username_Login.Text = "Inserisci Nome utente";
            txt_Username_Login.Font = new Font("Cinzel Decorative", 9, FontStyle.Regular);

            // txt_Password
            txt_Password_Login.BackColor = Color.FromArgb(229, 208, 181);
            txt_Password_Login.Text = "Inserisci Password";
            txt_Password_Login.Font = new Font("Cinzel Decorative", 9, FontStyle.Regular);

            //Lingua
            comboBox_Lingua.Items.AddRange(new string[] { "ITA", "ENG" });
            comboBox_Lingua.Text = "ITA";
            comboBox_Lingua.BackColor = Color.FromArgb(229, 208, 181);

            //Ip
            txt_Ip.BackColor = Color.FromArgb(229, 208, 181);
            txt_Ip.Text = "IP: AUTO";
            txt_Ip.Font = new Font("Cinzel Decorative", 9, FontStyle.Regular);
            txt_Ip.ReadOnly = true;

            //Log
            txt_Log.BackColor = Color.FromArgb(229, 208, 181);
            txt_Log.Text = "LOG";
            txt_Log.ForeColor = Color.Black;
            txt_Log.Font = new Font("Cinzel Decorative", 8, FontStyle.Regular);

            //Versione Client
            txt_Versione_Attuale.BackColor = Color.FromArgb(229, 208, 181);
            txt_Versione_Attuale.Text = "Versione attuale: " + Variabili_Client.versione_Client_Attuale;
            txt_Versione_Attuale.ForeColor = Color.Black;
            txt_Versione_Attuale.Font = new Font("Cinzel Decorative", 8, FontStyle.Regular);

            //Versione Necessaria
            txt_Stato_Server.BackColor = Color.FromArgb(229, 208, 181);
            txt_Stato_Server.ForeColor = Color.Black;
            txt_Stato_Server.Font = new Font("Cinzel Decorative", 8, FontStyle.Regular);
            TentativoConnessione();
        }
        async void TentativoConnessione()
        {
            //Controlla se siamo in locale... 
            if (txt_Ip.Text != "IP: AUTO") ClientConnection.TestClient._ServerIp = txt_Ip.Text;
            else
            {
                string subjectName = Environment.MachineName; //Ottine il nome della macchina (hostname)
                //if (subjectName == "DESKTOP-DOBLVTI" || subjectName == "ADLO") ClientConnection.TestClient._ServerIp = "localhost";
            }

            int tentativi = 1;
            while (Variabili_Client.Utente.User_Login == false)
            {
                if (tentativi >= 3) return;
                txt_Log.Text = $"Tentativo connessione automatica... [{tentativi}/{2}]";
                await ClientConnection.TestClient.InitializeClient(); // Connessione server
                await Sleep(2);
                if (ClientConnection.client_Connesso) break;
                tentativi++;
            }
            //Check versione necessaria client
            txt_Log.Text = $"Controllo aggiornamenti disponibili...";
            if (!await VersioneDisponibile()) return;
            txt_Log.Text = $"Versione client corretta.\nNessun nuovo aggiornamento diponibile";
        }

        private void txt_Username_Login_MouseClick(object sender, MouseEventArgs e)
        {
            txt_Username_Login.Text = "";
            txt_Username_Login.ForeColor = Color.Black;
        }

        private void txt_Password_Login_MouseClick(object sender, MouseEventArgs e)
        {
            txt_Password_Login.Text = "";
            txt_Password_Login.ForeColor = Color.Black;
        }

        private void txt_Username_Login_TextChanged(object sender, EventArgs e)
        {
            txt_Username_Login.ForeColor = Color.Black;
        }

        private void txt_Password_Login_TextChanged(object sender, EventArgs e)
        {
            txt_Password_Login.ForeColor = Color.Black;
        }

        private void txt_Ip_MouseClick(object sender, MouseEventArgs e)
        {
            if (txt_Ip.ReadOnly) return;
            txt_Ip.Text = "";
            txt_Ip.ForeColor = Color.Black;
        }

        private async void Btn_Login_Click(object sender, EventArgs e)
        {
            txt_Log.Font = new Font("Cinzel Decorative", 8, FontStyle.Regular);
            this.ActiveControl = lbl_Titolo;
            Btn_Login.Enabled = false;
            Btn_New_Game.Enabled = false;
            txt_Log.Text = "Connessione...";

            //Controlla se siamo in locale... 
            if (txt_Ip.Text != "IP: AUTO") ClientConnection.TestClient._ServerIp = txt_Ip.Text;
            else
            {
                string subjectName = Environment.MachineName; //Ottine il nome della macchina (hostname)
                if (subjectName == "DESKTOP-DOBLVTI" || subjectName == "ADLO") ClientConnection.TestClient._ServerIp = "localhost";
            }

            ControlloDati(); //Controlla la validità dei dati inseriti
            await Sleep(2);
            txt_Log.Text = "Login...";
            await Sleep(2);
            ClientConnection.TestClient.Send($"Login|{txt_Username_Login.Text}|{txt_Password_Login.Text}|{lingua_Selezionata}");
            await Loop_Login(4);
            await Sleep(2);

            if (Variabili_Client.Utente.User_Login == true)
            {
                Variabili_Client.Utente.Email = txt_Email.Text;
                Variabili_Client.Utente.Username = txt_Username_Login.Text;
                Variabili_Client.Utente.Password = txt_Password_Login.Text;
                this.DialogResult = DialogResult.OK; // Se il login riesce
            }
            else
            {
                Btn_Login.Enabled = true;
                Btn_New_Game.Enabled = true;
            }
            if (login_data != "") txt_Log.Text = login_data;
        }
        private async void Btn_New_Game_Click(object sender, EventArgs e)
        {
            txt_Log.Font = new Font("Cinzel Decorative", 8, FontStyle.Regular);
            this.ActiveControl = lbl_Titolo;
            Btn_New_Game.Enabled = false;
            Btn_Login.Enabled = false;
            txt_Log.Text = "Connessione...";

            //Controlla se siamo in locale... 
            if (txt_Ip.Text != "IP: AUTO") ClientConnection.TestClient._ServerIp = txt_Ip.Text;
            else
            {
                string subjectName = Environment.MachineName; //Ottine il nome della macchina (hostname)
                if (subjectName == "DESKTOP-DOBLVTI" || subjectName == "ADLO") ClientConnection.TestClient._ServerIp = "localhost";
            }

            ControlloDati(); //Controlla la validità dei dati inseriti
            await Sleep(2);
            txt_Log.Text = "Contattando il server...";
            await Sleep(2);
            ClientConnection.TestClient.Send($"New Player|{txt_Username_Login.Text}|{txt_Password_Login.Text}|{lingua_Selezionata}|{txt_Email.Text}");
            await Sleep(2);

            if (Variabili_Client.Utente.User_Login == true)
            {
                Variabili_Client.Utente.Email = txt_Email.Text;
                Variabili_Client.Utente.Username = txt_Username_Login.Text;
                Variabili_Client.Utente.Password = txt_Password_Login.Text;
                this.DialogResult = DialogResult.OK; // Se il login riesce
            }
            else
            {
                Btn_Login.Enabled = true;
                Btn_New_Game.Enabled = true;
            }
            if (login_data != "") txt_Log.Text = login_data;
        }

        async Task<bool> VersioneDisponibile()
        {
            if (!ClientConnection.client_Connesso) //Controlla l'avvenuta connessione
            {
                panel_Connessione.BackgroundImage = Properties.Resources.Disconnesso_V2;
                Btn_Login.Enabled = true;
                Btn_New_Game.Enabled = true;
                txt_Log.Font = new Font("Cinzel Decorative", 8, FontStyle.Bold);
                txt_Log.Text = "Impossibile connettersi al server!";
                return false;
            }
            else panel_Connessione.BackgroundImage = Properties.Resources.Connesso_V2;

            if (Variabili_Client.versione_Client_Necessario != Variabili_Client.versione_Client_Attuale)
            {
                var versioneNecessaria = Variabili_Client.versione_Client_Necessario.Split('.');
                var versioneAttuale = Variabili_Client.versione_Client_Attuale.Split('.');

                if (versioneNecessaria.Count() != 4 || versioneAttuale.Count() != 4) return false;
                if (versioneNecessaria[0] != versioneAttuale[0] || versioneNecessaria[1] != versioneAttuale[1] || versioneNecessaria[2] != versioneAttuale[2])
                {
                    Btn_Login.Enabled = false;
                    Btn_New_Game.Enabled = false;
                    lbl_Aggiornamento_Disponibile.Visible = true;
                    btn_Aggiorna.Visible = true;

                    lbl_Aggiornamento_Disponibile.Text = "Necessario aggiornamento: " + Variabili_Client.versione_Client_Necessario;
                    txt_Log.Text = $"Devi scaricare l'aggiornamento obbligatorio per continuare a giocare...";
                    using (Graphics g = this.CreateGraphics())
                    {
                        float scaleFactor = g.DpiX / 96f; // Se lo zoom è 125%, scaleFactor sarà 1.25

                        int newWidth = (int)(251 * scaleFactor);
                        int newHeight = (int)(446 * scaleFactor);

                        this.Size = new Size(newWidth, newHeight);
                    }
                    Aggiornamento();
                    return false;
                }
                else if (versioneNecessaria[3] != versioneAttuale[3])
                {
                    lbl_Aggiornamento_Disponibile.Visible = true;
                    btn_Aggiorna.Visible = true;

                    lbl_Aggiornamento_Disponibile.Text = "Disponibile aggiornamento: " + Variabili_Client.versione_Client_Necessario;
                    txt_Log.Text = $"Aggiornamento disponibile...";
                    using (Graphics g = this.CreateGraphics())
                    {
                        float scaleFactor = g.DpiX / 96f; // Se lo zoom è 125%, scaleFactor sarà 1.25

                        int newWidth = (int)(322 * scaleFactor);
                        int newHeight = (int)(446 * scaleFactor);

                        this.Size = new Size(newWidth, newHeight);
                    }
                    if (!avviso_Aggiornamento)
                    {
                        avviso_Aggiornamento = true;
                        return false;
                    }
                    return true;
                }
            }
            else if (Variabili_Client.versione_Client_Necessario == Variabili_Client.versione_Client_Attuale) return true;
            return false;
        }

        public static async Task<bool> Sleep(int secondi)
        {
            await Task.Delay(1000 * secondi);
            return true;
        }
        public async Task<bool> Loop_Login(int tentativi_Max)
        {
            int tentativi = 1;
            while (Variabili_Client.Utente.User_Login == false)
            {
                if (tentativi >= tentativi_Max) return false;
                txt_Log.Text = $"Tentativo Login... [{tentativi}/{tentativi_Max}]";
                await Task.Delay(2000);
                tentativi++;
            }
            if (Variabili_Client.Utente.User_Login == true)
                txt_Log.Text = $"Login completato con successo, buon game!";
            else txt_Log.Text = $"Login fallito!";
            return true;
        }
        public async void Aggiornamento()
        {
            int i = 0;
            while (Variabili_Client.Utente.User_Login == false)
            {
                panel_Connessione.BackgroundImage = Properties.Resources.Connesso_V2;
                if (i >= 10) return;
                await Task.Delay(500);
                panel_Connessione.BackgroundImage = Properties.Resources.Disconnesso_V2;
                await Task.Delay(500);
                i++;
            }
        }
        void ControlloDati()
        {
            if (txt_Username_Login.Text == "Inserisci Nome utente")
            {
                txt_Log.Text = "Inserisci un nome utente valido!";
                Btn_Login.Enabled = true;
                Btn_New_Game.Enabled = true;
                txt_Log.Font = new Font("Cinzel Decorative", 8, FontStyle.Bold);
                return;
            }
            else if (txt_Password_Login.Text == "Inserisci Password")
            {
                txt_Log.Text = "Inserisci una password valida!";
                Btn_Login.Enabled = true;
                Btn_New_Game.Enabled = true;
                txt_Log.Font = new Font("Cinzel Decorative", 8, FontStyle.Bold);
                return;
            }
            else if (txt_Email.Text == "Inserisci Email")
            {
                txt_Log.Text = "Inserisci un indirizzo email valido!";
                Btn_Login.Enabled = true;
                Btn_New_Game.Enabled = true;
                txt_Log.Font = new Font("Cinzel Decorative", 8, FontStyle.Bold);
                return;
            }
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Variabili_Client.lingua_Selezionata = lingua_Selezionata;
            MusicManager.Stop();
        }

        private void btn_Aggiorna_Click(object sender, EventArgs e)
        {

            // Apre la pagina GitHub Releases nel browser
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = "https://github.com/adlos96/Warrior-and-Wealth/releases/latest",
                UseShellExecute = true
            });
            Close();
        }

        private void comboBox_Lingua_TextChanged(object sender, EventArgs e)
        {
            lingua_Selezionata = comboBox_Lingua.Text;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) txt_Password_Login.UseSystemPasswordChar = true;
            else txt_Password_Login.UseSystemPasswordChar = false;

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked) txt_Ip.ReadOnly = true;
            else txt_Ip.ReadOnly = false;
        }

        private void lbl_Password_Reset_MouseClick(object sender, MouseEventArgs e)
        {
            A();

            //Loop per check lettura cambio stato dal server per reset password
            B();
        }
        void A()
        {
            lbl_Username_Login.Text = "Codice Reset Password";
            txt_Username_Login.Text = "Inserisci Codice";
            lbl_Password_Login.Visible = false;
            txt_Password_Login.Visible = false;
            lbl_Password_Reset.Visible = false;
            lbl_Ip.Visible = false;
            txt_Ip.Visible = false;
            checkBox2.Visible = false;
            Btn_Login.Enabled = false;

            Btn_New_Game.Text = "Send Code";
            if (Variabili_Client.change_Password && txt_Email.Text == txt_Username_Login.Text)
                Btn_Login.Enabled = true;
            Btn_Login.Text = "Reset Password";
        }
        void B()
        {
            if (Variabili_Client.change_Password)
            {
                lbl_Email.Text = "Nuova Password";
                txt_Email.Text = "Inserisci Nuova Password";
                lbl_Username_Login.Text = "Nuova Password";
                txt_Username_Login.Text = "Ripeti Password";
            }
        }
}
