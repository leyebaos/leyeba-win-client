using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Util;
using Util.ConfigManage;
using ControlEx;
using System.Diagnostics;

namespace leyeba
{
    public partial class UserCtrlUserSetting : UserControl
    {
        private UserSetting userSetting = null;
        private SystemSetting sysSetting = null;

        public UserCtrlUserSetting()
        {
            InitializeComponent();
            initControl();
        }

        private void initControl()
        {
            cboProxy.Items.Add("http代理");
            cboProxy.Text = "http代理";
            cboProxy.SelectedValue = "http代理";
            List<KeyValuePair<string, int>> lstWorkHour = 
                new List<KeyValuePair<string, int>>();
            cboWorkHour.PopupHeight = 150;
            for (int i = 1; i <= 24; i++)
            {
                lstWorkHour.Add(
                    new KeyValuePair<string, int>(
                        string.Format("{0}小时", i), 
                        i));
            }
            cboWorkHour.DisplayMember = "Key";
            cboWorkHour.ValueMember = "Value";
            cboWorkHour.DataSource = lstWorkHour;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            loadConfig();
            this.Focus();
        }

        private void loadConfig()
        {
            userSetting = ConfigHelper.UserSettingConfig;
            sysSetting = ConfigHelper.SystemSettingConfig;

            if (userSetting != null)
            {
                chkExitConfirm.Checked = userSetting.ExitPrompt;
                chkAutoLogin.Checked = userSetting.AutoLogin;
                if (userSetting.DailyWorkHour > 0)
                {
                    cboWorkHour.SelectedValue = userSetting.DailyWorkHour;
                    pnlContainer.Select();
                }
                if (!string.IsNullOrEmpty(userSetting.FloatClickType))
                {
                    if (userSetting.FloatClickType.Equals("0"))
                        rdoClick.Checked = true;
                    else
                        rdoDClick.Checked = true;
                }
            }
            if (sysSetting != null)
            {
                chkAutoLaunch.Checked = sysSetting.AutoLaunch;
                if (sysSetting.Proxy != null &&
                    sysSetting.Proxy.Enabled)
                {
                    chkEnabledProxy.Checked = true;
                    txtHost.Text = sysSetting.Proxy.Host;
                    txtPort.Text = sysSetting.Proxy.Port.ToString();
                    txtUsrname.Text = sysSetting.Proxy.UserName;
                    txtPassword.Text = sysSetting.Proxy.Password;
                    txtDomain.Text = sysSetting.Proxy.Domain;
                }
                if (sysSetting.AutoUpdate)
                    rdoAutoUpdate.Checked = true;
                else
                    rdoPromptUpdate.Checked = true;
            }
        }
        //立即更新
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string autoUpdateFile =
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AutoUpdate.exe");
            if (File.Exists(Path.Combine(autoUpdateFile)))
            {
                Process proce =
                    Process.Start(
                    autoUpdateFile,
                    string.Format(
                    "{0}.exe {1} 1 1",
                    Application.ProductName,
                    Application.ProductVersion));
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            TBoolResult<string> result = validInputs();
            if (!result.Result)
            {
                PromptBox.Alert(result.Data, "提示");
                return;
            }
            try
            {
                //启动电脑时自动登陆乐业吧
                sysSetting.AutoLaunch = chkAutoLaunch.Checked;
                string exePath = Application.ExecutablePath;
                RegistryHelper.RunWhenStart(chkAutoLaunch.Checked, Path.GetFileNameWithoutExtension(exePath), exePath);
                //启用代理
                if (chkEnabledProxy.Checked)
                {
                    sysSetting.Proxy = getProxyInfo();
                    if (!validateProxy(sysSetting.Proxy))
                    {
                        PromptBox.Alert("代理设置信息错误，请核对后再保存！", "代理");
                        return;
                    }
                }
                //升级
                if (rdoAutoUpdate.Checked)
                    sysSetting.AutoUpdate = true;
                else if (rdoPromptUpdate.Checked)
                    sysSetting.AutoUpdate = false;


                userSetting.AutoLogin = chkAutoLogin.Checked;
                //退出提示
                userSetting.ExitPrompt = chkExitConfirm.Checked;
                //工时
                if (cboWorkHour.DataSource != null &&
                     cboWorkHour.SelectedValue != null &&
                     !cboWorkHour.SelectedValue.ToString().Equals("-1"))
                {
                    userSetting.DailyWorkHour = (int)cboWorkHour.SelectedValue;
                }
                //浮动窗体点击事件
                if (rdoClick.Checked)
                    userSetting.FloatClickType = "0";
                else if (rdoDClick.Checked)
                    userSetting.FloatClickType = "1";
                if (userSetting.Save() &&
                    sysSetting.Save())
                {
                    ConfigHelper.UserSettingConfig = userSetting;
                    ConfigHelper.SystemSettingConfig = sysSetting;
                    PromptBox.Alert("保存成功", "设置");
                }
                else
                {
                    PromptBox.Alert("保存失败，请查看错误日志。", "设置");
                }
            }
            catch (Exception exp)
            {
                PromptBox.Alert(exp.Message, "错误");
                Log.error(this.GetType(), (exp.InnerException == null ? "" : exp.InnerException.ToString()) + exp.Message);
            }
        }

