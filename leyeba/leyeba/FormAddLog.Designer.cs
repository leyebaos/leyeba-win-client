namespace leyeba
{
    partial class FormAddLog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAddLog));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cboProject = new ControlEx.ComboBoxBase();
            this.cboTask = new ControlEx.ComboBoxBase();
            this.txtDetail = new ControlEx.RichTextBoxBase();
            this.btnCancel = new ControlEx.ButtonBase();
            this.btnConfirm = new ControlEx.ButtonBase();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtRate = new ControlEx.TextBoxBase();
            this.txtWorkHour = new ControlEx.TextBoxTime();
            this.pnlContainer.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.panel2);
            this.pnlContainer.Controls.Add(this.panel1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.label1.Location = new System.Drawing.Point(46, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "项目名称";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.label2.Location = new System.Drawing.Point(46, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "任务名称";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.label3.Location = new System.Drawing.Point(70, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "工时";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.label4.Location = new System.Drawing.Point(52, 160);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "进度(%)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.label5.Location = new System.Drawing.Point(46, 195);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "工作详情";
            // 
            // cboProject
            // 
            this.cboProject.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cboProject.CanEdit = false;
            this.cboProject.Location = new System.Drawing.Point(105, 29);
            this.cboProject.MaxItemHeight = 0;
            this.cboProject.Name = "cboProject";
            this.cboProject.Padding = new System.Windows.Forms.Padding(1);
            this.cboProject.PopupHeight = 100;
            this.cboProject.PopupSize = new System.Drawing.Size(0, 0);
            this.cboProject.Size = new System.Drawing.Size(261, 26);
            this.cboProject.TabIndex = 2;
            // 
            // cboTask
            // 
            this.cboTask.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cboTask.CanEdit = false;
            this.cboTask.Location = new System.Drawing.Point(105, 69);
            this.cboTask.MaxItemHeight = 0;
            this.cboTask.Name = "cboTask";
            this.cboTask.Padding = new System.Windows.Forms.Padding(1);
            this.cboTask.PopupHeight = 300;
            this.cboTask.PopupSize = new System.Drawing.Size(0, 0);
            this.cboTask.Size = new System.Drawing.Size(261, 26);
            this.cboTask.TabIndex = 4;
            // 
            // txtDetail
            // 
            this.txtDetail.Location = new System.Drawing.Point(105, 195);
            this.txtDetail.Name = "txtDetail";
            this.txtDetail.Padding = new System.Windows.Forms.Padding(1);
            this.txtDetail.Size = new System.Drawing.Size(261, 202);
            this.txtDetail.TabIndex = 10;
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
            this.btnCancel.Location = new System.Drawing.Point(139, 16);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 27);
            this.btnCancel.TabIndex = 1;
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
            this.btnConfirm.Location = new System.Drawing.Point(43, 16);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(90, 27);
            this.btnConfirm.TabIndex = 0;
            this.btnConfirm.TabStop = true;
            this.btnConfirm.Text = "添加";
            this.btnConfirm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnConfirm);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(1, 444);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(407, 58);
            this.panel1.TabIndex = 17;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.cboProject);
            this.panel2.Controls.Add(this.txtRate);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.cboTask);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.txtDetail);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.txtWorkHour);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(1, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(407, 443);
            this.panel2.TabIndex = 0;
            // 
            // txtRate
            // 
            this.txtRate.BackColor = System.Drawing.Color.White;
            this.txtRate.Location = new System.Drawing.Point(105, 153);
            this.txtRate.MaxLength = 3;
            this.txtRate.Name = "txtRate";
            this.txtRate.Size = new System.Drawing.Size(261, 26);
            this.txtRate.TabIndex = 8;
            this.txtRate.Type = ControlEx.TextType.Interger;
            // 
            // txtWorkHour
            // 
            this.txtWorkHour.BackColor = System.Drawing.Color.White;
            this.txtWorkHour.Location = new System.Drawing.Point(105, 111);
            this.txtWorkHour.Name = "txtWorkHour";
            this.txtWorkHour.Size = new System.Drawing.Size(261, 26);
            this.txtWorkHour.TabIndex = 6;
            // 
            // FormAddLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 529);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormAddLog";
            this.ShowInTaskbar = false;
            this.Text = "添加日志";
            this.Controls.SetChildIndex(this.pnlContainer, 0);
            this.pnlContainer.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private ControlEx.ComboBoxBase cboProject;
        private ControlEx.ComboBoxBase cboTask;
        private ControlEx.RichTextBoxBase txtDetail;
        private ControlEx.ButtonBase btnCancel;
        private ControlEx.ButtonBase btnConfirm;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private ControlEx.TextBoxTime txtWorkHour;
        private ControlEx.TextBoxBase txtRate;
    }
}