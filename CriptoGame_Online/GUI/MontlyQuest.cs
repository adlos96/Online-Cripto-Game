using Strategico_V2;
using System.Diagnostics.Eventing.Reader;
using System.Windows.Forms;
using static Strategico_V2.ClientConnection;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;
using TextBox = System.Windows.Forms.TextBox;

namespace Warrior_and_Wealth
{
    public partial class MontlyQuest : Form
    {
        // Lista di Quest_Reward attuali
        public static List<ClientQuestData> CurrentQuests { get; set; } = new List<ClientQuestData>();
        int currentIndex = 0;

        public static List<int> CurrentRewardsNormali { get; set; } = new(); // Ricompense Normali
        public static List<int> CurrentRewardsVip { get; set; } = new(); // Ricompense VIP
        public static List<int> CurrentRewardPoints { get; set; } = new(); //Punti necessari per sbloccare le ricompense
        public static List<bool> CurrentRewardClaimNormal { get; set; } = new(); // Ricompense già ritirate
        public static List<bool> CurrentRewardClaimVip { get; set; } = new(); // Ricompense già ritirate

        private CancellationTokenSource cts = new CancellationTokenSource();
        public MontlyQuest()
        {
            InitializeComponent();
            this.ActiveControl = Btn_Costruzione; // assegna il focus al bottone
        }

        private void MontlyQuest_Load(object sender, EventArgs e)
        {
            AggiornaInterfacciaRewards();
            AggiornaInterfacciaQuest();
            Task.Run(() => Gui_Update(cts.Token), cts.Token);
        }

