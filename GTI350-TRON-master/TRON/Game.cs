using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace TRON
{
    public partial class Game : Form
    {
        public enum DIRECTION
        {
            UP = -1,
            DOWN = 1,
            LEFT = -1,
            RIGHT = 1,
            NULL = 0
        }

        private Menu menu;
        private GameOver gameover;

        private bool[,] arrayWindow; // arrayWindow.GetLength(0) --> horizontal
                                     // // arrayWindow.GetLength(1) --> vertical
        private int cellSize = 5;
        private static int NUM_CELLS_HORIZONTAL;
        private static int NUM_CELLS_VERTICAL;
        private bool CELLEMPTY = false;
        private bool CELLOCCUPIED = true;

        private Player mouse;
        private int cursor_posX_down;
        private int cursor_posY_down;

        private Player p1;
        private Player p2;

        int speed = 350;
        DateTime begin;
        DateTime end;

        bool start = false;



        public Game(Menu menu, Player p1, Player p2)
        {
            InitializeComponent();

            NUM_CELLS_HORIZONTAL = Width / cellSize;
            NUM_CELLS_VERTICAL = Height / cellSize;

            this.menu = menu;
            gameover = new GameOver(this);

            this.p1 = p1;
            this.p2 = p2;
            this.p1.Points = 0;
            this.p2.Points = 0;

            InitializePanelInstructions();
        }

        internal void StartGame()
        {
            start = true;
            speed = 350;
            CreateGraphics().Clear(Color.Black);
            arrayWindow = CreateWindowArray(NUM_CELLS_HORIZONTAL, NUM_CELLS_VERTICAL);

            initializePlayer(p1, DIRECTION.NULL, DIRECTION.UP, NUM_CELLS_HORIZONTAL / 2 - 5, NUM_CELLS_VERTICAL - cellSize - 3);
            initializePlayer(p2, DIRECTION.NULL, DIRECTION.DOWN, NUM_CELLS_HORIZONTAL / 2 + 5, -1);

            timerMove.Start();
        }

        private void initializePlayer(Player player, DIRECTION x_direction, DIRECTION y_direction, int x, int y)
        {
            player.Collision = false;
            player.Alive = true;

            initializePlayerPosition(player, x_direction, y_direction, x, y);
        }

        private void initializePlayerPosition(Player player, DIRECTION x_direction, DIRECTION y_direction, int x, int y)
        {
            player.X_Direction = x_direction;
            player.Y_Direction = y_direction;

            player.X0 = (Width - NUM_CELLS_HORIZONTAL * cellSize) / 2;
            player.Y0 = (Height - NUM_CELLS_VERTICAL * cellSize) / 2;

            player.X = x;
            player.Y = y;
        }

        private bool[,] CreateWindowArray(int numColumns, int numRows)
        {
            arrayWindow = new bool[numColumns, numRows];

            for (var c = 0; c < numColumns; c++)
            {

                for (var r = 0; r < numRows; r++)
                {
                    arrayWindow[c, r] = CELLEMPTY;
                }
            }
            return arrayWindow;
        }

        private void timerMove_Tick(object sender, EventArgs e)
        {
            end = DateTime.Now;
            if (begin == null)
                begin = DateTime.Now;

            TimeSpan span = end - begin;
            if (span.Seconds > 3)
            {
                if (speed > 50)
                    speed -= 50;
                begin = DateTime.Now;
            }

            timerMove.Interval = speed / 2;

            bool game = p1.Alive && p2.Alive;
            if (game)
            {
                MovePlayer(p1);
                MovePlayer(p2);

                Draw();
            }
            else
                RestartGame();
        }

        private void RestartGame()
        {
            if (start)
            {
                CreateGraphics().Clear(Color.Black);
                timerMove.Stop();

                gameover.ShowDialog(p1, p2);
            }
        }

        private void MovePlayer(Player player)
        {
            if (player.Alive)
            {
                int new1x = player.X + (int)player.X_Direction;
                int new1y = player.Y + (int)player.Y_Direction;

                // Check for collision with grid boundaries and with trail

                if (p1.X + (int)p1.X_Direction == p2.X + (int)p2.X_Direction &&
                    p1.Y + (int)p1.Y_Direction == p2.Y + (int)p2.Y_Direction)
                {
                    p1.Collision = false;
                    p2.Collision = false;
                    p1.Alive = false;
                    p2.Alive = false;

                }
                else if (
                 new1x < 0 || new1x >= NUM_CELLS_HORIZONTAL
                 || new1y < 0 || new1y >= (NUM_CELLS_VERTICAL - cellSize - 3)
                 || arrayWindow[new1x, new1y] == CELLOCCUPIED)
                {
                    player.Alive = false;

                    player.Collision = true;

                    TestMovePlayer(p1.Color == player.Color ? p2 : p1);

                    if (p1.Collision && !p2.Collision)
                        p2.Points++;
                    else if (p2.Collision && !p1.Collision)
                        p1.Points++;
                }
                else
                {
                    arrayWindow[new1x, new1y] = CELLOCCUPIED;
                    player.X = new1x;
                    player.Y = new1y;
                }
            }
        }

        private void TestMovePlayer(Player player)
        {
            int new1x = player.X + (int)player.X_Direction;
            int new1y = player.Y + (int)player.Y_Direction;

            if (new1x < 0 || new1x >= NUM_CELLS_HORIZONTAL
             || new1y < 0 || new1y >= (NUM_CELLS_VERTICAL - cellSize - 3)
             || arrayWindow[new1x, new1y] == CELLOCCUPIED)
            {
                player.Alive = false;
                player.Collision = true;
            }
        }

        public void Draw()
        {
            DrawPlayer(p1);
            DrawPlayer(p2);
        }

        private void DrawPlayer(Player player)
        {
            Graphics graphicsObj;

            graphicsObj = CreateGraphics();

            if (player.Alive)
            {
                Pen myPen = new Pen(player.Color, cellSize);

                if (arrayWindow[player.X, player.Y] == CELLOCCUPIED)
                {
                    Point point = new Point(player.X0 + player.X * cellSize + 1, player.Y0 + player.Y * cellSize + 5);
                    graphicsObj.DrawRectangle(myPen, new Rectangle(point.X, point.Y, cellSize - 2, cellSize - 2));
                }

                graphicsObj.Dispose();
            }
        }

        private void Game_MouseDown(object sender, MouseEventArgs e)
        {
            cursor_posX_down = Cursor.Position.X;
            cursor_posY_down = Cursor.Position.Y;
        }

        private void Game_MouseUp(object sender, MouseEventArgs e)
        {
            int delta_x = Cursor.Position.X - cursor_posX_down;
            int delta_y = Cursor.Position.Y - cursor_posY_down;

            if (Math.Abs(delta_x) > Math.Abs(delta_y))
            {
                if (delta_x > 0) mouse.UpdateDirection(DIRECTION.RIGHT, DIRECTION.NULL);
                else mouse.UpdateDirection(DIRECTION.LEFT, DIRECTION.NULL);
            }
            else
            {
                if (delta_y > 0) mouse.UpdateDirection(DIRECTION.NULL, DIRECTION.DOWN);
                else mouse.UpdateDirection(DIRECTION.NULL, DIRECTION.UP);
            }
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            if (!panelInstructions.Visible)
            {
                //P1
                if (e.KeyValue == 38) p1.UpdateDirection(DIRECTION.NULL, DIRECTION.UP);
                else if (e.KeyValue == 40) p1.UpdateDirection(DIRECTION.NULL, DIRECTION.DOWN);
                else if (e.KeyValue == 37) p1.UpdateDirection(DIRECTION.LEFT, DIRECTION.NULL);
                else if (e.KeyValue == 39) p1.UpdateDirection(DIRECTION.RIGHT, DIRECTION.NULL);

                //P2
                else if (e.KeyValue == 87) p2.UpdateDirection(DIRECTION.NULL, DIRECTION.UP);
                else if (e.KeyValue == 83) p2.UpdateDirection(DIRECTION.NULL, DIRECTION.DOWN);
                else if (e.KeyValue == 65) p2.UpdateDirection(DIRECTION.LEFT, DIRECTION.NULL);
                else if (e.KeyValue == 68) p2.UpdateDirection(DIRECTION.RIGHT, DIRECTION.NULL);

                //Game Commands
                else if (e.KeyValue == 80) timerMove.Stop();    // P - Pause
                else if (e.KeyValue == 82) StartGame();         // R - Restart Game
                else if (e.KeyValue == 71) timerMove.Start();   // G - Go
                else if (e.KeyValue == 81) QuitGame();          // Q - Quit
            }
        }

        private void Game_FormClosing(object sender, FormClosingEventArgs e)
        {
            QuitGame();
        }

        internal void QuitGame()
        {
            timerMove.Stop();
            menu.Show();
            Hide();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.UserClosing)
            {
                QuitGame();
            }
        }

        #region Instructions
        private void InitializePanelInstructions()
        {
            initializePlayerPosition(p1, DIRECTION.NULL, DIRECTION.NULL, NUM_CELLS_HORIZONTAL / 2 - 5, NUM_CELLS_VERTICAL - cellSize - 4);
            initializePlayerPosition(p2, DIRECTION.NULL, DIRECTION.NULL, NUM_CELLS_HORIZONTAL / 2 + 5, 0);

            this.mouse = this.p1;

            lblInstructions.Text = "welcome to TRON\n\np  -  pause\ng  -  go\nr  -  restart\nq  -  quit\n\npress anywhere to start...";

            lblPlayer1.Parent = panelInstructions;
            keysP1.Parent = panelInstructions;

            keyUp1.Parent = keysP1;
            keyUp1.Location = new Point(57, 23);

            keyDown1.Parent = keysP1;
            keyDown1.Location = new Point(56, 57);

            keyLeft1.Parent = keysP1;
            keyLeft1.Location = new Point(20, 58);

            keyRight1.Parent = keysP1;
            keyRight1.Location = new Point(95, 58);

            lblPlayer2.Parent = panelInstructions;
            keysP2.Parent = panelInstructions;

            keyUp2.Parent = keysP2;
            keyUp2.Location = new Point(52, 12);

            keyDown2.Parent = keysP2;
            keyDown2.Location = new Point(52, 48);

            keyLeft2.Parent = keysP2;
            keyLeft2.Location = new Point(15, 48);

            keyRight2.Parent = keysP2;
            keyRight2.Location = new Point(89, 48);

            lblInstructions.Parent = panelInstructions;

            lblPlayer1.ForeColor = p1.Color;
            keyUp1.ForeColor = p1.Color;
            keyDown1.ForeColor = p1.Color;
            keyLeft1.ForeColor = p1.Color;
            keyRight1.ForeColor = p1.Color;

            lblPlayer2.ForeColor = p2.Color;
            keyUp2.ForeColor = p2.Color;
            keyDown2.ForeColor = p2.Color;
            keyLeft2.ForeColor = p2.Color;
            keyRight2.ForeColor = p2.Color;

            lblInstructions.ForeColor = ColorTranslator.FromHtml("#44c8f5");
            lblAnd1.ForeColor = ColorTranslator.FromHtml("#44c8f5");
            lblAnd2.ForeColor = Color.White;

            ///Allows to write in a "Not a TrueType font"
            System.Drawing.Text.PrivateFontCollection privateFonts = new System.Drawing.Text.PrivateFontCollection();
            privateFonts.AddFontFile(Environment.CurrentDirectory + @"\baby blocks.ttf");
            Font font = new Font(privateFonts.Families[0], 20);
            lblPlayer1.Font = font;
            lblPlayer2.Font = font;
            keyUp2.Font = font;
            keyDown2.Font = font;
            keyLeft2.Font = font;
            keyRight2.Font = font;
            lblInstructions.Font = font;
            lblAnd1.Font = font;
            lblAnd2.Font = font;
        }

        private void panelInstructions_Paint(object sender, PaintEventArgs e)
        {
            Pen myPen1 = new Pen(p1.Color, cellSize);
            Point point1 = new Point(p1.X0 + p1.X * cellSize + 1, p1.Y0 + p1.Y * cellSize + 1);

            Pen myPen2 = new Pen(p2.Color, cellSize);
            Point point2 = new Point(p2.X0 + p2.X * cellSize + 1, p2.Y0 + p2.Y * cellSize + 1);

            e.Graphics.DrawRectangle(myPen1, new Rectangle(point1.X, point1.Y, cellSize - 2, cellSize - 2));
            e.Graphics.DrawRectangle(myPen2, new Rectangle(point2.X, point2.Y, cellSize - 2, cellSize - 2));
        }

        private void btnSelectMouse1_Click(object sender, EventArgs e)
        {
            mouse = p1;
            lblAnd1.ForeColor = ColorTranslator.FromHtml("#44c8f5");
            lblAnd2.ForeColor = Color.White;
            btnSelectMouse1.Image = Properties.Resources.mouse_selected;
            btnSelectMouse2.Image = Properties.Resources.mouse;
        }

        private void btnSelectMouse2_Click(object sender, EventArgs e)
        {
            mouse = p2;
            lblAnd1.ForeColor = Color.White;
            lblAnd2.ForeColor = ColorTranslator.FromHtml("#44c8f5");
            btnSelectMouse2.Image = Properties.Resources.mouse_selected;
            btnSelectMouse1.Image = Properties.Resources.mouse;
        }

        private void lblInstructions_Click(object sender, EventArgs e)
        {
            SkipInstructions();
        }

        private void panelInstructions_MouseClick(object sender, MouseEventArgs e)
        {
            SkipInstructions();
        }

        private void SkipInstructions()
        {
            panelInstructions.Visible = false;
            StartGame();
        }
        #endregion
    }
}
