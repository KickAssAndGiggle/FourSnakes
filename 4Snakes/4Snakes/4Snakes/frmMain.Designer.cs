namespace _4Snakes
{
    partial class frmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            board = new Panel();
            picGreenBody = new PictureBox();
            picGreenHead = new PictureBox();
            picPinkBody = new PictureBox();
            picPinkHead = new PictureBox();
            picYellowBody = new PictureBox();
            picYellowHead = new PictureBox();
            picTurquoiseBody = new PictureBox();
            picTurquoiseHead = new PictureBox();
            startTimer = new System.Windows.Forms.Timer(components);
            picMouse = new PictureBox();
            label1 = new Label();
            lblPinkScore = new Label();
            lblGreenScore = new Label();
            label4 = new Label();
            lblYellowScore = new Label();
            label6 = new Label();
            lblTealScore = new Label();
            label8 = new Label();
            lblExit = new Label();
            ((System.ComponentModel.ISupportInitialize)picGreenBody).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picGreenHead).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picPinkBody).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picPinkHead).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picYellowBody).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picYellowHead).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picTurquoiseBody).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picTurquoiseHead).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picMouse).BeginInit();
            SuspendLayout();
            // 
            // board
            // 
            board.BackColor = Color.FromArgb(64, 64, 64);
            board.BorderStyle = BorderStyle.FixedSingle;
            board.Location = new Point(50, 50);
            board.Name = "board";
            board.Size = new Size(802, 602);
            board.TabIndex = 0;
            // 
            // picGreenBody
            // 
            picGreenBody.Image = (Image)resources.GetObject("picGreenBody.Image");
            picGreenBody.Location = new Point(543, 745);
            picGreenBody.Name = "picGreenBody";
            picGreenBody.Size = new Size(25, 25);
            picGreenBody.TabIndex = 2;
            picGreenBody.TabStop = false;
            // 
            // picGreenHead
            // 
            picGreenHead.Image = (Image)resources.GetObject("picGreenHead.Image");
            picGreenHead.Location = new Point(574, 745);
            picGreenHead.Name = "picGreenHead";
            picGreenHead.Size = new Size(25, 25);
            picGreenHead.TabIndex = 3;
            picGreenHead.TabStop = false;
            // 
            // picPinkBody
            // 
            picPinkBody.Image = (Image)resources.GetObject("picPinkBody.Image");
            picPinkBody.Location = new Point(708, 745);
            picPinkBody.Name = "picPinkBody";
            picPinkBody.Size = new Size(25, 25);
            picPinkBody.TabIndex = 6;
            picPinkBody.TabStop = false;
            // 
            // picPinkHead
            // 
            picPinkHead.Image = (Image)resources.GetObject("picPinkHead.Image");
            picPinkHead.Location = new Point(739, 745);
            picPinkHead.Name = "picPinkHead";
            picPinkHead.Size = new Size(25, 25);
            picPinkHead.TabIndex = 7;
            picPinkHead.TabStop = false;
            // 
            // picYellowBody
            // 
            picYellowBody.Image = (Image)resources.GetObject("picYellowBody.Image");
            picYellowBody.Location = new Point(92, 745);
            picYellowBody.Name = "picYellowBody";
            picYellowBody.Size = new Size(25, 25);
            picYellowBody.TabIndex = 10;
            picYellowBody.TabStop = false;
            // 
            // picYellowHead
            // 
            picYellowHead.Image = (Image)resources.GetObject("picYellowHead.Image");
            picYellowHead.Location = new Point(132, 745);
            picYellowHead.Name = "picYellowHead";
            picYellowHead.Size = new Size(25, 25);
            picYellowHead.TabIndex = 11;
            picYellowHead.TabStop = false;
            // 
            // picTurquoiseBody
            // 
            picTurquoiseBody.Image = (Image)resources.GetObject("picTurquoiseBody.Image");
            picTurquoiseBody.Location = new Point(290, 745);
            picTurquoiseBody.Name = "picTurquoiseBody";
            picTurquoiseBody.Size = new Size(25, 25);
            picTurquoiseBody.TabIndex = 14;
            picTurquoiseBody.TabStop = false;
            // 
            // picTurquoiseHead
            // 
            picTurquoiseHead.Image = (Image)resources.GetObject("picTurquoiseHead.Image");
            picTurquoiseHead.Location = new Point(321, 745);
            picTurquoiseHead.Name = "picTurquoiseHead";
            picTurquoiseHead.Size = new Size(25, 25);
            picTurquoiseHead.TabIndex = 15;
            picTurquoiseHead.TabStop = false;
            // 
            // picMouse
            // 
            picMouse.Image = (Image)resources.GetObject("picMouse.Image");
            picMouse.Location = new Point(441, 745);
            picMouse.Name = "picMouse";
            picMouse.Size = new Size(25, 25);
            picMouse.TabIndex = 16;
            picMouse.TabStop = false;
            // 
            // label1
            // 
            label1.Font = new Font("Showcard Gothic", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Fuchsia;
            label1.Location = new Point(926, 90);
            label1.Name = "label1";
            label1.Size = new Size(280, 50);
            label1.TabIndex = 17;
            label1.Text = "Peter The Pink";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblPinkScore
            // 
            lblPinkScore.Font = new Font("Showcard Gothic", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPinkScore.ForeColor = Color.White;
            lblPinkScore.Location = new Point(926, 140);
            lblPinkScore.Name = "lblPinkScore";
            lblPinkScore.Size = new Size(280, 50);
            lblPinkScore.TabIndex = 18;
            lblPinkScore.Text = "0";
            lblPinkScore.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblGreenScore
            // 
            lblGreenScore.Font = new Font("Showcard Gothic", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblGreenScore.ForeColor = Color.White;
            lblGreenScore.Location = new Point(926, 265);
            lblGreenScore.Name = "lblGreenScore";
            lblGreenScore.Size = new Size(280, 50);
            lblGreenScore.TabIndex = 20;
            lblGreenScore.Text = "0";
            lblGreenScore.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            label4.Font = new Font("Showcard Gothic", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.FromArgb(0, 192, 0);
            label4.Location = new Point(926, 215);
            label4.Name = "label4";
            label4.Size = new Size(280, 50);
            label4.TabIndex = 19;
            label4.Text = "Greg The Green";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblYellowScore
            // 
            lblYellowScore.Font = new Font("Showcard Gothic", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblYellowScore.ForeColor = Color.White;
            lblYellowScore.Location = new Point(926, 402);
            lblYellowScore.Name = "lblYellowScore";
            lblYellowScore.Size = new Size(280, 50);
            lblYellowScore.TabIndex = 22;
            lblYellowScore.Text = "0";
            lblYellowScore.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            label6.Font = new Font("Showcard Gothic", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.Yellow;
            label6.Location = new Point(926, 352);
            label6.Name = "label6";
            label6.Size = new Size(280, 50);
            label6.TabIndex = 21;
            label6.Text = "Yuri The Yellow";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTealScore
            // 
            lblTealScore.Font = new Font("Showcard Gothic", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTealScore.ForeColor = Color.White;
            lblTealScore.Location = new Point(926, 534);
            lblTealScore.Name = "lblTealScore";
            lblTealScore.Size = new Size(280, 50);
            lblTealScore.TabIndex = 24;
            lblTealScore.Text = "0";
            lblTealScore.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            label8.Font = new Font("Showcard Gothic", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.FromArgb(0, 192, 192);
            label8.Location = new Point(926, 484);
            label8.Name = "label8";
            label8.Size = new Size(280, 50);
            label8.TabIndex = 23;
            label8.Text = "Tom The Teal";
            label8.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblExit
            // 
            lblExit.Cursor = Cursors.Hand;
            lblExit.Font = new Font("Unispace", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblExit.ForeColor = Color.FromArgb(192, 0, 0);
            lblExit.Location = new Point(1196, 9);
            lblExit.Name = "lblExit";
            lblExit.Size = new Size(50, 56);
            lblExit.TabIndex = 25;
            lblExit.Text = "X";
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(11F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(1258, 717);
            Controls.Add(lblExit);
            Controls.Add(lblTealScore);
            Controls.Add(label8);
            Controls.Add(lblYellowScore);
            Controls.Add(label6);
            Controls.Add(lblGreenScore);
            Controls.Add(label4);
            Controls.Add(lblPinkScore);
            Controls.Add(label1);
            Controls.Add(picMouse);
            Controls.Add(picTurquoiseHead);
            Controls.Add(picTurquoiseBody);
            Controls.Add(picYellowHead);
            Controls.Add(picYellowBody);
            Controls.Add(picPinkHead);
            Controls.Add(picPinkBody);
            Controls.Add(picGreenHead);
            Controls.Add(picGreenBody);
            Controls.Add(board);
            DoubleBuffered = true;
            Font = new Font("Showcard Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(5, 4, 5, 4);
            Name = "frmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)picGreenBody).EndInit();
            ((System.ComponentModel.ISupportInitialize)picGreenHead).EndInit();
            ((System.ComponentModel.ISupportInitialize)picPinkBody).EndInit();
            ((System.ComponentModel.ISupportInitialize)picPinkHead).EndInit();
            ((System.ComponentModel.ISupportInitialize)picYellowBody).EndInit();
            ((System.ComponentModel.ISupportInitialize)picYellowHead).EndInit();
            ((System.ComponentModel.ISupportInitialize)picTurquoiseBody).EndInit();
            ((System.ComponentModel.ISupportInitialize)picTurquoiseHead).EndInit();
            ((System.ComponentModel.ISupportInitialize)picMouse).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel board;
        private PictureBox picGreenBody;
        private PictureBox picGreenHead;
        private PictureBox picPinkBody;
        private PictureBox picPinkHead;
        private PictureBox picYellowBody;
        private PictureBox picYellowHead;
        private PictureBox picTurquoiseBody;
        private PictureBox picTurquoiseHead;
        private System.Windows.Forms.Timer startTimer;
        private PictureBox picMouse;
        private Label label1;
        private Label lblPinkScore;
        private Label lblGreenScore;
        private Label label4;
        private Label lblYellowScore;
        private Label label6;
        private Label lblTealScore;
        private Label label8;
        private Label lblExit;
    }
}
