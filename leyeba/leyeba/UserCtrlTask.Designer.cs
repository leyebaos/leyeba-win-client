namespace leyeba
{
    partial class UserCtrlTask
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
            if (workTimer != null)
            {
                workTimer.Tick -= workTimer_Tick;
                workTimer.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserCtrlTask));
            this.panel3 = new System.Windows.Forms.Panel();
            this.listViewEx1 = new ControlEx.ListViewEx();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnReload = new ControlEx.ButtonBase();
            this.progressBarProject1 = new ControlEx.ProgressBarBase();
            this.cboProject = new ControlEx.ComboBoxProject();
            this.btnEndWork = new ControlEx.ButtonIcon();
            this.btnWork = new ControlEx.ButtonIcon();
            this.picProgress = new System.Windows.Forms.PictureBox();
            this.lblPercent = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProgress)).BeginInit();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.listViewEx1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 115);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(368, 319);
            this.panel3.TabIndex = 2;
            // 
            // listViewEx1
            // 
            this.listViewEx1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewEx1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader1});
            this.listViewEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewEx1.FullRowSelect = true;
            this.listViewEx1.GridLines = true;
            this.listViewEx1.HideSelection = false;
            this.listViewEx1.Location = new System.Drawing.Point(0, 0);
            this.listViewEx1.MultiSelect = false;
            this.listViewEx1.Name = "listViewEx1";
            this.listViewEx1.ShowItemToolTips = true;
            this.listViewEx1.Size = new System.Drawing.Size(368, 319);
            this.listViewEx1.TabIndex = 0;
            this.listViewEx1.UseCompatibleStateImageBehavior = false;
            this.listViewEx1.View = System.Windows.Forms.View.Details;
            this.listViewEx1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewEx1_ColumnClick);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "编号";
            this.columnHeader2.Width = 39;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "任务名称";
            this.columnHeader3.Width = 172;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "完成(%)";
            this.columnHeader4.Width = 57;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "计时";
            this.columnHeader1.Width = 78;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::leyeba.Properties.Resources.main_footbg_center;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 434);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(368, 42);
            this.panel2.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::leyeba.Properties.Resources.main_topbg_center;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.btnReload);
            this.panel1.Controls.Add(this.progressBarProject1);
            this.panel1.Controls.Add(this.cboProject);
            this.panel1.Controls.Add(this.btnEndWork);
            this.panel1.Controls.Add(this.btnWork);
            this.panel1.Controls.Add(this.picProgress);
            this.panel1.Controls.Add(this.lblPercent);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(368, 115);
            this.panel1.TabIndex = 0;
            // 
            // btnReload
            // 
            this.btnReload.BackColor = System.Drawing.Color.Transparent;
            this.btnReload.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReload.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReload.ForeColor = System.Drawing.Color.White;
            this.btnReload.ImageHover = global::leyeba.Properties.Resources.reload;
            this.btnReload.ImageNormal = global::leyeba.Properties.Resources.reload;
            this.btnReload.ImagePress = global::leyeba.Properties.Resources.reload;
            this.btnReload.Location = new System.Drawing.Point(317, 18);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(16, 16);
            this.btnReload.TabIndex = 12;
            this.btnReload.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // progressBarProject1
            // 
            this.progressBarProject1.Location = new System.Drawing.Point(128, 65);
            this.progressBarProject1.Name = "progressBarProject1";
            this.progressBarProject1.Size = new System.Drawing.Size(221, 9);
            this.progressBarProject1.TabIndex = 11;
            this.progressBarProject1.Value = 0F;
            // 
            // cboProject
            // 
            this.cboProject.DisplayMember = null;
            this.cboProject.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cboProject.Location = new System.Drawing.Point(126, 12);
            this.cboProject.MaxItemHeight = 80;
            this.cboProject.Name = "cboProject";
            this.cboProject.PopupHeight = 80;
            this.cboProject.Size = new System.Drawing.Size(185, 30);
            this.cboProject.TabIndex = 10;
            this.cboProject.ValueMember = null;
            this.cboProject.SelectedIndexChanged += new System.EventHandler(this.cboProject_SelectedIndexChanged);
            // 
            // btnEndWork
            // 
            this.btnEndWork.BackColor = System.Drawing.Color.Transparent;
            this.btnEndWork.CanChecked = false;
            this.btnEndWork.Checked = false;
            this.btnEndWork.Image = global::leyeba.Properties.Resources.icon_stop;
            this.btnEndWork.Location = new System.Drawing.Point(193, 81);
            this.btnEndWork.Name = "btnEndWork";
            this.btnEndWork.Size = new System.Drawing.Size(75, 27);
            this.btnEndWork.TabIndex = 9;
            this.btnEndWork.Text = "结束工作";
            this.btnEndWork.Click += new System.EventHandler(this.btnEndWork_Click);
            // 
            // btnWork
            // 
            this.btnWork.BackColor = System.Drawing.Color.Transparent;
            this.btnWork.CanChecked = true;
            this.btnWork.Checked = false;
            this.btnWork.Image = ((System.Drawing.Image)(resources.GetObject("btnWork.Image")));
            this.btnWork.Location = new System.Drawing.Point(128, 81);
            this.btnWork.Name = "btnWork";
            this.btnWork.Size = new System.Drawing.Size(59, 27);
            this.btnWork.TabIndex = 8;
            this.btnWork.Text = "开始";
            this.btnWork.Click += new System.EventHandler(this.btnWork_Click);
            // 
            // picProgress
            // 
            this.picProgress.Image = global::leyeba.Properties.Resources.progress_ball_bg;
            this.picProgress.Location = new System.Drawing.Point(13, 7);
            this.picProgress.Name = "picProgress";
            this.picProgress.Size = new System.Drawing.Size(101, 101);
            this.picProgress.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picProgress.TabIndex = 7;
            this.picProgress.TabStop = false;
            // 
            // lblPercent
            // 
            this.lblPercent.AutoSize = true;
            this.lblPercent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(106)))), ((int)(((byte)(189)))));
            this.lblPercent.Location = new System.Drawing.Point(332, 50);
            this.lblPercent.Name = "lblPercent";
            this.lblPercent.Size = new System.Drawing.Size(17, 12);
            this.lblPercent.TabIndex = 5;
            this.lblPercent.Text = "0%";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.label2.Location = new System.Drawing.Point(126, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "项目整体进度";
            // 
            // UserCtrlTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "UserCtrlTask";
            this.Size = new System.Drawing.Size(368, 476);
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProgress)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private ControlEx.ListViewEx listViewEx1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblPercent;
        private System.Windows.Forms.PictureBox picProgress;
        private ControlEx.ButtonIcon btnWork;
        private ControlEx.ButtonIcon btnEndWork;
        private ControlEx.ComboBoxProject cboProject;
        private ControlEx.ProgressBarBase progressBarProject1;
        private System.Windows.Forms.Panel panel3;
        private ControlEx.ButtonBase btnReload;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}