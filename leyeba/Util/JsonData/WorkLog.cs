using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Web;

namespace Util.JsonData
{
    /// <summary>
    /// 提交日志类型
    /// </summary>
    [Serializable]
    [DataContract]
    public enum SubmitLogType
    {
        /// <summary>
        /// 添加
        /// </summary>
        Add = 0,
        /// <summary>
        /// 修改
        /// </summary>
        Modify = 1
    }
    /// <summary>
    /// 日志数据
    /// </summary>
    [Serializable]
    [DataContract]
    public class LogData : EventArgs
    {
        /// <summary>
        /// 日志唯一编号
        /// </summary>
        [DataMember(Name = "logid")]
        public int LogId { get; set; }
        /// <summary>
        /// 项目唯一编号
        /// </summary>
        [DataMember(Name = "pid")]
        public int ProjectId { get; set; }
        /// <summary>
        /// 项目任务编号
        /// </summary>
        [DataMember(Name = "projecttaskid")]
        public string ProjectTaskId { get; set; }
        /// <summary>
        /// 项目标题
        /// </summary>
        [DataMember(Name = "title")]
        public string ProjectTitle { get; set; }

        private string pdate = string.Empty;
        /// <summary>
        /// 开发日期
        /// </summary>
        [DataMember(Name = "pdate")]
        public string PDate 
        {
            get {
                return pdate;
            }
            set {
                pdate = value;
                long date_JAVA_Long = 0L;
                long.TryParse(pdate, out date_JAVA_Long);
                DateTime dt_1970 = new DateTime(1970, 1, 1);
                long time_tricks = dt_1970.Ticks + date_JAVA_Long * 10000;//日志日期刻度
                _projectDate = new DateTime(time_tricks).AddHours(8);//转化为DateTime
            }
        }

        private DateTime _projectDate;

        public DateTime ProjectDate
        {
            get { 
                return _projectDate; 
            }
        }
        