        async void Gui_Update(CancellationToken token)
        {
            this.ActiveControl = label1;
            while (!token.IsCancellationRequested)
            {
                if (panel1.IsHandleCreated && !panel1.IsDisposed)
                {
                    panel1.BeginInvoke((Action)(() =>
                    {
                        AggiornaInterfacciaQuest();
                        Update_Reward();
                        Check_Unlock_Reward();
                        Check_Unlock_Reward_GamePass_Base();
                        this.ActiveControl = null;
                        progressBar1.Maximum = Convert.ToInt32(CurrentRewardPoints[19]);

                        int point = Convert.ToInt32(Variabili_Client.Utente.Montly_Quest_Point);
                        if (point > progressBar1.Maximum) point = progressBar1.Maximum;
                        else if (point <= CurrentRewardPoints[0]) progressBar1.Value = (int)(point * 0.45);
                        else if (point <= CurrentRewardPoints[1] && point > CurrentRewardPoints[0]) progressBar1.Value = (int)(point * 0.95);
                        else if (point <= CurrentRewardPoints[2] && point > CurrentRewardPoints[1]) progressBar1.Value = (int)(point * 1.05);
                        else if (point <= CurrentRewardPoints[3] && point > CurrentRewardPoints[2]) progressBar1.Value = (int)(point * 1.02);
                        else if (point <= CurrentRewardPoints[4] && point > CurrentRewardPoints[3]) progressBar1.Value = (int)(point * 0.55);
                        else if (point <= CurrentRewardPoints[5] && point > CurrentRewardPoints[4]) progressBar1.Value = (int)(point * 0.875);
                        else if (point <= CurrentRewardPoints[6] && point > CurrentRewardPoints[5]) progressBar1.Value = (int)(point * 0.82);
                        else if (point <= CurrentRewardPoints[7] && point > CurrentRewardPoints[6]) progressBar1.Value = (int)(point * 0.78);
                        else if (point <= CurrentRewardPoints[8] && point > CurrentRewardPoints[7]) progressBar1.Value = (int)(point * 0.755);
                        else if (point <= CurrentRewardPoints[9] && point > CurrentRewardPoints[8]) progressBar1.Value = (int)(point * 0.75);
                        else if (point <= CurrentRewardPoints[10] && point > CurrentRewardPoints[9]) progressBar1.Value = (int)(point * 0.765);
                        else if (point <= CurrentRewardPoints[11] && point > CurrentRewardPoints[10]) progressBar1.Value = (int)(point * 0.795);
                        else if (point <= CurrentRewardPoints[12] && point > CurrentRewardPoints[11]) progressBar1.Value = (int)(point * 0.82);
                        else if (point <= CurrentRewardPoints[13] && point > CurrentRewardPoints[12]) progressBar1.Value = (int)(point * 0.85);
                        else if (point <= CurrentRewardPoints[14] && point > CurrentRewardPoints[13]) progressBar1.Value = (int)(point * 0.9);
                        else if (point <= CurrentRewardPoints[15] && point > CurrentRewardPoints[14]) progressBar1.Value = (int)(point * 0.94);
                        else if (point <= CurrentRewardPoints[16] && point > CurrentRewardPoints[15]) progressBar1.Value = (int)(point * 0.99);
                        else if (point <= CurrentRewardPoints[17] && point > CurrentRewardPoints[16]) progressBar1.Value = (int)(point * 1.035);
                        else if (point <= CurrentRewardPoints[18] && point > CurrentRewardPoints[17]) progressBar1.Value = (int)(point * 1.08);
                        else if (point <= CurrentRewardPoints[19] && point > CurrentRewardPoints[18]) progressBar1.Value = (int)(point * 0.975);
                        else progressBar1.Value = (int)point;

                        textBox_Punti_Quest.Text = $"Punti: {point}";
                    }));
                }
                await Task.Delay(750); // meglio di Thread.Sleep
            }

        }
        void AggiornaInterfacciaQuest()
        {
            if (CurrentQuests.Count > 0)
            {
                int count = Math.Min(10, CurrentQuests.Count);
                for (int i = 0; i < count; i++)
                {
                    int questIndex = (currentIndex + i) % CurrentQuests.Count;
                    if (i == 0)
                    {
                        txt_Quest_Desc_1.Text = CurrentQuests[questIndex].Quest_Description;
                        txt_Quest_1.Text = $"[{CurrentQuests[questIndex].Experience:#,0.}] Exp   {CurrentQuests[questIndex].Progress:#,0.}/{CurrentQuests[questIndex].Require:#,0.}";
                    }
                    if (i == 1)
                    {
                        txt_Quest_Desc_2.Text = CurrentQuests[questIndex].Quest_Description;
                        txt_Quest_2.Text = $"[{CurrentQuests[questIndex].Experience:#,0.}] Exp   {CurrentQuests[questIndex].Progress:#,0.}/{CurrentQuests[questIndex].Require:#,0.}";
                    }
                    if (i == 2)
                    {
                        txt_Quest_Desc_3.Text = CurrentQuests[questIndex].Quest_Description;
                        txt_Quest_3.Text = $"[{CurrentQuests[questIndex].Experience:#,0.}] Exp   {CurrentQuests[questIndex].Progress:#,0.}/{CurrentQuests[questIndex].Require:#,0.}";
                    }
                    if (i == 3)
                    {
                        txt_Quest_Desc_4.Text = CurrentQuests[questIndex].Quest_Description;
                        txt_Quest_4.Text = $"[{CurrentQuests[questIndex].Experience:#,0.}] Exp   {CurrentQuests[questIndex].Progress:#,0.}/{CurrentQuests[questIndex].Require:#,0.}";
                    }
                    if (i == 4)
                    {
                        txt_Quest_Desc_5.Text = CurrentQuests[questIndex].Quest_Description;
                        txt_Quest_5.Text = $"[{CurrentQuests[questIndex].Experience:#,0.}] Exp   {CurrentQuests[questIndex].Progress:#,0.}/{CurrentQuests[questIndex].Require:#,0.}";
                    }
                    if (i == 5)
                    {
                        txt_Quest_Desc_6.Text = CurrentQuests[questIndex].Quest_Description;
                        txt_Quest_6.Text = $"[{CurrentQuests[questIndex].Experience:#,0.}] Exp   {CurrentQuests[questIndex].Progress:#,0.}/{CurrentQuests[questIndex].Require:#,0.}";
                    }
                    if (i == 6)
                    {
                        txt_Quest_Desc_7.Text = CurrentQuests[questIndex].Quest_Description;
                        txt_Quest_7.Text = $"[{CurrentQuests[questIndex].Experience:#,0.}] Exp   {CurrentQuests[questIndex].Progress:#,0.}/{CurrentQuests[questIndex].Require:#,0.}";
                    }
                    if (i == 7)
                    {
                        txt_Quest_Desc_8.Text = CurrentQuests[questIndex].Quest_Description;
                        txt_Quest_8.Text = $"[{CurrentQuests[questIndex].Experience:#,0.}] Exp   {CurrentQuests[questIndex].Progress:#,0.}/{CurrentQuests[questIndex].Require:#,0.}";
                    }
                    if (i == 8)
                    {
                        txt_Quest_Desc_9.Text = CurrentQuests[questIndex].Quest_Description;
                        txt_Quest_9.Text = $"[{CurrentQuests[questIndex].Experience:#,0.}] Exp   {CurrentQuests[questIndex].Progress:#,0.}/{CurrentQuests[questIndex].Require:#,0.}";
                    }
                    if (i == 9)
                    {
                        txt_Quest_Desc_10.Text = CurrentQuests[questIndex].Quest_Description;
                        txt_Quest_10.Text = $"[{CurrentQuests[questIndex].Experience:#,0.}] Exp   {CurrentQuests[questIndex].Progress:#,0.}/{CurrentQuests[questIndex].Require:#,0.}";
                    }
                }

            }
        }
        void AggiornaInterfacciaRewards()
        {
            if (CurrentRewardPoints.Count == 0) return;

            int n = Math.Min(20, CurrentRewardPoints.Count);

            for (int i = 1; i <= n; i++)
            {
                var txtPunti = (TextBox)Controls.Find($"txt_Punti_Reward_{i}", true)[0];
                var txtNormale = (TextBox)Controls.Find($"txt_Reward_{i}", true)[0];
                var txtVip = (TextBox)Controls.Find($"txt_Reward_Vip_{i}", true)[0];

                txtPunti.Text = CurrentRewardPoints[i - 1].ToString();
                txtNormale.Text = CurrentRewardsNormali[i - 1].ToString();
                txtVip.Text = CurrentRewardsVip[i - 1].ToString();
            }
        }
        private void Check_Unlock_Reward()
        {
            int point = Convert.ToInt32(Variabili_Client.Utente.Montly_Quest_Point);

            txt_Punti_Reward_1.BackColor = Color.FromArgb(15, 123, 15);
            txt_Punti_Reward_2.BackColor = Color.FromArgb(15, 123, 15);
            txt_Punti_Reward_3.BackColor = Color.FromArgb(15, 123, 15);
            txt_Punti_Reward_4.BackColor = Color.FromArgb(15, 123, 15);
            txt_Punti_Reward_5.BackColor = Color.FromArgb(15, 123, 15);
            txt_Punti_Reward_6.BackColor = Color.FromArgb(15, 123, 15);
            txt_Punti_Reward_7.BackColor = Color.FromArgb(15, 123, 15);
            txt_Punti_Reward_8.BackColor = Color.FromArgb(15, 123, 15);
            txt_Punti_Reward_9.BackColor = Color.FromArgb(15, 123, 15);
            txt_Punti_Reward_10.BackColor = Color.FromArgb(15, 123, 15);
            txt_Punti_Reward_11.BackColor = Color.FromArgb(15, 123, 15);
            txt_Punti_Reward_12.BackColor = Color.FromArgb(15, 123, 15);
            txt_Punti_Reward_13.BackColor = Color.FromArgb(15, 123, 15);
            txt_Punti_Reward_14.BackColor = Color.FromArgb(15, 123, 15);
            txt_Punti_Reward_15.BackColor = Color.FromArgb(15, 123, 15);
            txt_Punti_Reward_16.BackColor = Color.FromArgb(15, 123, 15);
            txt_Punti_Reward_17.BackColor = Color.FromArgb(15, 123, 15);
            txt_Punti_Reward_18.BackColor = Color.FromArgb(15, 123, 15);
            txt_Punti_Reward_19.BackColor = Color.FromArgb(15, 123, 15);
            txt_Punti_Reward_20.BackColor = Color.FromArgb(15, 123, 15);

            if (point >= CurrentRewardPoints[0])
                if (CurrentRewardClaimNormal[0] == false)
                {
                    btn_Reward_1.Enabled = true;
                    btn_Reward_1.BackColor = Color.FromArgb(6, 176, 37);
                }
                else if (CurrentRewardClaimNormal[0] == false)
                    btn_Reward_1.BackColor = Color.FromArgb(55, 47, 36);
                else btn_Reward_1.BackColor = Color.FromArgb(90, 80, 70);  
            else txt_Punti_Reward_1.BackColor = Color.FromArgb(230, 230, 230);

            if (point >= CurrentRewardPoints[1])
                if (CurrentRewardClaimNormal[1] == false && CurrentRewardClaimNormal[0] == true)
                {
                    btn_Reward_2.Enabled = true;
                    btn_Reward_2.BackColor = Color.FromArgb(6, 176, 37);
                }
                else if (CurrentRewardClaimNormal[1] == false)
                    btn_Reward_2.BackColor = Color.FromArgb(55, 47, 36);
                else btn_Reward_2.BackColor = Color.FromArgb(90, 80, 70);
            else txt_Punti_Reward_2.BackColor = Color.FromArgb(230, 230, 230);

            if (point >= CurrentRewardPoints[2])
                if (CurrentRewardClaimNormal[2] == false && CurrentRewardClaimNormal[1] == true)
                {
                    btn_Reward_3.Enabled = true;
                    btn_Reward_3.BackColor = Color.FromArgb(6, 176, 37);
                }
                else if (CurrentRewardClaimNormal[2] == false)
                    btn_Reward_3.BackColor = Color.FromArgb(55, 47, 36);
                else btn_Reward_3.BackColor = Color.FromArgb(90, 80, 70);
            else txt_Punti_Reward_3.BackColor = Color.FromArgb(230, 230, 230);

            if (point >= CurrentRewardPoints[3])
                if (CurrentRewardClaimNormal[3] == false && CurrentRewardClaimNormal[2] == true)
                {
                    btn_Reward_4.Enabled = true;
                    btn_Reward_4.BackColor = Color.FromArgb(6, 176, 37);
                }
                else if (CurrentRewardClaimNormal[3] == false)
                    btn_Reward_4.BackColor = Color.FromArgb(55, 47, 36);
                else btn_Reward_4.BackColor = Color.FromArgb(90, 80, 70);
            else txt_Punti_Reward_4.BackColor = Color.FromArgb(230, 230, 230);

            if (point >= CurrentRewardPoints[4])
                if (CurrentRewardClaimNormal[4] == false && CurrentRewardClaimNormal[3] == true)
                {
                    btn_Reward_5.Enabled = true;
                    btn_Reward_5.BackColor = Color.FromArgb(6, 176, 37);
                }
                else if (CurrentRewardClaimNormal[4] == false)
                    btn_Reward_5.BackColor = Color.FromArgb(55, 47, 36);
                else btn_Reward_5.BackColor = Color.FromArgb(90, 80, 70);
            else txt_Punti_Reward_5.BackColor = Color.FromArgb(230, 230, 230);

            if (point >= CurrentRewardPoints[5])
                if (CurrentRewardClaimNormal[5] == false && CurrentRewardClaimNormal[4] == true)
                {
                    btn_Reward_6.Enabled = true;
                    btn_Reward_6.BackColor = Color.FromArgb(6, 176, 37);
                }
                else if (CurrentRewardClaimNormal[5] == false)
                    btn_Reward_6.BackColor = Color.FromArgb(55, 47, 36);
                else btn_Reward_6.BackColor = Color.FromArgb(90, 80, 70);
            else txt_Punti_Reward_6.BackColor = Color.FromArgb(230, 230, 230);

            if (point >= CurrentRewardPoints[6])
                if (CurrentRewardClaimNormal[6] == false && CurrentRewardClaimNormal[5] == true)
                {
                    btn_Reward_7.Enabled = true;
                    btn_Reward_7.BackColor = Color.FromArgb(6, 176, 37);
                }
                else if (CurrentRewardClaimNormal[6] == false)
                    btn_Reward_7.BackColor = Color.FromArgb(55, 47, 36);
                else btn_Reward_7.BackColor = Color.FromArgb(90, 80, 70);
            else txt_Punti_Reward_7.BackColor = Color.FromArgb(230, 230, 230);

            if (point >= CurrentRewardPoints[7])
                if (CurrentRewardClaimNormal[7] == false && CurrentRewardClaimNormal[6] == true)
                {
                    btn_Reward_8.Enabled = true;
                    btn_Reward_8.BackColor = Color.FromArgb(6, 176, 37);
                }
                else if (CurrentRewardClaimNormal[7] == false)
                    btn_Reward_8.BackColor = Color.FromArgb(55, 47, 36);
                else btn_Reward_8.BackColor = Color.FromArgb(90, 80, 70);
            else txt_Punti_Reward_8.BackColor = Color.FromArgb(230, 230, 230);

            if (point >= CurrentRewardPoints[8])
                if (CurrentRewardClaimNormal[8] == false && CurrentRewardClaimNormal[7] == true)
                {
                    btn_Reward_9.Enabled = true;
                    btn_Reward_9.BackColor = Color.FromArgb(6, 176, 37);
                }
                else if (CurrentRewardClaimNormal[8] == false)
                    btn_Reward_9.BackColor = Color.FromArgb(55, 47, 36);
                else btn_Reward_9.BackColor = Color.FromArgb(90, 80, 70);
            else txt_Punti_Reward_9.BackColor = Color.FromArgb(230, 230, 230);

            if (point >= CurrentRewardPoints[9])
                if (CurrentRewardClaimNormal[9] == false && CurrentRewardClaimNormal[8] == true)
                {
                    btn_Reward_10.Enabled = true;
                    btn_Reward_10.BackColor = Color.FromArgb(6, 176, 37);
                }
                else if (CurrentRewardClaimNormal[9] == false)
                    btn_Reward_10.BackColor = Color.FromArgb(55, 47, 36);
                else btn_Reward_10.BackColor = Color.FromArgb(90, 80, 70);
            else txt_Punti_Reward_10.BackColor = Color.FromArgb(230, 230, 230);

            if (point >= CurrentRewardPoints[10])
                if (CurrentRewardClaimNormal[10] == false && CurrentRewardClaimNormal[9] == true)
                {
                    btn_Reward_11.Enabled = true;
                    btn_Reward_11.BackColor = Color.FromArgb(6, 176, 37);
                }
                else if (CurrentRewardClaimNormal[10] == false)
                    btn_Reward_11.BackColor = Color.FromArgb(55, 47, 36);
                else btn_Reward_11.BackColor = Color.FromArgb(90, 80, 70);
            else txt_Punti_Reward_11.BackColor = Color.FromArgb(230, 230, 230);

            if (point >= CurrentRewardPoints[11])
                if (CurrentRewardClaimNormal[11] == false && CurrentRewardClaimNormal[10] == true)
                {
                    btn_Reward_12.Enabled = true;
                    btn_Reward_12.BackColor = Color.FromArgb(6, 176, 37);
                }
                else if (CurrentRewardClaimNormal[11] == false)
                    btn_Reward_12.BackColor = Color.FromArgb(55, 47, 36);
                else btn_Reward_12.BackColor = Color.FromArgb(90, 80, 70);
            else txt_Punti_Reward_12.BackColor = Color.FromArgb(230, 230, 230);

            if (point >= CurrentRewardPoints[12])
                if (CurrentRewardClaimNormal[12] == false && CurrentRewardClaimNormal[11] == true)
                {
                    btn_Reward_13.Enabled = true;
                    btn_Reward_13.BackColor = Color.FromArgb(6, 176, 37);
                }
                else if (CurrentRewardClaimNormal[12] == false)
                    btn_Reward_13.BackColor = Color.FromArgb(55, 47, 36);
                else btn_Reward_13.BackColor = Color.FromArgb(90, 80, 70);
            else txt_Punti_Reward_13.BackColor = Color.FromArgb(230, 230, 230);

            if (point >= CurrentRewardPoints[13])
                if (CurrentRewardClaimNormal[13] == false && CurrentRewardClaimNormal[12] == true)
                {
                    btn_Reward_14.Enabled = true;
                    btn_Reward_14.BackColor = Color.FromArgb(6, 176, 37);
                }
                else if (CurrentRewardClaimNormal[13] == false)
                    btn_Reward_14.BackColor = Color.FromArgb(55, 47, 36);
                else btn_Reward_14.BackColor = Color.FromArgb(90, 80, 70);
            else txt_Punti_Reward_14.BackColor = Color.FromArgb(230, 230, 230);

            if (point >= CurrentRewardPoints[14])
                if (CurrentRewardClaimNormal[14] == false && CurrentRewardClaimNormal[13] == true)
                {
                    btn_Reward_15.Enabled = true;
                    btn_Reward_15.BackColor = Color.FromArgb(6, 176, 37);
                }
                else if (CurrentRewardClaimNormal[14] == false)
                    btn_Reward_15.BackColor = Color.FromArgb(55, 47, 36);
                else btn_Reward_15.BackColor = Color.FromArgb(90, 80, 70);
            else txt_Punti_Reward_15.BackColor = Color.FromArgb(230, 230, 230);

            if (point >= CurrentRewardPoints[15])
                if (CurrentRewardClaimNormal[15] == false && CurrentRewardClaimNormal[14] == true)
                {
                    btn_Reward_16.Enabled = true;
                    btn_Reward_16.BackColor = Color.FromArgb(6, 176, 37);
                }
                else if (CurrentRewardClaimNormal[15] == false)
                    btn_Reward_16.BackColor = Color.FromArgb(55, 47, 36);
                else btn_Reward_16.BackColor = Color.FromArgb(90, 80, 70);
            else txt_Punti_Reward_16.BackColor = Color.FromArgb(230, 230, 230);

            if (point >= CurrentRewardPoints[16])
                if (CurrentRewardClaimNormal[16] == false && CurrentRewardClaimNormal[15] == true)
                {
                    btn_Reward_17.Enabled = true;
                    btn_Reward_17.BackColor = Color.FromArgb(6, 176, 37);
                }
                else if (CurrentRewardClaimNormal[16] == false)
                    btn_Reward_17.BackColor = Color.FromArgb(55, 47, 36);
                else btn_Reward_17.BackColor = Color.FromArgb(90, 80, 70);
            else txt_Punti_Reward_17.BackColor = Color.FromArgb(230, 230, 230);

            if (point >= CurrentRewardPoints[17])
                if (CurrentRewardClaimNormal[17] == false && CurrentRewardClaimNormal[16] == true)
                {
                    btn_Reward_18.Enabled = true;
                    btn_Reward_18.BackColor = Color.FromArgb(6, 176, 37);
                }
                else if (CurrentRewardClaimNormal[17] == false)
                    btn_Reward_18.BackColor = Color.FromArgb(55, 47, 36);
                else btn_Reward_18.BackColor = Color.FromArgb(90, 80, 70);
            else txt_Punti_Reward_18.BackColor = Color.FromArgb(230, 230, 230);

            if (point >= CurrentRewardPoints[18])
                if (CurrentRewardClaimNormal[18] == false && CurrentRewardClaimNormal[17] == true)
                {
                    btn_Reward_19.Enabled = true;
                    btn_Reward_19.BackColor = Color.FromArgb(6, 176, 37);
                }
                else if (CurrentRewardClaimNormal[18] == false)
                    btn_Reward_19.BackColor = Color.FromArgb(55, 47, 36);
                else btn_Reward_19.BackColor = Color.FromArgb(90, 80, 70);
            else txt_Punti_Reward_19.BackColor = Color.FromArgb(230, 230, 230);

            if (point >= CurrentRewardPoints[19])
                if (CurrentRewardClaimNormal[19] == false && CurrentRewardClaimNormal[18] == true)
                {
                    btn_Reward_20.Enabled = true;
                    btn_Reward_20.BackColor = Color.FromArgb(6, 176, 37);
                }
                else if (CurrentRewardClaimNormal[19] == false)
                    btn_Reward_20.BackColor = Color.FromArgb(55, 47, 36);
                else btn_Reward_20.BackColor = Color.FromArgb(90, 80, 70);
            else txt_Punti_Reward_20.BackColor = Color.FromArgb(230, 230, 230);
        }
        private void Check_Unlock_Reward_GamePass_Base()
        {
            if (Variabili_Client.Utente.User_GamePass_Base == false) return; // GamePass base attivo?
            int point = Convert.ToInt32(Variabili_Client.Utente.Montly_Quest_Point);

            if (point >= CurrentRewardPoints[0])
                if (CurrentRewardClaimVip[0] == false)
                {
                    btn_Reward_Vip_1.Enabled = true;
                    btn_Reward_Vip_1.BackColor = Color.FromArgb(6, 176, 37);
                }
                else btn_Reward_Vip_1.BackColor = Color.FromArgb(90, 80, 70);

            if (point >= CurrentRewardPoints[1])
                if (CurrentRewardClaimVip[0] == true && CurrentRewardClaimVip[1] == false)
                {
                    btn_Reward_Vip_2.Enabled = true;
                    btn_Reward_Vip_2.BackColor = Color.FromArgb(6, 176, 37);
                }
                else if (CurrentRewardClaimVip[1] == false)
                    btn_Reward_Vip_2.BackColor = Color.FromArgb(55, 47, 36);
                else btn_Reward_Vip_2.BackColor = Color.FromArgb(90, 80, 70);

            if (point >= CurrentRewardPoints[2])
                if (CurrentRewardClaimVip[1] == true && CurrentRewardClaimVip[2] == false)
                {
                    btn_Reward_Vip_3.Enabled = true;
                    btn_Reward_Vip_3.BackColor = Color.FromArgb(6, 176, 37);
                }
                else if (CurrentRewardClaimVip[2] == false)
                    btn_Reward_Vip_3.BackColor = Color.FromArgb(55, 47, 36);
                else btn_Reward_Vip_3.BackColor = Color.FromArgb(90, 80, 70);

            if (point >= CurrentRewardPoints[3])
                if (CurrentRewardClaimVip[2] == true && CurrentRewardClaimVip[3] == false)
                {
                    btn_Reward_Vip_4.Enabled = true;
                    btn_Reward_Vip_4.BackColor = Color.FromArgb(6, 176, 37);
                }
                else if (CurrentRewardClaimVip[3] == false)
                    btn_Reward_Vip_4.BackColor = Color.FromArgb(55, 47, 36);
                else btn_Reward_Vip_4.BackColor = Color.FromArgb(90, 80, 70);

            if (point >= CurrentRewardPoints[4])
                if (CurrentRewardClaimVip[3] == true && CurrentRewardClaimVip[4] == false)
                {
                    btn_Reward_Vip_5.Enabled = true;
                    btn_Reward_Vip_5.BackColor = Color.FromArgb(6, 176, 37);
                }
                else if (CurrentRewardClaimVip[4] == false)
                    btn_Reward_Vip_5.BackColor = Color.FromArgb(55, 47, 36);
                else btn_Reward_Vip_5.BackColor = Color.FromArgb(90, 80, 70);

            if (point >= CurrentRewardPoints[5])
                if (CurrentRewardClaimVip[4] == true && CurrentRewardClaimVip[5] == false)
                {
                    btn_Reward_Vip_6.Enabled = true;
                    btn_Reward_Vip_6.BackColor = Color.FromArgb(6, 176, 37);
                }
                else if (CurrentRewardClaimVip[5] == false)
                    btn_Reward_Vip_6.BackColor = Color.FromArgb(55, 47, 36);
                else btn_Reward_Vip_6.BackColor = Color.FromArgb(90, 80, 70);

            if (point >= CurrentRewardPoints[6])
                if (CurrentRewardClaimVip[5] == true && CurrentRewardClaimVip[6] == false)
                {
                    btn_Reward_Vip_7.Enabled = true;
                    btn_Reward_Vip_7.BackColor = Color.FromArgb(6, 176, 37);
                }
                else if (CurrentRewardClaimVip[6] == false)
                    btn_Reward_Vip_7.BackColor = Color.FromArgb(55, 47, 36);
                else btn_Reward_Vip_7.BackColor = Color.FromArgb(90, 80, 70);

            if (point >= CurrentRewardPoints[7])
                if (CurrentRewardClaimVip[6] == true && CurrentRewardClaimVip[7] == false)
                {
                    btn_Reward_Vip_8.Enabled = true;
                    btn_Reward_Vip_8.BackColor = Color.FromArgb(6, 176, 37);
                }
                else if (CurrentRewardClaimVip[7] == false)
                    btn_Reward_Vip_8.BackColor = Color.FromArgb(55, 47, 36);
                else btn_Reward_Vip_8.BackColor = Color.FromArgb(90, 80, 70);

            if (point >= CurrentRewardPoints[8])
                if (CurrentRewardClaimVip[7] == true && CurrentRewardClaimVip[8] == false)
                {
                    btn_Reward_Vip_9.Enabled = true;
                    btn_Reward_Vip_9.BackColor = Color.FromArgb(6, 176, 37);
                }
                else if (CurrentRewardClaimVip[8] == false)
                    btn_Reward_Vip_9.BackColor = Color.FromArgb(55, 47, 36);
                else btn_Reward_Vip_9.BackColor = Color.FromArgb(90, 80, 70);

            if (point >= CurrentRewardPoints[9])
                if (CurrentRewardClaimVip[8] == true && CurrentRewardClaimVip[9] == false)
                {
                    btn_Reward_Vip_10.Enabled = true;
                    btn_Reward_Vip_10.BackColor = Color.FromArgb(6, 176, 37);
                }
                else if (CurrentRewardClaimVip[9] == false)
                    btn_Reward_Vip_10.BackColor = Color.FromArgb(55, 47, 36);
                else btn_Reward_Vip_10.BackColor = Color.FromArgb(90, 80, 70);

            if (point >= CurrentRewardPoints[10])
                if (CurrentRewardClaimVip[9] == true && CurrentRewardClaimVip[10] == false)
                {
                    btn_Reward_Vip_11.Enabled = true;
                    btn_Reward_Vip_11.BackColor = Color.FromArgb(6, 176, 37);
                }
                else if (CurrentRewardClaimVip[10] == false)
                    btn_Reward_Vip_11.BackColor = Color.FromArgb(55, 47, 36);
                else btn_Reward_Vip_11.BackColor = Color.FromArgb(90, 80, 70);

            if (point >= CurrentRewardPoints[11])
                if (CurrentRewardClaimVip[10] == true && CurrentRewardClaimVip[11] == false)
                {
                    btn_Reward_Vip_12.Enabled = true;
                    btn_Reward_Vip_12.BackColor = Color.FromArgb(6, 176, 37);
                }
                else if (CurrentRewardClaimVip[11] == false)
                    btn_Reward_Vip_12.BackColor = Color.FromArgb(55, 47, 36);
                else btn_Reward_Vip_12.BackColor = Color.FromArgb(90, 80, 70);

            if (point >= CurrentRewardPoints[12])
                if (CurrentRewardClaimVip[11] == true && CurrentRewardClaimVip[12] == false)
                {
                    btn_Reward_Vip_13.Enabled = true;
                    btn_Reward_Vip_13.BackColor = Color.FromArgb(6, 176, 37);
                }
                else if (CurrentRewardClaimVip[12] == false)
                    btn_Reward_Vip_13.BackColor = Color.FromArgb(55, 47, 36);
                else btn_Reward_Vip_13.BackColor = Color.FromArgb(90, 80, 70);

            if (point >= CurrentRewardPoints[13])
                if (CurrentRewardClaimVip[12] == true && CurrentRewardClaimVip[13] == false)
                {
                    btn_Reward_Vip_14.Enabled = true;
                    btn_Reward_Vip_14.BackColor = Color.FromArgb(6, 176, 37);
                }
                else if (CurrentRewardClaimVip[13] == false)
                    btn_Reward_Vip_14.BackColor = Color.FromArgb(55, 47, 36);
                else btn_Reward_Vip_14.BackColor = Color.FromArgb(90, 80, 70);

            if (point >= CurrentRewardPoints[14])
                if (CurrentRewardClaimVip[13] == true && CurrentRewardClaimVip[14] == false)
                {
                    btn_Reward_Vip_15.Enabled = true;
                    btn_Reward_Vip_15.BackColor = Color.FromArgb(6, 176, 37);
                }
                else if (CurrentRewardClaimVip[14] == false)
                    btn_Reward_Vip_15.BackColor = Color.FromArgb(55, 47, 36);
                else btn_Reward_Vip_15.BackColor = Color.FromArgb(90, 80, 70);

            if (point >= CurrentRewardPoints[15])
                if (CurrentRewardClaimVip[14] == true && CurrentRewardClaimVip[15] == false)
                {
                    btn_Reward_Vip_16.Enabled = true;
                    btn_Reward_Vip_16.BackColor = Color.FromArgb(6, 176, 37);
                }
                else if (CurrentRewardClaimVip[15] == false)
                    btn_Reward_Vip_16.BackColor = Color.FromArgb(55, 47, 36);
                else btn_Reward_Vip_16.BackColor = Color.FromArgb(90, 80, 70);

            if (point >= CurrentRewardPoints[16])
                if (CurrentRewardClaimVip[15] == true && CurrentRewardClaimVip[16] == false)
                {
                    btn_Reward_Vip_17.Enabled = true;
                    btn_Reward_Vip_17.BackColor = Color.FromArgb(6, 176, 37);
                }
                else if (CurrentRewardClaimVip[16] == false)
                    btn_Reward_Vip_17.BackColor = Color.FromArgb(55, 47, 36);
                else btn_Reward_Vip_17.BackColor = Color.FromArgb(90, 80, 70);

            if (point >= CurrentRewardPoints[17])
                if (CurrentRewardClaimVip[16] == true && CurrentRewardClaimVip[17] == false)
                {
                    btn_Reward_Vip_18.Enabled = true;
                    btn_Reward_Vip_18.BackColor = Color.FromArgb(6, 176, 37);
                }
                else if (CurrentRewardClaimVip[17] == false)
                    btn_Reward_Vip_18.BackColor = Color.FromArgb(55, 47, 36);
                else btn_Reward_Vip_18.BackColor = Color.FromArgb(90, 80, 70);

            if (point >= CurrentRewardPoints[18])
                if (CurrentRewardClaimVip[17] == true && CurrentRewardClaimVip[18] == false)
                {
                    btn_Reward_Vip_19.Enabled = true;
                    btn_Reward_Vip_19.BackColor = Color.FromArgb(6, 176, 37);
                }
                else if (CurrentRewardClaimVip[18] == false)
                    btn_Reward_Vip_19.BackColor = Color.FromArgb(55, 47, 36);
                else btn_Reward_Vip_19.BackColor = Color.FromArgb(90, 80, 70);

            if (point >= CurrentRewardPoints[19])
                if (CurrentRewardClaimVip[18] == true && CurrentRewardClaimVip[19] == false)
                {
                    btn_Reward_Vip_20.Enabled = true;
                    btn_Reward_Vip_20.BackColor = Color.FromArgb(6, 176, 37);
                }
                else if (CurrentRewardClaimVip[19] == false)
                    btn_Reward_Vip_20.BackColor = Color.FromArgb(55, 47, 36);
                else btn_Reward_Vip_20.BackColor = Color.FromArgb(90, 80, 70);
        }
        private void Update_Reward()
        {
            int n = Math.Min(20, CurrentRewardPoints.Count);

            for (int i = 1; i <= n; i++)
            {
                var txtPunti = (TextBox)Controls.Find($"txt_Punti_Reward_{i}", true)[0];
                var txtNormale = (TextBox)Controls.Find($"txt_Reward_{i}", true)[0];
                var txtVip = (TextBox)Controls.Find($"txt_Reward_Vip_{i}", true)[0];
                var btnNormale = (Button)Controls.Find($"btn_Reward_{i}", true)[0];
                var btnVip = (Button)Controls.Find($"btn_Reward_Vip_{i}", true)[0];

                txtPunti.Text = CurrentRewardPoints[i - 1].ToString();
                txtNormale.Text = CurrentRewardsNormali[i - 1].ToString();
                txtVip.Text = CurrentRewardsVip[i - 1].ToString();
                btnNormale.Enabled = false;
                btnVip.Enabled = false;
            }
        }

