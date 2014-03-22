using ControlEx;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Util;
using Util.JsonData;

namespace leyeba
{
    public partial class UserCtrlLog : UserControl
    {
        private WorkLog workLog;
        private TaskTiming taskTiming = null;

        public UserCtrlLog()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (User.CurrentUser == null) return;
            initEvent();
            loadLogs(dateTimePicker1.Value.Date);
        }        

        private void loadLocalLog()
        {
            string taskidList = string.Empty;
            List<LogData> newlogList = null;
            taskTiming = ProjectTask.GetAutoSaveWorkTiming(dateTimePicker1.Value.Date);
            if (taskTiming != null &&
                taskTiming.EndWork &&
                taskTiming.WorkLogList != null &&
                taskTiming.WorkLogList.Count > 0)
            {
                newlogList = taskTiming.WorkLogList;

                StringBuilder taskids = new StringBuilder();
                foreach (LogData log in taskTiming.WorkLogList)
                {
                    if (log.CompleteRate <= 0)
                        continue;
                    taskids.Append(log.TaskId);
                    taskids.Append(",");
                }
                if (taskids.Length > 0)
                {
                    taskids.Remove(taskids.Length - 1, 1);
                    taskidList = taskids.ToString();
                }
            }
            loadLogs(dateTimePicker1.Value.Date, taskidList);
            if (newlogList != null && 
                newlogList.Count > 0)
                AppendLogs(newlogList, true);
        }
        /// <summary>
        /// 根据日期日志加载日志
        /// </summary>
        private void loadLogs(DateTime dt, string taskidList = "")
        {
            if (dataGridView1.Rows.Count > 0) 
                dataGridView1.Rows.Clear();
            workLog =
                WorkLog.GetWorkLogAtDate(User.CurrentUser.Token, dt.ToString("yyyy-MM-dd"), taskidList);
            if (workLog == null ||
                workLog.LogList == null ||
                workLog.LogList.Count == 0)
                return;
            if (workLog.Status.Equals("0"))
            {
                PromptBox.Alert(workLog.Reason, "错误");
                return;
            }
            foreach (LogData log in workLog.LogList)
                addDataGridViewRow(log, false);
        }
        private TextBoxTime txtTime = new TextBoxTime();
        private void initEvent()
        {            
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            dataGridView1.DataError += dataGridView1_DataError;
            dataGridView1.CellEndEdit += dataGridView1_CellEndEdit;
        }

        void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.EndEdit();
            if (!dataGridView1.Columns[e.ColumnIndex].Name.Equals("workHourColumn"))
                return;
            DataGridViewCell cell = dataGridView1["workHourColumn", e.RowIndex];
            if (cell.Value == null)
            {
                PopupBox.Alert("工时不允许为空！", "提示");
                cell.Tag = "";
                return;
            }

