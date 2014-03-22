using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Util;

namespace ControlEx
{
    public partial class PromptBox : Form
    {
        public PromptBox()
        {
            InitializeComponent();
            settingNoneTransparencyRegion();
        }

        public PromptBox(PromptBoxIcon icon, string text, string caption)
            : this()
        {
            this.Text = caption;
            this.lblTitle.Text = caption;
            this.lblContent.Text = text;
            switch (icon)
            {
                case PromptBoxIcon.Information:
                    this.picIcon.Image = ControlEx.Properties.Resources.prompt_info;
                    this.btnConfirm.Location = new Point(168, 168);
                    this.btnCancel.Visible = false;
                    break;
                case PromptBoxIcon.Question:
                    this.picIcon.Image = ControlEx.Properties.Resources.prompt_qustion;
                    this.btnConfirm.Location = new Point(72, 168);
                    this.btnCancel.Visible = true;
                    break;
            }
        }

        #region//移动窗体
        private int isDownX = 0;
        private int isDownY = 0;
        private bool isMoseDown = false;
        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left && !isMoseDown)
            {
                isDownX = e.X;
                isDownY = e.Y;
                isMoseDown = true;
            }
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (isMoseDown)
            {
                this.Location = new Point(Left + (e.X - isDownX), Top + (e.Y - isDownY));
            }
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (isMoseDown)
            {
                isMoseDown = false;
                isDownX = 0;
                isDownY = 0;
            }
        }
        #endregion

        private void settingNoneTransparencyRegion()
        {
            //设置非透明区域
            Bitmap img = (Bitmap)this.BackgroundImage;
            GraphicsPath grapth = BitmapHandler.GetNoneTransparencyRegion(img, 10);
            this.Region = new Region(grapth);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        public static void Alert(string text, string caption)
        {
            PromptBox prompt = new PromptBox(PromptBoxIcon.Information, text, caption);
            prompt.ShowDialog();
        }

        public static DialogResult Question(string text, string caption)
        {
            PromptBox prompt = new PromptBox(PromptBoxIcon.Question, text, caption);
            return prompt.ShowDialog();
        }

        public static DialogResult Question(string text, string caption, FormStartPosition pos)
        {
            PromptBox prompt = new PromptBox(PromptBoxIcon.Question, text, caption);
            prompt.StartPosition = pos;
            return prompt.ShowDialog();
        }
    }

    public enum PromptBoxIcon
    {
        // 摘要:
        //     消息框未包含符号。
        None = 0,
        //
        // 摘要:
        //     该消息框包含一个符号，该符号是由一个红色背景的圆圈及其中的白色 X 组成的。
        Error = 16,
        //
        // 摘要:
        //     该消息框包含一个符号，该符号是由一个红色背景的圆圈及其中的白色 X 组成的。
        Hand = 16,
        //
        // 摘要:
        //     该消息框包含一个符号，该符号是由一个红色背景的圆圈及其中的白色 X 组成的。
        Stop = 16,
        //
        // 摘要:
        //     该消息框包含一个符号，该符号是由一个圆圈和其中的一个问号组成的。不再建议使用问号消息图标，原因是该图标无法清楚地表示特定类型的消息，并且问号形式的消息表述可应用于任何消息类型。此外，用户还可能将问号消息符号与帮助信息混淆。因此，请不要在消息框中使用此问号消息符号。系统继续支持此符号只是为了向后兼容。
        Question = 32,
        //
        // 摘要:
        //     该消息框包含一个符号，该符号是由一个黄色背景的三角形及其中的一个感叹号组成的。
        Exclamation = 48,
        //
        // 摘要:
        //     该消息框包含一个符号，该符号是由一个黄色背景的三角形及其中的一个感叹号组成的。
        Warning = 48,
        //
        // 摘要:
        //     该消息框包含一个符号，该符号是由一个圆圈及其中的小写字母 i 组成的。
        Information = 64,
        //
        // 摘要:
        //     该消息框包含一个符号，该符号是由一个圆圈及其中的小写字母 i 组成的。
        Asterisk = 64,
    }
}
