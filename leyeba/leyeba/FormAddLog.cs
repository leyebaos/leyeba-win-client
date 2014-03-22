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
    public partial class FormAddLog : BaseSubForm
    {
        //public LogData NewLog { get; set; }

        public event EventHandler<LogData> AddLog;

        public FormAddLog()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            List<KeyValuePair<string, int>> taskKVPList =
                new List<KeyValuePair<string, int>>();
            taskKVPList.Add(new KeyValuePair<string, int>("临时任务", -1));
            setCboDataSource(cboTask, taskKVPList);
            List<KeyValuePair<string, int>> projKVPList = 
                WorkProject.ProjKVPList;
            if (projKVPList == null)
            {
                //异步下载数据
                WorkProject.GetProjectListAsync(User.CurrentUser.Token, project_DownloadDataCompleted);
            }
            else
            {
                setCboDataSource(cboProject, projKVPList);
                initCboProjEvent();
                if (projKVPList.Count == 2)
                {
                    cboProject.SelectedValue = projKVPList[1].Value;
                }
            }
            base.OnLoad(e);
        }

        private void initCboProjEvent()
        {
            cboProject.SelectedIndexChanged += cboProject_SelectedIndexChanged;
        }

        void cboProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<KeyValuePair<string, int>> taskKVPList =
                new List<KeyValuePair<string, int>>();
            taskKVPList.Add(new KeyValuePair<string, int>("临时任务", -1));
            setCboDataSource(cboTask, taskKVPList);
            ComboBoxBase cbo = sender as ComboBoxBase;
            if (cbo == null || 
                cbo.DataSource == null)
                return;
            if (cbo.SelectedValue == null ||
                cbo.SelectedValue.ToString().Equals("-1"))
                return;
            if (WorkProject.Project == null || 
                WorkProject.Project.ProjectList == null || 
                WorkProject.Project.ProjectList.Count == 0) 
                return;
            ProjectData data =
                WorkProject.Project.ProjectList.FirstOrDefault(p => p.PId.Equals(cbo.SelectedValue)); //获取项目数据类
            int popupWidth = 0;
            if (data != null &&
                data.Tasks != null &&
                data.Tasks.Count > 0)
            {
                foreach (ProjectTask task in data.Tasks)
                {
                    popupWidth = Math.Max(TextRenderer.MeasureText(task.Name, cboProject.Font).Width, popupWidth);
                    taskKVPList.Add(new KeyValuePair<string, int>(task.Name, task.ID));
                }
            }
            setCboDataSource(cboTask, taskKVPList, popupWidth);
        }

        //项目数据下载完成
        private void project_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            if (e.Result == null || e.Result.Length == 0) return;
            WorkProject proj = JsonHelper.FromJsonTo<WorkProject>(Encoding.UTF8.GetString(e.Result));  //反序列化
            if (proj == null ||
                proj.ProjectList == null ||
                proj.ProjectList.Count == 0)
                return;
            if (proj.Status.Equals("0"))
            {
                PromptBox.Alert(proj.Reason, "提示");
                return;
            }
            WorkProject.Project = proj;
            List<KeyValuePair<string, int>> projKVPList =
                new List<KeyValuePair<string, int>>();
            //添加项目到ComboBox
            projKVPList.Add(new KeyValuePair<string, int>("请选择项目", -1));
            int popupWidth = 0;
            foreach (ProjectData data in proj.ProjectList)
            {
                popupWidth = Math.Max(TextRenderer.MeasureText(data.Title, cboProject.Font).Width, popupWidth);
                projKVPList.Add(new KeyValuePair<string, int>(data.Title, data.PId));
            }
            WorkProject.ProjKVPList = projKVPList;
            setCboDataSource(cboProject, projKVPList, popupWidth);
            initCboProjEvent();
            if (proj.ProjectList.Count == 1)
            {
                cboProject.SelectedValue = proj.ProjectList[0].PId;
            }
        }

        private void setCboDataSource(ComboBoxBase cbo, List<KeyValuePair<string, int>> dataSource, int popupWidth = 0)
        {
            cbo.MaxItemHeight = this.Height;
            cbo.PopupSize = new Size(popupWidth + 9, (dataSource.Count + 1) * 20);
            cbo.DisplayMember = "Key";
            cbo.ValueMember = "Value";
            cbo.DataSource = dataSource;
        }

        private void btnConfirm_Click(object sender, EventArgs e) 
        {
            TBoolResult<string> validate = validInputs();
            if (!validate.Result)
            {
                PromptBox.Alert(validate.Data, "提示");
                return;
            }
            LogData log = new LogData();
            KeyValuePair<string, int> projItem = (KeyValuePair<string, int>)cboProject.SelectedItem;
            log.ProjectId = projItem.Value;
            log.ProjectTitle = projItem.Key;
            KeyValuePair<string, int> taskItem = (KeyValuePair<string, int>)cboTask.SelectedItem;
            log.TaskId = taskItem.Value;
            ProjectData data =
                WorkProject.Project.ProjectList.FirstOrDefault(p => p.PId.Equals(cboProject.SelectedValue)); //获取项目数据类
            if (data != null &&
                data.Tasks != null &&
                data.Tasks.Count > 0)
            {
                ProjectTask task = data.Tasks.Find(m => m.ID.Equals(taskItem.Value));
                if (task != null)
                    log.ProjectTaskId = task.ProjectTaskId;
            }
            log.PDate = DateTime.Now.ToString("yyyy-MM-dd");
            log.TaskName = taskItem.Key;
            log.WorkHour = txtWorkHour.Time;
            log.CompleteRate = int.Parse(txtRate.Text);
            log.WorkDetail = txtDetail.Text.Trim();
            //this.DialogResult = DialogResult.OK;
            if (AddLog != null)
            {
                AddLog(this, log);
                cboTask.SelectedValue = -1;
                txtWorkHour.Reset();
                txtRate.Text = string.Empty;                
                txtDetail.Text = string.Empty;
            }
        }

        private TBoolResult<string> validInputs()
        {
            TBoolResult<string> result = new TBoolResult<string>();
            if (cboProject.SelectedValue == null ||
                cboProject.SelectedValue.ToString().Equals("-1"))
            {
                result.Result = false;
                result.Data = "请选择项目。";
                return result;
            }
            if (cboTask.SelectedValue == null)
            {
                result.Result = false;
                result.Data = "请选择任务。";
                return result;
            }
            if (txtWorkHour.Time == 0 ||
                txtWorkHour.Time >= 24)
            {
                result.Result = false;
                result.Data = "输入工时有误，请输入1 - 23表示的时间数。";
                txtWorkHour.Select();
                return result;
            }
            if (string.IsNullOrWhiteSpace(txtRate.Text.Trim()))
            {
                result.Result = false;
                result.Data = "请输入进度。";
                txtRate.Select();
                return result;
            }
            else
            {
                int rate = 0;
                if (int.TryParse(txtRate.Text, out rate))
                {
                    if (rate > 100)
                    {
                        result.Result = false;
                        result.Data = "进度不能超过100。";
                        txtRate.Select();
                        txtRate.SelectAll();
                        return result;
                    }
                }
                else
                {
                    result.Result = false;
                    result.Data = "请输入阿拉伯数字的进度值。";
                    txtRate.Select();
                    txtRate.SelectAll();
                    return result;
                }
            }
            if (string.IsNullOrWhiteSpace(txtDetail.Text.Trim()))
            {
                result.Result = false;
                result.Data = "请输入工作详情。";
                txtDetail.Select();
                return result;
            }
            result.Result = true;
            return result;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
