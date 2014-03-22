using System;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ControlEx;
using Util;
using Util.ConfigManage;
using Util.JsonData;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Configuration;

namespace leyeba
{
    public partial class FormLogin : Form
    {
        private Form menuForm = null;
        private bool switchUser = false;
        //系统设置
        private UserSetting userSetting;
        //异步登陆委托
        private delegate User LoginHandler(string username, string password);
        private Thread loginThread = null;

        public FormLogin()
        {
            InitializeComponent();
            initContextMenu();
            lblVersion.Text = string.Format("版本：{0}", Global.GetAssemblyFileVersion());
            settingNoneTransparencyRegion();
        }

        /// <summary>
        /// 更改用户
        /// </summary>
        /// <param name="flag"></param>
        public FormLogin(bool flag)
            :this()
        {
            switchUser = flag;
        }

        private static FormLogin instance = new FormLogin();
        public static FormLogin Instance
        {
            get
            {
                if (instance == null ||
                    instance.IsDisposed)
                    instance = new FormLogin();
                return instance;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //无标题窗体右键菜单和最大化最小化
            int windowLong = (Win32API.GetWindowLong(new HandleRef(this, this.Handle), Win32API.GWL_STYLE));
            Win32API.SetWindowLong(
                new HandleRef(this, this.Handle), 
                Win32API.GWL_STYLE,
                windowLong | 
                Win32API.WS_SYSMENU | 
                Win32API.WS_MINIMIZEBOX);

            userSetting = ConfigHelper.UserSettingConfig;
            string[] users = userSetting.GetUsers();
            comboboxUserName1.Items.AddRange(users);
            initUser(userSetting);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Win32API.SetForegroundWindow(this.Handle);
            pnlLogin.VisibleChanged += pnlLogin_VisibleChanged;
        }

        void pnlLogin_VisibleChanged(object sender, EventArgs e)
        {
            lblNetSetting.Visible = pnlLogin.Visible;
            this.loginingControl1.Enabled = !pnlLogin.Visible;
        }

        #region//右键菜单
        private void initContextMenu()
        {
            int menuHeight = 1;
            ContextMenuBase contextMenu = new ContextMenuBase();

            ContextMenuItem itemShowMain = new ContextMenuItem();
            itemShowMain.Text = "显示主界面";
            itemShowMain.DrawSeparator = true;
            itemShowMain.Dock = DockStyle.Top;
            itemShowMain.Click += itemShowMain_Click;
            menuHeight += itemShowMain.Height;

            ContextMenuItem itemExit = new ContextMenuItem();
            itemExit.Text = "退出";
            itemExit.Dock = DockStyle.Top;
            itemExit.Click += itemExit_Click;
            menuHeight += itemExit.Height;

            contextMenu.Controls.Add(itemExit);
            contextMenu.Controls.Add(itemShowMain);
            contextMenu.Height = menuHeight;

            menuForm = new Form();
            menuForm.FormBorderStyle = FormBorderStyle.None;
            menuForm.Size = contextMenu.Size;
            menuForm.ShowInTaskbar = false;
            menuForm.StartPosition = FormStartPosition.Manual;
            menuForm.Deactivate += menuForm_Deactivate;            
            menuForm.TopMost = true;
            menuForm.Controls.Add(contextMenu);
        }

        void menuForm_Deactivate(object sender, EventArgs e)
        {
            Form form = sender as Form;
            if (form == null ||
                !form.Visible)
                return;
            form.Visible = false;
        }

        void itemShowMain_Click(object sender, EventArgs e)
        {
            if (menuForm != null &&
                menuForm.Visible)
                menuForm.Visible = false;
            if (this.WindowState == FormWindowState.Minimized)
                this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        void itemExit_Click(object sender, EventArgs e)
        {
            if (menuForm != null &&
                menuForm.Visible)
                menuForm.Visible = false;
            this.exit();
        }
        #endregion

        private void initUser(UserSetting sysSetting)
        {
            if (string.IsNullOrEmpty(sysSetting.UserId))
            {
                comboboxUserName1.Select();
                return;
            }
            comboboxUserName1.Text = sysSetting.UserId;
            comboboxUserName1.SelectedItem = sysSetting.UserId;
            comboboxUserName1.SelectedValueChanged -= comboboxUserName1_SelectedValueChanged;
            comboboxUserName1.SelectedValueChanged += comboboxUserName1_SelectedValueChanged;
            //if (switchUser) return;
            if (!string.IsNullOrWhiteSpace(comboboxUserName1.Text.Trim()))
                txtPwd.Select();            
            if (sysSetting.AutoLogin)
            {
                txtPwd.Text = sysSetting.Pwd;
                chkRemember.Checked = sysSetting.RememberPwd;
                chkAutoLogin.Checked = true;
                login();
            }
            else if (sysSetting.RememberPwd)
            {
                chkRemember.Checked = true;
                txtPwd.Text = sysSetting.Pwd;
                txtPwd.Select();
                txtPwd.TextBox.SelectionStart = txtPwd.TextBox.TextLength;
            }
            else
            {
                txtPwd.Text = string.Empty;
                chkAutoLogin.Checked = false;
                chkRemember.Checked = false;                
            }
        }

        void comboboxUserName1_SelectedValueChanged(object sender, EventArgs e)
        {
            userSetting.Init(comboboxUserName1.Text);
            initUser(userSetting);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Enter)
            {
                login();
            }
        }

        private void settingNoneTransparencyRegion()
        {
            //设置非透明区域
            Bitmap img = (Bitmap)this.BackgroundImage;
            GraphicsPath grapth = BitmapHandler.GetNoneTransparencyRegion(img, 10);
            this.Region = new Region(grapth);
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

        /// <summary>
        /// 登陆
        /// </summary>
        private void login()
        {
            TBoolResult<string> result = validInputs();
            if (!result.Result)
            {
                PromptBox.Alert(result.Data, "提示");
                return;
            }
            string uid = comboboxUserName1.Text.Trim().Replace(" ", "").Replace("'", "");
            string pwd = txtPwd.Text;
            if (!userSetting.RememberPwd || 
                !userSetting.Pwd.Equals(pwd))
            {
                pwd = Security.MD5Encrypt(pwd, Global.EncryptSaltValue);
                txtPwd.Text = pwd;
            }
            lblWelcome.Text = uid;
            lblWelcome.Left = (pnlLoading.Width - lblWelcome.Width) / 2;
            pnlLogin.Visible = false;
            LoginHandler login = User.Login;
            //return;
            System.Threading.SynchronizationContext syncContext =
                System.Threading.SynchronizationContext.Current;
            loginThread = new Thread(() => {
                User currentUser = User.Login(uid, pwd);
                if (currentUser == null)
                {
                    PromptBox.Alert("发生未知性错误，具体原因请查看错误日志。", "登陆失败");
                    syncContext.Send(d => {
                        pnlLogin.Visible = true;
                    }, null);
                    Thread.CurrentThread.Abort();
                    return;
                }
                if (!string.IsNullOrWhiteSpace(currentUser.Reason) &&
                    currentUser.Status.Equals("0"))
                {
                    PromptBox.Alert(currentUser.Reason, "登陆失败");
                    syncContext.Send(d => {
                        pnlLogin.Visible = true;
                    }, null);
                    Thread.CurrentThread.Abort();
                    return;
                }
                currentUser.UserId = uid;
                User.CurrentUser = currentUser;
                //保存设置
                userSetting.RememberPwd = chkRemember.Checked;
                userSetting.AutoLogin = chkAutoLogin.Checked;
                userSetting.UserId = uid;
                userSetting.Pwd = userSetting.RememberPwd ? pwd : "";
                userSetting.Save();
                ConfigHelper.UserSettingConfig = userSetting;
                //登录成功
                syncContext.Send(d => {
                    this.Hide();
                    pnlLogin.Visible = true;
                    notifyIcon.Visible = false;
                    notifyIcon.Dispose();
                    FormFloat main = FormFloat.Instance;
                    main.Show();
                }, null);
                //提交自动日志
                AutoLogs.Submit();
                //处理超时文件
                WorkLog.ProcessTimeoutLog();
                Thread.CurrentThread.Abort();
            });
            loginThread.Name = "login";
            loginThread.IsBackground = true;
            loginThread.Start();
        }
        /// <summary>
        /// 输入验证
        /// </summary>
        /// <returns></returns>
        private TBoolResult<string> validInputs()
        {
            TBoolResult<string> result = new TBoolResult<string>();
            if (string.IsNullOrWhiteSpace(comboboxUserName1.Text.Trim()))
            {
                result.Result = false;
                result.Data = "请输入登陆用户名。";
                comboboxUserName1.Select();
                return result;
            }
            if (string.IsNullOrWhiteSpace(txtPwd.Text.Trim()))
            {
                result.Result = false;
                result.Data = "请输入登陆密码。";
                txtPwd.Select();
                return result;
            }
            result.Result = true;
            return result;
        }

        private void exit()
        {
            notifyIcon.Visible = false;
            notifyIcon.Dispose();
            this.Close();
            this.Dispose();
            Environment.Exit(Environment.ExitCode);
        }

        /// <summary>
        /// 登陆按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.login();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.exit();
        }

        private void btnMini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void lblReg_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string url = ConfigurationManager.ConnectionStrings["regUrl"].ConnectionString;
                if (string.IsNullOrEmpty(url))
                {
                    PromptBox.Alert("注册账号URL地址为空，请配置注册账号URL地址！", "提示");
                    return;
                }
                System.Diagnostics.Process.Start(url);
            }
            catch (Exception exp)
            {
                Log.error(this.GetType(), (exp.InnerException == null ? "" : exp.InnerException.ToString()) + "注册账号" + exp.Message);
                PromptBox.Alert(exp.Message, "错误");
            }
        }

