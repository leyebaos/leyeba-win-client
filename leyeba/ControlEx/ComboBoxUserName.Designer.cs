namespace ControlEx
{
    partial class ComboBoxUserName
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
            this.pnlDropDown = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBox = new System.Windows.Forms.TextBox();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlDropDown
            // 
            this.pnlDropDown.BackColor = System.Drawing.Color.Transparent;
            this.pnlDropDown.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlDropDown.Location = new System.Drawing.Point(165, 0);
            this.pnlDropDown.Name = "pnlDropDown";
            this.pnlDropDown.Size = new System.Drawing.Size(25, 28);
            this.pnlDropDown.TabIndex = 0;
            this.pnlDropDown.Tag = "";
            this.pnlDropDown.Click += new System.EventHandler(this.pnlDropDown_Click);
            this.pnlDropDown.MouseEnter += new System.EventHandler(this.OnMouseEnter);
            this.pnlDropDown.MouseLeave += new System.EventHandler(this.OnMouseLeave);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.textBox);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(165, 28);
            this.panel2.TabIndex = 1;
            this.panel2.MouseEnter += new System.EventHandler(this.OnMouseEnter);
            this.panel2.MouseLeave += new System.EventHandler(this.OnMouseLeave);
            // 
            // textBox
            // 
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox.Location = new System.Drawing.Point(4, 7);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(156, 14);
            this.textBox.TabIndex = 0;
            this.textBox.MouseEnter += new System.EventHandler(this.OnMouseEnter);
            this.textBox.MouseLeave += new System.EventHandler(this.OnMouseLeave);
            // 
            // ComboBoxUserName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ControlEx.Properties.Resources.inputid_nor;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlDropDown);
            this.DoubleBuffered = true;
            this.Name = "ComboBoxUserName";
            this.Size = new System.Drawing.Size(190, 28);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlDropDown;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBox;

    }
}
