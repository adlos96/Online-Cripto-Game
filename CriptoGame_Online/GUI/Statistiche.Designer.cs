namespace Warrior_and_Wealth.GUI
{
    partial class Statistiche
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
            groupBox1 = new GroupBox();
            panel2 = new Panel();
            label3 = new Label();
            lbl_Giocatore_Valore = new Label();
            lbl_Giocatore_Testo = new Label();
            groupBox2 = new GroupBox();
            panel1 = new Panel();
            lbl_Statistiche = new Label();
            lbl_Statistiche_Valore = new Label();
            lbl_Statistiche_Titolo = new Warrior_and_Wealth.Strumenti.TransparentLabel();
            lbl_Giocatore_Titolo = new Warrior_and_Wealth.Strumenti.TransparentLabel();
            groupBox1.SuspendLayout();
            panel2.SuspendLayout();
            groupBox2.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            groupBox1.AutoSize = true;
            groupBox1.BackColor = Color.Transparent;
            groupBox1.Controls.Add(lbl_Giocatore_Titolo);
            groupBox1.Controls.Add(panel2);
            groupBox1.Location = new Point(7, 12);
            groupBox1.Margin = new Padding(2, 3, 2, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(2, 3, 2, 3);
            groupBox1.Size = new Size(316, 449);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel2.AutoScroll = true;
            panel2.Controls.Add(label3);
            panel2.Controls.Add(lbl_Giocatore_Valore);
            panel2.Controls.Add(lbl_Giocatore_Testo);
            panel2.Location = new Point(4, 38);
            panel2.Margin = new Padding(2, 3, 2, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(308, 406);
            panel2.TabIndex = 5;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label3.Location = new Point(312, 2);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(65, 17);
            label3.TabIndex = 3;
            label3.Text = "1.000.000";
            // 
            // lbl_Giocatore_Valore
            // 
            lbl_Giocatore_Valore.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbl_Giocatore_Valore.AutoSize = true;
            lbl_Giocatore_Valore.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lbl_Giocatore_Valore.Location = new Point(191, 3);
            lbl_Giocatore_Valore.Margin = new Padding(2, 0, 2, 0);
            lbl_Giocatore_Valore.Name = "lbl_Giocatore_Valore";
            lbl_Giocatore_Valore.Size = new Size(110, 17);
            lbl_Giocatore_Valore.TabIndex = 2;
            lbl_Giocatore_Valore.Text = "00d 00h 00m 00s";
            // 
            // lbl_Giocatore_Testo
            // 
            lbl_Giocatore_Testo.AutoSize = true;
            lbl_Giocatore_Testo.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lbl_Giocatore_Testo.Location = new Point(3, 3);
            lbl_Giocatore_Testo.Margin = new Padding(2, 0, 2, 0);
            lbl_Giocatore_Testo.Name = "lbl_Giocatore_Testo";
            lbl_Giocatore_Testo.Size = new Size(72, 17);
            lbl_Giocatore_Testo.TabIndex = 1;
            lbl_Giocatore_Testo.Text = "Statistiche";
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox2.AutoSize = true;
            groupBox2.BackColor = Color.Transparent;
            groupBox2.Controls.Add(lbl_Statistiche_Titolo);
            groupBox2.Controls.Add(panel1);
            groupBox2.Location = new Point(322, 12);
            groupBox2.Margin = new Padding(2, 3, 2, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(2, 3, 2, 3);
            groupBox2.Size = new Size(317, 449);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.AutoScroll = true;
            panel1.Controls.Add(lbl_Statistiche);
            panel1.Controls.Add(lbl_Statistiche_Valore);
            panel1.Location = new Point(4, 38);
            panel1.Margin = new Padding(2, 3, 2, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(309, 406);
            panel1.TabIndex = 4;
            // 
            // lbl_Statistiche
            // 
            lbl_Statistiche.AutoSize = true;
            lbl_Statistiche.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lbl_Statistiche.Location = new Point(3, 3);
            lbl_Statistiche.Margin = new Padding(2, 0, 2, 0);
            lbl_Statistiche.Name = "lbl_Statistiche";
            lbl_Statistiche.Size = new Size(72, 17);
            lbl_Statistiche.TabIndex = 1;
            lbl_Statistiche.Text = "Statistiche";
            // 
            // lbl_Statistiche_Valore
            // 
            lbl_Statistiche_Valore.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbl_Statistiche_Valore.AutoSize = true;
            lbl_Statistiche_Valore.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lbl_Statistiche_Valore.Location = new Point(211, 3);
            lbl_Statistiche_Valore.Margin = new Padding(2, 0, 2, 0);
            lbl_Statistiche_Valore.Name = "lbl_Statistiche_Valore";
            lbl_Statistiche_Valore.Size = new Size(90, 17);
            lbl_Statistiche_Valore.TabIndex = 3;
            lbl_Statistiche_Valore.Text = "1.000.000.000";
            // 
            // lbl_Statistiche_Titolo
            // 
            lbl_Statistiche_Titolo.BackColor = Color.Transparent;
            lbl_Statistiche_Titolo.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            lbl_Statistiche_Titolo.Location = new Point(4, 10);
            lbl_Statistiche_Titolo.Multiline = false;
            lbl_Statistiche_Titolo.Name = "lbl_Statistiche_Titolo";
            lbl_Statistiche_Titolo.Size = new Size(308, 27);
            lbl_Statistiche_Titolo.TabIndex = 5;
            lbl_Statistiche_Titolo.Text = "Statistiche";
            lbl_Statistiche_Titolo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lbl_Giocatore_Titolo
            // 
            lbl_Giocatore_Titolo.BackColor = Color.Transparent;
            lbl_Giocatore_Titolo.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            lbl_Giocatore_Titolo.Location = new Point(3, 10);
            lbl_Giocatore_Titolo.Multiline = false;
            lbl_Giocatore_Titolo.Name = "lbl_Giocatore_Titolo";
            lbl_Giocatore_Titolo.Size = new Size(308, 27);
            lbl_Giocatore_Titolo.TabIndex = 6;
            lbl_Giocatore_Titolo.Text = "Giocatore";
            lbl_Giocatore_Titolo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Statistiche
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackgroundImage = Properties.Resources._11111111111;
            ClientSize = new Size(649, 480);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Margin = new Padding(2, 3, 2, 3);
            Name = "Statistiche";
            Text = "Giocatore";
            FormClosing += Statistiche_FormClosing;
            Load += Giocatore_Load;
            groupBox1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            groupBox2.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox1;
        private Label lbl_Giocatore_Testo;
        private GroupBox groupBox2;
        private Label lbl_Statistiche;
        private Label lbl_Giocatore_Valore;
        private Label lbl_Statistiche_Valore;
        private Panel panel2;
        private Label label3;
        private Panel panel1;
        private Strumenti.TransparentLabel lbl_Statistiche_Titolo;
        private Strumenti.TransparentLabel lbl_Giocatore_Titolo;
    }
}