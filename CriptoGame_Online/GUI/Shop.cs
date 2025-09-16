using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CriptoGame_Online
{
    public partial class Shop : Form
    {
        public Shop()
        {
            InitializeComponent();
        }

        private void Shop_Load(object sender, EventArgs e)
        {
            this.ActiveControl = panel_Diamond_Image_2; // assegna il focus al bottone
            panel1.BackColor = Color.FromArgb(100, 229, 208, 181);
            panel1.BackColor = Color.Transparent;

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
        }

        private void panel_Bottone_2_MouseClick(object sender, MouseEventArgs e)
        {
            txt_Shop_2.Text = "700";
        }
    }
}