        private void Btn_Costruzione_Click(object sender, EventArgs e)
        {
            currentIndex = (currentIndex + 10) % CurrentQuests.Count; //Varia l'index per cambiare le Quest_Reward mostrate
            AggiornaInterfacciaQuest();
        }

        async void Scroll_Panel(int Valore)
        {
            Thread.Sleep(400); // Attendi 100 millisecondi
            if ((Variabili_Client.Utente.User_Vip == true && CurrentRewardClaimVip[Valore] == true && CurrentRewardClaimNormal[Valore] == true) ||
                (Variabili_Client.Utente.User_Vip == false && CurrentRewardClaimNormal[Valore] == true))
                panel1.AutoScrollPosition = new Point(Math.Abs(panel1.AutoScrollPosition.X) + 80); // Scorri di 100 pixel verso destra


            Check_Unlock_Reward();
            Check_Unlock_Reward_GamePass_Base();
            this.ActiveControl = null;
        }

        #region btn_Reward_Click F2P
        private async void btn_Reward_1_Click(object sender, EventArgs e)
        {
            if (CurrentRewardClaimNormal[0] == false)
                ComandiInvio.Quest_Reward("Normale", 1);
            else btn_Reward_1.Enabled = false;

            Scroll_Panel(0);
        }

        private void btn_Reward_2_Click(object sender, EventArgs e)
        {
            if (CurrentRewardClaimNormal[1] == false)
                ComandiInvio.Quest_Reward("Normale", 2);
            else btn_Reward_2.Enabled = false;

            Scroll_Panel(1);
        }

