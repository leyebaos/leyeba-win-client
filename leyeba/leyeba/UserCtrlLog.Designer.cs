namespace leyeba
{
    partial class UserCtrlLog
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSubmitLog = new ControlEx.ButtonIcon();
            this.btnAddLog = new ControlEx.ButtonIcon();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.idColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.projNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.workHourColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.detailColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.delColumn = new System.Windows.Forms.DataGridViewLinkColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.btnSubmitLog);
            this.panel1.Controls.Add(this.btnAddLog);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(622, 80);
            this.panel1.TabIndex = 0;
            // 
            // btnSubmitLog
            // 
            this.btnSubmitLog.BackColor = System.Drawing.Color.Transparent;
            this.btnSubmitLog.CanChecked = false;
            this.btnSubmitLog.Checked = false;
            this.btnSubmitLog.Image = global::leyeba.Properties.Resources.icon_submit;
            this.btnSubmitLog.Location = new System.Drawing.Point(209, 48);
            this.btnSubmitLog.Name = "btnSubmitLog";
            this.btnSubmitLog.Size = new System.Drawing.Size(56, 21);
            this.btnSubmitLog.TabIndex = 5;
            this.btnSubmitLog.Text = "提交";
            this.btnSubmitLog.Click += new System.EventHandler(this.btnSubmitLog_Click);
            // 
            // btnAddLog
            // 
            this.btnAddLog.BackColor = System.Drawing.Color.Transparent;
            this.btnAddLog.CanChecked = false;
            this.btnAddLog.Checked = false;
            this.btnAddLog.Image = global::leyeba.Properties.Resources.icon_add;
            this.btnAddLog.Location = new System.Drawing.Point(148, 48);
            this.btnAddLog.Name = "btnAddLog";
            this.btnAddLog.Size = new System.Drawing.Size(55, 21);
            this.btnAddLog.TabIndex = 4;
            this.btnAddLog.Text = "添加";
            this.btnAddLog.Click += new System.EventHandler(this.btnAddLog_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(106)))), ((int)(((byte)(189)))));
            this.label1.Location = new System.Drawing.Point(10, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 31);
            this.label1.TabIndex = 3;
            this.label1.Text = "开发日志";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(16, 48);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(126, 21);
            this.dateTimePicker1.TabIndex = 0;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::leyeba.Properties.Resources.main_footbg_center;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 392);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(622, 42);
            this.panel2.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.CausesValidation = false;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(251)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeight = 20;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idColumn,
            this.projNameColumn,
            this.taskNameColumn,
            this.workHourColumn,
            this.rateColumn,
            this.detailColumn,
            this.delColumn});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(251)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.RowHeadersWidth = 5;
            this.dataGridView1.RowTemplate.Height = 20;
            this.dataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.Size = new System.Drawing.Size(622, 312);
            this.dataGridView1.TabIndex = 0;
            // 
            // idColumn
            // 
            this.idColumn.FillWeight = 50F;
            this.idColumn.HeaderText = "编号";
            this.idColumn.Name = "idColumn";
            this.idColumn.ReadOnly = true;
            this.idColumn.Width = 45;
            // 
            // projNameColumn
            // 
            this.projNameColumn.HeaderText = "项目名称";
            this.projNameColumn.Name = "projNameColumn";
            this.projNameColumn.ReadOnly = true;
            this.projNameColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.projNameColumn.Width = 150;
            // 
            // taskNameColumn
            // 
            this.taskNameColumn.FillWeight = 87.05584F;
            this.taskNameColumn.HeaderText = "任务名称";
            this.taskNameColumn.Name = "taskNameColumn";
            this.taskNameColumn.ReadOnly = true;
            this.taskNameColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.taskNameColumn.Width = 150;
            // 
            // workHourColumn
            // 
            this.workHourColumn.FillWeight = 87.05584F;
            this.workHourColumn.HeaderText = "工时";
            this.workHourColumn.Name = "workHourColumn";
            this.workHourColumn.Width = 50;
            // 
            // rateColumn
            // 
            this.rateColumn.FillWeight = 87.05584F;
            this.rateColumn.HeaderText = "完成（%）";
            this.rateColumn.Name = "rateColumn";
            this.rateColumn.Width = 65;
            // 
            // detailColumn
            // 
            this.detailColumn.FillWeight = 87.05584F;
            this.detailColumn.HeaderText = "工作详情";
            this.detailColumn.Name = "detailColumn";
            // 
            // delColumn
            // 
            this.delColumn.FillWeight = 87.05584F;
            this.delColumn.HeaderText = "操作";
            this.delColumn.Name = "delColumn";
            this.delColumn.ReadOnly = true;
            this.delColumn.Width = 40;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dataGridView1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 80);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(622, 312);
            this.panel3.TabIndex = 2;
            // 
            // UserCtrlLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "UserCtrlLog";
            this.Size = new System.Drawing.Size(622, 434);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private ControlEx.ButtonIcon btnAddLog;
        private ControlEx.ButtonIcon btnSubmitLog;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridViewTextBoxColumn idColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn projNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn workHourColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn detailColumn;
        private System.Windows.Forms.DataGridViewLinkColumn delColumn;
    }
}