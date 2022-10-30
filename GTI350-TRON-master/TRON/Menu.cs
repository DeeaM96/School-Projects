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
    public partial class Menu : Form
    {
        Colors colors;
        Game game;

        Player p1 = new Player(Color.Blue);
        Player p2 = new Player(Color.Yellow);

        public Menu()
        {
            InitializeComponent();

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            btnNewGame.Parent = menuPicture;
            btnColors.Parent = menuPicture;
            btnExit.Parent = menuPicture;

            newGameHover.Parent = menuPicture;
            colorsHover.Parent = menuPicture;
            exitHover.Parent = menuPicture;
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            game = new Game(this, p1, p2);
            Hide();

            game.Show();
        }

        private void btnColors_Click(object sender, EventArgs e)
        {
            colors = new Colors(this, p1, p2);
            Hide();

            colors.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnNewGame_MouseHover(object sender, EventArgs e)
        {
            newGameHover.Visible = true;
        }

        private void btnNewGame_MouseLeave(object sender, EventArgs e)
        {
            newGameHover.Visible = false;
        }

        private void btnColors_MouseHover(object sender, EventArgs e)
        {
            colorsHover.Visible = true;
        }

        private void btnColors_MouseLeave(object sender, EventArgs e)
        {
            colorsHover.Visible = false;
        }

        private void btnExit_MouseHover(object sender, EventArgs e)
        {
            exitHover.Visible = true;
        }

        private void btnExit_MouseLeave(object sender, EventArgs e)
        {
            exitHover.Visible = false;
        }
    }
}