        /// <summary>
        /// 任务唯一编号
        /// </summary>
        [DataMember(Name = "taskid")]
        public int TaskId { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        [DataMember(Name = "taskname")]
        public string TaskName { get; set; }
        /// <summary>
        /// 工作时间(单位：秒)
        /// </summary>
        public int WorkSeconds { get; set; }
        /// <summary>
        /// 工时
        /// </summary>
        [DataMember(Name = "workhour")]
        public decimal WorkHour { get; set; }
        /// <summary>
        /// 任务进度
        /// </summary>
        [DataMember(Name = "completerate")]
        public int CompleteRate { get; set; }
        /// <summary>
        /// 工作详情
        /// </summary>
        [DataMember(Name = "workdetail")]
        public string WorkDetail { get; set; }
    }
    /// <summary>
    /// 工作日志
    /// </summary>
    [DataContract]
    public class WorkLog : BaseJsonData
    {
        /// <summary>
        /// 日志数据列表 
        /// </summary>
        [DataMember(Name = "Data")]
        public List<LogData> LogList { get; set; }
        /// <summary>
        /// 获取日志
        /// </summary>
        /// <param name="token">用户身份令牌</param>
        /// <param name="date">日志日期（YYYY-MM-DD格式）</param>
        /// <returns>工作日志类</returns>
        public static WorkLog GetWorkLogAtDate(string token, string strDate, string taskidList)
        {
            string url = ConfigurationManager.ConnectionStrings["queryLogUrl"].ConnectionString;
            if (string.IsNullOrEmpty(url))
                return new WorkLog {
                    Status = "0",
                    Reason = "配置当中未找到queryLogUrl！"
                };
            NameValueCollection c = new NameValueCollection();
            c.Add("Token", token);
            c.Add("pdate", strDate);
            c.Add("Taskidlist", taskidList);
            string result = WebHelper.GetWebResponseString(url, c, Encoding.UTF8);
            if (result == null) 
                return null;
            return JsonHelper.FromJsonTo<WorkLog>(result);
        }
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="token">用户身份令牌</param>
        /// <param name="log">日志内容</param>
        /// <returns>Result</returns>
        public static Result Add(string token, LogData log)
        {
            string url = ConfigurationManager.ConnectionStrings["addLogUrl"].ConnectionString;
            if (string.IsNullOrEmpty(url))
                return new Result {
                    Status = "0",
                    Reason = "配置当中未找到addLogUrl！"
                };
            return submit(url, token, log);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="token">用户身份令牌</param>
        /// <param name="log">日志内容</param>
        /// <returns>Result</returns>
        public static Result Modify(string token, LogData log)
        {
            string url = ConfigurationManager.ConnectionStrings["modifyLogUrl"].ConnectionString;
            if (string.IsNullOrEmpty(url))
                return new Result
                {
                    Status = "0",
                    Reason = "配置当中未找到modifyLogUrl！"
                };
            return submit(url, token, log);
        }

        public static Result submit(string url, string token, LogData log)
        {
            NameValueCollection c = new NameValueCollection();
            c.Add("Token", token);
            c.Add("logid", log.LogId.ToString());
            c.Add("pid", log.ProjectId.ToString());
            c.Add("pdate", log.PDate);
            c.Add("taskid", log.TaskId.ToString());
            c.Add("taskname", Convert.ToBase64String(Encoding.UTF8.GetBytes(log.TaskName)));
            c.Add("workhour", log.WorkHour.ToString());
            c.Add("completerate", log.CompleteRate.ToString());
            c.Add("workdetail", Convert.ToBase64String(Encoding.UTF8.GetBytes(log.WorkDetail)));
            string result = WebHelper.GetWebResponseString(url, c, Encoding.UTF8);
            if (result == null)
                return new Result
                {
                    Status = "0",
                    Reason = "网络连接已断开或超时，详情信息请查看错误日志。",
                    Timeout = true
                };
            return JsonHelper.FromJsonTo<Result>(result);
        }

        /// <summary>
        /// 删除日志
        /// </summary>
        /// <param name="token">用户身份令牌</param>
        /// <param name="logId">日志ID</param>
        /// <returns>Result</returns>
        public static Result Delete(string token, string logId)
        {
            string url = ConfigurationManager.ConnectionStrings["delLogUrl"].ConnectionString;
            if (string.IsNullOrEmpty(url))
                return new Result {
                    Status = "0",
                    Reason = "配置当中未找到delLogUrl！"
                };
            NameValueCollection c = new NameValueCollection();
            c.Add("Token", token);
            c.Add("logid", logId); 
            string result = WebHelper.GetWebResponseString(url, c, Encoding.UTF8);
            if (result == null)
                return new Result {
                    Status = "0",
                    Reason = "网络连接已断开或超时，详情信息请查看错误日志。",
                    Timeout = true
                };
            return JsonHelper.FromJsonTo<Result>(result);
        }

        private static List<KeyValuePair<SubmitLogType, LogData>> timeoutLogs = null;
        public static List<KeyValuePair<SubmitLogType, LogData>> TimeoutLogs
        {
            get {
                return timeoutLogs;
            }
        }
        /// <summary>
        /// 提交日志
        /// </summary>
        /// <param name="pair"></param>
        public static List<Result> SubmitLog(List<KeyValuePair<SubmitLogType, LogData>> logs)
        {
            List<Result> lstResult = new List<Result>();
            if (logs == null || 
                logs.Count == 0) 
                return lstResult;
            timeoutLogs = new List<KeyValuePair<SubmitLogType, LogData>>();
            foreach (KeyValuePair<SubmitLogType, LogData> pair in logs)
            {
                Result result = null;
                switch (pair.Key)
                {
                    case SubmitLogType.Add:
                        result = WorkLog.Add(User.CurrentUser.Token, pair.Value);
                        break;
                    case SubmitLogType.Modify:
                        result = WorkLog.Modify(User.CurrentUser.Token, pair.Value);
                        break;
                }
                if (result == null)
                    continue;
                if (validateSubmitLogResult(pair, result))
                    continue;
                result.ID = pair.Value.LogId;
                result.Name = pair.Value.TaskName;
                lstResult.Add(result);
            }
            if (timeoutLogs != null &&
                timeoutLogs.Count > 0)
                SaveLogsToFile(timeoutLogs);
            return lstResult;
        }
        /// <summary>
        /// 验证提交日志结果
        /// </summary>
        /// <param name="pair"></param>
        /// <param name="result"></param>
        private static bool validateSubmitLogResult(KeyValuePair<SubmitLogType, LogData> pair, Result result)
        {
            if (result == null)
                return false;
            if (!result.Status.Equals("0"))
                return true;
            if (result.Timeout)  //超时
                timeoutLogs.Add(new KeyValuePair<SubmitLogType, LogData>(pair.Key, pair.Value));
            else
                Log.error(typeof(WorkLog), "提交日志：" + result.Reason);
            return false;
        }

        private static string usrpath = Path.Combine(Global.TempDirs, User.CurrentUser.UserId);
        /// <summary>
        /// 将提交超时的日志保存为文件
        /// </summary>
        /// <param name="logs">超时日志字典</param>
        /// <returns></returns>
        public static bool SaveLogsToFile(List<KeyValuePair<SubmitLogType, LogData>> logs)
        {
            if (!Directory.Exists(usrpath))
            {
                Directory.CreateDirectory(usrpath);
                File.SetAttributes(Global.TempDirs, FileAttributes.Hidden);
            }
            string fileName = 
                string.Format(
                "timeoutlog{0}.log", 
                DateTime.Now.ToString("yyyyMMddHHmmss"));
            return BinaryHelper.SaveObjectToFile(Path.Combine(usrpath, fileName), logs);
        }

        public static void ProcessTimeoutLog()
        {
            //目录不存在直接返回
            if (!Directory.Exists(usrpath))
                return;
            string[] worklogFiles =
                Directory.GetFiles(usrpath, "timeoutlog*");
            if (worklogFiles.Length == 0)
                return;
            ThreadPool.QueueUserWorkItem((state) =>
            {
                try
                {
                    foreach (string fileName in worklogFiles)
                    {
                        if (!File.Exists(fileName)) return;
                        List<KeyValuePair<SubmitLogType, LogData>> logs =
                            BinaryHelper.FromObjectTo<List<KeyValuePair<SubmitLogType, LogData>>>(fileName);
                        List<Result> lstResult = WorkLog.SubmitLog(logs);
                        if (lstResult == null ||
                            lstResult.Count == 0)
                        {
                            File.Delete(fileName);
                            continue;
                        }
                        List<Result> timeoutResult =
                            lstResult.FindAll(m => m.Timeout);
                        if (timeoutResult.Count != 0 &&
                            timeoutResult.Count != logs.Count)
                        {
                            File.Delete(fileName);
                            continue;
                        }
                    }
                }
                catch (Exception exp)
                {
                    Log.error(typeof(WorkLog), (exp.InnerException == null ? "" : exp.InnerException.ToString()) + exp.Message);
                }
            });
        }
    }
}
