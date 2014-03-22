namespace ControlEx
{
    partial class TextBoxPassword
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
            this.textBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.BackColor = System.Drawing.Color.White;
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox.ForeColor = System.Drawing.Color.Gray;
            this.textBox.Location = new System.Drawing.Point(3, 6);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(182, 14);
            this.textBox.TabIndex = 0;
            this.textBox.Text = "密码";
            this.textBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.textBox.Enter += new System.EventHandler(this.textBox_Enter);
            this.textBox.Leave += new System.EventHandler(this.textBox_Leave);
            this.textBox.MouseEnter += new System.EventHandler(this.OnMouseEnter);
            this.textBox.MouseLeave += new System.EventHandler(this.OnMouseLeave);
            // 
            // TextBoxPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImage = global::ControlEx.Properties.Resources.inputpassword_nor;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.textBox);
            this.DoubleBuffered = true;
            this.MaximumSize = new System.Drawing.Size(188, 26);
            this.MinimumSize = new System.Drawing.Size(188, 26);
            this.Name = "TextBoxPassword";
            this.Size = new System.Drawing.Size(188, 26);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox;
    }
}
