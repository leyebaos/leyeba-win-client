using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ControlEx
{
    public class ButtonIcon : Control
    {
        ///<summary>
        /// 控件状态
        ///</summary>
        public enum State
        {
            ///<summary>
            /// 无
            ///</summary>
            Normal = 0,
            ///<summary>
            /// 获得焦点
            ///</summary>
            Focused = 1,
            ///<summary>
            /// 失去焦点
            ///</summary>
            LostFocused = 2,
            ///<summary>
            /// 鼠标指针进入控件
            ///</summary>
            MouseEnter = 3
        }

        private Size imgSize = new Size(16, 16);

        public ButtonIcon()
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

        private Image img = ControlEx.Properties.Resources.icon_01;

        public Image Image
        {
            get { return img; }
            set { 
                img = value;
                this.Refresh();
            }
        }        

        private bool canChecked;

        public bool CanChecked
        {
            get { return canChecked; }
            set { canChecked = value; }
        }        

        private bool isChecked = false;
        public bool Checked 
        {
            get {
                return isChecked;
            }
            set {
                isChecked = value;
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

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics gh = e.Graphics;
            gh.SmoothingMode = SmoothingMode.HighQuality;
            Rectangle backRec = new Rectangle(0, 0, Width - 1, Height - 1);
            if (CanChecked && isChecked)
            {
                gh.DrawImage(ControlEx.Properties.Resources.btn01_press, backRec);
                gh.DrawRectangle(new Pen(Color.FromArgb(176, 194, 216)), backRec);
            }
            else if (this.BackgroundImage != null)
            {
                gh.DrawImage(this.BackgroundImage, backRec);
                gh.DrawRectangle(new Pen(Color.FromArgb(176, 194, 216)), backRec);
            }
            
            SizeF textSize = gh.MeasureString(this.Text, this.Font);
            int totalWidth = imgSize.Width + (int)textSize.Width + 5;
            Point contentPos = new Point((this.Width - totalWidth) / 2, (this.Height - (int)imgSize.Height) / 2);
            Rectangle imgRec = new Rectangle(contentPos, imgSize);
            if (img != null)
                gh.DrawImage(img, imgRec);
            PointF textPos =
                new PointF(
                    contentPos.X + imgSize.Width + 5,
                    (this.Height - textSize.Height) / 2);
            gh.DrawString(
                this.Text,
                this.Font,
                new SolidBrush(this.ForeColor),
                textPos);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            if (canChecked && isChecked) return;
            this.BackgroundImage = ControlEx.Properties.Resources.btn01_on;
            this.Refresh();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.BackgroundImage = null;
            this.Refresh();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            this.BackgroundImage = null;
            this.Refresh();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button != MouseButtons.Left)
                return;
            if (canChecked && isChecked)
                this.BackgroundImage = ControlEx.Properties.Resources.btn01_press;
            this.Refresh();
        }
    }
}
