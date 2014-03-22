using ControlEx;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Util;
using Util.JsonData;

namespace leyeba
{
    public partial class FormLeyebaMsg : BaseSubForm
    {
        private Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
        private MessageLeyeba msg = null;
        private int currentIndex = 1;
        private List<int?> readList = new List<int?>();
        private List<int?> starList = new List<int?>();

        public FormLeyebaMsg()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (this.Visible)
            {
                InitMessage();
            }
        }

        public void InitMessage()
        {
            int x = workingArea.Width - this.Width;
            int y = workingArea.Height - this.Height;
            this.Location = new Point(x, y);
            getmsg();
        }

        private void getmsg()
        {
            if (User.CurrentUser == null)
            {
                PromptBox.Alert("用户没有登陆！", "错误");
                return;
            }
            msg = MessageLeyeba.Message;            
            if (msg == null ||
                msg.MessageList == null ||
                msg.MessageList.Count == 0)
                return;
            if (msg.Status.Equals("0"))
            {
                PromptBox.Alert(msg.Reason, "提示");
                return;
            }
            showMessage(currentIndex);
        }

        private void showMessage(int index)
        {
            if (msg.MessageList == null)
                return;
            this.Text = string.Format("消息（{0}/{1}）", currentIndex, msg.MessageList.Count);
            if (msg.MessageList.Count == 0)
            {
                richTxtMessage.Clear();
                return;
            }
            MessageData msgData = msg.MessageList[index - 1];
            richTxtMessage.Text = msgData.Message;
            if (readList.FirstOrDefault(p => p.Equals(msgData.Id)) != null)
            {
                richTxtMessage.Font = new Font("宋体", 9, FontStyle.Regular);
            }
            else
            {
                richTxtMessage.Font = new Font("宋体", 9, FontStyle.Bold);
                readList.Add(msgData.Id);
            }

            if (msgData.Links != null &&
                msgData.Links.Count > 0)
            {
                for (int i = 0; i < msgData.Links.Count; i++)
                {                    
                    int startIndex = richTxtMessage.Text.Length;
                    string link = msgData.Links[i].Title;
                    richTxtMessage.AppendText(link);
                    if (i != msgData.Links.Count - 1)
                        richTxtMessage.AppendText("，");
                    richTxtMessage.Select(startIndex, link.Length);
                    RichTextBoxEx.SetSelectTextAsLink(richTxtMessage, true);
                }
                richTxtMessage.SelectionLength = 0;
            }

            if (starList.FirstOrDefault(p => p.Equals(msgData.Id)) != null)
            {
                btnStar.ForeColor = Color.Gray;
                btnStar.Enabled = false;
            }
            else
            {
                btnStar.ForeColor = Color.Black;
                btnStar.Enabled = true;
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (msg == null ||
                msg.MessageList == null ||
                msg.MessageList.Count == 0)
                return;
            currentIndex--;
            if (currentIndex <= 0)
            {
                currentIndex = 1;
                return;
            }
            showMessage(currentIndex);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (msg == null ||
                msg.MessageList == null ||
                msg.MessageList.Count == 0)
                return;
            currentIndex++;
            if (currentIndex > msg.MessageList.Count)
            {
                currentIndex = msg.MessageList.Count;
                return;
            }
            showMessage(currentIndex);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (msg == null ||
                msg.MessageList == null ||
                msg.MessageList.Count == 0)
                return;
            Result result = 
                MessageLeyeba.Delete(User.CurrentUser.Token, msg.MessageList[currentIndex - 1]);
            if (result.Status != "0")
            {
                PopupBox.Alert("删除成功", "删除");
                msg.MessageList.Remove(msg.MessageList[currentIndex - 1]);
                if (currentIndex > msg.MessageList.Count)
                {
                    currentIndex = msg.MessageList.Count;
                }
                showMessage(currentIndex);
            }
            else
            {
                PopupBox.Alert(result.Reason, "删除");
            }
        }

        private void btnStar_Click(object sender, EventArgs e)
        {
            if (msg == null ||
                msg.MessageList == null ||
                msg.MessageList.Count == 0)
                return;
            Result result = 
                MessageLeyeba.Star(User.CurrentUser.Token, msg.MessageList[currentIndex - 1]);
            if (result.Status != "0")
            {
                starList.Add(msg.MessageList[currentIndex - 1].Id);
                PopupBox.Alert("操作成功", "加星");
            }
            else
            {
                PopupBox.Alert(result.Reason, "加星");
            }
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void richTxtMessage_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            if (msg == null ||
                msg.MessageList == null ||
                msg.MessageList.Count == 0)
                return;
            string title = e.LinkText;
            try
            {
                Link link = msg.MessageList[currentIndex - 1].Links.Find(m => m.Title.Equals(title));
                if (link != null)
                {
                    string leyebaUrl = ConfigurationManager.ConnectionStrings["regUrl"].ConnectionString;
                    string urlString = link.Url;
                    if (!urlString.StartsWith(leyebaUrl))
                        urlString = leyebaUrl + urlString;
                    System.Diagnostics.Process.Start(urlString);
                }
            }
            catch (Exception exp)
            {
                Log.error(this.GetType(), (exp.InnerException == null ? "" : exp.InnerException.ToString()) + exp.Message);
                PromptBox.Alert(exp.Message, "错误");
            }
        }
    }
}
