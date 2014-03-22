namespace ControlEx
{
    partial class ComboBoxBase
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
            this.pnlText = new System.Windows.Forms.Panel();
            this.textBox = new System.Windows.Forms.TextBox();
            this.pnlDropDown = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pnlText.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlText
            // 
            this.pnlText.BackColor = System.Drawing.Color.White;
            this.pnlText.Controls.Add(this.textBox);
            this.pnlText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlText.Location = new System.Drawing.Point(1, 1);
            this.pnlText.Name = "pnlText";
            this.pnlText.Size = new System.Drawing.Size(234, 24);
            this.pnlText.TabIndex = 3;
            // 
            // textBox
            // 
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox.Location = new System.Drawing.Point(5, 6);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(227, 14);
            this.textBox.TabIndex = 0;
            // 
            // pnlDropDown
            // 
            this.pnlDropDown.BackColor = System.Drawing.Color.Transparent;
            this.pnlDropDown.BackgroundImage = global::ControlEx.Properties.Resources.arrow02;
            this.pnlDropDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnlDropDown.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlDropDown.Location = new System.Drawing.Point(235, 1);
            this.pnlDropDown.Name = "pnlDropDown";
            this.pnlDropDown.Size = new System.Drawing.Size(25, 24);
            this.pnlDropDown.TabIndex = 2;
            this.pnlDropDown.Tag = "0";
            // 
            // ComboBoxBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.pnlText);
            this.Controls.Add(this.pnlDropDown);
            this.DoubleBuffered = true;
            this.Name = "ComboBoxBase";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Size = new System.Drawing.Size(261, 26);
            this.pnlText.ResumeLayout(false);
            this.pnlText.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlText;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Panel pnlDropDown;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
