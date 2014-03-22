using ControlEx;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Util;
using Util.JsonData;

namespace leyeba
{
    public partial class FormMain : Form
    {
        private MainTabControl tabControlTask = new MainTabControl();
        private MainTabControl tabControlLog = new MainTabControl();
        private MainTabControl tabControlConfig = new MainTabControl();
        private List<MainTabControl> tabControlList = new List<MainTabControl>();

        private Image leftMenuOnBg = leyeba.Properties.Resources.menu_on_bg;
        private Image leftMenuBg = leyeba.Properties.Resources.leftmenu_bg;
        private Image leftMenuLogo = leyeba.Properties.Resources.leftmenu_logo;
        private int currentIndex = -1;

        private UserCtrlTask task = null;
        private UserCtrlLog log = null;
        private UserCtrlUserSetting userSetting = null;

        public FormMain()
        {
            InitializeComponent();
            User currentUser = User.CurrentUser;
            if (currentUser != null)
            {
                lblTitle.Text = string.Format("{0}，欢迎使用乐业吧", currentUser.UserName);
            }
            initTabControl();
            settingRegion();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //无标题窗体右键菜单和最大化最小化
            int windowLong = (Win32API.GetWindowLong(new HandleRef(this, this.Handle), Win32API.GWL_STYLE));
            Win32API.SetWindowLong(new HandleRef(this, this.Handle), Win32API.GWL_STYLE, windowLong | Win32API.WS_SYSMENU | Win32API.WS_MINIMIZEBOX);

            this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            init(0);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            loadTabControl();
            this.VisibleChanged += FormMain_VisibleChanged;            
        }

