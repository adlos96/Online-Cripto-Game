using Strategico_V2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CriptoGame_Online
{
    public partial class MontlyQuest : Form
    {
        public MontlyQuest()
        {
            InitializeComponent();

        }

        private void SetProgressValue(int newValue)
        {
            progressBar1.Value += newValue;
            OnProgressValueChanged();

        }

        private void OnProgressValueChanged()
        {
            if (progressBar1.Value > 20)
                txt_Punti_Reward_1.BackColor = Color.FromArgb(6, 176, 37);
        }

        private void MontlyQuest_Load(object sender, EventArgs e)
        {
            GUI();
            Update();
        }
        private void GUI()
        {
            Update_Reward();
            Update_Quest();
            Check_Unlock_Reward();
            Check_Unlock_Reward_Vip();
        }
        private void Update()
        {

        }

        private void Check_Unlock_Reward()
        {
            int point = Convert.ToInt32(Variabili_Client.Quest_Reward.Point_Montly.Points_1);

            if (point >= Convert.ToInt32(txt_Punti_Reward_1.Text))
            {
                btn_Reward_1.Enabled = true;
                btn_Reward_1.BackColor = Color.FromArgb(6, 176, 37);
            }
            if (point >= Convert.ToInt32(txt_Punti_Reward_2.Text))
            {
                btn_Reward_2.Enabled = true;
                btn_Reward_2.BackColor = Color.FromArgb(6, 176, 37);
            }
            if (point >= Convert.ToInt32(txt_Punti_Reward_3.Text))
            {
                btn_Reward_3.Enabled = true;
                btn_Reward_3.BackColor = Color.FromArgb(6, 176, 37);
            }
            if (point >= Convert.ToInt32(txt_Punti_Reward_4.Text))
            {
                btn_Reward_4.Enabled = true;
                btn_Reward_4.BackColor = Color.FromArgb(6, 176, 37);
            }
            if (point >= Convert.ToInt32(txt_Punti_Reward_5.Text))
            {
                btn_Reward_5.Enabled = true;
                btn_Reward_5.BackColor = Color.FromArgb(6, 176, 37);
            }
            if (point >= Convert.ToInt32(txt_Punti_Reward_6.Text))
            {
                btn_Reward_6.Enabled = true;
                btn_Reward_6.BackColor = Color.FromArgb(6, 176, 37);
            }
            if (point >= Convert.ToInt32(txt_Punti_Reward_7.Text))
            {
                btn_Reward_7.Enabled = true;
                btn_Reward_7.BackColor = Color.FromArgb(6, 176, 37);
            }
            if (point >= Convert.ToInt32(txt_Punti_Reward_8.Text))
            {
                btn_Reward_8.Enabled = true;
                btn_Reward_8.BackColor = Color.FromArgb(6, 176, 37);
            }
            if (point >= Convert.ToInt32(txt_Punti_Reward_9.Text))
            {
                btn_Reward_9.Enabled = true;
                btn_Reward_9.BackColor = Color.FromArgb(6, 176, 37);
            }
            if (point >= Convert.ToInt32(txt_Punti_Reward_10.Text))
            {
                btn_Reward_10.Enabled = true;
                btn_Reward_10.BackColor = Color.FromArgb(6, 176, 37);
            }
            if (point >= Convert.ToInt32(txt_Punti_Reward_11.Text))
            {
                btn_Reward_11.Enabled = true;
                btn_Reward_11.BackColor = Color.FromArgb(6, 176, 37);
            }
            if (point >= Convert.ToInt32(txt_Punti_Reward_12.Text))
            {
                btn_Reward_12.Enabled = true;
                btn_Reward_12.BackColor = Color.FromArgb(6, 176, 37);
            }
            if (point >= Convert.ToInt32(txt_Punti_Reward_13.Text))
            {
                btn_Reward_13.Enabled = true;
                btn_Reward_13.BackColor = Color.FromArgb(6, 176, 37);
            }
            if (point >= Convert.ToInt32(txt_Punti_Reward_14.Text))
            {
                btn_Reward_14.Enabled = true;
                btn_Reward_14.BackColor = Color.FromArgb(6, 176, 37);
            }
            if (point >= Convert.ToInt32(txt_Punti_Reward_15.Text))
            {
                btn_Reward_15.Enabled = true;
                btn_Reward_15.BackColor = Color.FromArgb(6, 176, 37);
            }
            if (point >= Convert.ToInt32(txt_Punti_Reward_16.Text))
            {
                btn_Reward_16.Enabled = true;
                btn_Reward_16.BackColor = Color.FromArgb(6, 176, 37);
            }
            if (point >= Convert.ToInt32(txt_Punti_Reward_17.Text))
            {
                btn_Reward_17.Enabled = true;
                btn_Reward_17.BackColor = Color.FromArgb(6, 176, 37);
            }
            if (point >= Convert.ToInt32(txt_Punti_Reward_18.Text))
            {
                btn_Reward_18.Enabled = true;
                btn_Reward_18.BackColor = Color.FromArgb(6, 176, 37);
            }
            if (point >= Convert.ToInt32(txt_Punti_Reward_19.Text))
            {
                btn_Reward_19.Enabled = true;
                btn_Reward_19.BackColor = Color.FromArgb(6, 176, 37);
            }
            if (point >= Convert.ToInt32(txt_Punti_Reward_20.Text))
            {
                btn_Reward_20.Enabled = true;
                btn_Reward_20.BackColor = Color.FromArgb(6, 176, 37);
            }
        }
        private void Check_Unlock_Reward_Vip()
        {
            if (Variabili_Client.Utente.User_Vip == false) return; // Vip attivo?
            int point = Convert.ToInt32(Variabili_Client.Quest_Reward.Point_Montly.Points_1);

            if (point >= Convert.ToInt32(txt_Punti_Reward_1.Text))
            {
                btn_Reward_Vip_1.Enabled = true;
                btn_Reward_Vip_1.BackColor = Color.FromArgb(6, 176, 37);
            }
            if (point >= Convert.ToInt32(txt_Punti_Reward_2.Text))
            {
                btn_Reward_Vip_2.Enabled = true;
                btn_Reward_Vip_2.BackColor = Color.FromArgb(6, 176, 37);
            }
            if (point >= Convert.ToInt32(txt_Punti_Reward_3.Text))
            {
                btn_Reward_Vip_3.Enabled = true;
                btn_Reward_Vip_3.BackColor = Color.FromArgb(6, 176, 37);
            }
            if (point >= Convert.ToInt32(txt_Punti_Reward_4.Text))
            {
                btn_Reward_Vip_4.Enabled = true;
                btn_Reward_Vip_4.BackColor = Color.FromArgb(6, 176, 37);
            }
            if (point >= Convert.ToInt32(txt_Punti_Reward_5.Text))
            {
                btn_Reward_Vip_5.Enabled = true;
                btn_Reward_Vip_5.BackColor = Color.FromArgb(6, 176, 37);
            }
            if (point >= Convert.ToInt32(txt_Punti_Reward_6.Text))
            {
                btn_Reward_Vip_6.Enabled = true;
                btn_Reward_Vip_6.BackColor = Color.FromArgb(6, 176, 37);
            }
            if (point >= Convert.ToInt32(txt_Punti_Reward_7.Text))
            {
                btn_Reward_Vip_7.Enabled = true;
                btn_Reward_Vip_7.BackColor = Color.FromArgb(6, 176, 37);
            }
            if (point >= Convert.ToInt32(txt_Punti_Reward_8.Text))
            {
                btn_Reward_Vip_8.Enabled = true;
                btn_Reward_Vip_8.BackColor = Color.FromArgb(6, 176, 37);
            }
            if (point >= Convert.ToInt32(txt_Punti_Reward_9.Text))
            {
                btn_Reward_Vip_9.Enabled = true;
                btn_Reward_Vip_9.BackColor = Color.FromArgb(6, 176, 37);
            }
            if (point >= Convert.ToInt32(txt_Punti_Reward_10.Text))
            {
                btn_Reward_Vip_10.Enabled = true;
                btn_Reward_Vip_10.BackColor = Color.FromArgb(6, 176, 37);
            }
            if (point >= Convert.ToInt32(txt_Punti_Reward_11.Text))
            {
                btn_Reward_Vip_11.Enabled = true;
                btn_Reward_Vip_11.BackColor = Color.FromArgb(6, 176, 37);
            }
            if (point >= Convert.ToInt32(txt_Punti_Reward_12.Text))
            {
                btn_Reward_Vip_12.Enabled = true;
                btn_Reward_Vip_12.BackColor = Color.FromArgb(6, 176, 37);
            }
            if (point >= Convert.ToInt32(txt_Punti_Reward_13.Text))
            {
                btn_Reward_Vip_13.Enabled = true;
                btn_Reward_Vip_13.BackColor = Color.FromArgb(6, 176, 37);
            }
            if (point >= Convert.ToInt32(txt_Punti_Reward_14.Text))
            {
                btn_Reward_Vip_14.Enabled = true;
                btn_Reward_Vip_14.BackColor = Color.FromArgb(6, 176, 37);
            }
            if (point >= Convert.ToInt32(txt_Punti_Reward_15.Text))
            {
                btn_Reward_Vip_15.Enabled = true;
                btn_Reward_Vip_15.BackColor = Color.FromArgb(6, 176, 37);
            }
            if (point >= Convert.ToInt32(txt_Punti_Reward_16.Text))
            {
                btn_Reward_Vip_16.Enabled = true;
                btn_Reward_Vip_16.BackColor = Color.FromArgb(6, 176, 37);
            }
            if (point >= Convert.ToInt32(txt_Punti_Reward_17.Text))
            {
                btn_Reward_Vip_17.Enabled = true;
                btn_Reward_Vip_17.BackColor = Color.FromArgb(6, 176, 37);
            }
            if (point >= Convert.ToInt32(txt_Punti_Reward_18.Text))
            {
                btn_Reward_Vip_18.Enabled = true;
                btn_Reward_Vip_18.BackColor = Color.FromArgb(6, 176, 37);
            }
            if (point >= Convert.ToInt32(txt_Punti_Reward_19.Text))
            {
                btn_Reward_Vip_19.Enabled = true;
                btn_Reward_Vip_19.BackColor = Color.FromArgb(6, 176, 37);
            }
            if (point >= Convert.ToInt32(txt_Punti_Reward_20.Text))
            {
                btn_Reward_Vip_20.Enabled = true;
                btn_Reward_Vip_20.BackColor = Color.FromArgb(6, 176, 37);
            }
        }
        private void Update_Reward()
        {
            //Reward 1
            txt_Punti_Reward_1.Text = Variabili_Client.Quest_Reward.Point_Montly.Points_1;
            txt_Reward_1.Text = Variabili_Client.Quest_Reward.Normali_Montly.Reward_1;
            txt_Reward_Vip_1.Text = Variabili_Client.Quest_Reward.Vip_Montly.Reward_1;
            btn_Reward_1.Enabled = false;
            btn_Reward_Vip_1.Enabled = false;

            //Reward 2
            txt_Punti_Reward_2.Text = Variabili_Client.Quest_Reward.Point_Montly.Points_2;
            txt_Reward_2.Text = Variabili_Client.Quest_Reward.Normali_Montly.Reward_2;
            txt_Reward_Vip_2.Text = Variabili_Client.Quest_Reward.Vip_Montly.Reward_2;
            btn_Reward_2.Enabled = false;
            btn_Reward_Vip_2.Enabled = false;


            //Reward 3
            txt_Punti_Reward_3.Text = Variabili_Client.Quest_Reward.Point_Montly.Points_3;
            txt_Reward_3.Text = Variabili_Client.Quest_Reward.Normali_Montly.Reward_3;
            txt_Reward_Vip_3.Text = Variabili_Client.Quest_Reward.Vip_Montly.Reward_3;
            btn_Reward_3.Enabled = false;
            btn_Reward_Vip_3.Enabled = false;

            //Reward 4
            txt_Punti_Reward_4.Text = Variabili_Client.Quest_Reward.Point_Montly.Points_4;
            txt_Reward_4.Text = Variabili_Client.Quest_Reward.Normali_Montly.Reward_4;
            txt_Reward_Vip_4.Text = Variabili_Client.Quest_Reward.Vip_Montly.Reward_4;
            btn_Reward_4.Enabled = false;
            btn_Reward_Vip_4.Enabled = false;

            //Reward 5
            txt_Punti_Reward_5.Text = Variabili_Client.Quest_Reward.Point_Montly.Points_5;
            txt_Reward_5.Text = Variabili_Client.Quest_Reward.Normali_Montly.Reward_5;
            txt_Reward_Vip_5.Text = Variabili_Client.Quest_Reward.Vip_Montly.Reward_5;
            btn_Reward_5.Enabled = false;
            btn_Reward_Vip_5.Enabled = false;

            //Reward 6
            txt_Punti_Reward_6.Text = Variabili_Client.Quest_Reward.Point_Montly.Points_6;
            txt_Reward_6.Text = Variabili_Client.Quest_Reward.Normali_Montly.Reward_6;
            txt_Reward_Vip_6.Text = Variabili_Client.Quest_Reward.Vip_Montly.Reward_6;
            btn_Reward_6.Enabled = false;
            btn_Reward_Vip_6.Enabled = false;

            //Reward 7
            txt_Punti_Reward_7.Text = Variabili_Client.Quest_Reward.Point_Montly.Points_7;
            txt_Reward_7.Text = Variabili_Client.Quest_Reward.Normali_Montly.Reward_7;
            txt_Reward_Vip_7.Text = Variabili_Client.Quest_Reward.Vip_Montly.Reward_7;
            btn_Reward_7.Enabled = false;
            btn_Reward_Vip_7.Enabled = false;

            //Reward 8
            txt_Punti_Reward_8.Text = Variabili_Client.Quest_Reward.Point_Montly.Points_8;
            txt_Reward_8.Text = Variabili_Client.Quest_Reward.Normali_Montly.Reward_8;
            txt_Reward_Vip_8.Text = Variabili_Client.Quest_Reward.Vip_Montly.Reward_8;
            btn_Reward_8.Enabled = false;
            btn_Reward_Vip_8.Enabled = false;

            //Reward 9
            txt_Punti_Reward_9.Text = Variabili_Client.Quest_Reward.Point_Montly.Points_9;
            txt_Reward_9.Text = Variabili_Client.Quest_Reward.Normali_Montly.Reward_9;
            txt_Reward_Vip_9.Text = Variabili_Client.Quest_Reward.Vip_Montly.Reward_9;
            btn_Reward_9.Enabled = false;
            btn_Reward_Vip_9.Enabled = false;

            //Reward 4
            txt_Punti_Reward_10.Text = Variabili_Client.Quest_Reward.Point_Montly.Points_10;
            txt_Reward_10.Text = Variabili_Client.Quest_Reward.Normali_Montly.Reward_10;
            txt_Reward_Vip_10.Text = Variabili_Client.Quest_Reward.Vip_Montly.Reward_10;
            btn_Reward_10.Enabled = false;
            btn_Reward_Vip_10.Enabled = false;

            //Reward 11
            txt_Punti_Reward_11.Text = Variabili_Client.Quest_Reward.Point_Montly.Points_11;
            txt_Reward_11.Text = Variabili_Client.Quest_Reward.Normali_Montly.Reward_11;
            txt_Reward_Vip_11.Text = Variabili_Client.Quest_Reward.Vip_Montly.Reward_11;
            btn_Reward_11.Enabled = false;
            btn_Reward_Vip_11.Enabled = false;

            //Reward 12
            txt_Punti_Reward_12.Text = Variabili_Client.Quest_Reward.Point_Montly.Points_12;
            txt_Reward_12.Text = Variabili_Client.Quest_Reward.Normali_Montly.Reward_12;
            txt_Reward_Vip_12.Text = Variabili_Client.Quest_Reward.Vip_Montly.Reward_12;
            btn_Reward_12.Enabled = false;
            btn_Reward_Vip_12.Enabled = false;

            //Reward 13
            txt_Punti_Reward_13.Text = Variabili_Client.Quest_Reward.Point_Montly.Points_13;
            txt_Reward_13.Text = Variabili_Client.Quest_Reward.Normali_Montly.Reward_13;
            txt_Reward_Vip_13.Text = Variabili_Client.Quest_Reward.Vip_Montly.Reward_13;
            btn_Reward_13.Enabled = false;
            btn_Reward_Vip_13.Enabled = false;

            //Reward 14
            txt_Punti_Reward_14.Text = Variabili_Client.Quest_Reward.Point_Montly.Points_14;
            txt_Reward_14.Text = Variabili_Client.Quest_Reward.Normali_Montly.Reward_14;
            txt_Reward_Vip_14.Text = Variabili_Client.Quest_Reward.Vip_Montly.Reward_14;
            btn_Reward_14.Enabled = false;
            btn_Reward_Vip_14.Enabled = false;

            //Reward 15
            txt_Punti_Reward_15.Text = Variabili_Client.Quest_Reward.Point_Montly.Points_15;
            txt_Reward_15.Text = Variabili_Client.Quest_Reward.Normali_Montly.Reward_15;
            txt_Reward_Vip_15.Text = Variabili_Client.Quest_Reward.Vip_Montly.Reward_15;
            btn_Reward_15.Enabled = false;
            btn_Reward_Vip_15.Enabled = false;

            //Reward 16
            txt_Punti_Reward_16.Text = Variabili_Client.Quest_Reward.Point_Montly.Points_16;
            txt_Reward_16.Text = Variabili_Client.Quest_Reward.Normali_Montly.Reward_16;
            txt_Reward_Vip_16.Text = Variabili_Client.Quest_Reward.Vip_Montly.Reward_16;
            btn_Reward_16.Enabled = false;
            btn_Reward_Vip_16.Enabled = false;

            //Reward 17
            txt_Punti_Reward_17.Text = Variabili_Client.Quest_Reward.Point_Montly.Points_17;
            txt_Reward_17.Text = Variabili_Client.Quest_Reward.Normali_Montly.Reward_17;
            txt_Reward_Vip_17.Text = Variabili_Client.Quest_Reward.Vip_Montly.Reward_17;
            btn_Reward_17.Enabled = false;
            btn_Reward_Vip_17.Enabled = false;

            //Reward 18
            txt_Punti_Reward_18.Text = Variabili_Client.Quest_Reward.Point_Montly.Points_18;
            txt_Reward_18.Text = Variabili_Client.Quest_Reward.Normali_Montly.Reward_18;
            txt_Reward_Vip_18.Text = Variabili_Client.Quest_Reward.Vip_Montly.Reward_18;
            btn_Reward_18.Enabled = false;
            btn_Reward_Vip_18.Enabled = false;

            //Reward 19
            txt_Punti_Reward_19.Text = Variabili_Client.Quest_Reward.Point_Montly.Points_19;
            txt_Reward_19.Text = Variabili_Client.Quest_Reward.Normali_Montly.Reward_19;
            txt_Reward_Vip_19.Text = Variabili_Client.Quest_Reward.Vip_Montly.Reward_19;
            btn_Reward_19.Enabled = false;
            btn_Reward_Vip_19.Enabled = false;

            //Reward 20
            txt_Punti_Reward_20.Text = Variabili_Client.Quest_Reward.Point_Montly.Points_20;
            txt_Reward_20.Text = Variabili_Client.Quest_Reward.Normali_Montly.Reward_20;
            txt_Reward_Vip_20.Text = Variabili_Client.Quest_Reward.Vip_Montly.Reward_20;
            btn_Reward_20.Enabled = false;
            btn_Reward_Vip_20.Enabled = false;
        }
        private void Update_Quest()
        {
            txt_Quest_Desc_2.Text = "[75 punti] Quest_Reward di prova";
            txt_Quest_2.Text = "0/4";
        }

        private void Btn_Costruzione_Click(object sender, EventArgs e)
        {
            SetProgressValue(5);
        }


        #region btn_Reward_Click F2P
        private void btn_Reward_1_Click(object sender, EventArgs e)
        {
            btn_Reward_1.Enabled = false;
            btn_Reward_1.BackColor = Color.FromArgb(55, 47, 36);
        }

        private void btn_Reward_2_Click(object sender, EventArgs e)
        {
            btn_Reward_2.Enabled = false;
            btn_Reward_2.BackColor = Color.FromArgb(55, 47, 36);
        }

        private void btn_Reward_3_Click(object sender, EventArgs e)
        {
            btn_Reward_3.Enabled = false;
            btn_Reward_3.BackColor = Color.FromArgb(55, 47, 36);
        }

        private void btn_Reward_4_Click(object sender, EventArgs e)
        {
            btn_Reward_4.Enabled = false;
            btn_Reward_4.BackColor = Color.FromArgb(55, 47, 36);
        }

        private void btn_Reward_5_Click(object sender, EventArgs e)
        {
            btn_Reward_5.Enabled = false;
            btn_Reward_5.BackColor = Color.FromArgb(55, 47, 36);
        }

        private void btn_Reward_6_Click(object sender, EventArgs e)
        {
            btn_Reward_6.Enabled = false;
            btn_Reward_6.BackColor = Color.FromArgb(55, 47, 36);
        }

        private void btn_Reward_7_Click(object sender, EventArgs e)
        {
            btn_Reward_7.Enabled = false;
            btn_Reward_7.BackColor = Color.FromArgb(55, 47, 36);
        }

        private void btn_Reward_8_Click(object sender, EventArgs e)
        {
            btn_Reward_8.Enabled = false;
            btn_Reward_8.BackColor = Color.FromArgb(55, 47, 36);
        }

        private void btn_Reward_9_Click(object sender, EventArgs e)
        {
            btn_Reward_9.Enabled = false;
            btn_Reward_9.BackColor = Color.FromArgb(55, 47, 36);
        }

        private void btn_Reward_10_Click(object sender, EventArgs e)
        {
            btn_Reward_10.Enabled = false;
            btn_Reward_10.BackColor = Color.FromArgb(55, 47, 36);
        }

        private void btn_Reward_11_Click(object sender, EventArgs e)
        {
            btn_Reward_11.Enabled = false;
            btn_Reward_11.BackColor = Color.FromArgb(55, 47, 36);
        }

        private void btn_Reward_12_Click(object sender, EventArgs e)
        {
            btn_Reward_12.Enabled = false;
            btn_Reward_12.BackColor = Color.FromArgb(55, 47, 36);
        }

        private void btn_Reward_13_Click(object sender, EventArgs e)
        {
            btn_Reward_13.Enabled = false;
            btn_Reward_13.BackColor = Color.FromArgb(55, 47, 36);
        }

        private void btn_Reward_14_Click(object sender, EventArgs e)
        {
            btn_Reward_14.Enabled = false;
            btn_Reward_14.BackColor = Color.FromArgb(55, 47, 36);
        }

        private void btn_Reward_15_Click(object sender, EventArgs e)
        {
            btn_Reward_15.Enabled = false;
            btn_Reward_15.BackColor = Color.FromArgb(55, 47, 36);
        }

        private void btn_Reward_16_Click(object sender, EventArgs e)
        {
            btn_Reward_16.Enabled = false;
            btn_Reward_16.BackColor = Color.FromArgb(55, 47, 36);
        }

        private void btn_Reward_17_Click(object sender, EventArgs e)
        {
            btn_Reward_17.Enabled = false;
            btn_Reward_17.BackColor = Color.FromArgb(55, 47, 36);
        }

        private void btn_Reward_18_Click(object sender, EventArgs e)
        {
            btn_Reward_18.Enabled = false;
            btn_Reward_18.BackColor = Color.FromArgb(55, 47, 36);
        }

        private void btn_Reward_19_Click(object sender, EventArgs e)
        {
            btn_Reward_19.Enabled = false;
            btn_Reward_19.BackColor = Color.FromArgb(55, 47, 36);
        }

        private void btn_Reward_20_Click(object sender, EventArgs e)
        {
            btn_Reward_20.Enabled = false;
            btn_Reward_20.BackColor = Color.FromArgb(55, 47, 36);
        }
        #endregion
        #region btn_Reward_Click VIP
        private void btn_Reward_Vip_1_Click(object sender, EventArgs e)
        {
            btn_Reward_Vip_1.Enabled = false;
            btn_Reward_Vip_1.BackColor = Color.FromArgb(55, 47, 36);
        }

        private void btn_Reward_Vip_2_Click(object sender, EventArgs e)
        {
            btn_Reward_Vip_2.Enabled = false;
            btn_Reward_Vip_2.BackColor = Color.FromArgb(55, 47, 36);
        }

        private void btn_Reward_Vip_3_Click(object sender, EventArgs e)
        {
            btn_Reward_Vip_3.Enabled = false;
            btn_Reward_Vip_3.BackColor = Color.FromArgb(55, 47, 36);
        }

        private void btn_Reward_Vip_4_Click(object sender, EventArgs e)
        {
            btn_Reward_Vip_4.Enabled = false;
            btn_Reward_Vip_4.BackColor = Color.FromArgb(55, 47, 36);
        }

        private void btn_Reward_Vip_5_Click(object sender, EventArgs e)
        {
            btn_Reward_Vip_5.Enabled = false;
            btn_Reward_Vip_5.BackColor = Color.FromArgb(55, 47, 36);
        }

        private void btn_Reward_Vip_6_Click(object sender, EventArgs e)
        {
            btn_Reward_Vip_6.Enabled = false;
            btn_Reward_Vip_6.BackColor = Color.FromArgb(55, 47, 36);
        }

        private void btn_Reward_Vip_7_Click(object sender, EventArgs e)
        {
            btn_Reward_Vip_7.Enabled = false;
            btn_Reward_Vip_7.BackColor = Color.FromArgb(55, 47, 36);
        }

        private void btn_Reward_Vip_8_Click(object sender, EventArgs e)
        {
            btn_Reward_Vip_8.Enabled = false;
            btn_Reward_Vip_8.BackColor = Color.FromArgb(55, 47, 36);
        }

        private void btn_Reward_Vip_9_Click(object sender, EventArgs e)
        {
            btn_Reward_Vip_9.Enabled = false;
            btn_Reward_Vip_9.BackColor = Color.FromArgb(55, 47, 36);
        }

        private void btn_Reward_Vip_10_Click(object sender, EventArgs e)
        {
            btn_Reward_Vip_10.Enabled = false;
            btn_Reward_Vip_10.BackColor = Color.FromArgb(55, 47, 36);
        }

        private void btn_Reward_Vip_11_Click(object sender, EventArgs e)
        {
            btn_Reward_Vip_11.Enabled = false;
            btn_Reward_Vip_11.BackColor = Color.FromArgb(55, 47, 36);
        }

        private void btn_Reward_Vip_12_Click(object sender, EventArgs e)
        {
            btn_Reward_Vip_12.Enabled = false;
            btn_Reward_Vip_12.BackColor = Color.FromArgb(55, 47, 36);
        }

        private void btn_Reward_Vip_13_Click(object sender, EventArgs e)
        {
            btn_Reward_Vip_13.Enabled = false;
            btn_Reward_Vip_13.BackColor = Color.FromArgb(55, 47, 36);
        }

        private void btn_Reward_Vip_14_Click(object sender, EventArgs e)
        {
            btn_Reward_Vip_14.Enabled = false;
            btn_Reward_Vip_14.BackColor = Color.FromArgb(55, 47, 36);
        }

        private void btn_Reward_Vip_15_Click(object sender, EventArgs e)
        {
            btn_Reward_Vip_15.Enabled = false;
            btn_Reward_Vip_15.BackColor = Color.FromArgb(55, 47, 36);
        }

        private void btn_Reward_Vip_16_Click(object sender, EventArgs e)
        {
            btn_Reward_Vip_16.Enabled = false;
            btn_Reward_Vip_16.BackColor = Color.FromArgb(55, 47, 36);
        }

        private void btn_Reward_Vip_17_Click(object sender, EventArgs e)
        {
            btn_Reward_Vip_17.Enabled = false;
            btn_Reward_Vip_17.BackColor = Color.FromArgb(55, 47, 36);
        }

        private void btn_Reward_Vip_18_Click(object sender, EventArgs e)
        {
            btn_Reward_Vip_18.Enabled = false;
            btn_Reward_Vip_18.BackColor = Color.FromArgb(55, 47, 36);
        }

        private void btn_Reward_Vip_19_Click(object sender, EventArgs e)
        {
            btn_Reward_Vip_19.Enabled = false;
            btn_Reward_Vip_19.BackColor = Color.FromArgb(55, 47, 36);
        }

        private void btn_Reward_Vip_20_Click(object sender, EventArgs e)
        {
            btn_Reward_Vip_20.Enabled = false;
            btn_Reward_Vip_20.BackColor = Color.FromArgb(55, 47, 36);
        }
        #endregion
    }
}