        private void btn_Reward_3_Click(object sender, EventArgs e)
        {
            if (CurrentRewardClaimNormal[2] == false)
                ComandiInvio.Quest_Reward("Normale", 3);
            else btn_Reward_3.Enabled = false;

            Scroll_Panel(2);
        }

        private void btn_Reward_4_Click(object sender, EventArgs e)
        {
            if (CurrentRewardClaimNormal[3] == false)
                ComandiInvio.Quest_Reward("Normale", 4);
            else btn_Reward_4.Enabled = false;

            Scroll_Panel(3);
        }

        private void btn_Reward_5_Click(object sender, EventArgs e)
        {
            if (CurrentRewardClaimNormal[4] == false)
                ComandiInvio.Quest_Reward("Normale", 5);
            else btn_Reward_5.Enabled = false;

            Scroll_Panel(4);
        }

        private void btn_Reward_6_Click(object sender, EventArgs e)
        {
            if (CurrentRewardClaimNormal[5] == false)
                ComandiInvio.Quest_Reward("Normale", 6);
            else btn_Reward_6.Enabled = false;

            Scroll_Panel(5);
        }

        private void btn_Reward_7_Click(object sender, EventArgs e)
        {
            if (CurrentRewardClaimNormal[6] == false)
                ComandiInvio.Quest_Reward("Normale", 7);
            else btn_Reward_7.Enabled = false;

            Scroll_Panel(6);
        }

