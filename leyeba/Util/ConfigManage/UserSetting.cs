using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Util;

namespace Util.ConfigManage
{
    /// <summary>
    /// 系统设置，存储类。
    /// </summary>
    public class UserSetting
    {
        public bool AutoLogin { get; set; }
        public bool RememberPwd { get; set; }
        /// <summary>
        /// 0退出，1提示
        /// </summary>
        public bool ExitPrompt{ get; set; }
        public String UserId { get; set; }
        public string Pwd { get; set; }
        public Point FloatPos { get; set; }
        public int DailyWorkHour { get; set; }
        public string FloatClickType { get; set; }

        private const string pwdAesKey = "leyebaAESKEY";
        private string settingFile = string.Empty;
        private string configContent = string.Empty;
        private string userPath = "./user[@id=\"{0}\"]";
        private ConfigManager confManager = null;
       
        public UserSetting()
        {
            string confPath = AppDomain.CurrentDomain.BaseDirectory + "\\conf\\";
            settingFile = confPath + "\\UserSetting.xml";
            configContent =
"<user id=\"{0}\" pwd=\"\" default=\"false\">" +
"<autologin>0</autologin>    <!--自动登录,0表示false，1表示自动登录-->" +
"<rememberpwd>0</rememberpwd><!--记住密码,0表示false，1表示记住密码-->" +
"<exitprompt>0</exitprompt>  <!--关闭程序时，0表示隐藏，1表示退出-->" +
"<float x=\"\" y=\"\" />     <!--记录上次退出程序时浮动窗口的坐标-->" +
"<dailyworkhour>8</dailyworkhour>  <!--记录上次退出程序时浮动窗口的坐标-->" +
"<floatclick>1</floatclick>   <!--浮动窗体点击设置，0单击显示主界面，1表示双击显示主界面-->" +
"</user>";

            try
            {
                if (!Directory.Exists(confPath))
                {
                    //配置目录不存在，自动创建。
                    Directory.CreateDirectory(confPath);
                }

                confManager = new ConfigManager(settingFile);
                this.UserId = confManager.GetNodeValue("./user[@default=\"true\"]/@id", true);
                Init(this.UserId);
            }
            catch (Exception exp)
            {
                Log.error(typeof(UserSetting), (exp.InnerException == null ? "" : exp.InnerException.ToString()) + exp.Message);
            }
        }

        public void Init(string id)
        {
            if (string.IsNullOrEmpty(id)) return;
            string parentPath = string.Format(userPath, id);
            this.AutoLogin = confManager.GetNodeValue(parentPath + "/autologin") == "1" ? true : false;
            this.RememberPwd = confManager.GetNodeValue(parentPath + "/rememberpwd") == "1" ? true : false;
            this.ExitPrompt = confManager.GetNodeValue(parentPath + "/exitprompt") == "1" ? true : false;
            this.UserId = confManager.GetNodeValue(parentPath + "/@id", true);
            this.Pwd = confManager.GetNodeValue(parentPath + "/@pwd", true);
            //浮动窗位置
            string x = confManager.GetNodeValue(parentPath + "/float/@x", true);
            string y = confManager.GetNodeValue(parentPath + "/float/@y", true);
            int left = 0, top = 0;
            if (int.TryParse(x, out left) &&
                int.TryParse(y, out top))
            {
                this.FloatPos = new Point(left, top); 
            }
            //工时
            string workhourstr = confManager.GetNodeValue(parentPath + "/dailyworkhour");
            int workhour = 0;
            if (int.TryParse(workhourstr, out workhour))
            {
                this.DailyWorkHour = workhour;
            }
            //浮动窗点击执行事件
            this.FloatClickType = confManager.GetNodeValue(parentPath + "/floatclick");
        }

        public string[] GetUsers()
        {
            List<System.Xml.Linq.XElement> lstEle = confManager.GetElements("user");
            List<string> users = new List<string>();
            foreach (System.Xml.Linq.XElement ele in lstEle)
            {
                users.Add(ele.Attribute("id").Value);
            }

            return users.ToArray();
        }

        public bool Save()
        {
            try
            {
                string parentPath = string.Format(userPath, this.UserId);
                if (!confManager.IsExitElement(parentPath))
                {
                    confManager.AppendXElement(string.Format(configContent, this.UserId));
                }
                confManager.UpdateNode("autologin", parentPath, AutoLogin == true ? "1" : "0");
                confManager.UpdateNode("rememberpwd", parentPath, RememberPwd == true ? "1" : "0");
                confManager.UpdateNode("exitprompt", parentPath, ExitPrompt == true ? "1" : "0");
                confManager.UpdateNode("dailyworkhour", parentPath, DailyWorkHour == 0 ? "8" : DailyWorkHour.ToString());
                if (FloatClickType != null)
                    confManager.UpdateNode("floatclick", parentPath, FloatClickType);
                confManager.UpdateNodeAttributes(parentPath + "/@id", this.UserId);
                confManager.UpdateNodeAttributes(parentPath + "/@pwd", this.Pwd);
                confManager.UpdateAllAttribute("user", "default", "false");
                confManager.UpdateNodeAttributes(parentPath + "/@default", "true");
                confManager.Save();
                this.Init(this.UserId);
                return true;
            }
            catch (Exception exp)
            {
                Log.error(typeof(UserSetting), (exp.InnerException == null ? "" : exp.InnerException.ToString()) + exp.Message);
                return false;
            }
        }

        public void UpdateFloatPos(Point pt)
        {
            string parentPath = string.Format(userPath, this.UserId);
            confManager.UpdateNodeAttributes(parentPath + "/float/@x", pt.X.ToString());
            confManager.UpdateNodeAttributes(parentPath + "/float/@y", pt.Y.ToString());
            confManager.Save();
        }
    }
}
