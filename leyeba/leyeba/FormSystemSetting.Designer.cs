namespace leyeba
{
    partial class FormSystemSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSystemSetting));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new ControlEx.ButtonBase();
            this.btnConfirm = new ControlEx.ButtonBase();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rdoAutoUpdate = new System.Windows.Forms.RadioButton();
            this.rdoPromptUpdate = new System.Windows.Forms.RadioButton();
            this.titleControl4 = new ControlEx.TitleControl();
            this.titleControl3 = new ControlEx.TitleControl();
            this.titleControl2 = new ControlEx.TitleControl();
            this.btnUpdate = new ControlEx.ButtonBase();
            this.btnTestProxy = new ControlEx.ButtonBase();
            this.txtDomain = new ControlEx.TextBoxBase();
            this.txtPassword = new ControlEx.TextBoxBase();
            this.txtUsrname = new ControlEx.TextBoxBase();
            this.txtPort = new ControlEx.TextBoxBase();
            this.txtHost = new ControlEx.TextBoxBase();
            this.cboProxy = new ControlEx.ComboBoxBase();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.chkEnabledProxy = new ControlEx.CheckBoxBase();
            this.titleControl1 = new ControlEx.TitleControl();
            this.chkAutoLaunch = new ControlEx.CheckBoxBase();
            this.pnlContainer.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.panel2);
            this.pnlContainer.Controls.Add(this.panel1);
            this.pnlContainer.Size = new System.Drawing.Size(409, 518);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnConfirm);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(1, 459);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(407, 58);
            this.panel1.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.ActiveLinkColor = System.Drawing.Color.White;
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.ImageHover = global::leyeba.Properties.Resources.btn_gray_bg_hot;
            this.btnCancel.ImageNormal = global::leyeba.Properties.Resources.btn_gray_bg_nor;
            this.btnCancel.ImagePress = global::leyeba.Properties.Resources.btn_gray_bg_down;
            this.btnCancel.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.btnCancel.LinkColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(109, 15);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 27);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.TabStop = true;
            this.btnCancel.Text = "关闭";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.ActiveLinkColor = System.Drawing.Color.White;
            this.btnConfirm.BackColor = System.Drawing.Color.Transparent;
            this.btnConfirm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnConfirm.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConfirm.ForeColor = System.Drawing.Color.White;
            this.btnConfirm.ImageHover = ((System.Drawing.Image)(resources.GetObject("btnConfirm.ImageHover")));
            this.btnConfirm.ImageNormal = ((System.Drawing.Image)(resources.GetObject("btnConfirm.ImageNormal")));
            this.btnConfirm.ImagePress = ((System.Drawing.Image)(resources.GetObject("btnConfirm.ImagePress")));
            this.btnConfirm.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.btnConfirm.LinkColor = System.Drawing.Color.White;
            this.btnConfirm.Location = new System.Drawing.Point(13, 15);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(90, 27);
            this.btnConfirm.TabIndex = 2;
            this.btnConfirm.TabStop = true;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.titleControl4);
            this.panel2.Controls.Add(this.titleControl3);
            this.panel2.Controls.Add(this.titleControl2);
            this.panel2.Controls.Add(this.btnUpdate);
            this.panel2.Controls.Add(this.btnTestProxy);
            this.panel2.Controls.Add(this.txtDomain);
            this.panel2.Controls.Add(this.txtPassword);
            this.panel2.Controls.Add(this.txtUsrname);
            this.panel2.Controls.Add(this.txtPort);
            this.panel2.Controls.Add(this.txtHost);
            this.panel2.Controls.Add(this.cboProxy);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.chkEnabledProxy);
            this.panel2.Controls.Add(this.titleControl1);
            this.panel2.Controls.Add(this.chkAutoLaunch);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(1, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(407, 458);
            this.panel2.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Location = new System.Drawing.Point(40, 500);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(200, 40);
            this.panel4.TabIndex = 47;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.rdoAutoUpdate);
            this.panel3.Controls.Add(this.rdoPromptUpdate);
            this.panel3.Location = new System.Drawing.Point(40, 414);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(200, 53);
            this.panel3.TabIndex = 46;
            // 
            // rdoAutoUpdate
            // 
            this.rdoAutoUpdate.AutoSize = true;
            this.rdoAutoUpdate.Checked = true;
            this.rdoAutoUpdate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.rdoAutoUpdate.Location = new System.Drawing.Point(2, 3);
            this.rdoAutoUpdate.Name = "rdoAutoUpdate";
            this.rdoAutoUpdate.Size = new System.Drawing.Size(143, 16);
            this.rdoAutoUpdate.TabIndex = 18;
            this.rdoAutoUpdate.TabStop = true;
            this.rdoAutoUpdate.Text = "自动升级模式（推荐）";
            this.rdoAutoUpdate.UseVisualStyleBackColor = true;
            // 
            // rdoPromptUpdate
            // 
            this.rdoPromptUpdate.AutoSize = true;
            this.rdoPromptUpdate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.rdoPromptUpdate.Location = new System.Drawing.Point(2, 28);
            this.rdoPromptUpdate.Name = "rdoPromptUpdate";
            this.rdoPromptUpdate.Size = new System.Drawing.Size(95, 16);
            this.rdoPromptUpdate.TabIndex = 19;
            this.rdoPromptUpdate.Text = "提示升级模式";
            this.rdoPromptUpdate.UseVisualStyleBackColor = true;
            // 
            // titleControl4
            // 
            this.titleControl4.BackColor = System.Drawing.Color.Transparent;
            this.titleControl4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.titleControl4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(100)))), ((int)(((byte)(176)))));
            this.titleControl4.Location = new System.Drawing.Point(15, 388);
            this.titleControl4.Name = "titleControl4";
            this.titleControl4.Size = new System.Drawing.Size(372, 23);
            this.titleControl4.TabIndex = 44;
            this.titleControl4.Text = "选择升级模式";
            // 
            // titleControl3
            // 
            this.titleControl3.BackColor = System.Drawing.Color.Transparent;
            this.titleControl3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.titleControl3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.titleControl3.Location = new System.Drawing.Point(40, 139);
            this.titleControl3.Name = "titleControl3";
            this.titleControl3.Size = new System.Drawing.Size(347, 23);
            this.titleControl3.TabIndex = 31;
            this.titleControl3.Text = "网络设置";
            // 
            // titleControl2
            // 
            this.titleControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.titleControl2.BackColor = System.Drawing.Color.Transparent;
            this.titleControl2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.titleControl2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(100)))), ((int)(((byte)(176)))));
            this.titleControl2.Location = new System.Drawing.Point(15, 73);
            this.titleControl2.Name = "titleControl2";
            this.titleControl2.Size = new System.Drawing.Size(0, 23);
            this.titleControl2.TabIndex = 29;
            this.titleControl2.Text = "代理设置";
            // 
            // btnUpdate
            // 
            this.btnUpdate.ActiveLinkColor = System.Drawing.Color.White;
            this.btnUpdate.BackColor = System.Drawing.Color.Transparent;
            this.btnUpdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUpdate.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnUpdate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnUpdate.ImageHover = global::leyeba.Properties.Resources.btn_gray_bg_hot;
            this.btnUpdate.ImageNormal = global::leyeba.Properties.Resources.btn_gray_bg_nor;
            this.btnUpdate.ImagePress = global::leyeba.Properties.Resources.btn_gray_bg_down;
            this.btnUpdate.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.btnUpdate.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnUpdate.Location = new System.Drawing.Point(40, 470);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(90, 27);
            this.btnUpdate.TabIndex = 45;
            this.btnUpdate.TabStop = true;
            this.btnUpdate.Text = "立即更新";
            this.btnUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnTestProxy
            // 
            this.btnTestProxy.ActiveLinkColor = System.Drawing.Color.White;
            this.btnTestProxy.BackColor = System.Drawing.Color.Transparent;
            this.btnTestProxy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTestProxy.Enabled = false;
            this.btnTestProxy.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTestProxy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnTestProxy.ImageHover = global::leyeba.Properties.Resources.btn_gray_bg_hot;
            this.btnTestProxy.ImageNormal = global::leyeba.Properties.Resources.btn_gray_bg_nor;
            this.btnTestProxy.ImagePress = global::leyeba.Properties.Resources.btn_gray_bg_down;
            this.btnTestProxy.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.btnTestProxy.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnTestProxy.Location = new System.Drawing.Point(101, 347);
            this.btnTestProxy.Name = "btnTestProxy";
            this.btnTestProxy.Size = new System.Drawing.Size(90, 27);
            this.btnTestProxy.TabIndex = 43;
            this.btnTestProxy.TabStop = true;
            this.btnTestProxy.Text = "连接测试";
            this.btnTestProxy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnTestProxy.Click += new System.EventHandler(this.btnTestProxy_Click);
            // 
            // txtDomain
            // 
            this.txtDomain.BackColor = System.Drawing.Color.White;
            this.txtDomain.Location = new System.Drawing.Point(103, 306);
            this.txtDomain.MaxLength = 32767;
            this.txtDomain.Name = "txtDomain";
            this.txtDomain.Size = new System.Drawing.Size(191, 26);
            this.txtDomain.TabIndex = 42;
            this.txtDomain.Type = ControlEx.TextType.Text;
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.White;
            this.txtPassword.Location = new System.Drawing.Point(103, 272);
            this.txtPassword.MaxLength = 32767;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(191, 26);
            this.txtPassword.TabIndex = 40;
            this.txtPassword.Type = ControlEx.TextType.Text;
            // 
            // txtUsrname
            // 
            this.txtUsrname.BackColor = System.Drawing.Color.White;
            this.txtUsrname.Location = new System.Drawing.Point(103, 238);
            this.txtUsrname.MaxLength = 32767;
            this.txtUsrname.Name = "txtUsrname";
            this.txtUsrname.Size = new System.Drawing.Size(191, 26);
            this.txtUsrname.TabIndex = 38;
            this.txtUsrname.Type = ControlEx.TextType.Text;
            // 
            // txtPort
            // 
            this.txtPort.BackColor = System.Drawing.Color.White;
            this.txtPort.Location = new System.Drawing.Point(103, 204);
            this.txtPort.MaxLength = 32767;
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(191, 26);
            this.txtPort.TabIndex = 36;
            this.txtPort.Type = ControlEx.TextType.Text;
            // 
            // txtHost
            // 
            this.txtHost.BackColor = System.Drawing.Color.White;
            this.txtHost.Location = new System.Drawing.Point(103, 170);
            this.txtHost.MaxLength = 32767;
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(191, 26);
            this.txtHost.TabIndex = 34;
            this.txtHost.Type = ControlEx.TextType.Text;
            // 
            // cboProxy
            // 
            this.cboProxy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cboProxy.CanEdit = false;
            this.cboProxy.Location = new System.Drawing.Point(103, 102);
            this.cboProxy.MaxItemHeight = 80;
            this.cboProxy.Name = "cboProxy";
            this.cboProxy.Padding = new System.Windows.Forms.Padding(1);
            this.cboProxy.PopupHeight = 30;
            this.cboProxy.PopupSize = new System.Drawing.Size(0, 0);
            this.cboProxy.Size = new System.Drawing.Size(191, 26);
            this.cboProxy.TabIndex = 32;
            this.cboProxy.SelectedIndexChanged += new System.EventHandler(this.cboProxy_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.label8.Location = new System.Drawing.Point(38, 313);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 41;
            this.label8.Text = "域";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.label7.Location = new System.Drawing.Point(38, 278);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 39;
            this.label7.Text = "密  码";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.label6.Location = new System.Drawing.Point(38, 246);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 37;
            this.label6.Text = "用户名";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.label5.Location = new System.Drawing.Point(38, 211);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 35;
            this.label5.Text = "端  口";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.label4.Location = new System.Drawing.Point(38, 177);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 33;
            this.label4.Text = "服务器";
            // 
            // chkEnabledProxy
            // 
            this.chkEnabledProxy.BackColor = System.Drawing.Color.Transparent;
            this.chkEnabledProxy.Checked = false;
            this.chkEnabledProxy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.chkEnabledProxy.ImageNormal = global::leyeba.Properties.Resources.choose_nor01;
            this.chkEnabledProxy.ImgeChecked = global::leyeba.Properties.Resources.choose_down;
            this.chkEnabledProxy.Location = new System.Drawing.Point(25, 108);
            this.chkEnabledProxy.Name = "chkEnabledProxy";
            this.chkEnabledProxy.Size = new System.Drawing.Size(72, 16);
            this.chkEnabledProxy.TabIndex = 30;
            this.chkEnabledProxy.Text = "启用代理";
            this.chkEnabledProxy.CheckedChanged += new System.EventHandler(this.chkEnabledProxy_CheckedChanged);
            // 
            // titleControl1
            // 
            this.titleControl1.BackColor = System.Drawing.Color.Transparent;
            this.titleControl1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.titleControl1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(100)))), ((int)(((byte)(176)))));
            this.titleControl1.Location = new System.Drawing.Point(15, 17);
            this.titleControl1.Name = "titleControl1";
            this.titleControl1.Size = new System.Drawing.Size(372, 23);
            this.titleControl1.TabIndex = 2;
            this.titleControl1.Text = "启动、登陆和退出";
            // 
            // chkAutoLaunch
            // 
            this.chkAutoLaunch.BackColor = System.Drawing.Color.Transparent;
            this.chkAutoLaunch.Checked = false;
            this.chkAutoLaunch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.chkAutoLaunch.ImageNormal = global::leyeba.Properties.Resources.choose_nor01;
            this.chkAutoLaunch.ImgeChecked = global::leyeba.Properties.Resources.choose_down;
            this.chkAutoLaunch.Location = new System.Drawing.Point(25, 46);
            this.chkAutoLaunch.Name = "chkAutoLaunch";
            this.chkAutoLaunch.Size = new System.Drawing.Size(168, 16);
            this.chkAutoLaunch.TabIndex = 3;
            this.chkAutoLaunch.Text = "开机时自动启动乐业吧";
            // 
            // FormSystemSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 544);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormSystemSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "网络设置";
            this.Controls.SetChildIndex(this.pnlContainer, 0);
            this.pnlContainer.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private ControlEx.TitleControl titleControl1;
        private ControlEx.CheckBoxBase chkAutoLaunch;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton rdoAutoUpdate;
        private System.Windows.Forms.RadioButton rdoPromptUpdate;
        private ControlEx.TitleControl titleControl4;
        private ControlEx.TitleControl titleControl3;
        private ControlEx.TitleControl titleControl2;
        private ControlEx.ButtonBase btnUpdate;
        private ControlEx.ButtonBase btnTestProxy;
        private ControlEx.TextBoxBase txtDomain;
        private ControlEx.TextBoxBase txtPassword;
        private ControlEx.TextBoxBase txtUsrname;
        private ControlEx.TextBoxBase txtPort;
        private ControlEx.TextBoxBase txtHost;
        private ControlEx.ComboBoxBase cboProxy;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private ControlEx.CheckBoxBase chkEnabledProxy;
        private System.Windows.Forms.Panel panel4;
        private ControlEx.ButtonBase btnCancel;
        private ControlEx.ButtonBase btnConfirm;



    }
}