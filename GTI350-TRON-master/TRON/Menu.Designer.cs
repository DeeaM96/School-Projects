namespace TRON
{
    partial class Menu
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
            this.menuPicture = new System.Windows.Forms.PictureBox();
            this.btnNewGame = new System.Windows.Forms.PictureBox();
            this.btnColors = new System.Windows.Forms.PictureBox();
            this.btnExit = new System.Windows.Forms.PictureBox();
            this.newGameHover = new System.Windows.Forms.PictureBox();
            this.colorsHover = new System.Windows.Forms.PictureBox();
            this.exitHover = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.menuPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnNewGame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnColors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.newGameHover)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorsHover)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitHover)).BeginInit();
            this.SuspendLayout();
            // 
            // menuPicture
            // 
            this.menuPicture.Image = ((System.Drawing.Image)(resources.GetObject("menuPicture.Image")));
            this.menuPicture.Location = new System.Drawing.Point(0, 0);
            this.menuPicture.Name = "menuPicture";
            this.menuPicture.Size = new System.Drawing.Size(605, 405);
            this.menuPicture.TabIndex = 1;
            this.menuPicture.TabStop = false;
            // 
            // btnNewGame
            // 
            this.btnNewGame.BackColor = System.Drawing.Color.Transparent;
            this.btnNewGame.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnNewGame.Image = ((System.Drawing.Image)(resources.GetObject("btnNewGame.Image")));
            this.btnNewGame.Location = new System.Drawing.Point(337, 214);
            this.btnNewGame.Name = "btnNewGame";
            this.btnNewGame.Size = new System.Drawing.Size(257, 39);
            this.btnNewGame.TabIndex = 2;
            this.btnNewGame.TabStop = false;
            this.btnNewGame.Click += new System.EventHandler(this.btnNewGame_Click);
            this.btnNewGame.MouseLeave += new System.EventHandler(this.btnNewGame_MouseLeave);
            this.btnNewGame.MouseHover += new System.EventHandler(this.btnNewGame_MouseHover);
            // 
            // btnColors
            // 
            this.btnColors.BackColor = System.Drawing.Color.Transparent;
            this.btnColors.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnColors.Image = ((System.Drawing.Image)(resources.GetObject("btnColors.Image")));
            this.btnColors.Location = new System.Drawing.Point(337, 271);
            this.btnColors.Name = "btnColors";
            this.btnColors.Size = new System.Drawing.Size(257, 39);
            this.btnColors.TabIndex = 3;
            this.btnColors.TabStop = false;
            this.btnColors.Click += new System.EventHandler(this.btnColors_Click);
            this.btnColors.MouseLeave += new System.EventHandler(this.btnColors_MouseLeave);
            this.btnColors.MouseHover += new System.EventHandler(this.btnColors_MouseHover);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(337, 328);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(257, 39);
            this.btnExit.TabIndex = 4;
            this.btnExit.TabStop = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            this.btnExit.MouseLeave += new System.EventHandler(this.btnExit_MouseLeave);
            this.btnExit.MouseHover += new System.EventHandler(this.btnExit_MouseHover);
            // 
            // newGameHover
            // 
            this.newGameHover.BackColor = System.Drawing.Color.Transparent;
            this.newGameHover.Image = ((System.Drawing.Image)(resources.GetObject("newGameHover.Image")));
            this.newGameHover.Location = new System.Drawing.Point(304, 202);
            this.newGameHover.Name = "newGameHover";
            this.newGameHover.Size = new System.Drawing.Size(29, 55);
            this.newGameHover.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.newGameHover.TabIndex = 5;
            this.newGameHover.TabStop = false;
            this.newGameHover.Visible = false;
            // 
            // colorsHover
            // 
            this.colorsHover.BackColor = System.Drawing.Color.Transparent;
            this.colorsHover.Image = ((System.Drawing.Image)(resources.GetObject("colorsHover.Image")));
            this.colorsHover.Location = new System.Drawing.Point(304, 260);
            this.colorsHover.Name = "colorsHover";
            this.colorsHover.Size = new System.Drawing.Size(29, 55);
            this.colorsHover.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.colorsHover.TabIndex = 6;
            this.colorsHover.TabStop = false;
            this.colorsHover.Visible = false;
            // 
            // exitHover
            // 
            this.exitHover.BackColor = System.Drawing.Color.Transparent;
            this.exitHover.Image = ((System.Drawing.Image)(resources.GetObject("exitHover.Image")));
            this.exitHover.Location = new System.Drawing.Point(304, 318);
            this.exitHover.Name = "exitHover";
            this.exitHover.Size = new System.Drawing.Size(29, 55);
            this.exitHover.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.exitHover.TabIndex = 7;
            this.exitHover.TabStop = false;
            this.exitHover.Visible = false;
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(606, 405);
            this.Controls.Add(this.exitHover);
            this.Controls.Add(this.colorsHover);
            this.Controls.Add(this.newGameHover);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnColors);
            this.Controls.Add(this.btnNewGame);
            this.Controls.Add(this.menuPicture);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "Menu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TRON";
            ((System.ComponentModel.ISupportInitialize)(this.menuPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnNewGame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnColors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.newGameHover)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorsHover)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitHover)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox menuPicture;
        private System.Windows.Forms.PictureBox btnNewGame;
        private System.Windows.Forms.PictureBox btnColors;
        private System.Windows.Forms.PictureBox btnExit;
        private System.Windows.Forms.PictureBox newGameHover;
        private System.Windows.Forms.PictureBox colorsHover;
        private System.Windows.Forms.PictureBox exitHover;
    }
}