        void FormMain_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                loadTabControl();
            }
            else
            {
                foreach (MainTabControl tabctl in tabControlList)
                    tabctl.Visible = false;
            }            
        }
            
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (this.WindowState != 
                FormWindowState.Minimized)
            {
                this.pnlLeft.Refresh();
            }
        }

        private void settingRegion()
        {
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(new Rectangle(pnlLeft.Width - leftMenuBg.Width, 11, 38, this.Height - 11 * 2));
            path.AddRectangle(new Rectangle(pnlLeft.Width, 0, this.Width - pnlLeft.Width, this.Height));
            this.Region = new Region(path);
        }

        private void initTabControl()
        {
            tabControlTask.ImageNormal = leyeba.Properties.Resources.menu1_nor;
            tabControlTask.ImageChecked = leyeba.Properties.Resources.menu1_on;
            tabControlTask.Checked = true;
            tabControlTask.Owner = this;
            tabControlTask.Tag = 0;
            tabControlTask.Text = "任务";
            tabControlTask.Click += OnTabControlClick;
            tabControlList.Add(tabControlTask);
            
            tabControlLog.ImageNormal = leyeba.Properties.Resources.menu2_nor;
            tabControlLog.ImageChecked = leyeba.Properties.Resources.menu2_on;
            tabControlLog.Owner = this;
            tabControlLog.Tag = 1;
            tabControlLog.Text = "日志";
            tabControlLog.Click += OnTabControlClick;
            tabControlList.Add(tabControlLog);

            tabControlConfig.ImageNormal = leyeba.Properties.Resources.menu3_nor;
            tabControlConfig.ImageChecked = leyeba.Properties.Resources.menu3_on;
            tabControlConfig.Owner = this;
            tabControlConfig.Tag = 2;
            tabControlConfig.Text = "配置";
            tabControlConfig.Click += OnTabControlClick;
            tabControlList.Add(tabControlConfig);
        }

        private void loadTabControl()
        {
            int left = this.Left + pnlLeft.Width - leftMenuOnBg.Width + 1;
            tabControlTask.Visible = true;
            tabControlTask.Location = new Point(left, this.Top + 22);
            tabControlTask.Show();

            tabControlLog.Location = new Point(left, tabControlTask.Top + tabControlTask.Height);
            tabControlLog.Show();

            tabControlConfig.Location = new Point(left, tabControlLog.Top + tabControlLog.Height);
            tabControlConfig.Show(); 
        }

        private void pnlLeft_Paint(object sender, PaintEventArgs e)
        {
            Panel pnl = sender as Panel;
            if (pnl == null) return;
            Graphics gh = e.Graphics;
            gh.SmoothingMode = SmoothingMode.HighQuality;
            ImageAttributes imgAttr = new ImageAttributes();
            imgAttr.SetWrapMode(WrapMode.Tile);
            gh.DrawImage(
                leftMenuBg,
                new Rectangle(pnl.Width - leftMenuBg.Width, 0, leftMenuBg.Width, pnl.Height),
                0,
                0,
                leftMenuBg.Width,
                leftMenuBg.Height,
                GraphicsUnit.Pixel,
                imgAttr);
            gh.DrawImage(leftMenuLogo, new Point(pnl.Width - leftMenuBg.Width, pnl.Height - leftMenuLogo.Height - 9));
            Pen linePen = new Pen(Color.FromArgb(31, 43, 53));
            gh.DrawRectangle(linePen, new Rectangle(pnl.Width - leftMenuBg.Width, 0, leftMenuBg.Width, pnl.Height - 1));
        }

        private void init(int index)
        {
            if (pnlMain.Controls.Count > 0)
                pnlMain.Controls.Clear();

            foreach (MainTabControl tabctl in tabControlList)
            {
                if (tabctl.Tag.ToString().Equals(index.ToString()))
                    tabctl.Checked = true;
                else
                    tabctl.Checked = false;
            }
            
            switch (index)
            {
                case 0:
                    showTask();
                    break;
                case 1:
                    showLog();
                    break;
                case 2:
                    showSetting();
                    break;
            }
        }

        private void showTask()
        {
            if (task == null || task.IsDisposed)
            {
                task = new UserCtrlTask();
                task.BackColor = Color.Transparent;
                task.Dock = DockStyle.Fill;
            }

            pnlMain.Controls.Add(task);
            task.Focus();
        }

        private void showLog()
        {
            if (log == null || log.IsDisposed)
            {
                log = new UserCtrlLog();
                log.BackColor = Color.Transparent;
                log.Dock = DockStyle.Fill;
            }

            pnlMain.Controls.Add(log);
            log.Focus();
        }

        private void showSetting()
        {
            if (userSetting == null || userSetting.IsDisposed)
            {
                userSetting = new UserCtrlUserSetting();
                userSetting.BackColor = Color.Transparent;
                userSetting.Dock = DockStyle.Fill;
            }

            pnlMain.Controls.Add(userSetting);
            userSetting.Focus();
        }

        private void OnTabControlClick(object sender, EventArgs args)
        {
            Control ctl = sender as Control;
            if (ctl == null ||
                ctl.Tag == null)
                return;
            int index = -1;
            if (!int.TryParse(ctl.Tag.ToString(), out index))
                return;
            if (index == currentIndex)
                return;
            currentIndex = index;
            init(currentIndex);
        }

        public void FillWorkLog(List<LogData> logs)
        {
            OnTabControlClick(tabControlLog, EventArgs.Empty);
            log.AppendLogs(logs);
        }

        public void LoadLogsAt(DateTime dt)
        {
            OnTabControlClick(tabControlLog, EventArgs.Empty);
            log.LoadLogsAt(dt);
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);
            int left = this.Left + pnlLeft.Width - leftMenuOnBg.Width + 1;
            tabControlTask.Location = new Point(left, this.Top + 22);
            tabControlLog.Location = new Point(left, tabControlTask.Top + tabControlTask.Height);
            tabControlConfig.Location = new Point(left, tabControlLog.Top + tabControlLog.Height);
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void pnlRight_Paint(object sender, PaintEventArgs e)
        {
            Panel pnl = sender as Panel;
            if (pnl == null) return;
            Graphics gh = e.Graphics;
            gh.SmoothingMode = SmoothingMode.HighQuality;
            gh.DrawRectangle(new Pen(Color.FromArgb(182, 196, 218)), new Rectangle(0, 0, pnl.Width - 1, pnl.Height - 1));
        }

        private Point rememberPt = new Point();
        private void btnMax_Click(object sender, EventArgs e)
        {
            if (btnMax.Tag == null)
                return;
            string tag = btnMax.Tag.ToString();
            if (tag.Equals("0"))
            {
                rememberPt = this.Location;
                this.WindowState = FormWindowState.Maximized;
                btnMax.ImageNormal = leyeba.Properties.Resources.btn_main_back_nor;
                btnMax.ImageHover = leyeba.Properties.Resources.btn_main_back_hot;
                btnMax.ImagePress = leyeba.Properties.Resources.btn_main_back_down;
                btnMax.Tag = "1";
            }
            else if(tag.Equals("1"))
            {
                this.WindowState = FormWindowState.Normal;
                this.Location = rememberPt;
                btnMax.ImageNormal = leyeba.Properties.Resources.btn_main_max_nor;
                btnMax.ImageHover = leyeba.Properties.Resources.btn_main_max_hot;
                btnMax.ImagePress = leyeba.Properties.Resources.btn_main_max_down;
                btnMax.Tag = "0";
            }
            pnlLeft.Refresh();
            settingRegion();
        }

        public void AutoSave()
        {
            if (task == null ||
                task.IsDisposed)
                return;
            task.GenKeyMouseAutoLog();
            task.AutoSave();
        }

        public void StartWorking()
        {
            if (task == null || 
                task.IsDisposed)
                return;
            task.StartWorking();
        }

        public void StopWoring()
        {
            if (task == null ||
                task.IsDisposed)
                return;
            task.StopWorking();
        }

        private void OnTopDoubleClick(object sender, EventArgs e)
        {            
            btnMax_Click(btnMax, e);
        }

        private void lblFeedback_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string url = ConfigurationManager.ConnectionStrings["feedbackUrl"].ConnectionString;
                if (string.IsNullOrEmpty(url))
                {
                    PromptBox.Alert("反馈URL地址为空，请配置反馈URL地址！", "提示");
                    return;
                }
                System.Diagnostics.Process.Start(url);
            }
            catch (Exception exp)
            {
                Log.error(this.GetType(), (exp.InnerException == null ? "" : exp.InnerException.ToString()) + "反馈" + exp.Message);
                PromptBox.Alert(exp.Message, "错误");
            }
        }
    }
}
