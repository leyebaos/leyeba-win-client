using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ControlEx
{
    public class ContextMenuItem : Control
    {
        private bool isMouseEnter = false;

        public ContextMenuItem()
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
            this.Size = new Size(190, 30);
        }

        public bool DrawSeparator { get; set; }

        public Image Icon { get; set; }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics gh = e.Graphics;
            gh.SmoothingMode = SmoothingMode.HighQuality;
            if (isMouseEnter)
            {
                int left = (this.Width - (this.Width - 4)) / 2;
                int top = 0, height = 0;
                if (DrawSeparator)
                {
                    height = this.Height - 5;
                    top = (this.Height - height) / 2 - 1;
                }
                else
                {
                    height = this.Height - 3;
                    top = (this.Height - height) / 2 + 1;
                }
                gh.FillRectangle(
                    new SolidBrush(Color.FromArgb(88, 145, 210)),
                    new Rectangle(left, top, this.Width - 4, height));
            }
            if (Icon != null)
            {
                int left = (27 - Icon.Width) / 2 + 2;
                int top = (this.Height - Icon.Height) / 2;
                gh.DrawImage(this.Icon, new Point(left, top));
            }
            SizeF textSize = gh.MeasureString(this.Text, this.Font);
            gh.DrawString(
                this.Text,
                this.Font,
                Brushes.Black,
                new PointF(
                    37,
                    (this.Height - textSize.Height) / 2)
                );
            if (DrawSeparator)
            {
                gh.DrawLine(
                    new Pen(Color.FromArgb(175, 198, 214)),
                    new Point(30, this.Height - 1),
                    new Point(this.Width - 3, this.Height - 1));
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            isMouseEnter = true;
            this.Refresh();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.isMouseEnter = false;
            this.Refresh();
        }
    }
}
