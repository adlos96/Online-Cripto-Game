namespace Warrior_and_Wealth
{
    partial class Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            txt_Email = new TextBox();
            checkBox2 = new CheckBox();
            lbl_Titolo = new Warrior_and_Wealth.Strumenti.TransparentLabel();
            checkBox1 = new CheckBox();
            comboBox_Lingua = new ComboBox();
            panel_Connessione = new Panel();
            txt_Versione_Attuale = new TextBox();
            txt_Stato_Server = new TextBox();
            txt_Log = new TextBox();
            txt_Ip = new TextBox();
            label1 = new Label();
            Btn_Login = new Button();
            Btn_New_Game = new Button();
            txt_Password_Login = new TextBox();
            txt_Username_Login = new TextBox();
            lbl_Aggiornamento_Disponibile = new Label();
            banner_2 = new Panel();
            banner_1 = new Panel();
            btn_Aggiorna = new Button();
            lbl_Password_Reset = new Warrior_and_Wealth.Strumenti.TransparentLabel();
            label2 = new Warrior_and_Wealth.Strumenti.TransparentLabel();
            lbl_Username_Login = new Warrior_and_Wealth.Strumenti.TransparentLabel();
            lbl_Password_Login = new Warrior_and_Wealth.Strumenti.TransparentLabel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(lbl_Password_Login);
            panel1.Controls.Add(lbl_Username_Login);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(lbl_Password_Reset);
            panel1.Controls.Add(txt_Email);
            panel1.Controls.Add(checkBox2);
            panel1.Controls.Add(lbl_Titolo);
            panel1.Controls.Add(checkBox1);
            panel1.Controls.Add(comboBox_Lingua);
            panel1.Controls.Add(panel_Connessione);
            panel1.Controls.Add(txt_Versione_Attuale);
            panel1.Controls.Add(txt_Stato_Server);
            panel1.Controls.Add(txt_Log);
            panel1.Controls.Add(txt_Ip);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(Btn_Login);
            panel1.Controls.Add(Btn_New_Game);
            panel1.Controls.Add(txt_Password_Login);
            panel1.Controls.Add(txt_Username_Login);
            panel1.Location = new Point(21, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(268, 383);
            panel1.TabIndex = 1;
            // 
            // txt_Email
            // 
            txt_Email.Location = new Point(55, 102);
            txt_Email.Name = "txt_Email";
            txt_Email.Size = new Size(152, 23);
            txt_Email.TabIndex = 22;
            txt_Email.TextAlign = HorizontalAlignment.Center;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Checked = true;
            checkBox2.CheckState = CheckState.Checked;
            checkBox2.Location = new Point(210, 248);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(46, 19);
            checkBox2.TabIndex = 21;
            checkBox2.Text = "Edit";
            checkBox2.UseVisualStyleBackColor = true;
            checkBox2.CheckedChanged += checkBox2_CheckedChanged;
            // 
            // lbl_Titolo
            // 
            lbl_Titolo.BackColor = Color.Transparent;
            lbl_Titolo.Font = new Font("Segoe UI", 8.5F);
            lbl_Titolo.Location = new Point(3, -4);
            lbl_Titolo.Multiline = false;
            lbl_Titolo.Name = "lbl_Titolo";
            lbl_Titolo.Size = new Size(262, 28);
            lbl_Titolo.TabIndex = 20;
            lbl_Titolo.Text = "Warrior & Wealth";
            lbl_Titolo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(210, 192);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(51, 19);
            checkBox1.TabIndex = 19;
            checkBox1.Text = "Hide";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // comboBox_Lingua
            // 
            comboBox_Lingua.FormattingEnabled = true;
            comboBox_Lingua.Location = new Point(150, 34);
            comboBox_Lingua.Name = "comboBox_Lingua";
            comboBox_Lingua.Size = new Size(62, 23);
            comboBox_Lingua.TabIndex = 19;
            comboBox_Lingua.TextChanged += comboBox_Lingua_TextChanged;
            // 
            // panel_Connessione
            // 
            panel_Connessione.BackColor = Color.Transparent;
            panel_Connessione.BackgroundImage = Properties.Resources.Disconnesso_2_V2;
            panel_Connessione.BackgroundImageLayout = ImageLayout.Stretch;
            panel_Connessione.Location = new Point(126, 36);
            panel_Connessione.Name = "panel_Connessione";
            panel_Connessione.Size = new Size(18, 18);
            panel_Connessione.TabIndex = 18;
            // 
            // txt_Versione_Attuale
            // 
            txt_Versione_Attuale.BackColor = Color.White;
            txt_Versione_Attuale.BorderStyle = BorderStyle.None;
            txt_Versione_Attuale.Location = new Point(42, 63);
            txt_Versione_Attuale.Name = "txt_Versione_Attuale";
            txt_Versione_Attuale.ReadOnly = true;
            txt_Versione_Attuale.Size = new Size(170, 16);
            txt_Versione_Attuale.TabIndex = 11;
            txt_Versione_Attuale.Text = "Versione Attuale: 0.1.11.0";
            // 
            // txt_Stato_Server
            // 
            txt_Stato_Server.BackColor = Color.White;
            txt_Stato_Server.BorderStyle = BorderStyle.None;
            txt_Stato_Server.Location = new Point(42, 38);
            txt_Stato_Server.Name = "txt_Stato_Server";
            txt_Stato_Server.ReadOnly = true;
            txt_Stato_Server.Size = new Size(102, 16);
            txt_Stato_Server.TabIndex = 12;
            txt_Stato_Server.Text = "Stato Server:";
            txt_Stato_Server.TextAlign = HorizontalAlignment.Center;
            // 
            // txt_Log
            // 
            txt_Log.BorderStyle = BorderStyle.FixedSingle;
            txt_Log.Location = new Point(3, 275);
            txt_Log.Multiline = true;
            txt_Log.Name = "txt_Log";
            txt_Log.ReadOnly = true;
            txt_Log.Size = new Size(262, 60);
            txt_Log.TabIndex = 9;
            // 
            // txt_Ip
            // 
            txt_Ip.Location = new Point(56, 246);
            txt_Ip.Name = "txt_Ip";
            txt_Ip.ReadOnly = true;
            txt_Ip.Size = new Size(152, 23);
            txt_Ip.TabIndex = 7;
            txt_Ip.Text = "AUTO";
            txt_Ip.TextAlign = HorizontalAlignment.Center;
            txt_Ip.MouseClick += txt_Ip_MouseClick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold);
            label1.Location = new Point(33, 252);
            label1.Name = "label1";
            label1.Size = new Size(17, 13);
            label1.TabIndex = 8;
            label1.Text = "IP";
            // 
            // Btn_Login
            // 
            Btn_Login.FlatAppearance.BorderSize = 0;
            Btn_Login.FlatStyle = FlatStyle.Flat;
            Btn_Login.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btn_Login.Location = new Point(135, 341);
            Btn_Login.Name = "Btn_Login";
            Btn_Login.Size = new Size(92, 32);
            Btn_Login.TabIndex = 5;
            Btn_Login.Text = "Login";
            Btn_Login.UseVisualStyleBackColor = true;
            Btn_Login.Click += Btn_Login_Click;
            // 
            // Btn_New_Game
            // 
            Btn_New_Game.FlatAppearance.BorderSize = 0;
            Btn_New_Game.FlatStyle = FlatStyle.Flat;
            Btn_New_Game.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btn_New_Game.Location = new Point(40, 341);
            Btn_New_Game.Name = "Btn_New_Game";
            Btn_New_Game.Size = new Size(89, 32);
            Btn_New_Game.TabIndex = 4;
            Btn_New_Game.Text = "New Game";
            Btn_New_Game.UseVisualStyleBackColor = true;
            Btn_New_Game.Click += Btn_New_Game_Click;
            // 
            // txt_Password_Login
            // 
            txt_Password_Login.Location = new Point(56, 190);
            txt_Password_Login.Name = "txt_Password_Login";
            txt_Password_Login.Size = new Size(152, 23);
            txt_Password_Login.TabIndex = 2;
            txt_Password_Login.TextAlign = HorizontalAlignment.Center;
            txt_Password_Login.MouseClick += txt_Password_Login_MouseClick;
            txt_Password_Login.TextChanged += txt_Password_Login_TextChanged;
            // 
            // txt_Username_Login
            // 
            txt_Username_Login.Location = new Point(55, 146);
            txt_Username_Login.Name = "txt_Username_Login";
            txt_Username_Login.Size = new Size(152, 23);
            txt_Username_Login.TabIndex = 0;
            txt_Username_Login.TextAlign = HorizontalAlignment.Center;
            txt_Username_Login.MouseClick += txt_Username_Login_MouseClick;
            txt_Username_Login.TextChanged += txt_Username_Login_TextChanged;
            // 
            // lbl_Aggiornamento_Disponibile
            // 
            lbl_Aggiornamento_Disponibile.AutoSize = true;
            lbl_Aggiornamento_Disponibile.BackColor = Color.Transparent;
            lbl_Aggiornamento_Disponibile.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl_Aggiornamento_Disponibile.Location = new Point(25, 401);
            lbl_Aggiornamento_Disponibile.Name = "lbl_Aggiornamento_Disponibile";
            lbl_Aggiornamento_Disponibile.Size = new Size(178, 13);
            lbl_Aggiornamento_Disponibile.TabIndex = 11;
            lbl_Aggiornamento_Disponibile.Text = "Necessario aggiornamento : 0.1.1";
            lbl_Aggiornamento_Disponibile.Visible = false;
            // 
            // banner_2
            // 
            banner_2.BackgroundImage = Properties.Resources.Banner_Blue_removebg_preview;
            banner_2.BackgroundImageLayout = ImageLayout.Stretch;
            banner_2.Location = new Point(265, 2);
            banner_2.Name = "banner_2";
            banner_2.Size = new Size(33, 55);
            banner_2.TabIndex = 17;
            // 
            // banner_1
            // 
            banner_1.BackgroundImage = Properties.Resources.Banner_Red_removebg_preview;
            banner_1.BackgroundImageLayout = ImageLayout.Stretch;
            banner_1.Location = new Point(12, 2);
            banner_1.Name = "banner_1";
            banner_1.Size = new Size(33, 55);
            banner_1.TabIndex = 16;
            // 
            // btn_Aggiorna
            // 
            btn_Aggiorna.FlatAppearance.BorderSize = 0;
            btn_Aggiorna.FlatStyle = FlatStyle.Flat;
            btn_Aggiorna.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_Aggiorna.Location = new Point(61, 422);
            btn_Aggiorna.Name = "btn_Aggiorna";
            btn_Aggiorna.Size = new Size(187, 27);
            btn_Aggiorna.TabIndex = 18;
            btn_Aggiorna.Text = "Update Client";
            btn_Aggiorna.UseVisualStyleBackColor = true;
            btn_Aggiorna.Visible = false;
            btn_Aggiorna.Click += btn_Aggiorna_Click;
            // 
            // lbl_Password_Reset
            // 
            lbl_Password_Reset.BackColor = Color.Transparent;
            lbl_Password_Reset.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_Password_Reset.Location = new Point(3, 217);
            lbl_Password_Reset.Multiline = false;
            lbl_Password_Reset.Name = "lbl_Password_Reset";
            lbl_Password_Reset.Size = new Size(262, 12);
            lbl_Password_Reset.TabIndex = 24;
            lbl_Password_Reset.Text = "Hai perso la password?";
            lbl_Password_Reset.TextAlign = ContentAlignment.MiddleCenter;
            lbl_Password_Reset.MouseClick += lbl_Password_Reset_MouseClick;
            // 
            // label2
            // 
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(3, 85);
            label2.Multiline = false;
            label2.Name = "label2";
            label2.Size = new Size(262, 12);
            label2.TabIndex = 25;
            label2.Text = "Email";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lbl_Username_Login
            // 
            lbl_Username_Login.BackColor = Color.Transparent;
            lbl_Username_Login.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_Username_Login.Location = new Point(3, 128);
            lbl_Username_Login.Multiline = false;
            lbl_Username_Login.Name = "lbl_Username_Login";
            lbl_Username_Login.Size = new Size(262, 12);
            lbl_Username_Login.TabIndex = 26;
            lbl_Username_Login.Text = "Nome Utente";
            lbl_Username_Login.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lbl_Password_Login
            // 
            lbl_Password_Login.BackColor = Color.Transparent;
            lbl_Password_Login.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_Password_Login.Location = new Point(3, 172);
            lbl_Password_Login.Multiline = false;
            lbl_Password_Login.Name = "lbl_Password_Login";
            lbl_Password_Login.Size = new Size(262, 12);
            lbl_Password_Login.TabIndex = 27;
            lbl_Password_Login.Text = "Password";
            lbl_Password_Login.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackgroundImage = Properties.Resources.freepik__upload__73441_AAA;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(309, 458);
            Controls.Add(lbl_Aggiornamento_Disponibile);
            Controls.Add(btn_Aggiorna);
            Controls.Add(panel1);
            Controls.Add(banner_1);
            Controls.Add(banner_2);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "Login";
            Text = "Login";
            FormClosing += Login_FormClosing;
            Load += Gioco_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel panel1;
        private TextBox txt_Username_Login;
        private Button Btn_New_Game;
        private TextBox txt_Password_Login;
        private Button Btn_Login;
        private Panel banner_2;
        private Panel banner_1;
        private TextBox txt_Ip;
        private Label label1;
        private TextBox txt_Log;
        private Label lbl_Aggiornamento_Disponibile;
        private Button btn_Aggiorna;
        private TextBox txt_Versione_Attuale;
        private TextBox txt_Stato_Server;
        private Panel panel_Connessione;
        private ComboBox comboBox_Lingua;
        private CheckBox checkBox1;
        private Strumenti.TransparentLabel lbl_Titolo;
        private CheckBox checkBox2;
        private TextBox txt_Email;
        private Strumenti.TransparentLabel lbl_Password_Reset;
        private Strumenti.TransparentLabel lbl_Password_Login;
        private Strumenti.TransparentLabel lbl_Username_Login;
        private Strumenti.TransparentLabel label2;
    }
}