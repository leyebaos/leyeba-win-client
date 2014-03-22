using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ControlEx
{
    public class LoginingControl : Control
    {
        private string text = "登录中...";
        private string displayText = "登录中...";

        private Timer timer = new Timer();

        public LoginingControl()
            : base()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                ControlStyles.DoubleBuffer |
                ControlStyles.OptimizedDoubleBuffer, true);               //双缓冲防止重绘时闪烁
            SetStyle(ControlStyles.UserPaint, true);                      //自定义绘制控件内容
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);   //模拟透明
            SetStyle(ControlStyles.Selectable, false);                    //不接收焦点
            SetStyle(ControlStyles.ResizeRedraw, true);
            BackColor = Color.Transparent;
            this.Enabled = false;
            this.ForeColor = Color.FromArgb(97, 97, 97);
            this.Font = new Font("微软雅黑", 18);
            timer.Interval = 500;
            timer.Tick += timer_Tick;
            timer.Enabled = false;
        }

        public new bool Enabled
        {
            get {
                return base.Enabled;
            }
            set {
                base.Enabled = value;
                if (this.Enabled)
                {
                    displayText = "登录中...";
                    timer.Start();
                }
                else
                {
                    displayText = "登录中";
                    timer.Stop();
                }
                this.Invalidate();
            }
        }

        private int counter = 3;
        void timer_Tick(object sender, EventArgs e)
        {
            switch (counter = ++counter % 4)
            {
                case 0:
                    displayText = "登录中";
                    break;
                case 1:
                    displayText = "登录中.";
                    break;
                case 2:
                    displayText = "登录中..";
                    break;
                case 3:
                    displayText = "登录中...";
                    break;
            }
            this.Invalidate();
        }

        [Browsable(false)]
        public override string Text
        {
            get
            {
                return text;
            }
        }        

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics gh = e.Graphics;
            gh.SmoothingMode = SmoothingMode.HighQuality;
            SizeF textSize = gh.MeasureString(this.Text, this.Font);
            float left = (this.Width - textSize.Width) / 2;
            float top = (this.Height - textSize.Height) / 2;
            gh.DrawString(displayText, this.Font, new SolidBrush(this.ForeColor), new PointF(left, top));
        }
    }
}
