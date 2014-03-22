using ControlEx;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Util;
using Util.ConfigManage;
using Util.JsonData;

namespace leyeba
{
    public partial class FormFloat : Form
    {
        private FormMain main = null;
        private Form menuForm = null;
        private ContextMenuItem itemShowMain = null;

        private Font progressFont = new Font("宋体", 11);
        private User currentUser = null;
        private Size defaultSize = new Size(60, 60);
        private Size frmMenuSize = new Size(202, 201);
        private Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
        private FormLeyebaMsg leyebaMsgForm = null;
        private MessageLeyeba leyebaMsg = null;

        private Bitmap bubbleDefault = leyeba.Properties.Resources.bubble_default;
        private Bitmap bubblePause = leyeba.Properties.Resources.qipao_pause;
        private Bitmap bubbleBg = leyeba.Properties.Resources.qipao_bg;
        private Bitmap bubbleColor = leyeba.Properties.Resources.qipao_color;
        private int bubbleOffset = 2;

        private Icon inform = leyeba.Properties.Resources.inform;
        private Icon informSpace = leyeba.Properties.Resources.inform_space;
        private bool iconFlag = true;
        private Timer newmsgTimer = null;
        private int clickCount = 0;
        private Timer clickTimer = null;
        private bool locationChanged = false;

        public FormFloat()
        {
            InitializeComponent();
            initContextMenu();

            newmsgTimer = new Timer();
            newmsgTimer.Interval = 500;
            newmsgTimer.Tick += newmsgTimer_Tick;

            clickTimer = new Timer();
            clickTimer.Interval = 360;
            clickTimer.Tick += clickTimer_Tick;

            RectangleF progressRec =
                new RectangleF(
                    bubbleOffset,
                    bubbleOffset,
                    bubbleColor.Width - 2 * bubbleOffset,
                    bubbleColor.Height - 2 * bubbleOffset);
            bubbleColor = bubbleColor.Clone(progressRec, bubbleColor.PixelFormat);
        }
        
