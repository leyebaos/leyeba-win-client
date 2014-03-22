namespace leyeba
{
    partial class FormLogin
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLogin));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.lblReg = new System.Windows.Forms.LinkLabel();
            this.lblForgot = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblNetSetting = new System.Windows.Forms.LinkLabel();
            this.pnlLogin = new System.Windows.Forms.Panel();
            this.pnlLoading = new System.Windows.Forms.Panel();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.btnLogin = new ControlEx.ButtonBase();
            this.comboboxUserName1 = new ControlEx.ComboBoxUserName();
            this.chkRemember = new ControlEx.CheckBoxBase();
            this.txtPwd = new ControlEx.TextBoxPassword();
            this.chkAutoLogin = new ControlEx.CheckBoxBase();
            this.btnMini = new ControlEx.ButtonBase();
            this.btnClose = new ControlEx.ButtonBase();
            this.loginingControl1 = new ControlEx.LoginingControl();
            this.btnCancelLogin = new ControlEx.ButtonBase();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnlLogin.SuspendLayout();
            this.pnlLoading.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(3, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "账号：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(3, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "密码：";
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipText = "欢迎回来";
            this.notifyIcon.BalloonTipTitle = "乐业吧客户端";
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "乐业吧客户端";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseUp += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseUp);
            // 
            // lblReg
            // 
            this.lblReg.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(122)))), ((int)(((byte)(230)))));
            this.lblReg.AutoSize = true;
            this.lblReg.BackColor = System.Drawing.Color.Transparent;
            this.lblReg.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lblReg.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(122)))), ((int)(((byte)(185)))));
            this.lblReg.Location = new System.Drawing.Point(248, 16);
            this.lblReg.Name = "lblReg";
            this.lblReg.Size = new System.Drawing.Size(53, 12);
            this.lblReg.TabIndex = 7;
            this.lblReg.Text = "注册账号";
            this.lblReg.TabStop = false;
            this.lblReg.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblReg_LinkClicked);
            // 
            // lblForgot
            // 
            this.lblForgot.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(122)))), ((int)(((byte)(230)))));
            this.lblForgot.AutoSize = true;
            this.lblForgot.BackColor = System.Drawing.Color.Transparent;
            this.lblForgot.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lblForgot.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(122)))), ((int)(((byte)(185)))));
            this.lblForgot.Location = new System.Drawing.Point(248, 49);
            this.lblForgot.Name = "lblForgot";
            this.lblForgot.Size = new System.Drawing.Size(53, 12);
            this.lblForgot.TabIndex = 8;
            this.lblForgot.Text = "找回密码";
            this.lblForgot.TabStop = false;
            this.lblForgot.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblForgot_LinkClicked);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::leyeba.Properties.Resources.login_logo;
            this.pictureBox1.Location = new System.Drawing.Point(8, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(59, 17);
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(105)))), ((int)(((byte)(140)))));
            this.lblVersion.Location = new System.Drawing.Point(16, 273);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(83, 12);
            this.lblVersion.TabIndex = 2;
            this.lblVersion.Text = "版本：1.0.0.0";
            // 
            // lblNetSetting
            // 
            this.lblNetSetting.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(122)))), ((int)(((byte)(230)))));
            this.lblNetSetting.AutoSize = true;
            this.lblNetSetting.BackColor = System.Drawing.Color.Transparent;
            this.lblNetSetting.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(122)))), ((int)(((byte)(185)))));
            this.lblNetSetting.Location = new System.Drawing.Point(311, 273);
            this.lblNetSetting.Name = "lblNetSetting";
            this.lblNetSetting.Size = new System.Drawing.Size(53, 12);
            this.lblNetSetting.TabIndex = 1;
            this.lblNetSetting.Text = "网络设置";
            this.lblNetSetting.TabStop = false;
            this.lblNetSetting.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblNetSetting_LinkClicked);
            // 
            // pnlLogin
            // 
            this.pnlLogin.BackColor = System.Drawing.Color.Transparent;
            this.pnlLogin.Controls.Add(this.label1);
            this.pnlLogin.Controls.Add(this.btnLogin);
            this.pnlLogin.Controls.Add(this.comboboxUserName1);
            this.pnlLogin.Controls.Add(this.lblReg);
            this.pnlLogin.Controls.Add(this.label2);
            this.pnlLogin.Controls.Add(this.lblForgot);
            this.pnlLogin.Controls.Add(this.chkRemember);
            this.pnlLogin.Controls.Add(this.txtPwd);
            this.pnlLogin.Controls.Add(this.chkAutoLogin);
            this.pnlLogin.Location = new System.Drawing.Point(38, 113);
            this.pnlLogin.Name = "pnlLogin";
            this.pnlLogin.Size = new System.Drawing.Size(304, 142);
            this.pnlLogin.TabIndex = 0;
            // 
            // pnlLoading
            // 
            this.pnlLoading.BackColor = System.Drawing.Color.Transparent;
            this.pnlLoading.Controls.Add(this.loginingControl1);
            this.pnlLoading.Controls.Add(this.btnCancelLogin);
            this.pnlLoading.Controls.Add(this.lblWelcome);
            this.pnlLoading.Location = new System.Drawing.Point(38, 113);
            this.pnlLoading.Name = "pnlLoading";
            this.pnlLoading.Size = new System.Drawing.Size(304, 142);
            this.pnlLoading.TabIndex = 21;
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWelcome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(97)))), ((int)(((byte)(97)))));
            this.lblWelcome.Location = new System.Drawing.Point(89, 19);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(127, 31);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "username";
            // 
            // btnLogin
            // 
            this.btnLogin.ActiveLinkColor = System.Drawing.Color.White;
            this.btnLogin.BackColor = System.Drawing.Color.Transparent;
            this.btnLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.ImageHover = global::leyeba.Properties.Resources.loginbtn_hot_png;
            this.btnLogin.ImageNormal = global::leyeba.Properties.Resources.loginbtn_normal;
            this.btnLogin.ImagePress = global::leyeba.Properties.Resources.loginbtn_press;
            this.btnLogin.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.btnLogin.LinkColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(50, 101);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(160, 35);
            this.btnLogin.TabIndex = 6;
            this.btnLogin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // comboboxUserName1
            // 
            this.comboboxUserName1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("comboboxUserName1.BackgroundImage")));
            this.comboboxUserName1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.comboboxUserName1.Location = new System.Drawing.Point(52, 8);
            this.comboboxUserName1.Name = "comboboxUserName1";
            this.comboboxUserName1.Size = new System.Drawing.Size(190, 28);
            this.comboboxUserName1.TabIndex = 1;
            // 
            // chkRemember
            // 
            this.chkRemember.BackColor = System.Drawing.Color.Transparent;
            this.chkRemember.Checked = false;
            this.chkRemember.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.chkRemember.ImageNormal = global::leyeba.Properties.Resources.choose_nor_login;
            this.chkRemember.ImgeChecked = global::leyeba.Properties.Resources.choose_checked_login;
            this.chkRemember.Location = new System.Drawing.Point(53, 77);
            this.chkRemember.Name = "chkRemember";
            this.chkRemember.Size = new System.Drawing.Size(72, 16);
            this.chkRemember.TabIndex = 4;
            this.chkRemember.TabStop = false;
            this.chkRemember.Text = "记住密码";
            // 
            // txtPwd
            // 
            this.txtPwd.BackColor = System.Drawing.Color.White;
            this.txtPwd.BackgroundImage = global::leyeba.Properties.Resources.inputpassword_nor;
            this.txtPwd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtPwd.Location = new System.Drawing.Point(53, 42);
            this.txtPwd.MaximumSize = new System.Drawing.Size(188, 26);
            this.txtPwd.MinimumSize = new System.Drawing.Size(188, 26);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.Size = new System.Drawing.Size(188, 26);
            this.txtPwd.TabIndex = 3;
            // 
            // chkAutoLogin
            // 
            this.chkAutoLogin.BackColor = System.Drawing.Color.Transparent;
            this.chkAutoLogin.Checked = false;
            this.chkAutoLogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.chkAutoLogin.ImageNormal = global::leyeba.Properties.Resources.choose_nor_login;
            this.chkAutoLogin.ImgeChecked = global::leyeba.Properties.Resources.choose_checked_login;
            this.chkAutoLogin.Location = new System.Drawing.Point(131, 77);
            this.chkAutoLogin.Name = "chkAutoLogin";
            this.chkAutoLogin.Size = new System.Drawing.Size(72, 16);
            this.chkAutoLogin.TabIndex = 5;
            this.chkAutoLogin.TabStop = false;
            this.chkAutoLogin.Text = "自动登陆";
            this.chkAutoLogin.CheckedChanged += new System.EventHandler(this.chkAutoLogin_CheckedChanged);
            // 
            // btnMini
            // 
            this.btnMini.ActiveLinkColor = System.Drawing.Color.White;
            this.btnMini.BackColor = System.Drawing.Color.Transparent;
            this.btnMini.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMini.ForeColor = System.Drawing.Color.White;
            this.btnMini.ImageHover = global::leyeba.Properties.Resources.btn_mini_press;
            this.btnMini.ImageNormal = global::leyeba.Properties.Resources.btn_mini_normal;
            this.btnMini.ImagePress = global::leyeba.Properties.Resources.btn_mini_down;
            this.btnMini.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.btnMini.LinkColor = System.Drawing.Color.White;
            this.btnMini.Location = new System.Drawing.Point(303, 0);
            this.btnMini.Name = "btnMini";
            this.btnMini.Size = new System.Drawing.Size(31, 23);
            this.btnMini.TabIndex = 19;
            this.btnMini.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnMini.Click += new System.EventHandler(this.btnMini_Click);
            // 
            // btnClose
            // 
            this.btnClose.ActiveLinkColor = System.Drawing.Color.White;
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.ImageHover = ((System.Drawing.Image)(resources.GetObject("btnClose.ImageHover")));
            this.btnClose.ImageNormal = ((System.Drawing.Image)(resources.GetObject("btnClose.ImageNormal")));
            this.btnClose.ImagePress = ((System.Drawing.Image)(resources.GetObject("btnClose.ImagePress")));
            this.btnClose.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.btnClose.LinkColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(335, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(45, 23);
            this.btnClose.TabIndex = 11;
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // loginingControl1
            // 
            this.loginingControl1.BackColor = System.Drawing.Color.Transparent;
            this.loginingControl1.Enabled = false;
            this.loginingControl1.Font = new System.Drawing.Font("微软雅黑", 18F);
            this.loginingControl1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(97)))), ((int)(((byte)(97)))));
            this.loginingControl1.Location = new System.Drawing.Point(94, 59);
            this.loginingControl1.Name = "loginingControl1";
            this.loginingControl1.Size = new System.Drawing.Size(116, 23);
            this.loginingControl1.TabIndex = 10;
            this.loginingControl1.Text = "登录中...";
            // 
            // btnCancelLogin
            // 
            this.btnCancelLogin.ActiveLinkColor = System.Drawing.Color.White;
            this.btnCancelLogin.BackColor = System.Drawing.Color.Transparent;
            this.btnCancelLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancelLogin.ForeColor = System.Drawing.Color.White;
            this.btnCancelLogin.ImageHover = global::leyeba.Properties.Resources.loginbtn_cancle_hot;
            this.btnCancelLogin.ImageNormal = global::leyeba.Properties.Resources.loginbtn_cancle_normal;
            this.btnCancelLogin.ImagePress = global::leyeba.Properties.Resources.loginbtn_cancle_press;
            this.btnCancelLogin.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.btnCancelLogin.LinkColor = System.Drawing.Color.White;
            this.btnCancelLogin.Location = new System.Drawing.Point(72, 100);
            this.btnCancelLogin.Name = "btnCancelLogin";
            this.btnCancelLogin.Size = new System.Drawing.Size(160, 35);
            this.btnCancelLogin.TabIndex = 9;
            this.btnCancelLogin.TabStop = true;
            this.btnCancelLogin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnCancelLogin.Click += new System.EventHandler(this.btnCancelLogin_Click);
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::leyeba.Properties.Resources.login_bg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(380, 294);
            this.Controls.Add(this.pnlLogin);
            this.Controls.Add(this.btnMini);
            this.Controls.Add(this.lblNetSetting);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pnlLoading);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FormLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登录";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnlLogin.ResumeLayout(false);
            this.pnlLogin.PerformLayout();
            this.pnlLoading.ResumeLayout(false);
            this.pnlLoading.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.LinkLabel lblReg;
        private System.Windows.Forms.LinkLabel lblForgot;
        private ControlEx.ButtonBase btnClose;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.LinkLabel lblNetSetting;
        private ControlEx.CheckBoxBase chkRemember;
        private ControlEx.CheckBoxBase chkAutoLogin;
        private ControlEx.TextBoxPassword txtPwd;
        private ControlEx.ButtonBase btnMini;
        private ControlEx.ButtonBase btnLogin;
        private ControlEx.ComboBoxUserName comboboxUserName1;
        private System.Windows.Forms.Panel pnlLogin;
        private System.Windows.Forms.Panel pnlLoading;
        private System.Windows.Forms.Label lblWelcome;
        private ControlEx.ButtonBase btnCancelLogin;
        private ControlEx.LoginingControl loginingControl1;
    }
}

