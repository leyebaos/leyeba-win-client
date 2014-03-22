using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ControlEx
{
    public class ProgressBarBase : Control
    { 
        public ProgressBarBase()
            : base()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                      ControlStyles.DoubleBuffer |
                      ControlStyles.OptimizedDoubleBuffer, true);         //双缓冲防止重绘时闪烁
            SetStyle(ControlStyles.UserPaint, true);                      //自定义绘制控件内容
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);   //模拟透明
            SetStyle(ControlStyles.Selectable, true);                     //接收焦点
            SetStyle(ControlStyles.ResizeRedraw, true);
        }
        private float progressValue = 0;

        public float Value 
        {
            get {
                return progressValue;
            }
            set {
                if (value == 0)
                    progressValue = 0;
                else
                    progressValue = (float)this.Width / (100f / (float)value);
                this.Refresh();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics gh = e.Graphics;
            gh.SmoothingMode = SmoothingMode.HighQuality;
            gh.FillRectangle(new SolidBrush(Color.FromArgb(184, 204, 233)), this.ClientRectangle);
            gh.DrawRectangle(new Pen(Color.FromArgb(164, 182, 207)), new Rectangle(1, 1, this.Width - 2, this.Height - 2));
            gh.FillRectangle(new SolidBrush(Color.FromArgb(255, 154, 9)), new RectangleF(0, 0, progressValue, this.Height - 1));
        }

        [Browsable(false)]
        public override string Text
        {
            get
            {
                return string.Empty;
            }
        }
    }
}
