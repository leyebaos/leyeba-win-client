using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using Com.jk.Leyeba.OssModule;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace Util.JsonData
{
    [DataContract]
    public class ScreenWatch
    {
        public Timer tmr = null;
        /// <summary>
        /// 截屏图片-Base64String
        /// </summary>
        [DataMember(Name = "Screenshotkey")]
        public string Key { get; set; }
        /// <summary>
        /// 截屏时间
        /// </summary>
        [DataMember(Name = "Screenshottime")]
        public DateTime Time { get; set; }

        public ScreenWatch() { }

        public static Result Upload(string token, ScreenWatch screen)
        {
            string url = ConfigurationManager.ConnectionStrings["uploadScreenUrl"].ConnectionString;
            if (string.IsNullOrEmpty(url))
                return new Result {
                    Status = "0",
                    Reason = "配置当中未找到uploadScreenUrl！"
                };
            NameValueCollection c = new NameValueCollection();
            c.Add("Token", token);
            c.Add("Screenshotkey", screen.Key);
            c.Add("Screenshottime", screen.Time.ToString("yyyy-MM-dd HH:mm:ss"));
            string result = WebHelper.GetWebResponseString(url, c, Encoding.UTF8);
            if (result == null) return null;
            return JsonHelper.FromJsonTo<Result>(result);
        }
                
        private static DataWatcher watcher = null;
        private static bool _launched = false;
        public static bool Launched
        {
            get {
                return _launched;
            }
        }
        private static void initDataWatcher()
        {
            bool initOss = OssUtil.Init(User.CurrentUser.Token);
            if (!initOss)
                return;
            watcher = new DataWatcher();
            watcher.Wait();
            watcher.DoAction = uploadScreen;
            _launched = true;
        }
        /// <summary>
        /// 启动桌面截图自动上传
        /// </summary>
        public static void LaunchAutoUploadScreen()
        {
            if (User.CurrentUser == null ||
                string.IsNullOrWhiteSpace(User.CurrentUser.Token))
                return;
            if (watcher == null &&
                !_launched)
                initDataWatcher();
            Random ro = new Random();
            int roNumber = ro.Next(15 * 60 * 1000, 20 * 60 * 1000);
            watcher.Change(0, roNumber);
        }

        private static string usrpath = Path.Combine(Global.TempDirs, User.CurrentUser.UserId);
        /// <summary>
        /// 上传桌面截屏
        /// </summary>
        private static void uploadScreen()
        {
            Bitmap screenBmp = null;
            try
            {
                if (!Directory.Exists(usrpath))
                {
                    Directory.CreateDirectory(usrpath);
                    File.SetAttributes(Global.TempDirs, FileAttributes.Hidden);
                }
                ScreenWatch screen = new ScreenWatch();
                screenBmp = BitmapHandler.PrintScreenHaveCursor();
                //上传长为1000的缩略图
                string guid = Guid.NewGuid().ToString("N");
                string fileName = Path.Combine(usrpath, guid + ".jpg");
                string imgkey = uploadScreen(screenBmp, fileName, 1000, PixelFormat.Format16bppRgb555);
                screen.Time = DateTime.Now;
                screen.Key = imgkey;
                ScreenWatch.Upload(User.CurrentUser.Token, screen);
                //上传长为250的缩略图
                fileName = Path.Combine(usrpath, guid + "_s.jpg");
                uploadScreen(screenBmp, fileName, 250, PixelFormat.Format16bppRgb555);
                Random ro = new Random();
                int roNumber = ro.Next(15 * 60 * 1000, 20 * 60 * 1000);
                watcher.Change(roNumber, roNumber);
            }
            catch (Exception exp)
            {
                Log.error(typeof(ScreenWatch), (exp.InnerException == null ? "" : exp.InnerException.ToString()) + exp.Message);
            }
            finally
            {
                if (screenBmp != null)
                    screenBmp.Dispose();
            }
        }

        private static string uploadScreen(Bitmap screen, string fileName, int width, PixelFormat format)
        {
            using (Bitmap scaleBmp = BitmapHandler.ScaleZoom(new Bitmap(screen), width, screen.Height))
            {
                using (Bitmap bitmap = new Bitmap(scaleBmp.Width, scaleBmp.Height, format))
                {
                    using (Graphics gh = Graphics.FromImage(bitmap))
                        gh.DrawImage(scaleBmp, 0, 0);
                    bitmap.Save(fileName, ImageFormat.Jpeg);
                }
            }
            return OssUtil.UploadImage(fileName);
        }
        /// <summary>
        /// 停止上传桌面截屏
        /// </summary>
        public static void StopAutoUploadScreen()
        {
            if (!_launched || 
                watcher == null)
                return;
            watcher.Wait();
        }

        public static void Dispose()
        {
            try
            {
                if (_launched)
                    _launched = false;
                if (watcher != null)
                {
                    watcher.Dispose();
                    watcher = null;
                }
                string[] pics = Directory.GetFiles(usrpath, "*.jpg");
                foreach (string pic in pics)
                    File.Delete(pic);
            }
            catch
            { 
            }
        }
    }
}
