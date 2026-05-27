using Strategico_V2;
using System.ComponentModel;

namespace Warrior_and_Wealth
{
    public partial class Shop : Form
    {

        private static readonly Dictionary<(int pagina, int bottone), string> _shopComandi = new()
        {
            { (1, 1), "Vip_1" },
            { (1, 2), "Vip_2" },
            { (1, 3), "Pacchetto_1" },
            { (1, 4), "Pacchetto_2" },
            { (1, 5), "Pacchetto_3" },
            { (1, 6), "Pacchetto_4" },
        
            { (2, 1), "Costruttori_24H" },
            { (2, 2), "Costruttori_48H" },
            { (2, 3), "Reclutatori_24H" },
            { (2, 4), "Reclutatori_48H" },
            { (2, 5), "Scudo_Pace_8H" },
            { (2, 6), "Scudo_Pace_24H" },
        
            { (3, 1), "Scudo_Pace_72H" },
            { (3, 2), "GamePass_Base" },
            { (3, 3), "GamePass_Avanzato" },
        };

        int pagina = 1; // pagina iniziale
        public static CustomToolTip toolTip1;
        public Shop()
        {
            InitializeComponent();

            toolTip1 = new CustomToolTip();

            // Imposta qualche proprietà opzionale
            toolTip1.InitialDelay = 150;
            toolTip1.AutoPopDelay = 15000;

            txt_Image_1.ForeColor = Color.FromArgb(205, 175, 0);
            txt_Image_2.ForeColor = Color.FromArgb(205, 175, 0);
            txt_Image_3.ForeColor = Color.FromArgb(205, 175, 0);
            txt_Image_4.ForeColor = Color.FromArgb(205, 175, 0);
            txt_Image_5.ForeColor = Color.FromArgb(205, 175, 0);
            txt_Image_6.ForeColor = Color.FromArgb(205, 175, 0);

            txt_Image_1.BackColor = Color.FromArgb(91, 45, 45);
            txt_Image_2.BackColor = Color.FromArgb(91, 45, 45);
            txt_Image_3.BackColor = Color.FromArgb(91, 45, 45);
            txt_Image_4.BackColor = Color.FromArgb(91, 45, 45);
            txt_Image_5.BackColor = Color.FromArgb(91, 45, 45);
            txt_Image_6.BackColor = Color.FromArgb(91, 45, 45);

            txt_Shop_1.ForeColor = Color.FromArgb(205, 175, 0);
            txt_Shop_2.ForeColor = Color.FromArgb(205, 175, 0);
            txt_Shop_3.ForeColor = Color.FromArgb(205, 175, 0);
            txt_Shop_4.ForeColor = Color.FromArgb(205, 175, 0);
            txt_Shop_5.ForeColor = Color.FromArgb(205, 175, 0);
            txt_Shop_6.ForeColor = Color.FromArgb(205, 175, 0);

            txt_Shop_1.Font = new Font("Cinzel Decorative", 9, FontStyle.Bold);
            txt_Shop_2.Font = new Font("Cinzel Decorative", 9, FontStyle.Bold);
            txt_Shop_3.Font = new Font("Cinzel Decorative", 9, FontStyle.Bold);
            txt_Shop_4.Font = new Font("Cinzel Decorative", 9, FontStyle.Bold);
            txt_Shop_5.Font = new Font("Cinzel Decorative", 9, FontStyle.Bold);
            txt_Shop_6.Font = new Font("Cinzel Decorative", 9, FontStyle.Bold);

            txt_Shop_1.BackColor = Color.FromArgb(124, 62, 63);
            txt_Shop_2.BackColor = Color.FromArgb(124, 62, 63);
            txt_Shop_3.BackColor = Color.FromArgb(124, 62, 63);
            txt_Shop_4.BackColor = Color.FromArgb(124, 62, 63);
            txt_Shop_5.BackColor = Color.FromArgb(124, 62, 63);
            txt_Shop_6.BackColor = Color.FromArgb(124, 62, 63);

            txt_Pacchetto_Desc_1.ForeColor = Color.FromArgb(205, 175, 0);
            txt_Pacchetto_Desc_2.ForeColor = Color.FromArgb(205, 175, 0);
            txt_Pacchetto_Desc_3.ForeColor = Color.FromArgb(205, 175, 0);
            txt_Pacchetto_Desc_4.ForeColor = Color.FromArgb(205, 175, 0);
            txt_Pacchetto_Desc_5.ForeColor = Color.FromArgb(205, 175, 0);
            txt_Pacchetto_Desc_6.ForeColor = Color.FromArgb(205, 175, 0);

            txt_Pacchetto_Desc_1.BackColor = Color.FromArgb(64, 29, 28);
            txt_Pacchetto_Desc_2.BackColor = Color.FromArgb(64, 29, 28);
            txt_Pacchetto_Desc_3.BackColor = Color.FromArgb(64, 29, 28);
            txt_Pacchetto_Desc_4.BackColor = Color.FromArgb(64, 29, 28);
            txt_Pacchetto_Desc_5.BackColor = Color.FromArgb(64, 29, 28);
            txt_Pacchetto_Desc_6.BackColor = Color.FromArgb(64, 29, 28);

            panel_Diamond_Image_1.BackColor = Color.Transparent;
            panel_Diamond_Image_2.BackColor = Color.Transparent;
            panel_Diamond_Image_3.BackColor = Color.Transparent;
            panel_Diamond_Image_4.BackColor = Color.Transparent;
            panel_Diamond_Image_5.BackColor = Color.Transparent;
            panel_Diamond_Image_6.BackColor = Color.Transparent;

            panel_Image_1.BackColor = Color.Transparent;
            panel_Image_2.BackColor = Color.Transparent;
            panel_Image_3.BackColor = Color.Transparent;
            panel_Image_4.BackColor = Color.Transparent;
            panel_Image_5.BackColor = Color.Transparent;
            panel_Image_6.BackColor = Color.Transparent;

            Update_UI();
        }

