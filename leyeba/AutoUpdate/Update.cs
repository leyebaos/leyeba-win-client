using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AutoUpdate
{
    [DataContract]
    public class Update
    {
        public Update() { }

        /// <summary>
        /// 0 没有更新版本 、 1 有更新版本
        /// </summary>
        [DataMember(Name = "Staus")]
        public string Status { get; set; }
        /// <summary>
        /// 更新版本下载地址
        /// </summary>
        [DataMember(Name = "Path")]
        public string Path { get; set; }
        /// <summary>
        /// 更新文件大小
        /// </summary>
        [DataMember(Name = "Size")]
        public int Size { get; set; }
        /// <summary>
        /// 更新发布时间
        /// </summary>
        [DataMember(Name = "UpdateTime")]
        public string UpdateTime { get; set; }

        public static Update GetUpdate(string mainVer)
        {
            string url = ConfigurationManager.ConnectionStrings["updateUrl"].ConnectionString;
            if (string.IsNullOrEmpty(url))
                return null;
            NameValueCollection c = new NameValueCollection();
            c.Add("Type", "1");
            c.Add("Version", mainVer);
            string result = WebHelper.GetWebResponseString(url, c, Encoding.UTF8);
            if (string.IsNullOrEmpty(result))
                return null;
            return JsonHelper.FromJsonTo<Update>(result);
        }
    }
}