        private void btn_Reward_8_Click(object sender, EventArgs e)
        {
            if (CurrentRewardClaimNormal[7] == false)
                ComandiInvio.Quest_Reward("Normale", 8);
            else btn_Reward_8.Enabled = false;

            Scroll_Panel(7);
        }

        private void btn_Reward_9_Click(object sender, EventArgs e)
        {
            if (CurrentRewardClaimNormal[8] == false)
                ComandiInvio.Quest_Reward("Normale", 9);
            else btn_Reward_9.Enabled = false;

            Scroll_Panel(8);
        }

        private void btn_Reward_10_Click(object sender, EventArgs e)
        {
            if (CurrentRewardClaimNormal[9] == false)
                ComandiInvio.Quest_Reward("Normale", 10);
            else btn_Reward_10.Enabled = false;

            Scroll_Panel(9);
        }

        private void btn_Reward_11_Click(object sender, EventArgs e)
        {
            if (CurrentRewardClaimNormal[10] == false)
                ComandiInvio.Quest_Reward("Normale", 11);
            else btn_Reward_11.Enabled = false;

            Scroll_Panel(10);
        }

        private void btn_Reward_12_Click(object sender, EventArgs e)
        {
            if (CurrentRewardClaimNormal[11] == false)
                ComandiInvio.Quest_Reward("Normale", 12);
            else btn_Reward_12.Enabled = false;

            Scroll_Panel(11);
        }