        private void Shop_Load(object sender, EventArgs e)
        {
            this.ActiveControl = panel_Diamond_Image_2; // assegna il focus al bottone
            panel1.BackColor = Color.FromArgb(100, 229, 208, 181);
            panel1.BackColor = Color.Transparent;
            Update_UI();
        }
        private void panel_Prossimo_Click(object sender, EventArgs e)
        {
            if (pagina >= 1 && pagina < 3)
                pagina++;
            Update_UI();
        }

        private void panel_Precedente_Click(object sender, EventArgs e)
        {
            if (pagina > 1 && pagina <= 3)
                pagina--;
            Update_UI();
        }
        void Update_UI()
        {
            if (pagina == 1)
            {
                txt_Pacchetto_Desc_1.Visible = true;
                txt_Pacchetto_Desc_2.Visible = true;
                txt_Pacchetto_Desc_3.Visible = false;
                txt_Pacchetto_Desc_4.Visible = false;
                txt_Pacchetto_Desc_5.Visible = false;
                txt_Pacchetto_Desc_6.Visible = false;

                panel_Bottone_1.Enabled = true;
                panel_Bottone_2.Enabled = true;
                panel_Bottone_3.Enabled = true;
                panel_Bottone_4.Enabled = true;
                panel_Bottone_5.Enabled = true;
                panel_Bottone_6.Enabled = true;

                panel_Image_1.BackgroundImage = Properties.Resources.Vip_Photoroom_1_; //Immagine principale
                panel_Image_2.BackgroundImage = Properties.Resources.Vip_Photoroom_1_;
                panel_Image_3.BackgroundImage = Properties.Resources.DiamanteViola_V2;
                panel_Image_4.BackgroundImage = Properties.Resources.DiamanteViola_V2;
                panel_Image_5.BackgroundImage = Properties.Resources.DiamanteViola_V2;
                panel_Image_6.BackgroundImage = Properties.Resources.DiamanteViola_V2;

                panel_Diamond_Image_1.BackgroundImage = Properties.Resources.DiamanteViola_V2; //Immagine costo
                panel_Diamond_Image_2.BackgroundImage = Properties.Resources.USDT_Logo_removebg_preview;
                panel_Diamond_Image_3.BackgroundImage = Properties.Resources.USDT_Logo_removebg_preview;
                panel_Diamond_Image_4.BackgroundImage = Properties.Resources.USDT_Logo_removebg_preview;
                panel_Diamond_Image_5.BackgroundImage = Properties.Resources.USDT_Logo_removebg_preview;
                panel_Diamond_Image_6.BackgroundImage = Properties.Resources.USDT_Logo_removebg_preview;

                txt_Shop_1.Text = Variabili_Client.Shop.Vip_1.Costo.ToString(); //Costo effettivo
                txt_Shop_2.Text = Variabili_Client.Shop.Vip_2.Costo.ToString();
                txt_Shop_3.Text = Variabili_Client.Shop.Pacchetto_Diamanti_1.Costo.ToString();
                txt_Shop_4.Text = Variabili_Client.Shop.Pacchetto_Diamanti_2.Costo.ToString();
                txt_Shop_5.Text = Variabili_Client.Shop.Pacchetto_Diamanti_3.Costo.ToString();
                txt_Shop_6.Text = Variabili_Client.Shop.Pacchetto_Diamanti_4.Costo.ToString();

                txt_Image_1.Text = Variabili_Client.Shop.Vip_1.Reward.ToString() + " H"; //Reward all'acquisto
                txt_Image_2.Text = Variabili_Client.Shop.Vip_2.Reward.ToString() + " H";
                txt_Image_3.Text = Variabili_Client.Shop.Pacchetto_Diamanti_1.Reward.ToString();
                txt_Image_4.Text = Variabili_Client.Shop.Pacchetto_Diamanti_2.Reward.ToString();
                txt_Image_5.Text = Variabili_Client.Shop.Pacchetto_Diamanti_3.Reward.ToString();
                txt_Image_6.Text = Variabili_Client.Shop.Pacchetto_Diamanti_4.Reward.ToString();

                txt_Pacchetto_Desc_1.Text = "VIP";
                txt_Pacchetto_Desc_2.Text = "VIP";

                toolTip1.SetToolTip(this.panel_Image_1, Variabili_Client.Shop.Vip_1.desc);
                toolTip1.SetToolTip(this.panel_Image_2, Variabili_Client.Shop.Vip_2.desc);
            }
            if (pagina == 2)
            {
                txt_Pacchetto_Desc_1.Visible = true;
                txt_Pacchetto_Desc_2.Visible = true;
                txt_Pacchetto_Desc_3.Visible = true;
                txt_Pacchetto_Desc_4.Visible = true;
                txt_Pacchetto_Desc_5.Visible = true;
                txt_Pacchetto_Desc_6.Visible = true;

                panel_Bottone_1.Enabled = true;
                panel_Bottone_2.Enabled = true;
                panel_Bottone_3.Enabled = true;
                panel_Bottone_4.Enabled = true;
                panel_Bottone_5.Enabled = true;
                panel_Bottone_6.Enabled = true;

                panel_Image_1.BackgroundImage = Properties.Resources.Costruttori_24H;
                panel_Image_2.BackgroundImage = Properties.Resources.Costruttori_48H;
                panel_Image_3.BackgroundImage = Properties.Resources.Addestratori_24H_removebg_preview;
                panel_Image_4.BackgroundImage = Properties.Resources.Addestratori_48H_removebg_preview;
                panel_Image_5.BackgroundImage = Properties.Resources.Scudo_Pace_1;
                panel_Image_6.BackgroundImage = Properties.Resources.Scudo_Pace_1;

                panel_Image_1.BackgroundImageLayout = ImageLayout.Zoom;
                panel_Image_2.BackgroundImageLayout = ImageLayout.Zoom;
                panel_Image_3.BackgroundImageLayout = ImageLayout.Zoom;
                panel_Image_4.BackgroundImageLayout = ImageLayout.Zoom;
                panel_Image_5.BackgroundImageLayout = ImageLayout.Zoom;
                panel_Image_6.BackgroundImageLayout = ImageLayout.Zoom;

                panel_Diamond_Image_1.BackgroundImage = Properties.Resources.DiamanteBlu_V2;
                panel_Diamond_Image_2.BackgroundImage = Properties.Resources.DiamanteBlu_V2;
                panel_Diamond_Image_3.BackgroundImage = Properties.Resources.DiamanteBlu_V2;
                panel_Diamond_Image_4.BackgroundImage = Properties.Resources.DiamanteBlu_V2;
                panel_Diamond_Image_5.BackgroundImage = Properties.Resources.DiamanteBlu_V2;
                panel_Diamond_Image_6.BackgroundImage = Properties.Resources.DiamanteBlu_V2;

                txt_Shop_1.Text = Variabili_Client.Shop.Costruttore_24h.Costo.ToString(); //Costo effettivo
                txt_Shop_2.Text = Variabili_Client.Shop.Costruttore_48h.Costo.ToString();
                txt_Shop_3.Text = Variabili_Client.Shop.Reclutatore_24h.Costo.ToString();
                txt_Shop_4.Text = Variabili_Client.Shop.Reclutatore_48h.Costo.ToString();
                txt_Shop_5.Text = Variabili_Client.Shop.Scudo_Pace_8h.Costo.ToString();
                txt_Shop_6.Text = Variabili_Client.Shop.Scudo_Pace_24h.Costo.ToString();

                txt_Image_1.Text = Variabili_Client.Shop.Costruttore_24h.Reward.ToString() + " H"; //Reward all'acquisto
                txt_Image_2.Text = Variabili_Client.Shop.Costruttore_48h.Reward.ToString() + " H";
                txt_Image_3.Text = Variabili_Client.Shop.Reclutatore_24h.Reward.ToString() + " H";
                txt_Image_4.Text = Variabili_Client.Shop.Reclutatore_48h.Reward.ToString() + " H";
                txt_Image_5.Text = Variabili_Client.Shop.Scudo_Pace_8h.Reward.ToString() + " H";
                txt_Image_6.Text = Variabili_Client.Shop.Scudo_Pace_24h.Reward.ToString() + " H";

                txt_Pacchetto_Desc_1.Text = $"{LocalizationManager.Current.Label_Costruttori()}";
                txt_Pacchetto_Desc_2.Text = $"{LocalizationManager.Current.Label_Costruttori()}";
                txt_Pacchetto_Desc_3.Text = $"{LocalizationManager.Current.Label_Reclutatori()}";
                txt_Pacchetto_Desc_4.Text = $"{LocalizationManager.Current.Label_Reclutatori()}";
                txt_Pacchetto_Desc_5.Text = $"{LocalizationManager.Current.Label_Scudo_Pace()}";
                txt_Pacchetto_Desc_6.Text = $"{LocalizationManager.Current.Label_Scudo_Pace()}";

                toolTip1.SetToolTip(this.panel_Image_1, Variabili_Client.Shop.Costruttore_24h.desc);
                toolTip1.SetToolTip(this.panel_Image_2, Variabili_Client.Shop.Costruttore_48h.desc);
                toolTip1.SetToolTip(this.panel_Image_3, Variabili_Client.Shop.Reclutatore_24h.desc);
                toolTip1.SetToolTip(this.panel_Image_4, Variabili_Client.Shop.Reclutatore_48h.desc);
                toolTip1.SetToolTip(this.panel_Image_5, Variabili_Client.Shop.Scudo_Pace_8h.desc);
                toolTip1.SetToolTip(this.panel_Image_6, Variabili_Client.Shop.Scudo_Pace_24h.desc);
            }
            if (pagina == 3)
            {
                txt_Pacchetto_Desc_2.Visible = true;
                txt_Pacchetto_Desc_3.Visible = true;
                txt_Pacchetto_Desc_4.Visible = true;
                txt_Pacchetto_Desc_5.Visible = true;
                txt_Pacchetto_Desc_6.Visible = true;

                panel_Bottone_1.Enabled = true;
                panel_Bottone_2.Enabled = true;
                panel_Bottone_3.Enabled = true;
                panel_Bottone_4.Enabled = false;
                panel_Bottone_5.Enabled = false;
                panel_Bottone_6.Enabled = false;

                panel_Image_1.BackgroundImage = Properties.Resources.Scudo_Pace_1;
                panel_Image_2.BackgroundImage = Properties.Resources.GamePass_Base;
                panel_Image_3.BackgroundImage = Properties.Resources.GamePass_Avanzato;
                panel_Image_4.BackgroundImage = Properties.Resources.Pacchetto_Risorse;
                panel_Image_5.BackgroundImage = null;
                panel_Image_6.BackgroundImage = null;

                panel_Image_1.BackgroundImageLayout = ImageLayout.Zoom;
                panel_Image_2.BackgroundImageLayout = ImageLayout.Zoom;
                panel_Image_3.BackgroundImageLayout = ImageLayout.Zoom;
                panel_Image_4.BackgroundImageLayout = ImageLayout.Zoom;
                panel_Image_5.BackgroundImageLayout = ImageLayout.Zoom;
                panel_Image_6.BackgroundImageLayout = ImageLayout.Zoom;

                panel_Diamond_Image_1.BackgroundImage = Properties.Resources.DiamanteBlu_V2;
                panel_Diamond_Image_2.BackgroundImage = Properties.Resources.USDT_Logo_removebg_preview;
                panel_Diamond_Image_3.BackgroundImage = Properties.Resources.USDT_Logo_removebg_preview;
                panel_Diamond_Image_4.BackgroundImage = Properties.Resources.DiamanteBlu_V2;
                panel_Diamond_Image_5.BackgroundImage = null;
                panel_Diamond_Image_6.BackgroundImage = null;

                txt_Shop_1.Text = Variabili_Client.Shop.Scudo_Pace_72h.Costo.ToString(); //Costo effettivo
                txt_Shop_2.Text = Variabili_Client.Shop.GamePass_Base.Costo.ToString();
                txt_Shop_3.Text = Variabili_Client.Shop.GamePass_Avanzato.Costo.ToString();
                txt_Shop_4.Text = "";
                txt_Shop_5.Text = "";
                txt_Shop_6.Text = "";

                txt_Image_1.Text = Variabili_Client.Shop.Scudo_Pace_72h.Reward.ToString() + " H"; //Reward all'acquisto
                txt_Image_2.Text = "30 G"; //Gamepass Base
                txt_Image_3.Text = "30 G"; //Gamepass Avanzato
                txt_Image_4.Text = "";
                txt_Image_5.Text = "";
                txt_Image_6.Text = "";

                txt_Pacchetto_Desc_1.Text = $"{LocalizationManager.Current.Label_Scudo_Pace()}";
                txt_Pacchetto_Desc_2.Text = $"GamePass Silver";
                txt_Pacchetto_Desc_3.Text = $"GamePass Gold";
                txt_Pacchetto_Desc_4.Text = "";
                txt_Pacchetto_Desc_5.Text = "";
                txt_Pacchetto_Desc_6.Text = "";

                toolTip1.SetToolTip(this.panel_Image_1, Variabili_Client.Shop.Scudo_Pace_72h.desc);
                toolTip1.SetToolTip(this.panel_Image_2, Variabili_Client.Shop.GamePass_Base.desc);
                toolTip1.SetToolTip(this.panel_Image_3, Variabili_Client.Shop.GamePass_Avanzato.desc);
            }
        }

