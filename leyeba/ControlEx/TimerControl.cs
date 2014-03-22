using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ControlEx
{
    public class TimerControl : Control
    {
        private int seconds;
        /// <summary>
        /// 当时运行时间秒数
        /// </summary>
        public int Seconds
        {
            get { 
                return seconds;
            }
            set {
                if (seconds < 0)
                    return;
                seconds = value;
            }
        }

        private bool isRunning = false;
        /// <summary>
        /// 计时器是否在运行
        /// </summary>
        public bool IsRunning
        {
            get { return isRunning; }
        }

        private bool allowRun = true;

        public bool AllowRun
        {
            get { return allowRun; }
            set { allowRun = value; }
        }

        public event EventHandler TimerClick;
        public event EventHandler TimerChanged;
        public event EventHandler RunTiming;

        private System.Threading.Timer timer = null;
        private const int interval = 1000;
        private SizeF imgSize = new SizeF(16f, 16f);
        private SizeF textSize = SizeF.Empty;
        private RectangleF imgRec = RectangleF.Empty;
        private PointF contentPos = PointF.Empty;
        private Image img = ControlEx.Properties.Resources.icon_01;

        public TimerControl()
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
            this.Text = "00:00:00";
            Graphics gh = this.CreateGraphics();
            textSize = gh.MeasureString(this.Text, this.Font);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            contentPos = new PointF(0, (this.Height - imgSize.Height) / 2);
        }

        /// <summary>
        /// 初始化Timer
        /// </summary>
        private void initTimer()
        {
            System.Threading.SynchronizationContext sysContext =
                System.Threading.SynchronizationContext.Current;
            System.Threading.TimerCallback callback =
                (object state) => {
                    seconds++;
                    sysContext.Send(d => {
                        Refresh();
                        OnRunTimeing(EventArgs.Empty);
                    }, null);
                };
            timer =
                new System.Threading.Timer(
                    callback,
                    null,
                    System.Threading.Timeout.Infinite,
                    System.Threading.Timeout.Infinite);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics gh = e.Graphics;
            gh.SmoothingMode = SmoothingMode.HighQuality;
            imgRec = new RectangleF(contentPos, imgSize);
            gh.DrawImage(img, imgRec);
            this.Text =
                string.Format(
                "{0:00}:{1:00}:{2:00}",
                seconds / 3600,
                seconds / 60 % 60,
                seconds % 60);
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

        protected override void OnClick(EventArgs e)
        {
            MouseEventArgs m = (MouseEventArgs)e;
            if (m == null ||
                m.Button != MouseButtons.Left) return;
            base.OnClick(e);
            if (!imgRec.Contains(m.Location)) return;
            OnTimerCliek(EventArgs.Empty);
            this.Refresh();
        }

        protected virtual void OnTimerCliek(EventArgs e)
        {
            if (TimerClick != null) 
                TimerClick(this, e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (imgRec.Contains(e.Location))
                this.Cursor = Cursors.Hand;
            else
                this.Cursor = Cursors.Default;
        }

        public bool Start()
        {
            if (timer == null) 
                initTimer();
            isRunning = true;
            OnTimerChanged(EventArgs.Empty);            
            img = ControlEx.Properties.Resources.icon_suspend;
            return timer.Change(0, interval);
        }

        public bool Pause()
        {
            if (timer == null)
                return false;
            isRunning = false;
            OnTimerChanged(EventArgs.Empty);
            img = ControlEx.Properties.Resources.icon_01;
            this.Refresh();
            return timer.Change(
                System.Threading.Timeout.Infinite,
                System.Threading.Timeout.Infinite);
        }

        public bool Continue()
        {
            return Start();
        }

        public bool Stop()
        {
            if (timer == null)
                return false;
            img = ControlEx.Properties.Resources.icon_01;
            timer.Dispose();
            seconds = 0;
            this.Refresh();
            GC.SuppressFinalize(timer);
            GC.Collect();
            timer = null;
            isRunning = false;
            return true;
        }

        public bool Reset()
        {
            if (seconds == 0)
                return false;
            seconds = 0;
            this.Refresh();
            return true;
        }

        protected virtual void OnRunTimeing(EventArgs args)
        {
            if (RunTiming != null)
            {
                RunTiming(this, args);
            }
        }

        protected virtual void OnTimerChanged(EventArgs args)
        {
            if (TimerChanged != null)
            {
                TimerChanged(this, args);
            }
        }

        public new void Dispose()
        {
            base.Dispose();
            Stop();
        }
    }
}
