namespace ControlEx
{
    partial class TextBoxTime
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
            this.txtHour = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMinute = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtHour
            // 
            this.txtHour.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHour.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtHour.Location = new System.Drawing.Point(4, 6);
            this.txtHour.Name = "txtHour";
            this.txtHour.Size = new System.Drawing.Size(15, 14);
            this.txtHour.TabIndex = 2;
            this.txtHour.Text = "00";
            this.txtHour.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtHour_MouseClick);
            this.txtHour.TextChanged += new System.EventHandler(this.txtHour_TextChanged);
            this.txtHour.Enter += new System.EventHandler(this.txtHour_Enter);
            this.txtHour.Leave += new System.EventHandler(this.txtHour_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(17, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "：";
            // 
            // txtMinute
            // 
            this.txtMinute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMinute.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMinute.Location = new System.Drawing.Point(28, 6);
            this.txtMinute.Name = "txtMinute";
            this.txtMinute.Size = new System.Drawing.Size(15, 14);
            this.txtMinute.TabIndex = 4;
            this.txtMinute.Text = "00";
            this.txtMinute.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtMinute_MouseClick);
            this.txtMinute.TextChanged += new System.EventHandler(this.txtMinute_TextChanged);
            this.txtMinute.Enter += new System.EventHandler(this.txtMinute_Enter);
            this.txtMinute.Leave += new System.EventHandler(this.txtMinute_Leave);
            // 
            // TextBoxTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.txtMinute);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtHour);
            this.Name = "TextBoxTime";
            this.Size = new System.Drawing.Size(261, 26);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtHour;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMinute;

    }
}