        private async Task OnBottoneShopClick(Panel bottone, int numeroBottone)
        {
            var result = MessageBox.Show(
                $"{LocalizationManager.Current.Label_Acquisto_Testo()}\n",
                $"{LocalizationManager.Current.Label_Conferma_Acquisto()}",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes && _shopComandi.TryGetValue((pagina, numeroBottone), out var comando))
                ClientConnection.TestClient.Send($"Shop|{Variabili_Client.Utente.Username}|{Variabili_Client.Utente.Password}|{comando}");

            bottone.Enabled = false;
            await Sleep(2);
            bottone.Enabled = true;
        }

        // I 6 handler diventano tutti uguali:
        private async void panel_Bottone_1_MouseClick(object sender, MouseEventArgs e) => await OnBottoneShopClick(panel_Bottone_1, 1);
        private async void panel_Bottone_2_MouseClick(object sender, MouseEventArgs e) => await OnBottoneShopClick(panel_Bottone_2, 2);
        private async void panel_Bottone_3_MouseClick(object sender, MouseEventArgs e) => await OnBottoneShopClick(panel_Bottone_3, 3);
        private async void panel_Bottone_4_MouseClick(object sender, MouseEventArgs e) => await OnBottoneShopClick(panel_Bottone_4, 4);
        private async void panel_Bottone_5_MouseClick(object sender, MouseEventArgs e) => await OnBottoneShopClick(panel_Bottone_5, 5);
        private async void panel_Bottone_6_MouseClick(object sender, MouseEventArgs e) => await OnBottoneShopClick(panel_Bottone_6, 6);

        public static async Task Sleep(int secondi) => await Task.Delay(secondi * 1000);
    }
}
