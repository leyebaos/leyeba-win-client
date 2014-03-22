using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Runtime.Serialization;

namespace Util.JsonData
{
    /// <summary>
    /// 项目任务
    /// </summary>
    [DataContract]
    public class ProjectTask : BaseJsonData
    {
        /// <summary>
        /// 任务唯一编号
        /// </summary>
        [DataMember(Name = "taskid")]
        public int ID { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        [DataMember(Name = "taskname")]
        public string Name { get; set; }
        /// <summary>
        /// 项目任务编号
        /// </summary>
        [DataMember(Name = "projecttaskid")]
        public string ProjectTaskId { get; set; }
        /// <summary>
        /// 责任人编号
        /// </summary>
        [DataMember(Name = "dutyuid")]
        public int DutyUid { get; set; }
        /// <summary>
        /// 任务进度
        /// </summary>
        [DataMember(Name = "completerate")]
        public int CompleteRate { get; set; }

        public ProjectTask() { }

        private static string usrpath = string.Empty;
        static ProjectTask()
        { 
            usrpath = Path.Combine(Global.TempDirs, User.CurrentUser.UserId);
        }

        /// <summary>
        /// 将提交超时的日志保存为文件
        /// </summary>
        /// <param name="logs">超时日志字典</param>
        /// <returns></returns>
        public static bool SaveTaskTimingToFile(TaskTiming timing)
        {
            if (!Directory.Exists(usrpath))
            {
                Directory.CreateDirectory(usrpath);
                File.SetAttributes(Global.TempDirs, FileAttributes.Hidden);
            }
            string fileName = 
                string.Format(
                "tasktiming{0}.log",
                timing.WorkDate.ToString("yyyyMMdd"));
            return BinaryHelper.SaveObjectToFile(Path.Combine(usrpath, fileName), timing);
        }

        public static void DeleteTaskTimingFile(DateTime dt)
        {
            string fileName =
                string.Format(
                "tasktiming{0}.log",
                dt.ToString("yyyyMMdd"));
            string timgingFile = Path.Combine(usrpath, fileName);
            try
            {
                if (File.Exists(timgingFile))
                    File.Delete(timgingFile);
            }
            catch (Exception exp)
            {
                Log.error(typeof(BinaryHelper), (exp.InnerException == null ? "" : exp.InnerException.ToString()) + exp.Message);
            }
        }

        public static TaskTiming TaskTiming
        {
            get {
                object obj = AppDomain.CurrentDomain.GetData("tasktiming");
                if (obj == null)
                {
                    //obj = GetAutoSaveWorkTiming(DateTime.Now);
                    obj = ProjectTask.GetPreviousWorkTiming();
                }
                
                return obj as TaskTiming;
            }
            set {
                AppDomain.CurrentDomain.SetData("tasktiming", value);
            }
        }

        public static TaskTiming GetPreviousWorkTiming()
        {
            //目录不存在直接返回
            if (!Directory.Exists(usrpath))
                return null;
            string[] files = 
                Directory.GetFiles(usrpath, "tasktiming*.log");
            if (files == null || 
                files.Length == 0)
                return null;
            Array.Sort(files);
            TaskTiming taskTiming =
                BinaryHelper.FromObjectTo<TaskTiming>(files[files.Length - 1]);
            ProjectTask.TaskTiming = taskTiming;
            return taskTiming;
        }

        public static TaskTiming GetAutoSaveWorkTiming(DateTime dt)
        {
            //目录不存在直接返回
            if (!Directory.Exists(usrpath))
                return null;
            string fileName =
                string.Format(
                "tasktiming{0}.log",
                dt.ToString("yyyyMMdd"));
            string taskTimgingFile = Path.Combine(usrpath, fileName);
            if (!File.Exists(taskTimgingFile))
                return null;
            TaskTiming taskTiming =
                BinaryHelper.FromObjectTo<TaskTiming>(taskTimgingFile);
            ProjectTask.TaskTiming = taskTiming;
            return taskTiming;
        }
    }
}