        private static FormFloat instance = new FormFloat();
        public static FormFloat Instance
        {
            get
            {
                if (instance == null ||
                    instance.IsDisposed)
                    instance = new FormFloat();
                return instance;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            settingDefaultImage();
            currentUser = User.CurrentUser;
            //定时更新消息
            leyebaMsg = new MessageLeyeba();
            leyebaMsg.NewMessage += leyebaMsg_NewMessage;
            leyebaMsg.RegularlyUpdateMessage();
            UserSetting userSetting = ConfigHelper.UserSettingConfig;
            if (userSetting != null)
            {
                //设置窗体位置
                if (!userSetting.FloatPos.IsEmpty &&
                    userSetting.FloatPos.X + this.Width < workingArea.Width &&
                    userSetting.FloatPos.Y + this.Width < workingArea.Height)
                {
                    this.Location = userSetting.FloatPos;
                }
                else
                {
                    int left = workingArea.Width - this.ClientSize.Width * 2 - this.ClientRectangle.Width / 2;
                    int top = workingArea.Location.Y + this.ClientSize.Height;
                    this.Location = new Point(left, top);
                }
            }

            this.TopMost = true;

            this.notifyIcon.ShowBalloonTip(
                1000,
                notifyIcon.BalloonTipTitle,
                string.Format("{0}，{1}！", notifyIcon.BalloonTipText, currentUser.UserName),
                ToolTipIcon.Info);

            TaskTiming timing = ProjectTask.TaskTiming;
            if (timing != null &&
                timing.WorkDate.Date == DateTime.Now.Date &&
                !timing.EndWork)
            {
                int workingSeconds = timing.WorkSeconds;
                if (workingSeconds > 0)
                {
                    this.WorkingPause();
                }
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            showMain();
            this.LocationChanged += FormFloat_LocationChanged;
        }

        void FormFloat_LocationChanged(object sender, EventArgs e)
        {
            this.locationChanged = true;            
        }

        #region//移动浮动窗口
        private Point mouseDownPos;
        private bool isMoseDown = false;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (!isMoseDown &&
                e.Button == MouseButtons.Left)
            {
                mouseDownPos = e.Location;
                isMoseDown = true;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (isMoseDown && e.Button == MouseButtons.Left)
            {
                int newX = this.Left + (e.X - mouseDownPos.X);
                int newY = this.Top + (e.Y - mouseDownPos.Y);
                int left = 0, top = 0;
                if (newX > workingArea.X) left = newX;
                if (newY > workingArea.Y) top = newY;
                if (newX + this.ClientSize.Width > workingArea.Width)
                    left = workingArea.Width - this.ClientSize.Width;
                if (newY + this.ClientSize.Height > workingArea.Height)
                    top = workingArea.Height - this.ClientSize.Height;
                this.Location = new Point(left, top);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (isMoseDown)
            {
                isMoseDown = false;
                mouseDownPos = Point.Empty;
                if (!locationChanged)
                {
                    clickCount++;
                    clickTimer.Enabled = true;                    
                }
                locationChanged = false;
            }

            if (e.Button ==
                MouseButtons.Right)
            {
                showMenu();
            }
        }
        #endregion

        void leyebaMsg_NewMessage(object sender, EventArgs e)
        {
            iconFlag = true;
            this.BeginInvoke((MethodInvoker)delegate {
                newmsgTimer.Start();
            });
        }

        void newmsgTimer_Tick(object sender, EventArgs e)
        {
            if (iconFlag)
                notifyIcon.Icon = informSpace;
            else
                notifyIcon.Icon = inform;
            iconFlag = !iconFlag;
        }
        
        void clickTimer_Tick(object sender, EventArgs e)
        {
            if (clickCount == 2)
            {
                resetClickTimer();
                executeMouseEvent("1");
            }
            else
            {
                resetClickTimer();
                executeMouseEvent("0");
            }
        }

        private void settingDefaultImage()
        {
            if (!this.IsHandleCreated)
                return;
            BitmapHandler.SetBits(
                bubbleDefault,
                this.Location,
                this.Handle);
        }

        protected override void WndProc(ref Message m)
        {
            int WM_QUERYENDSESSION = 0x0011;

            //系统关机时退出应用程序
            if (m.Msg == WM_QUERYENDSESSION)
            {
                Environment.Exit(Environment.ExitCode);
                return;
            }

            base.WndProc(ref m);
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

        protected override void OnSizeChanged(EventArgs e)
        {
            this.ClientSize = defaultSize;
            base.OnSizeChanged(e);
        }

        #region//右键菜单
        private void initContextMenu()
        {
            ContextMenuBase contextMenu = new ContextMenuBase();

            itemShowMain = new ContextMenuItem();            
            itemShowMain.Text = "显示主界面";
            itemShowMain.DrawSeparator = true;
            itemShowMain.Dock = DockStyle.Top;
            itemShowMain.Click += itemShowMain_Click;

            ContextMenuItem itemMsg = new ContextMenuItem();
            itemMsg.Text = "系统（项目）消息";
            itemMsg.DrawSeparator = true;
            itemMsg.Dock = DockStyle.Top;
            itemMsg.Icon = leyeba.Properties.Resources.inform_icon01;
            itemMsg.Click += itemMsg_Click;

            ContextMenuItem itemSwitch = new ContextMenuItem();
            itemSwitch.Text = "更改用户";
            itemSwitch.Dock = DockStyle.Top;
            itemSwitch.Click += itemSwitch_Click;

            ContextMenuItem itemExit = new ContextMenuItem();
            itemExit.Text = "退出";
            itemExit.Dock = DockStyle.Top;
            itemExit.Click += itemExit_Click;

            contextMenu.Controls.Add(itemExit);
            contextMenu.Controls.Add(itemSwitch);
            contextMenu.Controls.Add(itemMsg);
            contextMenu.Controls.Add(itemShowMain);

            menuForm = new Form();
            menuForm.FormBorderStyle = FormBorderStyle.None;
            menuForm.Size = contextMenu.Size;
            menuForm.ShowInTaskbar = false;
            menuForm.StartPosition = FormStartPosition.Manual;
            menuForm.Deactivate += menuForm_Deactivate;
            menuForm.TopMost = true;
            menuForm.Controls.Add(contextMenu);
        }

        void itemShowMain_Click(object sender, EventArgs e)
        {
            hideMenuForm();
            showMain();
        }

        void itemMsg_Click(object sender, EventArgs e)
        {
            hideMenuForm();
            showLeyebaMsg();
        }

        void itemSwitch_Click(object sender, EventArgs e)
        {
            hideMenuForm();
            disposeInstance();
            FormLogin login = FormLogin.Instance;
            login.Show();
        }

        void itemExit_Click(object sender, EventArgs e)
        {
            hideMenuForm();
            UserSetting sysSetting = 
                ConfigHelper.UserSettingConfig;
            if (sysSetting != null &&
                sysSetting.ExitPrompt)
            {
                DialogResult dialogResult =
                    PromptBox.Question(
                    "确定退出乐业吧？",
                    "退出",
                    FormStartPosition.CenterScreen);
                if (dialogResult == DialogResult.Cancel)
                    return;
            }
            exit();
        }

        private void hideMenuForm()
        {
            if (menuForm != null &&
                menuForm.Visible)
                menuForm.Visible = false;
        }

        void menuForm_Deactivate(object sender, EventArgs e)
        {
            Form form = sender as Form;
            if (form == null ||
                !form.Visible)
                return;
            form.Visible = false;
        }
        #endregion

        //NoifyIcon单击显示Leyeba消息
        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) 
                return;
            showLeyebaMsg();
        }
        
        /// <summary>
        /// 显示leyeba消息
        /// </summary>
        private void showLeyebaMsg()
        {
            if (leyebaMsgForm == null || 
                leyebaMsgForm.IsDisposed)
            {
                leyebaMsgForm = new FormLeyebaMsg();
            }
            leyebaMsgForm.Show();
            leyebaMsgForm.Activate();
            leyebaMsgForm.InitMessage();
            if (newmsgTimer.Enabled)
            {
                notifyIcon.Icon = inform;
                newmsgTimer.Stop();
            }
        }
       
        private void exit()
        {
            disposeInstance();
            Environment.Exit(Environment.ExitCode);
        }

        private void disposeInstance()
        {
            notifyIcon.Visible = false;
            notifyIcon.Dispose();
            UserSetting userSetting =
                ConfigHelper.UserSettingConfig;
            if (userSetting != null)
                userSetting.UpdateFloatPos(this.Location);
            this.Visible = false;
            if (main != null &&
                !main.IsDisposed)
            {
                main.AutoSave();
                main.StopWoring();
                KeyMouseHook.Dispose();
                ScreenWatch.Dispose();
                main.Close();
                main.Dispose();
            }
            if (leyebaMsg != null)
            {
                leyebaMsg.NewMessage -= leyebaMsg_NewMessage;
                leyebaMsg.StopUpdateMessage();
            }
            if (leyebaMsgForm != null &&
                !leyebaMsgForm.IsDisposed)
            {
                leyebaMsgForm.Close();
                leyebaMsgForm.Dispose();
            }
            if (newmsgTimer != null ||
                newmsgTimer.Enabled)
            {
                newmsgTimer.Stop();
                newmsgTimer.Dispose();
            }
            if (clickTimer != null ||
                clickTimer.Enabled)
            {
                clickTimer.Stop();
                clickTimer.Dispose();
            }
            logut();
            FormFloat.instance.Dispose();
            FormFloat.instance = null;
            this.Close();
        }

        private void logut()
        {
            if (currentUser != null)
                User.Logoff(currentUser.Token);
            User.CurrentUser = null;
            WorkProject.Project = null;
            WorkProject.ProjKVPList = null;
        }

        private void resetClickTimer()
        {
            clickCount = 0;
            clickTimer.Enabled = false; 
        }

        private void executeMouseEvent(string flag)
        {
            UserSetting sysSetting =
                ConfigHelper.UserSettingConfig;
            if (sysSetting == null ||
                string.IsNullOrEmpty(sysSetting.FloatClickType))
                return;
            switch (flag)
            {
                case "0":
                    //单击
                    if (sysSetting.FloatClickType.Equals("0"))
                    {
                        //显示主界面
                        showMain();
                    }
                    else if (sysSetting.FloatClickType.Equals("1"))
                    {
                        //继续工作
                        if (main != null && 
                            !main.IsDisposed)
                            main.StartWorking();
                    }
                    break;
                case "1":
                    //双击
                    if (sysSetting.FloatClickType.Equals("0"))
                    {
                        //继续工作
                        if (main != null && !main.IsDisposed)
                            main.StartWorking();
                    }
                    else if (sysSetting.FloatClickType.Equals("1"))
                    {
                        //显示主界面
                        showMain();
                    }
                    break;
            }
        }

        private void showMain()
        {
            if (main == null || main.IsDisposed)
            {
                main = new FormMain();
                main.StartPosition = FormStartPosition.Manual;
                main.VisibleChanged += main_VisibleChanged;
                main.Location = new Point(this.Left - main.Width, this.Top + this.Height);
            }
            if (main.Visible)
            {
                main.Visible = false;
            }
            else
            {
                main.Visible = true;
                if (main.WindowState == FormWindowState.Minimized)
                    main.WindowState = FormWindowState.Normal;
                main.Activate();
            }
        }

        void main_VisibleChanged(object sender, EventArgs e)
        {
            FormMain main = sender as FormMain;
            if (main == null)
                return;
            if (main.Visible)
            {
                if (itemShowMain != null)
                    itemShowMain.Text = "隐藏主界面";
            }
            else
            {
                if (itemShowMain != null)
                    itemShowMain.Text = "显示主界面";
            }
        }

        private void notifyIcon_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != 
                MouseButtons.Right)
                return;
            showMenu();
        }

