using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TRON
{
    public partial class GameOver : Form
    {
        private Game game;

        public GameOver(Game game)
        {
            InitializeComponent();
            this.game = game;
        }

        public void ShowDialog(Player p1, Player p2)
        {
            //lblScoreP1.ForeColor = ColorTranslator.FromHtml("#44c8f5");
            lblScoreP1.ForeColor = p1.Color;
            lblScoreP1.Text = p1.Points.ToString();

            //lblScoreP2.ForeColor = ColorTranslator.FromHtml("#44c8f5");
            lblScoreP2.ForeColor = p2.Color;
            lblScoreP2.Text = p2.Points.ToString();
            ShowDialog();
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            game.StartGame();
            Hide();
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            game.QuitGame();
            Hide();
        }

        private void btnRestart_MouseHover(object sender, EventArgs e)
        {
            restartHover.Visible = true;
            restartHover2.Visible = true;
        }

        private void btnRestart_MouseLeave(object sender, EventArgs e)
        {
            restartHover.Visible = false;
            restartHover2.Visible = false;
        }

        private void btnMenu_MouseHover(object sender, EventArgs e)
        {
            menuHover.Visible = true;
            menuHover2.Visible = true;
        }

        private void btnMenu_MouseLeave(object sender, EventArgs e)
        {
            menuHover.Visible = false;
            menuHover2.Visible = false;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.UserClosing)
            {
                game.QuitGame();
                Hide();
            }
        }
    }
}
