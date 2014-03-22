namespace ControlEx
{
    partial class ComboBoxProject
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlDropDown = new System.Windows.Forms.Panel();
            this.pnlText = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // pnlDropDown
            // 
            this.pnlDropDown.BackgroundImage = global::ControlEx.Properties.Resources.arrow;
            this.pnlDropDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnlDropDown.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlDropDown.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlDropDown.Location = new System.Drawing.Point(121, 0);
            this.pnlDropDown.Name = "pnlDropDown";
            this.pnlDropDown.Size = new System.Drawing.Size(28, 30);
            this.pnlDropDown.TabIndex = 0;
            this.pnlDropDown.Click += new System.EventHandler(this.OnDropDownClick);
            // 
            // pnlText
            // 
            this.pnlText.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlText.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pnlText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(106)))), ((int)(((byte)(189)))));
            this.pnlText.Location = new System.Drawing.Point(0, 0);
            this.pnlText.Name = "pnlText";
            this.pnlText.Size = new System.Drawing.Size(121, 30);
            this.pnlText.TabIndex = 1;
            this.pnlText.Click += new System.EventHandler(this.OnDropDownClick);
            this.pnlText.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlText_Paint);
            // 
            // ComboBoxProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlText);
            this.Controls.Add(this.pnlDropDown);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "ComboBoxProject";
            this.Size = new System.Drawing.Size(149, 30);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlDropDown;
        private System.Windows.Forms.Panel pnlText;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
