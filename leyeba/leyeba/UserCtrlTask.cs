using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using ControlEx;
using Util;
using Util.ConfigManage;
using Util.JsonData;
using System.Diagnostics;

namespace leyeba
{
    public partial class UserCtrlTask : UserControl
    {
        #region//私有变量
        private WorkProject proj = null;
        private Font perFont = new Font("方正姚体", 12, FontStyle.Bold);
        private Pen penArc = new Pen(Color.FromArgb(0, 178, 249), 4);
        private Rectangle recEllpise = Rectangle.Empty;

        private Bitmap progressBall = leyeba.Properties.Resources.progress_ball;
        private Bitmap progressBallBg = leyeba.Properties.Resources.progress_ball_bg;
        private int progressBallOffset = 6;

        private TimerControl workingTimerCtl = null;

        public DateTime workDate = DateTime.Now.Date;
        private bool isworking = false;
        private int lastWorkSeconds = 0;
        private int workingSeconds = 0;
        private Timer workTimer = null;
        private int currentWorkPer = 0;
        private TaskTiming taskTiming = null;
        private AutologsTiming autologsTiming = null;
        private List<LogData> lstWorkLog = new List<LogData>();
        private List<TimerControl> lstWorkCtl = new List<TimerControl>();
        private List<AutoLogs> lstAutologs = new List<AutoLogs>();
                
        #endregion

        public UserCtrlTask()
        {
            InitializeComponent();
            toolTip1.SetToolTip(btnReload, "刷新项目和任务列表");
            workTimer = new Timer();
            workTimer.Enabled = false;
            workTimer.Interval = 1000;
            workTimer.Tick += workTimer_Tick;

            initListView();

            RectangleF progressRec = 
                new RectangleF(
                    progressBallOffset, 
                    progressBallOffset,
                    progressBall.Width - 2 * progressBallOffset,
                    progressBall.Height - 2 * progressBallOffset);
            progressBall = progressBall.Clone(progressRec, progressBall.PixelFormat);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (User.CurrentUser == null) return;
            TaskTiming timing = ProjectTask.TaskTiming;
            if (timing != null)
            {
                if (timing.WorkDate.Date ==
                    DateTime.Now.Date &&
                    !timing.EndWork)
                {
                    workTimer.Start();
                    this.workingSeconds = timing.WorkSeconds;
                    this.lastWorkSeconds = timing.WorkSeconds;
                    lstWorkLog = timing.WorkLogList;
                }
                else
                {
                    if (timing.WorkLogList != null &&
                        timing.WorkLogList.Count > 0 &&
                        timing.WorkLogList.Find(m => m.WorkSeconds >= 30) != null)
                    {
                        if (!timing.EndWork)
                        {
                            timing.EndWork = true;
                            ProjectTask.SaveTaskTimingToFile(timing);
                        }
                        FormMain main = this.ParentForm as FormMain;
                        if (main == null)
                            return;
                        main.LoadLogsAt(timing.WorkDate);
                    }
                    else
                    {
                        ProjectTask.DeleteTaskTimingFile(timing.WorkDate);
                    }
                }
            }
            updateProgress(false);
            //异步加载项目数据
            WorkProject.GetProjectListAsync(
                User.CurrentUser.Token,
                project_DownloadDataCompleted);
        }

        #region//事件
        //项目数据下载完成
        private void project_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            if (e.Result == null ||
                e.Result.Length == 0)
                return;
            proj = JsonHelper.FromJsonTo<WorkProject>(Encoding.UTF8.GetString(e.Result));  //反序列化
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

            cboProject.DisplayMember = "Key";
            cboProject.ValueMember = "Value";
            cboProject.MaxItemHeight = this.Height;
            cboProject.PopupSize = new Size(popupWidth + 9, (projKVPList.Count + 1) * 20);
            cboProject.DataSource = projKVPList; //绑定数据源

