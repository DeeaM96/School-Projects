namespace TRON
{
    partial class Colors
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Colors));
            this.colorDialogP1 = new System.Windows.Forms.ColorDialog();
            this.colorDialogP2 = new System.Windows.Forms.ColorDialog();
            this.colorP1 = new System.Windows.Forms.PictureBox();
            this.colorP2 = new System.Windows.Forms.PictureBox();
            this.lblPlayer1 = new System.Windows.Forms.PictureBox();
            this.lblPlayer2 = new System.Windows.Forms.PictureBox();
            this.lblPickColor = new System.Windows.Forms.PictureBox();
            this.btnConfirm = new System.Windows.Forms.PictureBox();
            this.confirmHover = new System.Windows.Forms.PictureBox();
            this.confirmHover2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.colorP1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorP2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPlayer1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPlayer2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPickColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnConfirm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.confirmHover)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.confirmHover2)).BeginInit();
            this.SuspendLayout();
            // 
            // colorP1
            // 
            this.colorP1.BackColor = System.Drawing.Color.Blue;
            this.colorP1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.colorP1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.colorP1.Location = new System.Drawing.Point(405, 105);
            this.colorP1.Name = "colorP1";
            this.colorP1.Size = new System.Drawing.Size(45, 45);
            this.colorP1.TabIndex = 0;
            this.colorP1.TabStop = false;
            this.colorP1.Click += new System.EventHandler(this.colorP1_Click);
            // 
            // colorP2
            // 
            this.colorP2.BackColor = System.Drawing.Color.Yellow;
            this.colorP2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.colorP2.Location = new System.Drawing.Point(405, 155);
            this.colorP2.Name = "colorP2";
            this.colorP2.Size = new System.Drawing.Size(45, 45);
            this.colorP2.TabIndex = 1;
            this.colorP2.TabStop = false;
            this.colorP2.Click += new System.EventHandler(this.colorP2_Click);
            // 
            // lblPlayer1
            // 
            this.lblPlayer1.BackColor = System.Drawing.Color.Transparent;
            this.lblPlayer1.Image = ((System.Drawing.Image)(resources.GetObject("lblPlayer1.Image")));
            this.lblPlayer1.Location = new System.Drawing.Point(105, 110);
            this.lblPlayer1.Name = "lblPlayer1";
            this.lblPlayer1.Size = new System.Drawing.Size(244, 35);
            this.lblPlayer1.TabIndex = 2;
            this.lblPlayer1.TabStop = false;
            // 
            // lblPlayer2
            // 
            this.lblPlayer2.BackColor = System.Drawing.Color.Transparent;
            this.lblPlayer2.Image = ((System.Drawing.Image)(resources.GetObject("lblPlayer2.Image")));
            this.lblPlayer2.Location = new System.Drawing.Point(105, 160);
            this.lblPlayer2.Name = "lblPlayer2";
            this.lblPlayer2.Size = new System.Drawing.Size(244, 35);
            this.lblPlayer2.TabIndex = 3;
            this.lblPlayer2.TabStop = false;
            // 
            // lblPickColor
            // 
            this.lblPickColor.BackColor = System.Drawing.Color.Transparent;
            this.lblPickColor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.lblPickColor.Image = ((System.Drawing.Image)(resources.GetObject("lblPickColor.Image")));
            this.lblPickColor.Location = new System.Drawing.Point(12, 35);
            this.lblPickColor.Name = "lblPickColor";
            this.lblPickColor.Size = new System.Drawing.Size(531, 35);
            this.lblPickColor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.lblPickColor.TabIndex = 4;
            this.lblPickColor.TabStop = false;
            // 
            // btnConfirm
            // 
            this.btnConfirm.BackColor = System.Drawing.Color.Transparent;
            this.btnConfirm.Image = ((System.Drawing.Image)(resources.GetObject("btnConfirm.Image")));
            this.btnConfirm.Location = new System.Drawing.Point(12, 255);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(531, 26);
            this.btnConfirm.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btnConfirm.TabIndex = 5;
            this.btnConfirm.TabStop = false;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            this.btnConfirm.MouseLeave += new System.EventHandler(this.btnConfirm_MouseLeave);
            this.btnConfirm.MouseHover += new System.EventHandler(this.btnConfirm_MouseHover);
            // 
            // confirmHover
            // 
            this.confirmHover.BackColor = System.Drawing.Color.Transparent;
            this.confirmHover.Image = ((System.Drawing.Image)(resources.GetObject("confirmHover.Image")));
            this.confirmHover.Location = new System.Drawing.Point(184, 239);
            this.confirmHover.Name = "confirmHover";
            this.confirmHover.Size = new System.Drawing.Size(29, 55);
            this.confirmHover.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.confirmHover.TabIndex = 6;
            this.confirmHover.TabStop = false;
            this.confirmHover.Visible = false;
            // 
            // confirmHover2
            // 
            this.confirmHover2.BackColor = System.Drawing.Color.Transparent;
            this.confirmHover2.Image = ((System.Drawing.Image)(resources.GetObject("confirmHover2.Image")));
            this.confirmHover2.Location = new System.Drawing.Point(342, 239);
            this.confirmHover2.Name = "confirmHover2";
            this.confirmHover2.Size = new System.Drawing.Size(29, 55);
            this.confirmHover2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.confirmHover2.TabIndex = 7;
            this.confirmHover2.TabStop = false;
            this.confirmHover2.Visible = false;
            // 
            // Colors
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(555, 305);
            this.Controls.Add(this.confirmHover2);
            this.Controls.Add(this.confirmHover);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.lblPickColor);
            this.Controls.Add(this.lblPlayer2);
            this.Controls.Add(this.lblPlayer1);
            this.Controls.Add(this.colorP2);
            this.Controls.Add(this.colorP1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Colors";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TRON";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Colors_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.colorP1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorP2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPlayer1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPlayer2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPickColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnConfirm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.confirmHover)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.confirmHover2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColorDialog colorDialogP1;
        private System.Windows.Forms.ColorDialog colorDialogP2;
        private System.Windows.Forms.PictureBox colorP1;
        private System.Windows.Forms.PictureBox colorP2;
        private System.Windows.Forms.PictureBox lblPlayer1;
        private System.Windows.Forms.PictureBox lblPlayer2;
        private System.Windows.Forms.PictureBox lblPickColor;
        private System.Windows.Forms.PictureBox btnConfirm;
        private System.Windows.Forms.PictureBox confirmHover;
        private System.Windows.Forms.PictureBox confirmHover2;
    }
}