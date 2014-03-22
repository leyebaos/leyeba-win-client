namespace leyeba
{
    partial class FormMain
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
            if (task != null && 
                !task.IsDisposed)
                task.Dispose();
            if (log != null && 
                !log.IsDisposed)
                log.Dispose();
            if (userSetting != null &&
                !userSetting.IsDisposed)
                userSetting.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblFeedback = new System.Windows.Forms.LinkLabel();
            this.btnMax = new ControlEx.ButtonBase();
            this.btnClose = new ControlEx.ButtonBase();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pnlLeft);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(46, 490);
            this.panel1.TabIndex = 0;
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.Transparent;
            this.pnlLeft.Controls.Add(this.panel6);
            this.pnlLeft.Controls.Add(this.panel4);
            this.pnlLeft.Controls.Add(this.panel2);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Location = new System.Drawing.Point(0, 11);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(46, 468);
            this.pnlLeft.TabIndex = 1;
            this.pnlLeft.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlLeft_Paint);
            this.pnlLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
            this.pnlLeft.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseMove);
            this.pnlLeft.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Transparent;
            this.panel6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panel6.Location = new System.Drawing.Point(8, 129);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(38, 45);
            this.panel6.TabIndex = 2;
            this.panel6.Tag = "2";
            this.panel6.Click += new System.EventHandler(this.OnTabControlClick);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panel4.Location = new System.Drawing.Point(8, 72);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(38, 45);
            this.panel4.TabIndex = 1;
            this.panel4.Tag = "1";
            this.panel4.Click += new System.EventHandler(this.OnTabControlClick);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panel2.Location = new System.Drawing.Point(8, 16);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(38, 45);
            this.panel2.TabIndex = 0;
            this.panel2.Tag = "0";
            this.panel2.Click += new System.EventHandler(this.OnTabControlClick);
            // 
            // panel5
            // 
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 479);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(46, 11);
            this.panel5.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(46, 11);
            this.panel3.TabIndex = 0;
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.Color.White;
            this.pnlRight.BackgroundImage = global::leyeba.Properties.Resources.main_topbg_center;
            this.pnlRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlRight.Controls.Add(this.pnlMain);
            this.pnlRight.Controls.Add(this.pnlTop);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(46, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Padding = new System.Windows.Forms.Padding(1);
            this.pnlRight.Size = new System.Drawing.Size(367, 490);
            this.pnlRight.TabIndex = 1;
            this.pnlRight.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlRight_Paint);
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.Transparent;
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(1, 33);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(365, 456);
            this.pnlMain.TabIndex = 10;
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.Transparent;
            this.pnlTop.Controls.Add(this.lblFeedback);
            this.pnlTop.Controls.Add(this.btnMax);
            this.pnlTop.Controls.Add(this.btnClose);
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(1, 1);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(365, 32);
            this.pnlTop.TabIndex = 9;
            this.pnlTop.DoubleClick += new System.EventHandler(this.OnTopDoubleClick);
            this.pnlTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
            this.pnlTop.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseMove);
            this.pnlTop.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
            // 
            // lblFeedback
            // 
            this.lblFeedback.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(122)))), ((int)(((byte)(230)))));
            this.lblFeedback.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFeedback.AutoSize = true;
            this.lblFeedback.BackColor = System.Drawing.Color.Transparent;
            this.lblFeedback.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lblFeedback.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(122)))), ((int)(((byte)(185)))));
            this.lblFeedback.Location = new System.Drawing.Point(271, 10);
            this.lblFeedback.Name = "lblFeedback";
            this.lblFeedback.Size = new System.Drawing.Size(29, 12);
            this.lblFeedback.TabIndex = 3;
            this.lblFeedback.TabStop = true;
            this.lblFeedback.Text = "反馈";
            this.lblFeedback.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblFeedback_LinkClicked);
            // 
            // btnMax
            // 
            this.btnMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMax.BackColor = System.Drawing.Color.Transparent;
            this.btnMax.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMax.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnMax.ForeColor = System.Drawing.Color.White;
            this.btnMax.ImageHover = global::leyeba.Properties.Resources.btn_main_max_hot;
            this.btnMax.ImageNormal = global::leyeba.Properties.Resources.btn_main_max_nor;
            this.btnMax.ImagePress = global::leyeba.Properties.Resources.btn_main_max_down;
            this.btnMax.Location = new System.Drawing.Point(300, 6);
            this.btnMax.Name = "btnMax";
            this.btnMax.Size = new System.Drawing.Size(27, 21);
            this.btnMax.TabIndex = 2;
            this.btnMax.Tag = "0";
            this.btnMax.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnMax.Click += new System.EventHandler(this.btnMax_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.ImageHover = global::leyeba.Properties.Resources.btn_main_close_down;
            this.btnClose.ImageNormal = global::leyeba.Properties.Resources.btn_main_close_nor;
            this.btnClose.ImagePress = global::leyeba.Properties.Resources.btn_main_close_down;
            this.btnClose.Location = new System.Drawing.Point(333, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(27, 21);
            this.btnClose.TabIndex = 1;
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.ForeColor = System.Drawing.Color.Black;
            this.lblTitle.Location = new System.Drawing.Point(16, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(44, 12);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "乐业吧";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(413, 490);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.Text = "乐业吧";
            this.panel1.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel pnlTop;
        private ControlEx.ButtonBase btnClose;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel6;
        private ControlEx.ButtonBase btnMax;
        private System.Windows.Forms.LinkLabel lblFeedback;
    }
}