            string[] strs1 = cell.Value.ToString().Split(':');
            string[] strs2 = cell.Value.ToString().Split('：');
            if (strs1.Length != 2 &&
                strs2.Length != 2)
            {
                PopupBox.Alert("工时填写错误，正确的格式为“小时：分钟”！", "提示");
                cell.Tag = "";
                return;
            }
            int hour = 0, minute = 0;
            if (strs1.Length == 2)
            {
                if (!validateTime(strs1[0], strs1[1], ref hour, ref minute))
                {
                    cell.Tag = "";
                    return;
                }
            }
            else if (strs2.Length == 2)
            {
                if (!validateTime(strs2[0], strs2[1], ref hour, ref minute))
                {
                    cell.Tag = "";
                    return;
                }
            }
            cell.Tag = string.Format("{0}.{1:D2}", hour, minute);
        }

        private bool validateTime(string strHour, string strMinute, ref int hour,ref int minute)
        {
            if (!int.TryParse(strHour, out hour))
            {
                PopupBox.Alert("工时填写错误，正确的格式为“小时：分钟”！", "提示");
                return false;
            }
            if (hour > 23)
            {
                PopupBox.Alert("小时不能超过23点！", "提示");
                return false;
            }
            if (!int.TryParse(strMinute, out minute))
            {
                PopupBox.Alert("工时填写错误，正确的格式为“小时：分钟”。", "提示");
                return false;
            }
            if (minute > 59)
            {
                PopupBox.Alert("分钟不能超过59分！", "提示");
                return false;
            }
            return true;
        }

        void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
        }

        void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || 
                e.ColumnIndex == -1) 
                return;
            if (dataGridView1.Columns == null ||
                dataGridView1.Columns.Count == 0 ||
                dataGridView1.Rows == null ||
                dataGridView1.Rows.Count == 0)
                return;
            DataGridViewCell cell = dataGridView1[e.ColumnIndex, e.RowIndex];
            if (cell.GetType() != typeof(DataGridViewLinkCell)) return;
            if (dataGridView1["delColumn", e.RowIndex].Value == null) return;
            if (dataGridView1.Columns[e.ColumnIndex].Name.Equals("delColumn"))
            {
                DialogResult dialogResult = 
                    PromptBox.Question(
                    string.Format(
                    "确定要删除任务\"{0}\"？", 
                    dataGridView1["taskNameColumn", e.RowIndex].Value), 
                    "删除");
                if (DialogResult.OK == dialogResult)
                {
                    if (dataGridView1["idColumn", e.RowIndex].Tag != null &&
                        !dataGridView1["idColumn", e.RowIndex].Tag.ToString().Equals("0"))
                    {
                        Result result = WorkLog.Delete(User.CurrentUser.Token, dataGridView1["idColumn", e.RowIndex].Tag.ToString());
                        if (!result.Status.Equals("0"))
                        {
                            dataGridView1.Rows.RemoveAt(e.RowIndex);
                            PromptBox.Alert("删除成功！", "删除");
                        }
                        else
                        {
                            PromptBox.Alert(result.Reason, "删除");
                        }
                    }
                    else
                    {
                        object objId = dataGridView1["idColumn", e.RowIndex].Value;
                        if (objId == null)
                            return;
                        if (taskTiming != null &&
                            taskTiming.WorkLogList != null &&
                            taskTiming.WorkLogList.Count > 0)
                        {
                            LogData item = taskTiming.WorkLogList.Find(m => m.ProjectTaskId.Equals(objId.ToString()));
                            if (item != null)
                            {
                                taskTiming.WorkLogList.Remove(item);
                                if (taskTiming.WorkLogList.Count == 0)
                                {
                                    ProjectTask.DeleteTaskTimingFile(dateTimePicker1.Value);
                                }
                                else
                                {
                                    ProjectTask.SaveTaskTimingToFile(taskTiming);
                                }
                            }
                        }
                        dataGridView1.Rows.RemoveAt(e.RowIndex);                            
                    }
                }
            }
        }

        public void AppendLogs(List<LogData> logs, bool newLog = false)
        {
            if (logs == null ||
                logs.Count == 0)
                return;
            foreach (LogData log in logs)
                addDataGridViewRow(log, newLog);
        }

        public void LoadLogsAt(DateTime dt)
        {
            if (dateTimePicker1.Value.Date == dt.Date) 
                loadLocalLog();
            else
                dateTimePicker1.Value = dt.Date;
        }

        private void addDataGridViewRow(LogData log, bool newLog)
        {
            int hour = 0, minute = 0;
            DataGridViewRow row = null;
            if (log.TaskId != -1)
                row = getEqualsLogRowAt(log.TaskId.ToString());

            string workHour = string.Empty;
            if (!string.IsNullOrEmpty(log.WorkHour.ToString()))
            {
                string[] strs = log.WorkHour.ToString("F2").Split('.');
                if (strs.Length > 1)
                {
                    int.TryParse(strs[0], out hour);
                    int.TryParse(strs[1], out minute);
                    workHour = string.Format("{0:D2}:{1:00}", hour, minute);
                }
                else
                {
                    int.TryParse(strs[0], out hour);
                    workHour = string.Format("{0:D2}:00", hour);
                }
            }
            if (row == null)
            {
                int rowIndex = dataGridView1.Rows.Add();
                if (newLog)
                    this.dataGridView1.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 153);
                else
                    this.dataGridView1.Rows[rowIndex].DefaultCellStyle.BackColor = Color.White;
                this.dataGridView1["idColumn", rowIndex].Value = log.ProjectTaskId;
                if (dateTimePicker1.Value.Date == log.ProjectDate.Date)
                    this.dataGridView1["idColumn", rowIndex].Tag = log.LogId;
                this.dataGridView1["projNameColumn", rowIndex].Value = log.ProjectTitle;
                this.dataGridView1["projNameColumn", rowIndex].Tag = log.ProjectId;
                this.dataGridView1["taskNameColumn", rowIndex].Value = log.TaskName;
                this.dataGridView1["taskNameColumn", rowIndex].Tag = log.TaskId;
                this.dataGridView1["workHourColumn", rowIndex].Value = workHour;
                this.dataGridView1["workHourColumn", rowIndex].Tag = log.WorkHour;
                this.dataGridView1["rateColumn", rowIndex].Value = log.CompleteRate;
                this.dataGridView1["detailColumn", rowIndex].Value = log.WorkDetail;
                this.dataGridView1["delColumn", rowIndex].Value = "删除";
            }
            else
            {
                if (newLog)
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 153);
                else
                    row.DefaultCellStyle.BackColor = Color.White;
                if (row.Cells["workHourColumn"].Tag != null)
                {
                    decimal oldhour = 0m;
                    decimal.TryParse(row.Cells["workHourColumn"].Tag.ToString(), out oldhour);
                    decimal newhour = 0m;
                    decimal.TryParse(log.WorkHour.ToString(), out newhour);
                    decimal newTime = oldhour + newhour;
                    string wh = newTime.ToString("F2");
                    string[] strs = wh.Split('.');
                    if (strs.Length > 1)
                    {
                        int.TryParse(strs[0], out hour);
                        int.TryParse(strs[1], out minute);
                        workHour = string.Format("{0:D2}:{1:D2}", hour, minute);
                    }
                    else
                    {
                        int.TryParse(strs[0], out hour);
                        workHour = string.Format("{0:D2}:00", hour);
                    }
                    row.Cells["workHourColumn"].Tag = wh;
                }
                else
                {
                    row.Cells["workHourColumn"].Tag = log.WorkHour;
                }
                row.Cells["workHourColumn"].Value = workHour;
            }
        }

        private DataGridViewRow getEqualsLogRowAt(string taskid)
        {
            DataGridViewRow equalsRow = null;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                object obj = row.Cells["taskNameColumn"].Tag;
                if (obj == null)
                    continue;
                if (obj.ToString().Equals(taskid))
                    equalsRow = row;
            }
            return equalsRow;
        }

        private void btnAddLog_Click(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value.Date > DateTime.Now.Date)
            {
                PromptBox.Alert("当前选择日期有误，请重新选择。", "添加");
                return;
            }
            FormAddLog logDetail = new FormAddLog();
            logDetail.StartPosition = FormStartPosition.CenterParent;
            logDetail.AddLog +=
                (s, log) => {
                    if (log == null) 
                        return;
                    addDataGridViewRow(log, true);
                };
            logDetail.ShowDialog();
            //if (DialogResult.OK == 
            //    logDetail.ShowDialog())
            //{
            //    LogData data = logDetail.NewLog;
            //    if (data == null) return;
            //    addDataGridViewRow(data, true);
            //}
        }
        //提交日志
        private void btnSubmitLog_Click(object sender, EventArgs e)
        {
            try
            {
                btnSubmitLog.Enabled = false;
                if (dateTimePicker1.Value.Date > DateTime.Now.Date)
                {
                    PromptBox.Alert("当前选择日期有误，请重新选择。", "添加");
                    return;
                }
                if (dataGridView1.Rows.Count == 0)
                    return;
                dataGridView1.EndEdit();
                List<KeyValuePair<SubmitLogType, LogData>> submitLogs =
                    new List<KeyValuePair<SubmitLogType, LogData>>();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    int projid = 0;
                    int.TryParse(row.Cells["projNameColumn"].Tag.ToString(), out projid);
                    int taskid = 0;
                    int.TryParse(row.Cells["taskNameColumn"].Tag.ToString(), out taskid);
                    object objWorkHour = row.Cells["workHourColumn"].Tag ?? string.Empty;
                    if (string.IsNullOrEmpty(objWorkHour.ToString()))
                    {
                        PopupBox.Alert("工时信息错误，正确的格式为“小时：分钟”！", "提示");
                        selectedCell(row.Cells["workHourColumn"]);
                        return;
                    }
                    decimal workHour = 0m;
                    if (!decimal.TryParse(objWorkHour.ToString(), out workHour))
                    {
                        PopupBox.Alert("工时信息错误，正确的格式为“小时：分钟”！", "提示");
                        selectedCell(row.Cells["workHourColumn"]);
                        return;
                    }
                    if (workHour <= 0)
                    {
                        PopupBox.Alert("工时信息错误，不能小于或等于0！", "提示");
                        selectedCell(row.Cells["workHourColumn"]);
                        return;
                    }
                    int rate = 0;
                    if (row.Cells["rateColumn"].Value == null ||
                        !int.TryParse(row.Cells["rateColumn"].Value.ToString(), out rate))
                    {
                        PopupBox.Alert("完成率信息错误，正确的完成率为1-100之间的整数！", "提示");
                        selectedCell(row.Cells["rateColumn"]);
                        return;
                    }
                    if (rate <= 0 ||
                        rate > 100)
                    {
                        PopupBox.Alert("工作完成率填写错误，不能小于等于0或大于100！", "提示");
                        selectedCell(row.Cells["rateColumn"]);
                        return;
                    }
                    if (row.Cells["detailColumn"].Value == null ||
                        string.IsNullOrEmpty(row.Cells["detailColumn"].Value.ToString()))
                    {
                        PopupBox.Alert("工作详情不能为空！", "提示");
                        selectedCell(row.Cells["detailColumn"]);
                        return;
                    }

                    object logid = row.Cells["idColumn"].Tag;
                    if (logid == null ||
                        logid.ToString().Equals("0"))
                    {
                        //新添加
                        LogData log =
                            new LogData
                            {
                                ProjectId = projid,
                                PDate = dateTimePicker1.Value.ToString("yyyy-MM-dd"),
                                TaskId = taskid,
                                TaskName = row.Cells["taskNameColumn"].Value.ToString(),
                                WorkHour = workHour,
                                CompleteRate = rate,
                                WorkDetail = (row.Cells["detailColumn"].Value ?? "").ToString()
                            };
                        submitLogs.Add(new KeyValuePair<SubmitLogType, LogData>(SubmitLogType.Add, log));
                    }
                    else
                    {
                        //修改
                        if (workLog == null ||
                            workLog.LogList == null ||
                            workLog.LogList.Count == 0)
                            continue;
                        LogData originLog =
                            workLog.LogList.FirstOrDefault(p => p.LogId.Equals(logid));
                        if (originLog != null &&
                            originLog.WorkHour.Equals(workHour) &&
                            originLog.CompleteRate.Equals(rate) &&
                            originLog.WorkDetail.Equals(row.Cells["detailColumn"].Value.ToString()))
                            continue;
                        //if (rate < originLog.CompleteRate)
                        //{
                        //    PopupBox.Alert(
                        //        string.Format(
                        //        "工作完成率不能小于上次提交的值，上次提交的值为{0}。",
                        //        originLog.CompleteRate),
                        //        "提示");
                        //    selectedCell(row.Cells["rateColumn"]);
                        //    return;
                        //}
                        LogData log =
                            new LogData
                            {
                                LogId = originLog.LogId,
                                ProjectId = originLog.ProjectId,
                                PDate = dateTimePicker1.Value.ToString("yyyy-MM-dd"),
                                TaskId = originLog.TaskId,
                                TaskName = originLog.TaskName,
                                WorkHour = workHour,
                                CompleteRate = rate,
                                WorkDetail = row.Cells["detailColumn"].Value.ToString()
                            };
                        submitLogs.Add(new KeyValuePair<SubmitLogType, LogData>(SubmitLogType.Modify, log));
                    }
                }
                if (submitLogs == null ||
                    submitLogs.Count == 0)
                    return;
                ProjectTask.DeleteTaskTimingFile(dateTimePicker1.Value);
                List<Result> lstResult = WorkLog.SubmitLog(submitLogs);
                if (lstResult != null && lstResult.Count > 0)
                {
                    string err = Environment.NewLine;
                    foreach (Result result in lstResult)
                        err += string.Format(
                            "日志：{0}{1}，{2}{3}",
                            result.ID,
                            result.Name,
                            result.Reason,
                            Environment.NewLine);
                    err = err.TrimEnd(Environment.NewLine.ToCharArray());
                    err += "。";
                    PromptBox.Alert("提交失败，错误信息如下：" + err, "提交日志");
                    return;
                }                
                PromptBox.Alert("提交成功！", "提交日志");
                //TaskTiming timing = ProjectTask.GetPreviousWorkTiming();
                //if (timing != null &&
                //    timing.WorkLogList != null &&
                //    timing.WorkLogList.Count > 0)
                //{
                //    LoadLogsAt(timing.WorkDate);
                //}
                //else
                //{
                //    loadLogs(dateTimePicker1.Value);
                //}
                loadLogs(dateTimePicker1.Value.Date);
            }
            catch (Exception exp)
            {
                Log.error(this.GetType(), (exp.InnerException == null ? "" : exp.InnerException.ToString()) + exp.Message);
            }
            finally
            {
                btnSubmitLog.Enabled = true;
            }
        }

        private void selectedCell(DataGridViewCell cell)
        {
            this.ActiveControl = dataGridView1;
            dataGridView1.Focus();
            dataGridView1.ClearSelection();
            dataGridView1.CurrentCell = cell;
            cell.Selected = true;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            loadLocalLog();
        }
    }
}
