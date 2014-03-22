using ControlEx;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Util;
using Util.ConfigManage;

namespace leyeba
{
    public partial class FormSystemSetting : BaseSubForm
    {
        private SystemSetting sysSetting =
            ConfigHelper.SystemSettingConfig;

        public FormSystemSetting()
        {
            InitializeComponent();
            initControl();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            loadConfig();
        }
        private void initControl()
        {
            cboProxy.Items.Add("http代理");
            cboProxy.Text = "http代理";
            cboProxy.SelectedValue = "http代理";
        }

        private void loadConfig()
        {
            if (sysSetting == null) return;
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            TBoolResult<string> result = validInputs();
            if (!result.Result)
            {
                PromptBox.Alert(result.Data, "提示");
                return;
            }
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
            if (sysSetting.Save())
            {
                ConfigHelper.SystemSettingConfig = sysSetting;
                PromptBox.Alert("保存成功", "网络设置");
                this.Close();
            }
            else
            {
                PromptBox.Alert("保存失败，请查看错误日志。", "网络设置");
            }
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

        private void chkEnabledProxy_CheckedChanged(object sender, EventArgs e)
        {
            CheckBoxBase chk = sender as CheckBoxBase;
            if (chk == null) return;
            btnTestProxy.Enabled = chk.Checked;
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

        private void cboProxy_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel2.Select();
        }
    }
}
