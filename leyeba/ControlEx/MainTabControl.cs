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
    public partial class MainTabControl : Form
    {
        private Size defaultSize = new Size(50, 56);
        private Image drawImage = null;

        public MainTabControl()
        {
            InitializeComponent();
            this.Cursor = Cursors.Hand;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            settingNoneTransparencyRegion();
        }

        private void settingNoneTransparencyRegion()
        {
            if (!this.IsHandleCreated)
                return;
            BitmapHandler.SetBits(
                getDisplayBitmap(),
                this.Location,
                this.Handle);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.ClientSize = defaultSize;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cParms = base.CreateParams;
                cParms.ExStyle |= 0x00080000; // WS_EX_LAYERED
                return cParms;
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            UpdateStyles();
        }

        private bool isChecked = false;
        [Browsable(false)]
        public bool Checked
        {
            get { return isChecked; }
            set
            {
                isChecked = value;
                settingNoneTransparencyRegion();
            }
        }   

        private Image imgNormal = ControlEx.Properties.Resources.menu1_nor;

        public Image ImageNormal
        {
            get { return imgNormal; }
            set
            {
                imgNormal = value;
                drawImage = value;
                this.Refresh();
            }
        }

        private Image imgChecked = ControlEx.Properties.Resources.menu1_on;

        public Image ImageChecked
        {
            get { return imgChecked; }
            set
            {
                imgChecked = value;
                drawImage = value;
                this.Refresh();
            }
        }

        private Bitmap getDisplayBitmap()
        {
            Bitmap bmp = new Bitmap(ClientSize.Width, ClientSize.Height);
            Graphics gh = Graphics.FromImage(bmp);
            gh.SmoothingMode = SmoothingMode.HighQuality;
            if (isChecked)
            {
                gh.DrawImage(ControlEx.Properties.Resources.menu_on_bg, new Rectangle(0, 0, this.Width, this.Height));
                drawImage = imgChecked;
                this.ForeColor = Color.FromArgb(45, 106, 189);
            }
            else
            {
                drawImage = imgNormal;
                this.ForeColor = Color.White;
            }
            gh.DrawImage(drawImage, new Point(19, 10));
            gh.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), new PointF(14, 30));

            return bmp;
        }
    }
}
