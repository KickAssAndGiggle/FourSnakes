namespace _4Snakes
{
    partial class frmSnakeSelect
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
            lblPeter = new Label();
            lblGreg = new Label();
            lblYuri = new Label();
            lblTom = new Label();
            label4 = new Label();
            SuspendLayout();
            // 
            // lblPeter
            // 
            lblPeter.BackColor = Color.FromArgb(192, 0, 192);
            lblPeter.Cursor = Cursors.Hand;
            lblPeter.Font = new Font("Showcard Gothic", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPeter.Location = new Point(85, 213);
            lblPeter.Name = "lblPeter";
            lblPeter.Size = new Size(200, 80);
            lblPeter.TabIndex = 0;
            lblPeter.Text = "Play as Peter";
            lblPeter.TextAlign = ContentAlignment.MiddleCenter;
            lblPeter.Click += lblPeter_Click;
            // 
            // lblGreg
            // 
            lblGreg.BackColor = Color.FromArgb(0, 192, 0);
            lblGreg.Cursor = Cursors.Hand;
            lblGreg.Font = new Font("Showcard Gothic", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblGreg.Location = new Point(357, 213);
            lblGreg.Name = "lblGreg";
            lblGreg.Size = new Size(200, 80);
            lblGreg.TabIndex = 1;
            lblGreg.Text = "Play as Greg";
            lblGreg.TextAlign = ContentAlignment.MiddleCenter;
            lblGreg.Click += lblGreg_Click;
            // 
            // lblYuri
            // 
            lblYuri.BackColor = Color.Yellow;
            lblYuri.Cursor = Cursors.Hand;
            lblYuri.Font = new Font("Showcard Gothic", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblYuri.Location = new Point(85, 310);
            lblYuri.Name = "lblYuri";
            lblYuri.Size = new Size(200, 80);
            lblYuri.TabIndex = 2;
            lblYuri.Text = "Play as Yuri";
            lblYuri.TextAlign = ContentAlignment.MiddleCenter;
            lblYuri.Click += lblYuri_Click;
            // 
            // lblTom
            // 
            lblTom.BackColor = Color.FromArgb(0, 192, 192);
            lblTom.Cursor = Cursors.Hand;
            lblTom.Font = new Font("Showcard Gothic", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTom.Location = new Point(357, 310);
            lblTom.Name = "lblTom";
            lblTom.Size = new Size(200, 80);
            lblTom.TabIndex = 3;
            lblTom.Text = "Play as Tom";
            lblTom.TextAlign = ContentAlignment.MiddleCenter;
            lblTom.Click += lblTom_Click;
            // 
            // label4
            // 
            label4.BackColor = Color.FromArgb(35, 35, 35);
            label4.Cursor = Cursors.Hand;
            label4.Font = new Font("Showcard Gothic", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.White;
            label4.Location = new Point(41, 19);
            label4.Name = "label4";
            label4.Size = new Size(567, 181);
            label4.TabIndex = 4;
            label4.Text = "Four Player Snake \r\n\r\nBy Russell Lambert\r\n\r\nArrow keys to move\r\n";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // frmSnakeSelect
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(35, 35, 35);
            ClientSize = new Size(646, 420);
            Controls.Add(label4);
            Controls.Add(lblTom);
            Controls.Add(lblYuri);
            Controls.Add(lblGreg);
            Controls.Add(lblPeter);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmSnakeSelect";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Pick your snake";
            ResumeLayout(false);
        }

        #endregion

        private Label lblPeter;
        private Label lblGreg;
        private Label lblYuri;
        private Label lblTom;
        private Label label4;
    }
}