        private void btn_Reward_13_Click(object sender, EventArgs e)
        {
            if (CurrentRewardClaimNormal[12] == false)
                ComandiInvio.Quest_Reward("Normale", 13);
            else btn_Reward_13.Enabled = false;

            Scroll_Panel(12);
        }

        private void btn_Reward_14_Click(object sender, EventArgs e)
        {
            if (CurrentRewardClaimNormal[13] == false)
                ComandiInvio.Quest_Reward("Normale", 14);
            else btn_Reward_14.Enabled = false;

            Scroll_Panel(13);
        }

        private void btn_Reward_15_Click(object sender, EventArgs e)
        {
            if (CurrentRewardClaimNormal[14] == false)
                ComandiInvio.Quest_Reward("Normale", 15);
            else btn_Reward_15.Enabled = false;

            Scroll_Panel(14);
        }

        private void btn_Reward_16_Click(object sender, EventArgs e)
        {
            if (CurrentRewardClaimNormal[15] == false)
                ComandiInvio.Quest_Reward("Normale", 16);
            else btn_Reward_16.Enabled = false;

            Scroll_Panel(15);
        }

        private void btn_Reward_17_Click(object sender, EventArgs e)
        {
            if (CurrentRewardClaimNormal[16] == false)
                ComandiInvio.Quest_Reward("Normale", 17);
            else btn_Reward_17.Enabled = false;

            Scroll_Panel(16);
        }