        public void SetDefault()
        {
            this.settingDefaultImage();
        }

        public void WorkingPause()
        {
            BitmapHandler.SetBits(
                bubblePause,
                this.Location,
                this.Handle);
        }

        /// <summary>
        /// 更新项目整体进度百分比图
        /// </summary>
        /// <param name="value"></param>
        public void UpdateProgress(int value, string time, bool refreshRate)
        {
            Bitmap floatimg = new Bitmap(bubbleBg.Width, bubbleBg.Height);
            using (Graphics gh = Graphics.FromImage(floatimg))
            {
                gh.SmoothingMode = SmoothingMode.HighQuality;
                gh.Clear(Color.Transparent);
                gh.DrawImage(bubbleBg, new Point(0, 0));
                if (value > 0)
                {
                    int progressValue = 0;
                    if (refreshRate)
                        progressValue = (int)Math.Round((float)bubbleColor.Height / ((float)100 / value), MidpointRounding.AwayFromZero);
                    else
                        progressValue = bubbleColor.Height;
                    Rectangle progressRec = new Rectangle(0, bubbleColor.Height - progressValue, bubbleColor.Width, progressValue);
                    using (Bitmap progressBmp = bubbleColor.Clone(progressRec, bubbleColor.PixelFormat))
                    {
                        gh.DrawImage(progressBmp, new PointF(bubbleOffset, bubbleColor.Height - progressValue + bubbleOffset));
                    }
                }

                SizeF strSize = gh.MeasureString(time, progressFont);
                float left = (this.Width - strSize.Width) / 2;
                float top = (this.Height - strSize.Height) / 2;
                gh.DrawString(time, progressFont, Brushes.Black, new PointF(left, top));
            }

            BitmapHandler.SetBits(
                floatimg,
                this.Location, 
                this.Handle);
        }

        private void showMenu()
        {
            if (menuForm == null ||
                menuForm.IsDisposed)
            {
                initContextMenu();
            }
            Point menuPt = MousePosition;
            if (menuPt.X + menuForm.Width >
                Screen.PrimaryScreen.WorkingArea.Width)
                menuPt.X = menuPt.X - menuForm.Width;
            if (menuPt.Y + menuForm.Height >
                Screen.PrimaryScreen.WorkingArea.Height)
                menuPt.Y = menuPt.Y - menuForm.Height;

            menuForm.Location = menuPt;
            menuForm.Show();
            menuForm.Activate();
        }
    }
}
