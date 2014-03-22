using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Util.ConfigManage
{
    public class ProxyInfo
    {
        /// <summary>
        /// 是否启动
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// 服务器地址
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// 服务器端口
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 域
        /// </summary>
        public string Domain { get; set; }
    }
    public class SystemSetting
    {
        public bool AutoLaunch { get; set; }
        public bool AutoUpdate { get; set; }
        public ProxyInfo Proxy { get; set; }

        private string settingFile = string.Empty;
        private string configContent = string.Empty;

        private const string pwdAesKey = "leyebaAESKEY";
        private ConfigManager confManager = null;

        public SystemSetting()
        {
            string confPath = AppDomain.CurrentDomain.BaseDirectory + "\\conf\\";
            settingFile = confPath + "\\SystemSetting.xml";
            
            try
            {
                if (!Directory.Exists(confPath))
                {
                    //配置目录不存在，自动创建。
                    Directory.CreateDirectory(confPath);
                }

                if (!File.Exists(settingFile))
                {
                    StreamWriter sw = new StreamWriter(settingFile);
                    sw.Write(
@"<?xml version=""1.0"" encoding=""utf-8"" ?>
<document>
  <general>
    <autolaunch>0</autolaunch>  <!--自动启动,0表示false，1表示自动启动-->
    <proxy enabled=""false"">       <!--是否启用代理-->
      <host></host>             <!--服务器地址-->
      <port></port>             <!--端口号-->
      <usrname></usrname>       <!--用户名-->
      <pwd></pwd>               <!--密码-->
      <domain></domain>         <!--域-->
    </proxy>
    <autoupdate>1</autoupdate>  <!--自动升级，0表示flase，1表示true-->
  </general>
</document>");
                    sw.Close();
                    sw.Dispose();
                }

                confManager = new ConfigManager(settingFile);
                Init();
            }
            catch (Exception exp)
            {
                Log.error(typeof(UserSetting), (exp.InnerException == null ? "" : exp.InnerException.ToString()) + exp.Message);
            }
        }

        public void Init()
        {
            //自动启动
            this.AutoLaunch = confManager.GetNodeValue("./general/autolaunch") == "1" ? true : false;  
            //网络代理
            string enabledProxy = confManager.GetNodeValue("./general/proxy/@enabled", true);
            if (!string.IsNullOrWhiteSpace(enabledProxy) &&
                enabledProxy.Equals("true"))
            {
                this.Proxy = new ProxyInfo();
                Proxy.Enabled = true;
                Proxy.Host = confManager.GetNodeValue("./general/proxy/host");
                int port = 0;
                string servPort = confManager.GetNodeValue("./general/proxy/port");
                int.TryParse(servPort, out port);
                Proxy.Port = port;
                Proxy.UserName = confManager.GetNodeValue("./general/proxy/usrname");
                Proxy.Password = confManager.GetNodeValue("./general/proxy/pwd");
                if (!string.IsNullOrEmpty(Proxy.Password))
                {
                    try
                    {
                        Proxy.Password = Security.AESDecrypt(Proxy.Password, pwdAesKey);
                    }
                    catch { }
                }
                Proxy.Domain = confManager.GetNodeValue("./general/proxy/domain");
            }
            //是否自动升级
            this.AutoUpdate = confManager.GetNodeValue("./general/autoupdate") == "1" ? true : false;
        }

        public bool Save()
        {
            try
            {
                confManager.UpdateNode("autolaunch", "./general", AutoLaunch == true ? "1" : "0");
                confManager.UpdateNode("autoupdate", "./general", AutoUpdate == true ? "1" : "0");
                if (Proxy != null &&
                    Proxy.Enabled)
                {
                    confManager.UpdateNodeAttributes("./general/proxy/@enabled", "true");
                    confManager.UpdateNode("proxy/host", "./general", Proxy.Host);
                    confManager.UpdateNode("proxy/port", "./general", Proxy.Port.ToString());
                    confManager.UpdateNode("proxy/usrname", "./general", Proxy.UserName);
                    confManager.UpdateNode("proxy/pwd", "./general", Security.AESEncrypt(Proxy.Password, pwdAesKey));
                    confManager.UpdateNode("proxy/domain", "./general", Proxy.Domain);
                }
                confManager.Save();
                return true;
            }
            catch (Exception exp)
            {
                Log.error(typeof(UserSetting), (exp.InnerException == null ? "" : exp.InnerException.ToString()) + exp.Message);
                return false;
            }
        }
    }
}