        private void btn_Reward_18_Click(object sender, EventArgs e)
        {
            if (CurrentRewardClaimNormal[17] == false)
                ComandiInvio.Quest_Reward("Normale", 18);
            else btn_Reward_18.Enabled = false;

            Scroll_Panel(17);
        }

        private void btn_Reward_19_Click(object sender, EventArgs e)
        {
            if (CurrentRewardClaimNormal[18] == false)
                ComandiInvio.Quest_Reward("Normale", 19);
            else btn_Reward_19.Enabled = false;

            Scroll_Panel(18);
        }

        private void btn_Reward_20_Click(object sender, EventArgs e)
        {
            if (CurrentRewardClaimNormal[19] == false)
                ComandiInvio.Quest_Reward("Normale", 20);
            else btn_Reward_20.Enabled = false;

            Scroll_Panel(19);
        }
        #endregion
        #region btn_Reward_Click VIP
        private void btn_Reward_Vip_1_Click(object sender, EventArgs e)
        {
            if (CurrentRewardClaimVip[0] == false)
                ComandiInvio.Quest_Reward("Vip", 1);
            else btn_Reward_Vip_1.Enabled = false;

            Scroll_Panel(0);
        }

        private void btn_Reward_Vip_2_Click(object sender, EventArgs e)
        {
            if (CurrentRewardClaimVip[1] == false)
                ComandiInvio.Quest_Reward("Vip", 2);
            else btn_Reward_Vip_2.Enabled = false;

            Scroll_Panel(1);
        }

