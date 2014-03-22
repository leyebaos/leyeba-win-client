using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ControlEx
{
    public class CheckBoxBase : Control
    {
        public event EventHandler CheckedChanged;

        public CheckBoxBase()
            : base()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                ControlStyles.DoubleBuffer |
                ControlStyles.OptimizedDoubleBuffer, true);               //双缓冲防止重绘时闪烁
            SetStyle(ControlStyles.UserPaint, true);                      //自定义绘制控件内容
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);   //模拟透明
            SetStyle(ControlStyles.Selectable, true);                     //接收焦点
            SetStyle(ControlStyles.ResizeRedraw, true);
            BackColor = Color.Transparent;
        }

        #region//属性
        private Image imgNormal = ControlEx.Properties.Resources.choose_nor_login;
        public Image ImageNormal 
        {
            get {
                return imgNormal;
            }
            set {
                imgNormal = value;
                this.Refresh();
            }
        }

        private Image imgChecked = ControlEx.Properties.Resources.choose_checked_login;
        public Image ImgeChecked
        {
            get {
                return imgChecked;
            }
            set {
                imgChecked = value;
                this.Refresh();
            }
        }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                this.Refresh();
            }
        }

        private bool isChecked = false;
        public bool Checked 
        {
            get {
                return isChecked;
            }
            set {
                isChecked = value;
                OnCheckedChanged(EventArgs.Empty);
                this.Refresh();
            }
        }
        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics gh = e.Graphics;
            gh.SmoothingMode = SmoothingMode.HighQuality;
            Image img = null;
            if (this.isChecked)
                img = this.ImgeChecked;
            else
                img = this.ImageNormal;
            if (img == null) return;
            gh.DrawImage(
                img,
                new Point(0, (Height - img.Height) / 2));
            SizeF textSize = gh.MeasureString(Text, Font);
            gh.DrawString(
                this.Text, 
                this.Font, 
                new SolidBrush(this.ForeColor),
                new Point(img.Width + 1, (Height - (int)textSize.Height) / 2 + 1));
        }

        protected override void OnClick(EventArgs e)
        {
            MouseEventArgs m = (MouseEventArgs)e;
            if (m.Button == MouseButtons.Left)
            {
                base.OnClick(e);
                this.Checked = !this.Checked;
                this.Refresh();
                this.Select();
            }
        }

        protected virtual void OnCheckedChanged(EventArgs e)
        {
            if (this.CheckedChanged == null) return;
            CheckedChanged(this, e);
        }
    }
}
