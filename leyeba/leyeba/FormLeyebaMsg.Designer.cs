namespace leyeba
{
    partial class FormLeyebaMsg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLeyebaMsg));
            this.richTxtMessage = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnNext = new ControlEx.ButtonBase();
            this.btnStar = new ControlEx.ButtonBase();
            this.btnDelete = new ControlEx.ButtonBase();
            this.btnPrevious = new ControlEx.ButtonBase();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlContainer.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.panel2);
            this.pnlContainer.Controls.Add(this.panel1);
            this.pnlContainer.Size = new System.Drawing.Size(356, 216);
            // 
            // richTxtMessage
            // 
            this.richTxtMessage.BackColor = System.Drawing.Color.White;
            this.richTxtMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTxtMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTxtMessage.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.richTxtMessage.Location = new System.Drawing.Point(0, 0);
            this.richTxtMessage.Name = "richTxtMessage";
            this.richTxtMessage.ReadOnly = true;
            this.richTxtMessage.Size = new System.Drawing.Size(354, 155);
            this.richTxtMessage.TabIndex = 0;
            this.richTxtMessage.Text = "";
            this.richTxtMessage.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richTxtMessage_LinkClicked);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.btnNext);
            this.panel1.Controls.Add(this.btnStar);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnPrevious);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(1, 156);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(354, 59);
            this.panel1.TabIndex = 4;
            // 
            // btnNext
            // 
            this.btnNext.ActiveLinkColor = System.Drawing.Color.White;
            this.btnNext.BackColor = System.Drawing.Color.Transparent;
            this.btnNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNext.ForeColor = System.Drawing.Color.Black;
            this.btnNext.ImageHover = global::leyeba.Properties.Resources.btn_gray_bg_hot;
            this.btnNext.ImageNormal = global::leyeba.Properties.Resources.btn_gray_bg_nor;
            this.btnNext.ImagePress = global::leyeba.Properties.Resources.btn_gray_bg_down;
            this.btnNext.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.btnNext.LinkColor = System.Drawing.Color.Black;
            this.btnNext.Location = new System.Drawing.Point(261, 18);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 7;
            this.btnNext.TabStop = true;
            this.btnNext.Text = "下一条";
            this.btnNext.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnStar
            // 
            this.btnStar.ActiveLinkColor = System.Drawing.Color.White;
            this.btnStar.BackColor = System.Drawing.Color.Transparent;
            this.btnStar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnStar.ForeColor = System.Drawing.Color.Black;
            this.btnStar.ImageHover = global::leyeba.Properties.Resources.btn_gray_bg_hot;
            this.btnStar.ImageNormal = global::leyeba.Properties.Resources.btn_gray_bg_nor;
            this.btnStar.ImagePress = global::leyeba.Properties.Resources.btn_gray_bg_down;
            this.btnStar.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.btnStar.LinkColor = System.Drawing.Color.Black;
            this.btnStar.Location = new System.Drawing.Point(180, 18);
            this.btnStar.Name = "btnStar";
            this.btnStar.Size = new System.Drawing.Size(75, 23);
            this.btnStar.TabIndex = 6;
            this.btnStar.TabStop = true;
            this.btnStar.Text = "加星";
            this.btnStar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnStar.Click += new System.EventHandler(this.btnStar_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.ActiveLinkColor = System.Drawing.Color.White;
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.ForeColor = System.Drawing.Color.Black;
            this.btnDelete.ImageHover = global::leyeba.Properties.Resources.btn_gray_bg_hot;
            this.btnDelete.ImageNormal = global::leyeba.Properties.Resources.btn_gray_bg_nor;
            this.btnDelete.ImagePress = global::leyeba.Properties.Resources.btn_gray_bg_down;
            this.btnDelete.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.btnDelete.LinkColor = System.Drawing.Color.Black;
            this.btnDelete.Location = new System.Drawing.Point(99, 18);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.TabStop = true;
            this.btnDelete.Text = "删除";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.ActiveLinkColor = System.Drawing.Color.White;
            this.btnPrevious.BackColor = System.Drawing.Color.Transparent;
            this.btnPrevious.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPrevious.ForeColor = System.Drawing.Color.Black;
            this.btnPrevious.ImageHover = global::leyeba.Properties.Resources.btn_gray_bg_hot;
            this.btnPrevious.ImageNormal = global::leyeba.Properties.Resources.btn_gray_bg_nor;
            this.btnPrevious.ImagePress = global::leyeba.Properties.Resources.btn_gray_bg_down;
            this.btnPrevious.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.btnPrevious.LinkColor = System.Drawing.Color.Black;
            this.btnPrevious.Location = new System.Drawing.Point(18, 18);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(75, 23);
            this.btnPrevious.TabIndex = 4;
            this.btnPrevious.TabStop = true;
            this.btnPrevious.Text = "上一条";
            this.btnPrevious.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.richTxtMessage);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(1, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(354, 155);
            this.panel2.TabIndex = 5;
            // 
            // FormLeyebaMsg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(356, 242);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormLeyebaMsg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "消息（0）";
            this.TopMost = true;
            this.pnlContainer.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTxtMessage;
        private System.Windows.Forms.Panel panel1;
        private ControlEx.ButtonBase btnPrevious;
        private ControlEx.ButtonBase btnDelete;
        private ControlEx.ButtonBase btnStar;
        private ControlEx.ButtonBase btnNext;
        private System.Windows.Forms.Panel panel2;

    }
}