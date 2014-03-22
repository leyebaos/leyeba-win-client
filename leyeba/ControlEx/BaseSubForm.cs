using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ControlEx
{
    public partial class BaseSubForm : Form
    {
        private Color borderColor = Color.FromArgb(0, 44, 106);
        private Image topLeft = ControlEx.Properties.Resources.popupbox_topbg_left;
        private Image topCenter = ControlEx.Properties.Resources.popupbox_topbg_center;
        private Image topRight = ControlEx.Properties.Resources.popupbox_topbg_right;

        private Image bottomLeft = ControlEx.Properties.Resources.popupbox_footbg_left;
        private Image bottomCenter = ControlEx.Properties.Resources.popupbox_footbg_center;
        private Image bottomRight = ControlEx.Properties.Resources.popupbox_footbg_right;


        public BaseSubForm()
        {
            InitializeComponent();
            settingRegion();
        }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                this.lblTitle.Text = value;
                base.Text = value;
            }
        }

        private bool showFoot = true;
        public bool ShowFoot
        {
            get {
                return showFoot;
            }
            set {
                showFoot = value;
                this.Invalidate();
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

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            settingRegion();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics gh = e.Graphics;
            gh.SmoothingMode = SmoothingMode.HighQuality;
            Bitmap bmp = new Bitmap(this.Width - topLeft.Width * 2, topCenter.Height);
            gh.DrawImage(topLeft, new Point(0, 0));
            ImageAttributes imgAttr = new ImageAttributes();
            imgAttr.SetWrapMode(WrapMode.Tile);
            gh.DrawImage(bmp, new Point(topLeft.Width, 0));
            gh.DrawImage(topCenter, 
                new Rectangle(topLeft.Width, 0, this.Width - topLeft.Width * 2, topCenter.Height), 
                0, 
                0, 
                topCenter.Width, 
                topCenter.Height, 
                GraphicsUnit.Pixel, 
                imgAttr);
            gh.DrawImage(topRight, new Point(this.Width - topRight.Width, 0));
            if (showFoot)
            {
                gh.DrawImage(bottomLeft, new Point(0, this.Height - bottomLeft.Height));
                gh.DrawImage(
                    bottomCenter,
                    new Rectangle(bottomLeft.Width, this.Height - bottomCenter.Height, this.Width - topLeft.Width * 2, bottomCenter.Height),
                    0,
                    0,
                    bottomCenter.Width,
                    bottomCenter.Height,
                    GraphicsUnit.Pixel,
                    imgAttr);
                gh.DrawImage(bottomRight, new Point(this.Width - bottomRight.Width, this.Height - bottomRight.Height));
            }
            Rectangle rect = ClientRectangle;
            rect.Width--;
            rect.Height--;
            using (Pen pen = new Pen(borderColor))
            {
                using (GraphicsPath path = CreatePath(rect))
                {
                    gh.DrawPath(
                        pen,
                        path);
                }
            }
        }

        private void settingRegion()
        {
            using (GraphicsPath path = CreatePath(ClientRectangle))
            {
                Region = new Region(path);
            }
        }

        private GraphicsPath CreatePath(Rectangle rect)
        {
            GraphicsPath path = new GraphicsPath();

            path.AddArc(rect.X, rect.Y, 4, 4, 180, 90);
            path.AddArc(rect.Right - 4, rect.Y, 4, 4, 270, 90);
            path.AddArc(rect.Right - 4, rect.Bottom - 4, 4, 4, 0, 90);
            path.AddArc(rect.X, rect.Bottom - 4, 4, 4, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            Panel pnl = sender as Panel;
            if (pnl == null) return;
            Graphics gh = e.Graphics;
            gh.SmoothingMode = SmoothingMode.HighQuality;
            Pen linePen = new Pen(borderColor);
            gh.DrawLine(linePen, new Point(0, 0), new Point(0, pnl.Height));
            gh.DrawLine(linePen, new Point(pnl.Width - 1, 0), new Point(pnl.Width - 1, pnl.Height));
        }
    }
}
