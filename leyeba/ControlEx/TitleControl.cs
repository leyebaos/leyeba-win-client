using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ControlEx
{
    public class TitleControl : Control
    {
        public TitleControl()
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
            this.ForeColor = Color.FromArgb(37, 100, 176);
            this.Font = new Font("宋体", this.Font.Size, FontStyle.Bold);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics gh = e.Graphics;
            gh.SmoothingMode = SmoothingMode.HighQuality;
            SizeF textSize = gh.MeasureString(Text, Font);
            gh.DrawString(Text, Font, new SolidBrush(ForeColor), new PointF(0, (Height - textSize.Height) / 2));
            Pen linePen = new Pen(Color.FromArgb(204, 204, 204));
            linePen.DashPattern = new float[] { 3, 4 };
            gh.DrawLine(linePen, new PointF(textSize.Width + 7, (Height - 1) / 2), new PointF(Width, (Height - 1) / 2));
        }
    }
}
