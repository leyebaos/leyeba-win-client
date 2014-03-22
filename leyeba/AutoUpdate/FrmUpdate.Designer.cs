namespace AutoUpdate
{
    partial class FrmUpdate
    {
        private System.ComponentModel.Container components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUpdate));
            this.lblFinishProcess = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pbCurrentProgress = new AutoUpdate.ProgressBarBase();
            this.pnlContainer.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.panel2);
            this.pnlContainer.Size = new System.Drawing.Size(469, 112);
            // 
            // lblFinishProcess
            // 
            this.lblFinishProcess.AutoSize = true;
            this.lblFinishProcess.Location = new System.Drawing.Point(42, 37);
            this.lblFinishProcess.Name = "lblFinishProcess";
            this.lblFinishProcess.Size = new System.Drawing.Size(137, 12);
            this.lblFinishProcess.TabIndex = 7;
            this.lblFinishProcess.Text = "正在检查更新,请稍后...";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblFinishProcess);
            this.panel2.Controls.Add(this.pbCurrentProgress);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(1, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(467, 110);
            this.panel2.TabIndex = 9;
            // 
            // pbCurrentProgress
            // 
            this.pbCurrentProgress.Location = new System.Drawing.Point(42, 56);
            this.pbCurrentProgress.Name = "pbCurrentProgress";
            this.pbCurrentProgress.Size = new System.Drawing.Size(383, 17);
            this.pbCurrentProgress.TabIndex = 9;
            this.pbCurrentProgress.Value = 0F;
            // 
            // FrmUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(469, 138);
            this.ControlBox = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmUpdate";
            this.ShowFoot = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "自动更新";
            this.pnlContainer.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Panel panel2;
        private ProgressBarBase pbCurrentProgress;
        private System.Windows.Forms.Label lblFinishProcess;
    }
}