            if (selectedProject != null)
            {
                cboProject.SelectedValue = selectedProject;
                selectedProject = null;
            }
            else
            {
                if (proj.ProjectList.Count == 1)
                {
                    cboProject.SelectedValue = proj.ProjectList[0].PId;
                }
            }
        }
        //项目选择改变时
        private void cboProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            settingProgressBarValue(0);
            if (listViewEx1.Items.Count > 0)
                listViewEx1.Items.Clear();
            ComboBoxProject cbo = sender as ComboBoxProject;
            if (cbo == null ||
                cbo.DataSource == null)
                return;
            if (cbo.SelectedValue == null ||
                cbo.SelectedValue.ToString().Equals("-1"))
                return;
            ProjectData data =
                proj.ProjectList.FirstOrDefault(p => p.PId.Equals(cbo.SelectedValue)); //获取项目数据类
            if (data == null ||
                data.Tasks == null ||
                data.Tasks.Count == 0)
                return;
            settingProgressBarValue(data.CompleteRate);
            List<ProjectTask> tasks = new List<ProjectTask>(data.Tasks);
            foreach (ProjectTask task in tasks) //显示任务
                addListViewItem(task);
        }
        //点击列头排序
        private void listViewEx1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (this.listViewEx1.Columns[e.Column].Tag == null)
                this.listViewEx1.Columns[e.Column].Tag = true;
            bool flag = (bool)this.listViewEx1.Columns[e.Column].Tag;
            if (flag)
                this.listViewEx1.Columns[e.Column].Tag = false;
            else
                this.listViewEx1.Columns[e.Column].Tag = true;
            this.listViewEx1.ListViewItemSorter = new ListViewSort(e.Column, this.listViewEx1.Columns[e.Column].Tag);
            this.listViewEx1.Sort();//对列表进行自定义排序  
        }
        //工作时间计时器间隔发生事件
        void workTimer_Tick(object sender, EventArgs e)
        {
            if (isworking)
            {
                //计时
                workingSeconds++;
                //自动日志
                GenAutolog();
                
                if (workingSeconds % 60 == 0)
                {
                    AutoSave();
                }

                updateProgress(true);
            }

            if (DateTime.Now.ToString("HHmm").Equals("0000") &&
                DateTime.Now.Date > workDate)
            {
                this.AutoSave(true);
                foreach (TimerControl ctl in lstWorkCtl)
                    ctl.Reset();
                workingSeconds = 0;
                lstWorkLog.Clear();
                lstWorkCtl.Clear();
                lstAutologs.Clear();
                FormMain main = this.ParentForm as FormMain;
                if (main == null)
                    return;
                main.LoadLogsAt(workDate);
                if (!main.Visible)
                {
                    main.Show();
                }
                if (main.WindowState == FormWindowState.Minimized)
                {
                    main.WindowState = FormWindowState.Normal;
                }
                main.Activate();
                workDate = DateTime.Now.Date;
            }
        }        
        //时间器状态改变时
        void timerCtl_TimerChanged(object sender, EventArgs e)
        {
            TimerControl timerCtl =
                sender as TimerControl;
            if (timerCtl == null ||
                timerCtl.Tag == null)
                return;

            if (timerCtl.IsRunning &&
                !isworking)
            {
                this.StartWorking();
            }

            ListViewItem item =
                timerCtl.Tag as ListViewItem;
            if (item == null ||
                item.Tag == null)
                return;
            ProjectTask task =
                item.Tag as ProjectTask;
            if (task == null)
                return;
            updateWorkLog(task, timerCtl);
            if (!lstWorkCtl.Contains(timerCtl))
                lstWorkCtl.Add(timerCtl);
        }
        //计时单击事件
        void timerCtl_TimerClick(object sender, EventArgs e)
        {
            TimerControl timerCtl = sender as TimerControl;
            if (timerCtl == null ||
                timerCtl.Tag == null)
                return;
            ListViewItem item =
                timerCtl.Tag as ListViewItem;
            if (timerCtl.IsRunning)
            {
                timerCtl.Pause();
            }
            else
            {
                if (workingTimerCtl != null &&
                    workingTimerCtl.IsRunning)
                {
                    workingTimerCtl.Pause();
                }
                workingTimerCtl = timerCtl;
                timerCtl.Start();
            }
        }
        //开始或暂停工作
        private void btnWork_Click(object sender, EventArgs e)
        {
            if (isworking)
                PauseWorking();
            else
                working();
        }
        //结束工作
        private void btnEndWork_Click(object sender, EventArgs e)
        {
            FormFloat.Instance.SetDefault();
            timerCtl_TimerChanged(workingTimerCtl, EventArgs.Empty);
            this.AutoSave(true);
            AutoLogs.Submit();
            StopWorking();
            if (workingTimerCtl != null)
            {
                workingTimerCtl.Stop();
                workingTimerCtl = null;
            }
            foreach (TimerControl ctl in lstWorkCtl)
                ctl.Reset();
            FormMain main = this.ParentForm as FormMain;
            if (main == null)
                return;
            main.LoadLogsAt(DateTime.Now.Date);
            workingSeconds = 0;
            lastWorkSeconds = 0;
            lstWorkLog.Clear();
            lstWorkCtl.Clear();
            lstAutologs.Clear();
            updateProgress(false);
        }

        /// <summary>
        /// 生成键盘鼠标自动日志
        /// </summary>
        public void GenKeyMouseAutoLog()
        {
            int keyWorkSeconds = KeyMouseHook.KeyWorkSeconds;
            int mouseWorkSeconds = KeyMouseHook.MouseWorkSeconds;
            int totalWorkSeconds = workingSeconds - lastWorkSeconds;
            string totalUseTime = Global.ConvertToDecimalTimeAt(totalWorkSeconds).ToString("F2");
            string keyUseTime = Global.ConvertToDecimalTimeAt(keyWorkSeconds).ToString("F2");
            string mouseUseTime = Global.ConvertToDecimalTimeAt(mouseWorkSeconds).ToString("F2");

            AutoLogs totalLog = lstAutologs.Find(m => m.UseProgram.Equals("1"));
            if (totalLog != null)
            {
                totalLog.UseSeconds = totalWorkSeconds;
                totalLog.UseTime = totalUseTime;
            }
            else
            {
                lstAutologs.Add(
                    new AutoLogs {
                        PDate = DateTime.Now.Date,
                        UseProgram = "1",
                        UseSeconds = totalWorkSeconds,
                        UseTime = totalUseTime
                    });
            }
            AutoLogs keyLog = lstAutologs.Find(m => m.UseProgram.Equals("2"));
            if (keyLog != null)
            {
                keyLog.UseSeconds = keyWorkSeconds;
                keyLog.UseTime = keyUseTime;
            }
            else
            {
                lstAutologs.Add(
                    new AutoLogs {
                        PDate = DateTime.Now.Date,
                        UseProgram = "2",
                        UseSeconds = keyWorkSeconds,
                        UseTime = keyUseTime
                    });
            }
            AutoLogs mouseLog = lstAutologs.Find(m => m.UseProgram.Equals("3"));
            if (mouseLog != null)
            {
                mouseLog.UseSeconds = mouseWorkSeconds;
                mouseLog.UseTime = mouseUseTime;
            }
            else
            {
                lstAutologs.Add(
                    new AutoLogs {
                        PDate = DateTime.Now.Date,
                        UseProgram = "3",
                        UseSeconds = mouseWorkSeconds,
                        UseTime = mouseUseTime
                    });
            }
        }
        /// <summary>
        /// 生成自动日志
        /// </summary>
        private void GenAutolog()
        {
            int useSeconds = 0;
            string title = Global.GetActiveProcessName();
            if (!string.IsNullOrEmpty(title))
            {
                AutoLogs autoLog =
                    lstAutologs.Find(m => m.UseProgram.Equals(title, StringComparison.OrdinalIgnoreCase));
                if (autoLog != null)
                {
                    useSeconds = autoLog.UseSeconds;
                    useSeconds++;
                    autoLog.UseSeconds = useSeconds;
                    autoLog.UseTime = Global.ConvertToDecimalTimeAt(useSeconds).ToString("F2");
                }
                else
                {
                    useSeconds++;
                    lstAutologs.Add(
                        new AutoLogs
                        {
                            PDate = DateTime.Now.Date,
                            UseProgram = title,
                            UseSeconds = useSeconds,
                            UseTime = Global.ConvertToDecimalTimeAt(useSeconds).ToString("F2")
                        });
                }
            }
        }

        private object selectedProject = null;
        private void btnReload_Click(object sender, EventArgs e)
        {
            PauseWorking();
            selectedProject = cboProject.SelectedValue;
            //异步加载项目数据
            WorkProject.GetProjectListAsync(
                User.CurrentUser.Token,
                project_DownloadDataCompleted);
        }
        #endregion

        #region//私有方法
        /// <summary>
        /// 初始化ListView
        /// </summary>
        private void initListView()
        {
            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(1, 21);//分别是宽和高
            listViewEx1.SmallImageList = imgList;
        }
        /// <summary>
        /// 设置进度条值
        /// </summary>
        /// <param name="value"></param>
        private void settingProgressBarValue(int value)
        {
            this.progressBarProject1.Value = value;
            this.lblPercent.Text = string.Format("{0}%", value);
            updateProgressLblPos();
        }
        /// <summary>
        /// 设置显示进度标签位置与进度条右对齐
        /// </summary>
        private void updateProgressLblPos()
        {
            int width = progressBarProject1.Right;
            int pos = lblPercent.Right;
            if (pos < width)
            {
                lblPercent.Left = lblPercent.Left + (width - pos);
            }
            else if (pos > width)
            {
                lblPercent.Left = lblPercent.Left - (pos - width);
            }
        }
        /// <summary>
        /// 显示任务列表
        /// </summary>
        /// <param name="task">任务数据类</param>
        private void addListViewItem(ProjectTask task)
        {
            ListViewItem item = new ListViewItem(task.ProjectTaskId.ToString());
            item.ToolTipText = task.Name;
            item.UseItemStyleForSubItems = false;
            ListViewItem.ListViewSubItem subitemTaskName = new ListViewItem.ListViewSubItem();            
            subitemTaskName.ForeColor = Color.FromArgb(37, 100, 176);
            subitemTaskName.Font = new System.Drawing.Font(this.Font.FontFamily.Name, this.Font.Size, FontStyle.Bold);
            subitemTaskName.Text = task.Name;
            item.SubItems.Add(subitemTaskName);
            item.SubItems.Add(task.CompleteRate.ToString());
            item.SubItems.Add(string.Empty);
            this.listViewEx1.Items.Add(item);
            item.Tag = task;
            TimerControl timerCtl = new TimerControl();
            if (lstWorkLog != null && 
                lstWorkLog.Count > 0)
            {
                LogData log = 
                    lstWorkLog.Find(m => m.TaskId.Equals(task.ID));
                if (log != null &&
                    log.WorkSeconds > 0)
                {
                    timerCtl.Seconds = log.WorkSeconds;
                    lstWorkCtl.Add(timerCtl);
                    this.workingTimerCtl = timerCtl;
                }
            }
            timerCtl.Tag = item;
            timerCtl.TimerClick += timerCtl_TimerClick;
            timerCtl.TimerChanged += timerCtl_TimerChanged;
            listViewEx1.AddEmbeddedControl(timerCtl, listViewEx1.Columns.Count - 1, listViewEx1.Items.Count - 1, DockStyle.Fill);            
        }
        /// <summary>
        /// 更新工作时间进度
        /// </summary>
        /// <param name="flag">是否刷新主浮动窗体</param>
        private void updateProgress(bool flag)
        {
            //计算百分率
            UserSetting userSetting =
                ConfigHelper.UserSettingConfig;
            if (userSetting == null)
                return;
            int dailyWorkSeconds = userSetting.DailyWorkHour * 3600;
            currentWorkPer = (int)Math.Round(((float)workingSeconds / (float)dailyWorkSeconds) * 100, MidpointRounding.AwayFromZero);
            int hour = 0;
            int mintues = 0;
            if (workingSeconds / 3600 > 0)
            {
                hour = workingSeconds / 3600;
            }
            mintues = workingSeconds / 60 % 60;
            //更新显示时间和进度球
            string time = string.Format("{0:D2}:{1:D2}", hour, mintues);
            updateWorkPorgress(currentWorkPer, time, workingSeconds < dailyWorkSeconds);
            if (flag &&
                FormFloat.Instance!=null &&
                !FormFloat.Instance.IsDisposed)
            {
                FormFloat.Instance.UpdateProgress(
                    currentWorkPer, 
                    time, 
                    workingSeconds < dailyWorkSeconds);
            }
        }
        private bool drawColon = true;
        /// <summary>
        /// 更新工作时间进度
        /// </summary>
        /// <param name="value"></param>
        private void updateWorkPorgress(int value, string time, bool refreshRate)
        {
            Bitmap bmpProgress = new Bitmap(progressBallBg.Width, progressBallBg.Height);
            picProgress.Image = bmpProgress;

            using (Graphics gh = Graphics.FromImage(bmpProgress))
            {
                gh.CompositingQuality = CompositingQuality.HighQuality;
                gh.SmoothingMode = SmoothingMode.HighQuality;
                gh.PixelOffsetMode = PixelOffsetMode.HighQuality;
                gh.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gh.DrawImage(progressBallBg, new Point(0, 0));
                if (value > 0)
                {
                    int progressValue = 0;
                    if (refreshRate)
                        progressValue = (int)Math.Round((float)progressBall.Height / ((float)100 / value), MidpointRounding.AwayFromZero);
                    else
                        progressValue = progressBall.Height;
                    Rectangle progressRec = new Rectangle(0, progressBall.Height - progressValue, progressBall.Width, progressValue);
                    using (Bitmap progressBmp = progressBall.Clone(progressRec, progressBall.PixelFormat))
                    {
                        gh.DrawImage(progressBmp, new PointF(progressBallOffset, progressBall.Height - progressValue + progressBallOffset));
                    }
                }
                if (!string.IsNullOrEmpty(time))
                {
                    Bitmap timerBmp = new Bitmap(70, 16);
                    int x = 0, y = 0;
                    using (Graphics timerGh = Graphics.FromImage(timerBmp))
                    {
                        foreach (char c in time)
                        {
                            Bitmap numberBmp = getTimeNoAt(c);
                            //if (!drawColon &&
                            //    c == ':')
                            //    numberBmp = getTimeNoAt('\0');
                            //else
                            //    numberBmp = getTimeNoAt(c);
                            timerGh.DrawImage(numberBmp,
                                new Rectangle(
                                    x,
                                    y,
                                    numberBmp.Width,
                                    numberBmp.Height));
                            x += numberBmp.Width;
                        }
                    }
                    x = (bmpProgress.Width - timerBmp.Width) / 2;
                    y = (bmpProgress.Height - timerBmp.Height) / 2;
                    gh.DrawImage(timerBmp, new PointF(x, y));
                }
            }
            drawColon = !drawColon;

            picProgress.Refresh();
        }
        /// <summary>
        /// 获取数字图片
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private Bitmap getTimeNoAt(char c)
        {
            Bitmap bmp = leyeba.Properties.Resources.colon;
            switch (c)
            {
                case '0':
                    bmp = leyeba.Properties.Resources.zero;
                    break;
                case '1':
                    bmp = leyeba.Properties.Resources.one;
                    break;
                case '2':
                    bmp = leyeba.Properties.Resources.two;
                    break;
                case '3':
                    bmp = leyeba.Properties.Resources.three;
                    break;
                case '4':
                    bmp = leyeba.Properties.Resources.four;
                    break;
                case '5':
                    bmp = leyeba.Properties.Resources.five;
                    break;
                case '6':
                    bmp = leyeba.Properties.Resources.six;
                    break;
                case '7':
                    bmp = leyeba.Properties.Resources.seven;
                    break;
                case '8':
                    bmp = leyeba.Properties.Resources.eight;
                    break;
                case '9':
                    bmp = leyeba.Properties.Resources.nine;
                    break;
                case '\0':
                    bmp = new Bitmap(bmp.Width, bmp.Height);                    
                    break;
            }

            return bmp;
        }

        private void updateWorkLog(ProjectTask task, TimerControl timerCtl)
        {
            bool isNew = false;
            LogData log = null;
            if (lstWorkLog != null &&
                lstWorkLog.Count > 0)
            {
                log = lstWorkLog.Find(m => m.TaskId.Equals(task.ID));
            }

            if (log == null)
            {
                log = new LogData();
                isNew = true;
            }
            int pid = 0;
            if (cboProject != null)
                int.TryParse(cboProject.SelectedValue.ToString(), out pid);
            log.ProjectId = pid;
            
            KeyValuePair<string, int> projItem =
                (KeyValuePair<string, int>)cboProject.SelectedItem;
            log.ProjectTitle = projItem.Key;
            log.TaskId = task.ID;
            log.ProjectTaskId = task.ProjectTaskId;
            log.TaskName = task.Name;
            log.PDate = DateTime.Now.ToString("yyyy-MM-dd");
            int workSeconds = timerCtl.Seconds;            
            log.WorkSeconds = workSeconds;
            log.WorkHour = Global.ConvertToDecimalTimeAt(workSeconds);
            log.CompleteRate = task.CompleteRate;
            if (!isNew)
                return;
            lstWorkLog.Add(log);
        }

        private void working()
        {
            updateProgress(true);
            if (!isworking)
                isworking = true;
            if (!workTimer.Enabled)
            {
                workTimer_Tick(workTimer, EventArgs.Empty);
                workTimer.Start();
            }
            if (workingTimerCtl != null &&
                !workingTimerCtl.IsRunning)
                workingTimerCtl.Continue();
            btnWork.Text = "暂停";
            btnWork.Image = leyeba.Properties.Resources.icon_suspend;
            btnWork.Checked = true;
            //启动桌面截图自动上传
            ScreenWatch.LaunchAutoUploadScreen();
            //开始或继续鼠标键盘作业计时
            KeyMouseHook.StartWatcher();
        }

        private void fillWorkLog()
        {
            FormMain main = this.ParentForm as FormMain;
            if (main == null)
                return;
            main.FillWorkLog(this.lstWorkLog);
        }

        private void resumeWorkBtn()
        {
            btnWork.Text = "开始";
            btnWork.Image = leyeba.Properties.Resources.icon_01;
            if (btnWork.Checked)
                btnWork.Checked = false;
        }
        #endregion//私有方法

        #region//公共方法
        /// <summary>
        /// 自动保存
        /// </summary>
        public void AutoSave(bool endWork = false)
        {
            //1分钟保存一次当前记录工时
            if (workingSeconds <= 0)
                return;
            if (taskTiming == null)
                taskTiming = new TaskTiming();
            taskTiming.EndWork = endWork;
            if (endWork)
                GenKeyMouseAutoLog();
            taskTiming.WorkDate = workDate;
            taskTiming.WorkSeconds = workingSeconds;
            timerCtl_TimerChanged(workingTimerCtl, EventArgs.Empty);
            taskTiming.WorkLogList = lstWorkLog;
            ProjectTask.SaveTaskTimingToFile(taskTiming);
            if (lstAutologs != null &&
                lstAutologs.Count > 0)
            {
                if (autologsTiming == null)
                    autologsTiming = new AutologsTiming();
                autologsTiming.Date = workDate;
                autologsTiming.AutoLogList = lstAutologs;
                AutoLogs.SaveLogsToFile(autologsTiming);
            }
        }
        /// <summary>
        /// 开始工作
        /// </summary>
        public void StartWorking()
        {
            btnWork_Click(btnWork, EventArgs.Empty);
        }
        /// <summary>
        /// 暂停工作
        /// </summary>
        public void PauseWorking()
        {
            if (this.workingSeconds > 0)
                FormFloat.Instance.WorkingPause();
            if (workingTimerCtl != null)
                workingTimerCtl.Pause();

            if (isworking)
                isworking = false;
            resumeWorkBtn();
            KeyMouseHook.PauseWatcher();
            if (ScreenWatch.Launched)
                ScreenWatch.StopAutoUploadScreen();
        }
        /// <summary>
        /// 停止工作
        /// </summary>
        public void StopWorking()
        {
            if (isworking)
                isworking = false;
            resumeWorkBtn();
            StopWatch();
        }        
        /// <summary>
        /// 停止自动刷新计时器
        /// </summary>
        public void StopWatch()
        {
            KeyMouseHook.StopWatcher();
            if (ScreenWatch.Launched)
                ScreenWatch.StopAutoUploadScreen();
        }
        #endregion
    }

    class ListViewSort : IComparer
    {
        private int col;
        private bool descK;
        public ListViewSort()
        {
            col = 0;
        }
        public ListViewSort(int column, object Desc)
        {
            descK = (bool)Desc;
            col = column; //当前列
        }
        public int Compare(object x, object y)
        {
            int tempInt = String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
            if (descK) return -tempInt;
            else return tempInt;
        }
    }
}
