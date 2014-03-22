using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ControlEx
{
    public partial class ContextMenuBase : UserControl
    {
        public ContextMenuBase()
        {
            InitializeComponent();
            //settingRegion();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (Graphics gh = e.Graphics)
            {
                gh.SmoothingMode = SmoothingMode.HighQuality;
                gh.FillRectangle(new SolidBrush(Color.FromArgb(227, 235, 248)), new Rectangle(0, 0, 27, this.Height - 1));
                gh.DrawLine(new Pen(Color.FromArgb(184, 203, 233)), new Point(27, 0), new Point(27, this.Height));
                gh.DrawRectangle(new Pen(Color.FromArgb(149, 149, 149)), new Rectangle(0, 0, this.Width - 1, this.Height - 1));
                //using (GraphicsPath path = CreatePath(new Rectangle(0, 0, this.Width - 1, this.Height - 1)))
                //{
                //    e.Graphics.DrawPath(
                //        new Pen(Color.FromArgb(149, 149, 149)),
                //        path);
                //}
            }
        }

        //private void settingRegion()
        //{
        //    using (GraphicsPath path = CreatePath(ClientRectangle))
        //    {
        //        Region = new Region(path);
        //    }
        //}

        //private GraphicsPath CreatePath(Rectangle rect)
        //{
        //    GraphicsPath path = new GraphicsPath();

        //    path.AddArc(rect.X, rect.Y, 3, 3, 180, 90);            
        //    path.AddArc(rect.Right - 9, rect.Y, 3, 3, 270, 90);
        //    path.AddArc(rect.Right - 9, rect.Bottom - 9, 3, 3, 0, 90);
        //    path.AddArc(rect.X, rect.Bottom - 9, 3, 3, 90, 90);
        //    path.CloseFigure();
        //    return path;
        //}
    }
}
