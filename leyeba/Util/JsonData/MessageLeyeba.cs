using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Util.JsonData
{
    /// <summary>
    /// 详细信息类型
    /// </summary>
    [DataContract]
    public enum MessageType
    {
        /// <summary>
        /// 系统消息
        /// </summary>
        SystemMessage = 1,
        /// <summary>
        /// 项目消息
        /// </summary>
        ProjectMessage = 2
    }
    [DataContract]
    public class Link
    {
        [DataMember(Name = "title")]
        public string Title { get; set; }
        [DataMember(Name = "url")]
        public string Url { get; set; }
    }
    /// <summary>
    /// 消息内容
    /// </summary>
    [DataContract]
    public class MessageData
    {
        /// <summary>
        /// 详细信息类型
        /// </summary>
        [DataMember(Name = "MsgType")]
        public MessageType Type { get; set; }
        /// <summary>
        /// 消息id号
        /// </summary>
        [DataMember(Name = "Msgid")]
        public int Id { get; set; }
        /// <summary>
        /// 消息url（msgtype=1或2时有效）
        /// </summary>
        [DataMember(Name = "links")]
        public List<Link> Links { get; set; }
        /// <summary>
        /// 消息内容（msgtype=1或2时有效）
        /// </summary>
        [DataMember(Name = "message")]
        public string Message { get; set; }
        /// <summary>
        /// 发送时间（msgtype=1或2时有效）
        /// </summary>
        [DataMember(Name = "sendtime")]
        public string SendTime { get; set; } 
    }
    /// <summary>
    /// 乐业吧消息
    /// </summary>
    [DataContract]
    public class MessageLeyeba : BaseJsonData
    {
        private DataWatcher msgWatcher = null;
        public event EventHandler NewMessage;
        /// <summary>
        /// 消息列表
        /// </summary>
        [DataMember(Name = "Data")]
        public List<MessageData> MessageList { get; set; }
        /// <summary>
        /// 监控
        /// </summary>
        public MessageLeyeba() 
        {            
        }
        /// <summary>
        /// 定时更新消息，10分钟为一周期
        /// </summary>
        public void RegularlyUpdateMessage()
        {
            if (User.CurrentUser == null ||
                string.IsNullOrWhiteSpace(User.CurrentUser.Token))
                return;
            msgWatcher =
                new DataWatcher(10 * 60 * 1000);
            msgWatcher.DoAction = updateMessage;
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            updateMessage();
        }

        public void StopUpdateMessage()
        {
            if (msgWatcher != null)
                msgWatcher.Dispose();
        }
        private object objLock = new object();
        private void updateMessage()
        {
            lock (objLock)
            {
                MessageLeyeba newMessage = MessageLeyeba.GetUpdateMessage(User.CurrentUser.Token);
                if (newMessage != null &&
                    newMessage.MessageList != null &&
                    newMessage.MessageList.Count > 0)
                {
                    MessageLeyeba msg = MessageLeyeba.Message;
                    if (msg != null &&
                        msg.MessageList != null &&
                        msg.MessageList.Count > 0)
                    {
                        msg.MessageList.AddRange(newMessage.MessageList);
                        AppDomain.CurrentDomain.SetData("LeyebaMessage", msg);
                    }
                    else
                    {
                        AppDomain.CurrentDomain.SetData("LeyebaMessage", newMessage);
                    }
                    OnNewMessage(EventArgs.Empty);
                }
            }
        }

        protected virtual void OnNewMessage(EventArgs args)
        {
            if (NewMessage != null)
            {
                NewMessage(this, args);
            }
        }

        /// <summary>
        /// 乐业吧消息
        /// </summary>
        public static MessageLeyeba Message
        {
            get {
                return AppDomain.CurrentDomain.GetData("LeyebaMessage") as MessageLeyeba;
            }
        }

        /// <summary>
        /// 获取消息更新
        /// </summary>
        /// <param name="token">用户身份令牌</param>
        /// <returns>消息</returns>
        public static MessageLeyeba GetUpdateMessage(string token)
        {
            string url = ConfigurationManager.ConnectionStrings["updateMsgUrl"].ConnectionString;
            if (string.IsNullOrEmpty(url))
                return new MessageLeyeba {
                    Status = "0",
                    Reason = "配置当中未找到updateMsgUrl！"
                };
            NameValueCollection c = new NameValueCollection();
            c.Add("Token", token);
            string result = WebHelper.GetWebResponseString(url, c, Encoding.UTF8);
            if (result == null)
                return new MessageLeyeba {
                    Status = "0",
                    Reason = "网络连接超时！"
                };
            return JsonHelper.FromJsonTo<MessageLeyeba>(result);
        }
        /// <summary>
        /// 删除消息
        /// </summary>
        /// <param name="token">用户身份令牌</param>
        /// <param name="msg"></param>
        /// <returns>Result</returns>
        public static Result Delete(string token, MessageData msg)
        {
            string url = ConfigurationManager.ConnectionStrings["delMsgUrl"].ConnectionString;
            if (string.IsNullOrEmpty(url))
                return new Result {
                    Status = "0",
                    Reason = "配置当中未找到delMsgUrl！"
                };
            NameValueCollection c = new NameValueCollection();
            c.Add("Token", token);
            c.Add("MsgType", ((int)msg.Type).ToString());
            c.Add("msgId", msg.Id.ToString());
            string result = WebHelper.GetWebResponseString(url, c, Encoding.UTF8);
            if (result == null)
                return new Result {
                    Status = "0",
                    Reason = "网络连接超时！"
                };
            return JsonHelper.FromJsonTo<Result>(result);
        }
        /// <summary>
        /// 消息加星
        /// </summary>
        /// <param name="token">用户身份令牌</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static Result Star(string token, MessageData msg)
        {
            string url = ConfigurationManager.ConnectionStrings["starMsgUrl"].ConnectionString;
            if (string.IsNullOrEmpty(url))
                return new Result {
                    Status = "0",
                    Reason = "配置当中未找到starMsgUrl！"
                };
            NameValueCollection c = new NameValueCollection();
            c.Add("Token", token);
            c.Add("MsgType", ((int)msg.Type).ToString());
            c.Add("msgId", msg.Id.ToString());
            string result = WebHelper.GetWebResponseString(url, c, Encoding.UTF8);
            if (result == null)
                return new Result {
                    Status = "0",
                    Reason = "网络连接超时！"
                };
            return JsonHelper.FromJsonTo<Result>(result);
        }
    }
}
