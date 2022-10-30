using System;

namespace TRON
{
    partial class Game
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Game));
            this.timerMove = new System.Windows.Forms.Timer(this.components);
            this.panelInstructions = new System.Windows.Forms.Panel();
            this.keyDown2 = new System.Windows.Forms.Label();
            this.keyLeft2 = new System.Windows.Forms.Label();
            this.keyRight2 = new System.Windows.Forms.Label();
            this.keyUp2 = new System.Windows.Forms.Label();
            this.keyDown1 = new System.Windows.Forms.Label();
            this.keyLeft1 = new System.Windows.Forms.Label();
            this.keyRight1 = new System.Windows.Forms.Label();
            this.keyUp1 = new System.Windows.Forms.Label();
            this.keysP1 = new System.Windows.Forms.PictureBox();
            this.keysP2 = new System.Windows.Forms.PictureBox();
            this.lblInstructions = new System.Windows.Forms.Label();
            this.lblPlayer2 = new System.Windows.Forms.Label();
            this.lblPlayer1 = new System.Windows.Forms.Label();
            this.btnSelectMouse1 = new System.Windows.Forms.PictureBox();
            this.btnSelectMouse2 = new System.Windows.Forms.PictureBox();
            this.lblAnd1 = new System.Windows.Forms.Label();
            this.lblAnd2 = new System.Windows.Forms.Label();
            this.panelInstructions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.keysP1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.keysP2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSelectMouse1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSelectMouse2)).BeginInit();
            this.SuspendLayout();
            // 
            // timerMove
            // 
            this.timerMove.Tick += new System.EventHandler(this.timerMove_Tick);
            // 
            // panelInstructions
            // 
            this.panelInstructions.Controls.Add(this.lblAnd2);
            this.panelInstructions.Controls.Add(this.lblAnd1);
            this.panelInstructions.Controls.Add(this.btnSelectMouse2);
            this.panelInstructions.Controls.Add(this.btnSelectMouse1);
            this.panelInstructions.Controls.Add(this.keyDown2);
            this.panelInstructions.Controls.Add(this.keyLeft2);
            this.panelInstructions.Controls.Add(this.keyRight2);
            this.panelInstructions.Controls.Add(this.keyUp2);
            this.panelInstructions.Controls.Add(this.keyDown1);
            this.panelInstructions.Controls.Add(this.keyLeft1);
            this.panelInstructions.Controls.Add(this.keyRight1);
            this.panelInstructions.Controls.Add(this.keyUp1);
            this.panelInstructions.Controls.Add(this.keysP1);
            this.panelInstructions.Controls.Add(this.keysP2);
            this.panelInstructions.Controls.Add(this.lblInstructions);
            this.panelInstructions.Controls.Add(this.lblPlayer2);
            this.panelInstructions.Controls.Add(this.lblPlayer1);
            this.panelInstructions.Location = new System.Drawing.Point(0, 0);
            this.panelInstructions.Name = "panelInstructions";
            this.panelInstructions.Size = new System.Drawing.Size(1039, 651);
            this.panelInstructions.TabIndex = 0;
            this.panelInstructions.Paint += new System.Windows.Forms.PaintEventHandler(this.panelInstructions_Paint);
            this.panelInstructions.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panelInstructions_MouseClick);
            // 
            // keyDown2
            // 
            this.keyDown2.AutoSize = true;
            this.keyDown2.BackColor = System.Drawing.Color.Transparent;
            this.keyDown2.Font = new System.Drawing.Font("Nineteen Eighty Seven", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.keyDown2.ForeColor = System.Drawing.Color.White;
            this.keyDown2.Location = new System.Drawing.Point(683, 95);
            this.keyDown2.Name = "keyDown2";
            this.keyDown2.Size = new System.Drawing.Size(16, 12);
            this.keyDown2.TabIndex = 12;
            this.keyDown2.Text = "s";
            // 
            // keyLeft2
            // 
            this.keyLeft2.AutoSize = true;
            this.keyLeft2.BackColor = System.Drawing.Color.Transparent;
            this.keyLeft2.Font = new System.Drawing.Font("Nineteen Eighty Seven", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.keyLeft2.ForeColor = System.Drawing.Color.White;
            this.keyLeft2.Location = new System.Drawing.Point(646, 95);
            this.keyLeft2.Name = "keyLeft2";
            this.keyLeft2.Size = new System.Drawing.Size(16, 12);
            this.keyLeft2.TabIndex = 11;
            this.keyLeft2.Text = "a";
            // 
            // keyRight2
            // 
            this.keyRight2.AutoSize = true;
            this.keyRight2.BackColor = System.Drawing.Color.Transparent;
            this.keyRight2.Font = new System.Drawing.Font("Nineteen Eighty Seven", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.keyRight2.ForeColor = System.Drawing.Color.White;
            this.keyRight2.Location = new System.Drawing.Point(722, 95);
            this.keyRight2.Name = "keyRight2";
            this.keyRight2.Size = new System.Drawing.Size(16, 12);
            this.keyRight2.TabIndex = 10;
            this.keyRight2.Text = "d";
            // 
            // keyUp2
            // 
            this.keyUp2.AutoSize = true;
            this.keyUp2.BackColor = System.Drawing.Color.Transparent;
            this.keyUp2.Font = new System.Drawing.Font("Nineteen Eighty Seven", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.keyUp2.ForeColor = System.Drawing.Color.White;
            this.keyUp2.Location = new System.Drawing.Point(684, 60);
            this.keyUp2.Name = "keyUp2";
            this.keyUp2.Size = new System.Drawing.Size(18, 12);
            this.keyUp2.TabIndex = 9;
            this.keyUp2.Text = "w";
            // 
            // keyDown1
            // 
            this.keyDown1.AutoSize = true;
            this.keyDown1.BackColor = System.Drawing.Color.Transparent;
            this.keyDown1.Font = new System.Drawing.Font("Nineteen Eighty Seven", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.keyDown1.ForeColor = System.Drawing.Color.White;
            this.keyDown1.Location = new System.Drawing.Point(394, 581);
            this.keyDown1.Name = "keyDown1";
            this.keyDown1.Size = new System.Drawing.Size(16, 12);
            this.keyDown1.TabIndex = 8;
            this.keyDown1.Text = "v";
            // 
            // keyLeft1
            // 
            this.keyLeft1.AutoSize = true;
            this.keyLeft1.BackColor = System.Drawing.Color.Transparent;
            this.keyLeft1.Font = new System.Drawing.Font("Nineteen Eighty Seven", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.keyLeft1.ForeColor = System.Drawing.Color.White;
            this.keyLeft1.Location = new System.Drawing.Point(358, 581);
            this.keyLeft1.Name = "keyLeft1";
            this.keyLeft1.Size = new System.Drawing.Size(13, 12);
            this.keyLeft1.TabIndex = 7;
            this.keyLeft1.Text = "<";
            // 
            // keyRight1
            // 
            this.keyRight1.AutoSize = true;
            this.keyRight1.BackColor = System.Drawing.Color.Transparent;
            this.keyRight1.Font = new System.Drawing.Font("Nineteen Eighty Seven", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.keyRight1.ForeColor = System.Drawing.Color.White;
            this.keyRight1.Location = new System.Drawing.Point(433, 581);
            this.keyRight1.Name = "keyRight1";
            this.keyRight1.Size = new System.Drawing.Size(13, 12);
            this.keyRight1.TabIndex = 6;
            this.keyRight1.Text = ">";
            // 
            // keyUp1
            // 
            this.keyUp1.AutoSize = true;
            this.keyUp1.BackColor = System.Drawing.Color.Transparent;
            this.keyUp1.Font = new System.Drawing.Font("Nineteen Eighty Seven", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.keyUp1.ForeColor = System.Drawing.Color.White;
            this.keyUp1.Location = new System.Drawing.Point(395, 546);
            this.keyUp1.Name = "keyUp1";
            this.keyUp1.Size = new System.Drawing.Size(14, 12);
            this.keyUp1.TabIndex = 5;
            this.keyUp1.Text = "^";
            // 
            // keysP1
            // 
            this.keysP1.BackColor = System.Drawing.Color.Transparent;
            this.keysP1.Image = ((System.Drawing.Image)(resources.GetObject("keysP1.Image")));
            this.keysP1.Location = new System.Drawing.Point(338, 523);
            this.keysP1.Name = "keysP1";
            this.keysP1.Size = new System.Drawing.Size(126, 90);
            this.keysP1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.keysP1.TabIndex = 4;
            this.keysP1.TabStop = false;
            // 
            // keysP2
            // 
            this.keysP2.BackColor = System.Drawing.Color.Transparent;
            this.keysP2.Image = ((System.Drawing.Image)(resources.GetObject("keysP2.Image")));
            this.keysP2.Location = new System.Drawing.Point(657, 39);
            this.keysP2.Name = "keysP2";
            this.keysP2.Size = new System.Drawing.Size(126, 90);
            this.keysP2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.keysP2.TabIndex = 3;
            this.keysP2.TabStop = false;
            // 
            // lblInstructions
            // 
            this.lblInstructions.BackColor = System.Drawing.Color.Transparent;
            this.lblInstructions.ForeColor = System.Drawing.Color.White;
            this.lblInstructions.Location = new System.Drawing.Point(0, 61);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.Size = new System.Drawing.Size(1039, 528);
            this.lblInstructions.TabIndex = 2;
            this.lblInstructions.Text = "welcome to TRON";
            this.lblInstructions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblInstructions.Click += new System.EventHandler(this.lblInstructions_Click);
            // 
            // lblPlayer2
            // 
            this.lblPlayer2.AutoSize = true;
            this.lblPlayer2.ForeColor = System.Drawing.Color.White;
            this.lblPlayer2.Location = new System.Drawing.Point(579, 9);
            this.lblPlayer2.Name = "lblPlayer2";
            this.lblPlayer2.Size = new System.Drawing.Size(58, 13);
            this.lblPlayer2.TabIndex = 1;
            this.lblPlayer2.Text = "PLAYER 2";
            // 
            // lblPlayer1
            // 
            this.lblPlayer1.AutoSize = true;
            this.lblPlayer1.ForeColor = System.Drawing.Color.White;
            this.lblPlayer1.Location = new System.Drawing.Point(254, 616);
            this.lblPlayer1.Name = "lblPlayer1";
            this.lblPlayer1.Size = new System.Drawing.Size(58, 13);
            this.lblPlayer1.TabIndex = 0;
            this.lblPlayer1.Text = "PLAYER 1";
            this.lblPlayer1.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // btnSelectMouse1
            // 
            this.btnSelectMouse1.BackColor = System.Drawing.Color.Transparent;
            this.btnSelectMouse1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSelectMouse1.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectMouse1.Image")));
            this.btnSelectMouse1.Location = new System.Drawing.Point(279, 533);
            this.btnSelectMouse1.Name = "btnSelectMouse1";
            this.btnSelectMouse1.Size = new System.Drawing.Size(53, 70);
            this.btnSelectMouse1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnSelectMouse1.TabIndex = 13;
            this.btnSelectMouse1.TabStop = false;
            this.btnSelectMouse1.Click += new System.EventHandler(this.btnSelectMouse1_Click);
            // 
            // btnSelectMouse2
            // 
            this.btnSelectMouse2.BackColor = System.Drawing.Color.Transparent;
            this.btnSelectMouse2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSelectMouse2.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectMouse2.Image")));
            this.btnSelectMouse2.Location = new System.Drawing.Point(598, 50);
            this.btnSelectMouse2.Name = "btnSelectMouse2";
            this.btnSelectMouse2.Size = new System.Drawing.Size(53, 70);
            this.btnSelectMouse2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnSelectMouse2.TabIndex = 14;
            this.btnSelectMouse2.TabStop = false;
            this.btnSelectMouse2.Click += new System.EventHandler(this.btnSelectMouse2_Click);
            // 
            // lblAnd1
            // 
            this.lblAnd1.AutoSize = true;
            this.lblAnd1.BackColor = System.Drawing.Color.Transparent;
            this.lblAnd1.ForeColor = System.Drawing.Color.White;
            this.lblAnd1.Location = new System.Drawing.Point(326, 535);
            this.lblAnd1.Name = "lblAnd1";
            this.lblAnd1.Size = new System.Drawing.Size(25, 13);
            this.lblAnd1.TabIndex = 15;
            this.lblAnd1.Text = "and";
            // 
            // lblAnd2
            // 
            this.lblAnd2.AutoSize = true;
            this.lblAnd2.BackColor = System.Drawing.Color.Transparent;
            this.lblAnd2.ForeColor = System.Drawing.Color.White;
            this.lblAnd2.Location = new System.Drawing.Point(644, 52);
            this.lblAnd2.Name = "lblAnd2";
            this.lblAnd2.Size = new System.Drawing.Size(25, 13);
            this.lblAnd2.TabIndex = 16;
            this.lblAnd2.Text = "and";
            // 
            // Game
            // 
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1039, 651);
            this.Controls.Add(this.panelInstructions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Game";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TRON";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Game_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Game_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Game_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Game_MouseUp);
            this.panelInstructions.ResumeLayout(false);
            this.panelInstructions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.keysP1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.keysP2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSelectMouse1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSelectMouse2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Timer timerMove;
        private System.Windows.Forms.Panel panelInstructions;
        private System.Windows.Forms.Label lblPlayer1;
        private System.Windows.Forms.Label lblPlayer2;
        private System.Windows.Forms.Label lblInstructions;
        private System.Windows.Forms.PictureBox keysP2;
        private System.Windows.Forms.PictureBox keysP1;
        private System.Windows.Forms.Label keyUp1;
        private System.Windows.Forms.Label keyLeft1;
        private System.Windows.Forms.Label keyRight1;
        private System.Windows.Forms.Label keyDown1;
        private System.Windows.Forms.Label keyDown2;
        private System.Windows.Forms.Label keyLeft2;
        private System.Windows.Forms.Label keyRight2;
        private System.Windows.Forms.Label keyUp2;
        private System.Windows.Forms.PictureBox btnSelectMouse1;
        private System.Windows.Forms.PictureBox btnSelectMouse2;
        private System.Windows.Forms.Label lblAnd1;
        private System.Windows.Forms.Label lblAnd2;
    }
}