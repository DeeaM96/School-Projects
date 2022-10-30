using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TRON
{
    public partial class Colors : Form
    {
        Menu menu;

        Player p1;
        Player p2;

        public Colors(Menu menu, Player p1, Player p2)
        {
            InitializeComponent();

            this.menu = menu;

            this.p1 = p1;
            this.p2 = p2;

            colorP1.BackColor = p1.Color;
            colorP2.BackColor = p2.Color;
        }

        private void colorP1_Click(object sender, EventArgs e)
        {
            colorDialogP1 = new ColorDialog();
            colorDialogP1.AllowFullOpen = false;
            colorDialogP1.ShowHelp = false;
            colorDialogP1.Color = p1.Color;

            do
            {
                if (colorDialogP1.Color == colorP2.BackColor)
                    MessageBox.Show("Player 1 can't have the same color as Player 2", "Oops!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (colorDialogP1.Color == Color.Black)
                    MessageBox.Show("Player 1 color can't be black!", "Oops!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (colorDialogP1.ShowDialog() == DialogResult.OK)
                    colorP1.BackColor = colorDialogP1.Color;
            } while (colorDialogP1.Color == Color.Black || colorDialogP1.Color == colorP2.BackColor);
        }

        private void colorP2_Click(object sender, EventArgs e)
        {
            colorDialogP2 = new ColorDialog();
            colorDialogP2.AllowFullOpen = false;
            colorDialogP2.ShowHelp = false;
            colorDialogP2.Color = p2.Color;

            do
            {
                if (colorDialogP1.Color == colorP2.BackColor)
                    MessageBox.Show("Player 2 can't have the same color as Player 1", "Oops!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (colorDialogP2.Color == Color.Black)
                    MessageBox.Show("Player 2 color can't be black!", "Oops!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (colorDialogP2.ShowDialog() == DialogResult.OK)
                    colorP2.BackColor = colorDialogP2.Color;
            } while (colorDialogP2.Color == Color.Black || colorDialogP2.Color == colorP1.BackColor);
        }

        private void Colors_FormClosing(object sender, FormClosingEventArgs e)
        {
            Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            p1.Color = colorP1.BackColor;
            p2.Color = colorP2.BackColor;

            Close();
        }

        private new void Close()
        {
            menu.Show();
            Hide();
        }

        private void btnConfirm_MouseHover(object sender, EventArgs e)
        {
            confirmHover.Visible = true;
            confirmHover2.Visible = true;
        }

        private void btnConfirm_MouseLeave(object sender, EventArgs e)
        {
            confirmHover.Visible = false;
            confirmHover2.Visible = false;
        }
    }
}
