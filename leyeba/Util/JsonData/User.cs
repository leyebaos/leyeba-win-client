using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Net;
using System.Runtime.Serialization;
using System.Text;

namespace Util.JsonData
{
    /// <summary>
    /// 用户信息类
    /// </summary>
    [DataContract]
    public class User : BaseJsonData
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        [DataMember]
        public string UserName { get; set; }
        /// <summary>
        /// 用户身份令牌（Status=1时填写）
        /// </summary>
        [DataMember]
        public string Token { get; set; }

        public User() { }

        /// <summary>
        /// 获取当前登陆用户信息类
        /// </summary>
        public static User CurrentUser
        {
            get {
                return AppDomain.CurrentDomain.GetData("CurrentUser") as User;
            }
            set {
                AppDomain.CurrentDomain.SetData("CurrentUser", value);
            }
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>用户信息类</returns>
        public static User Login(string username, string password)
        {
            string url = ConfigurationManager.ConnectionStrings["loginUrl"].ConnectionString;
            if (string.IsNullOrEmpty(url))
                return new User {
                    Status = "0",
                    Reason = "配置当中未找到loginUrl！"
                };
            NameValueCollection c = new NameValueCollection();
            c.Add("UserName", username);
            c.Add("Password", password);
            string result = WebHelper.GetWebResponseString(url, c, Encoding.UTF8);
            if (result == null)
                return new User {
                    Status = "0",
                    Reason = "网络连接已断开或超时！"
                };
            return JsonHelper.FromJsonTo<User>(result);
        }
        /// <summary>
        /// 注销当前用户
        /// </summary>
        /// <param name="token"></param>
        public static void Logoff(string token)
        {
            string url = ConfigurationManager.ConnectionStrings["logoffUrl"].ConnectionString;
            if (string.IsNullOrEmpty(url)) {
                Log.error(typeof(WorkProject), "配置当中未找到logoffUrl！");
                return;
            }
            NameValueCollection c = new NameValueCollection();
            c.Add("Token", token);
            WebHelper.GetWebResponseData(url, c);
        }
    }
}
