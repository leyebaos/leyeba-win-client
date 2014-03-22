using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Net;
using System.Runtime.Serialization;
using System.Text;

namespace Util.JsonData
{
    /// <summary>
    /// 项目数据
    /// </summary>
    [DataContract]
    public class ProjectData
    {
        /// <summary>
        /// 项目唯一编号
        /// </summary>
        [DataMember(Name = "pid")]
        public int PId { get; set; }
        /// <summary>
        /// 项目标题
        /// </summary>
        [DataMember(Name = "title")]
        public string Title { get; set; }
        /// <summary>
        /// 项目进度
        /// </summary>
        [DataMember(Name = "completerate")]
        public int CompleteRate { get; set; }
        /// <summary>
        /// 项目任务
        /// </summary>
        [DataMember(Name = "tasks")]
        public List<ProjectTask> Tasks { get; set; }
    }
    /// <summary>
    /// 项目
    /// </summary>
    [DataContract]
    public class WorkProject : BaseJsonData
    {
        [DataMember(Name = "Data")]
        public List<ProjectData> ProjectList { get; set; }
        /// <summary>
        /// 获取项目列有
        /// </summary>
        /// <param name="token">用户token</param>
        /// <returns>项目列表</returns>
        public static WorkProject GetProjectList(string token)
        {
            string url = ConfigurationManager.ConnectionStrings["queryProjUrl"].ConnectionString;
            if (string.IsNullOrEmpty(url))
                return new WorkProject {
                    Status = "0",
                    Reason = "配置当中未找到queryProjUrl！"
                };
            NameValueCollection c = new NameValueCollection();
            c.Add("Token", token);
            string result = WebHelper.GetWebResponseString(url, c, Encoding.UTF8);
            if (result == null) return null;
            return JsonHelper.FromJsonTo<WorkProject>(result); 
        }
        /// <summary>
        /// 异步获取项目列表
        /// </summary>
        /// <param name="token">用户token</param>
        /// <param name="handler">事件</param>
        public static void GetProjectListAsync(string token, DownloadDataCompletedEventHandler handler)
        {
            string url = ConfigurationManager.ConnectionStrings["queryProjUrl"].ConnectionString;
            if (string.IsNullOrEmpty(url))
            {
                Log.error(typeof(WorkProject), "配置当中未找到queryProjUrl！");
                return;
            }
            NameValueCollection c = new NameValueCollection();
            c.Add("Token", token);
            WebHelper.GetWebResponseDataAsync(url, c, handler);
        }
        /// <summary>
        /// 获取项目类
        /// </summary>
        public static WorkProject Project
        {
            get {
                return AppDomain.CurrentDomain.GetData("Project") as WorkProject;
            }
            set {
                AppDomain.CurrentDomain.SetData("Project", value);
            }
        }

        public static List<KeyValuePair<string, int>> GetProjKVPList(WorkProject proj)
        {
            if (proj == null ||
                proj.ProjectList == null ||
                proj.ProjectList.Count == 0)
                return null;
            if (proj.Status.Equals("0"))
                return null;
            List<KeyValuePair<string, int>> kvpList =
                new List<KeyValuePair<string, int>>();
            //添加项目到ComboBox
            kvpList.Add(new KeyValuePair<string, int>("请选择项目", -1));
            foreach (ProjectData data in proj.ProjectList)
                kvpList.Add(new KeyValuePair<string, int>(data.Title, data.PId));
            return kvpList;
        }

        public static List<KeyValuePair<string, int>> ProjKVPList
        {
            get {
                object obj = AppDomain.CurrentDomain.GetData("ProjKVPList");
                if (obj == null)
                {
                    List<KeyValuePair<string, int>> kvpList = GetProjKVPList(Project);
                    if (kvpList == null) 
                        return null;
                    ProjKVPList = kvpList;
                    return ProjKVPList;
                }
                return obj as List<KeyValuePair<string, int>>;
            }
            set {
                AppDomain.CurrentDomain.SetData("ProjKVPList", value);
            }
        }
    }
}
