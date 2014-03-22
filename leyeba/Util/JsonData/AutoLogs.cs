using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;

namespace Util.JsonData
{
    [Serializable]
    [DataContract]
    public class AutoLogs
    {
        /// <summary>
        /// 开发日期
        /// </summary>
        [DataMember(Name = "pdate")]
        public DateTime PDate { get; set; }
        /// <summary>
        /// 使用的程序（一些特殊的时间也用该字段标识，1、所有开发时间，2、键盘使用时间、3、鼠标使用时间）
        /// </summary>
        [DataMember(Name = "useprogram")]
        public string UseProgram { get; set; }
        /// <summary>
        /// 使用时间
        /// </summary>
        [DataMember(Name = "usetime")]
        public string UseTime { get; set; }
        /// <summary>
        /// 使用的秒数
        /// </summary>
        public int UseSeconds { get; set; }

        public AutoLogs() { }

        public static Result Add(string token, AutoLogs log)
        {
            string url = ConfigurationManager.ConnectionStrings["autoLogsUrl"].ConnectionString;
            if (string.IsNullOrEmpty(url))
                return new Result {
                    Status = "0",
                    Reason = "配置当中未找到addLogUrl！"
                }; 
            NameValueCollection c = new NameValueCollection();
            c.Add("Token", token);
            c.Add("pdate", log.PDate.ToString("yyyy-MM-dd"));
            c.Add("useprogram", Convert.ToBase64String(Encoding.UTF8.GetBytes(log.UseProgram)));
            c.Add("usetime", log.UseTime); 
            string result = WebHelper.GetWebResponseString(url, c, Encoding.UTF8);
            if (result == null)
                return new Result {
                    Status = "0",
                    Reason = "网络连接已断开或超时，详情信息请查看错误日志。",
                    Timeout = true
                };
            return JsonHelper.FromJsonTo<Result>(result);
        }
        /// <summary>
        /// 提交自动日志
        /// </summary>
        /// <param name="logs"></param>
        /// <returns>提交失败的日志</returns>
        public static AutologsTiming Submit(AutologsTiming logs)
        {
            AutologsTiming fails = null;
            List<AutoLogs> failLogs = null;
            if (logs == null ||
                logs.AutoLogList == null ||
                logs.AutoLogList.Count == 0)
                return fails;
            try
            {
                foreach (AutoLogs autolog in logs.AutoLogList)
                {
                    if (autolog.UseSeconds < 30)
                        continue;
                    Result res = AutoLogs.Add(User.CurrentUser.Token, autolog);
                    if (res != null &&
                        res.Status.Equals("1"))
                        continue;
                    if (fails == null)
                        fails = new AutologsTiming();
                    if (failLogs == null)
                        failLogs = new List<AutoLogs>();
                    failLogs.Add(autolog);
                    fails.AutoLogList = failLogs;
                    fails.Date = logs.Date;
                }
            }
            catch (Exception exp)
            {
                Log.error(
                    typeof(AutoLogs),
                    (exp.InnerException == null ?
                    "" :
                    exp.InnerException.ToString()) + exp.Message);
            }
            return fails;
        }

        private static string usrpath = Path.Combine(Global.TempDirs, User.CurrentUser.UserId);
        /// <summary>
        /// 将提交超时的日志保存为文件
        /// </summary>
        /// <param name="logs">超时日志字典</param>
        /// <returns></returns>
        public static bool SaveLogsToFile(AutologsTiming logs)
        {
            if (!Directory.Exists(usrpath))
            {
                Directory.CreateDirectory(usrpath);
                File.SetAttributes(Global.TempDirs, FileAttributes.Hidden);
            }
            string fileName =
                string.Format(
                "autologs{0}.log",
                logs.Date.ToString("yyyyMMdd"));
            return BinaryHelper.SaveObjectToFile(Path.Combine(usrpath, fileName), logs);
        }
        /// <summary>
        /// 提交记录的自动日志
        /// </summary>
        public static void Submit()
        {
            //目录不存在直接返回
            if (!Directory.Exists(usrpath))
                return;
            string[] autologsFiles =
                Directory.GetFiles(usrpath, "autologs*");
            if (autologsFiles.Length == 0)
                return;
            ThreadPool.QueueUserWorkItem((state) =>
            {
                try
                {
                    foreach (string fileName in autologsFiles)
                    {
                        if (!File.Exists(fileName)) return;
                        AutologsTiming logs =
                            BinaryHelper.FromObjectTo<AutologsTiming>(fileName);
                        if (logs == null ||
                            logs.AutoLogList == null ||
                            logs.AutoLogList.Count == 0)
                        {
                            File.Delete(fileName);
                            continue;
                        }
                        AutologsTiming result = AutoLogs.Submit(logs);
                        if (result != null &&
                            result.AutoLogList != null &&
                            result.AutoLogList.Count > 0)
                        {
                            if (result.AutoLogList.Count != 
                                logs.AutoLogList.Count)
                            {
                                File.Delete(fileName);
                                AutoLogs.SaveLogsToFile(result);
                            }
                            continue;
                        }
                        else
                        {
                            File.Delete(fileName);
                        }
                    }
                }
                catch (Exception exp)
                {
                    Log.error(typeof(AutoLogs), (exp.InnerException == null ? "" : exp.InnerException.ToString()) + exp.Message);
                }
            });
        }
    }
}
