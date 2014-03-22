using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AutoUpdate
{
    public class ButtonBase : Label
    {
        public ButtonBase()
            : base()
        {
            base.AutoSize = false;
            this.BackColor = Color.Transparent;
            this.BackgroundImage = imgNormal;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.ForeColor = Color.White;
            this.Size = new System.Drawing.Size(90, 27);
            this.TextAlign = ContentAlignment.MiddleCenter;
        }

        private Image imgNormal = AutoUpdate.Properties.Resources.btn_close_nor;

        public Image ImageNormal
        {
            get { return imgNormal; }
            set { 
                imgNormal = value;
                this.BackgroundImage = value;
            }
        }

        private Image imgHover = AutoUpdate.Properties.Resources.btn_close_hot;

        public Image ImageHover
        {
            get { return imgHover; }
            set { imgHover = value; }
        }

        private Image imgPreess = AutoUpdate.Properties.Resources.btn_close_down;

        public Image ImagePress
        {
            get { return imgPreess; }
            set { imgPreess = value; }
        }
        
        
        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            if (mevent.Button != MouseButtons.Left) 
                return;
            base.OnMouseDown(mevent);
            if (imgPreess == null) 
                return;
            this.BackgroundImage = imgPreess;
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            if (mevent.Button != MouseButtons.Left)
                return;
            base.OnMouseUp(mevent);
            this.BackgroundImage = imgNormal;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            if (imgHover == null) 
                return;
            this.BackgroundImage = imgHover;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.BackgroundImage = imgNormal;
        }

        protected override void OnClick(EventArgs e)
        {
            MouseEventArgs args = e as MouseEventArgs;
            if (args == null ||
                args.Button != MouseButtons.Left)
                return;
            this.Select();
            base.OnClick(e);
        }

        [Browsable(false)]
        public override bool AutoSize
        {
            get
            {
                return false;
            }
        }
    }
}