        private void btn_Reward_Vip_3_Click(object sender, EventArgs e)
        {
            if (CurrentRewardClaimVip[2] == false)
                ComandiInvio.Quest_Reward("Vip", 3);
            else btn_Reward_Vip_3.Enabled = false;

            Scroll_Panel(2);
        }

        private void btn_Reward_Vip_4_Click(object sender, EventArgs e)
        {
            if (CurrentRewardClaimVip[3] == false)
                ComandiInvio.Quest_Reward("Vip", 4);
            else btn_Reward_Vip_4.Enabled = false;

            Scroll_Panel(3);
        }

        private void btn_Reward_Vip_5_Click(object sender, EventArgs e)
        {
            if (CurrentRewardClaimVip[4] == false)
                ComandiInvio.Quest_Reward("Vip", 5);
            else btn_Reward_Vip_5.Enabled = false;

            Scroll_Panel(4);
        }

        private void btn_Reward_Vip_6_Click(object sender, EventArgs e)
        {
            if (CurrentRewardClaimVip[5] == false)
                ComandiInvio.Quest_Reward("Vip", 6);
            else btn_Reward_Vip_6.Enabled = false;

            Scroll_Panel(5);
        }

        private void btn_Reward_Vip_7_Click(object sender, EventArgs e)
        {
            if (CurrentRewardClaimVip[6] == false)
                ComandiInvio.Quest_Reward("Vip", 7);
            else btn_Reward_Vip_7.Enabled = false;

            Scroll_Panel(6);
        }

        private void btn_Reward_Vip_8_Click(object sender, EventArgs e)
        {
            if (CurrentRewardClaimVip[7] == false)
                ComandiInvio.Quest_Reward("Vip", 8);
            else btn_Reward_Vip_8.Enabled = false;

            Scroll_Panel(7);
        }

        private void btn_Reward_Vip_9_Click(object sender, EventArgs e)
        {
            if (CurrentRewardClaimVip[8] == false)
                ComandiInvio.Quest_Reward("Vip", 9);
            else btn_Reward_Vip_9.Enabled = false;

            Scroll_Panel(8);
        }

        private void btn_Reward_Vip_10_Click(object sender, EventArgs e)
        {
            if (CurrentRewardClaimVip[9] == false)
                ComandiInvio.Quest_Reward("Vip", 10);
            else btn_Reward_Vip_10.Enabled = false;

            Scroll_Panel(9);
        }

        private void btn_Reward_Vip_11_Click(object sender, EventArgs e)
        {
            if (CurrentRewardClaimVip[10] == false)
                ComandiInvio.Quest_Reward("Vip", 11);
            else btn_Reward_Vip_11.Enabled = false;

            Scroll_Panel(10);
        }

        private void btn_Reward_Vip_12_Click(object sender, EventArgs e)
        {
            if (CurrentRewardClaimVip[11] == false)
                ComandiInvio.Quest_Reward("Vip", 12);
            else btn_Reward_Vip_12.Enabled = false;

            Scroll_Panel(11);
        }

        private void btn_Reward_Vip_13_Click(object sender, EventArgs e)
        {
            if (CurrentRewardClaimVip[12] == false)
                ComandiInvio.Quest_Reward("Vip", 13);
            else btn_Reward_Vip_13.Enabled = false;

            Scroll_Panel(12);
        }

        private void btn_Reward_Vip_14_Click(object sender, EventArgs e)
        {
            if (CurrentRewardClaimVip[14] == false)
                ComandiInvio.Quest_Reward("Vip", 14);
            else btn_Reward_Vip_14.Enabled = false;

            Scroll_Panel(13);
        }

        private void btn_Reward_Vip_15_Click(object sender, EventArgs e)
        {
            if (CurrentRewardClaimVip[14] == false)
                ComandiInvio.Quest_Reward("Vip", 15);
            else btn_Reward_Vip_15.Enabled = false;

            Scroll_Panel(14);
        }

        private void btn_Reward_Vip_16_Click(object sender, EventArgs e)
        {
            if (CurrentRewardClaimVip[15] == false)
                ComandiInvio.Quest_Reward("Vip", 16);
            else btn_Reward_Vip_16.Enabled = false;

            Scroll_Panel(15);
        }

        private void btn_Reward_Vip_17_Click(object sender, EventArgs e)
        {
            if (CurrentRewardClaimVip[16] == false)
                ComandiInvio.Quest_Reward("Vip", 17);
            else btn_Reward_Vip_17.Enabled = false;

            Scroll_Panel(16);
        }

        private void btn_Reward_Vip_18_Click(object sender, EventArgs e)
        {
            if (CurrentRewardClaimVip[17] == false)
                ComandiInvio.Quest_Reward("Vip", 18);
            else btn_Reward_Vip_18.Enabled = false;

            Scroll_Panel(17);
        }

        private void btn_Reward_Vip_19_Click(object sender, EventArgs e)
        {
            if (CurrentRewardClaimVip[18] == false)
                ComandiInvio.Quest_Reward("Vip", 19);
            else btn_Reward_Vip_19.Enabled = false;

            Scroll_Panel(18);
        }

        private void btn_Reward_Vip_20_Click(object sender, EventArgs e)
        {
            if (CurrentRewardClaimVip[19] == false)
                ComandiInvio.Quest_Reward("Vip", 20);
            else btn_Reward_Vip_20.Enabled = false;

            Scroll_Panel(19);
        }
        #endregion

        private void MontlyQuest_FormClosing(object sender, FormClosingEventArgs e)
        {
            cts.Cancel();
        }
    }
}