        private void lblForgot_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string url = ConfigurationManager.ConnectionStrings["forgotPwdUrl"].ConnectionString;
                if (string.IsNullOrEmpty(url))
                {
                    PromptBox.Alert("找回密码URL地址为空，请配置找回密码URL地址！", "提示");
                    return;
                }
                System.Diagnostics.Process.Start(url);
            }
            catch (Exception exp)
            {
                Log.error(this.GetType(), (exp.InnerException == null ? "" : exp.InnerException.ToString()) + "找回密码" + exp.Message);
                PromptBox.Alert(exp.Message, "错误");
            }
        }

        private void chkAutoLogin_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoLogin.Checked)
                chkRemember.Checked = true;
            else
                chkRemember.Checked = false;
        }

        #region//移动窗体
        private int isDownX = 0;
        private int isDownY = 0;
        private bool isMoseDown = false;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left && !isMoseDown)
            {
                isDownX = e.X;
                isDownY = e.Y;
                isMoseDown = true;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (isMoseDown)
            {
                this.Location = new Point(Left + (e.X - isDownX), Top + (e.Y - isDownY));
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
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

        private void notifyIcon_MouseUp(object sender, MouseEventArgs e)
        {
            if (menuForm == null ||
                e.Button != MouseButtons.Right)
                return;
            Point menuPt = new Point(MousePosition.X, MousePosition.Y - menuForm.Height);
            if (menuPt.X + menuForm.Width > 
                Screen.PrimaryScreen.WorkingArea.Width) 
                menuPt.X = menuPt.X - menuForm.Width;

            menuForm.Location = menuPt;
            menuForm.Show();
            menuForm.Activate();
        }

        private void lblNetSetting_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormSystemSetting setting = new FormSystemSetting();
            setting.ShowDialog();
        }

        private void btnCancelLogin_Click(object sender, EventArgs e)
        {
            if (loginThread == null)
                return;
            loginThread.Abort();
            loginThread.Join();
            pnlLogin.Visible = true;
        }
    }
}