        private ProxyInfo getProxyInfo()
        {
            ProxyInfo proxy = new ProxyInfo();
            proxy.Enabled = true;
            proxy.Host = txtHost.Text.Trim();
            int port = 0;
            int.TryParse(txtPort.Text, out port);
            proxy.Port = port;
            proxy.UserName = txtUsrname.Text.Trim();
            proxy.Password = txtPassword.Text;
            proxy.Domain = txtDomain.Text.Trim();
            return proxy;
        }

        private bool validateProxy(ProxyInfo proxyInfo)
        {
            bool success = WebHelper.ValidateProxy(proxyInfo);
            return success;
        }

        /// <summary>
        /// 输入验证
        /// </summary>
        /// <returns></returns>
        private TBoolResult<string> validInputs()
        {
            TBoolResult<string> result = new TBoolResult<string>();
            if (chkEnabledProxy.Checked)
            {
                if (string.IsNullOrWhiteSpace(txtHost.Text.Trim()))
                {
                    result.Result = false;
                    result.Data = "请输入服务器名。";
                    txtHost.Select();
                    return result;
                }
                if (string.IsNullOrWhiteSpace(txtPort.Text.Trim()))
                {
                    result.Result = false;
                    result.Data = "请输入端口号。";
                    txtPort.Select();
                    return result;
                }
                if (string.IsNullOrWhiteSpace(txtUsrname.Text.Trim()))
                {
                    result.Result = false;
                    result.Data = "请输入用户名。";
                    txtUsrname.Select();
                    return result;
                }
                if (string.IsNullOrWhiteSpace(txtPassword.Text.Trim()))
                {
                    result.Result = false;
                    result.Data = "请输入密码。";
                    txtPassword.Select();
                    return result;
                }
                if (string.IsNullOrWhiteSpace(txtDomain.Text.Trim()))
                {
                    result.Result = false;
                    result.Data = "请输入域名称。";
                    txtDomain.Select();
                    return result;
                }
            }
            result.Result = true;
            return result;
        }

        private void btnTestProxy_Click(object sender, EventArgs e)
        {
            TBoolResult<string> result = validInputs();
            if (!result.Result)
            {
                PromptBox.Alert(result.Data, "提示");
                return;
            }
            if (validateProxy(getProxyInfo()))
            {
                PromptBox.Alert("连接成功！", "代理");
            }
            else
            {
                PromptBox.Alert("连接失败，请核对代理信息！", "代理");
            }
        }

        private void chkEnabledProxy_CheckedChanged(object sender, EventArgs e)
        {
            CheckBoxBase chk = sender as CheckBoxBase;
            if (chk == null) return;
            btnTestProxy.Enabled = chk.Checked;
        }

        private void cboSelectedIndexChanged(object sender, EventArgs e)
        {
            pnlContainer.Select();
        }
    }
}
