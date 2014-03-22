using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ControlEx
{
    public class ButtonBase : LinkLabel
    {
        public ButtonBase()
            : base()
        {
            base.AutoSize = false;
            this.BackColor = Color.Transparent;
            this.BackgroundImage = imgNormal;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.ForeColor = Color.White;
            this.ActiveLinkColor = this.ForeColor;
            this.LinkColor = this.ForeColor;
            this.LinkVisited = false;
            this.LinkBehavior = LinkBehavior.NeverUnderline;            
            this.Size = new System.Drawing.Size(90, 27);
            this.TextAlign = ContentAlignment.MiddleCenter;
        }        

        private Image imgNormal = ControlEx.Properties.Resources.btn_blue_bg_nor;

        public Image ImageNormal
        {
            get { return imgNormal; }
            set { 
                imgNormal = value;
                this.BackgroundImage = value;
            }
        }

        private Image imgHover = ControlEx.Properties.Resources.btn_blue_bg_hot;

        public Image ImageHover
        {
            get { return imgHover; }
            set { imgHover = value; }
        }

        private Image imgPreess = ControlEx.Properties.Resources.btn_blue_bg_down;

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
