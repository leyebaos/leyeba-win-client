using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Shell32;

namespace Util
{
    public class Global
    {
        /// <summary>
        /// 加密盐值
        /// </summary>
        public const string EncryptSaltValue = "+AuTh$1EyeBa";
        /// <summary>
        /// 临时文件夹
        /// </summary>
        public static string TempDirs = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "temp");

        public static string GetAssemblyFileVersion()
        {
            return Assembly.GetEntryAssembly().GetName().Version.ToString();
        }

        public static decimal ConvertToDecimalTimeAt(int seconds)
        {
            int hour = 0, mintues = 0, second = 0;
            hour = seconds / 3600;
            mintues = seconds / 60 % 60;
            second = seconds % 60;
            if (second >= 30)
            {
                //大于30秒为1分钟
                mintues += 1;
            }
            string strDecimal = string.Format("{0}.{1:D2}", hour, mintues);
            decimal decimalTime;
            decimal.TryParse(strDecimal, out decimalTime);
            return decimalTime;
        }

        public static string GetActiveProcessName()
        {
            try
            {
                IntPtr hwnd = Win32API.GetForegroundWindow();
                string fileName = GetProcessPathByHandle(hwnd);
                if (string.IsNullOrEmpty(fileName))
                    return string.Empty;
                ShellClass sh = new ShellClass();
                Folder dir = sh.NameSpace(Path.GetDirectoryName(fileName));
                FolderItem item = dir.ParseName(Path.GetFileName(fileName));
                string fileDetail = dir.GetDetailsOf(item, -1);
                if (string.IsNullOrEmpty(fileDetail))
                    return string.Empty;

                return GetValue(fileDetail, "文件说明: ", "\n");
            }
            catch (Exception exp)
            {
                Log.error(typeof(Global), (exp.InnerException == null ? "" : exp.InnerException.ToString()) + exp.Message);
                return string.Empty;
            }
        }

        public static string GetProcessPathByHandle(IntPtr hwnd)
        {
            int proceID;
            //获取进程ID  
            Win32API.GetWindowThreadProcessId(hwnd, out proceID);
            IntPtr hRelProcess = Win32API.OpenProcess((uint)Win32API.ProcessAccessFlags.QueryInformation, 0, (uint)proceID);
            if (hRelProcess == IntPtr.Zero)
                return string.Empty;
            StringBuilder procImagePath = new StringBuilder(256);
            string strImageFilePath = string.Empty; 
            Win32API.GetProcessImageFileName(hRelProcess, procImagePath, procImagePath.Capacity);
            if (procImagePath.Length <= 0)
                return string.Empty;
            int iDeviceIndex = procImagePath.ToString().IndexOf("\\", "\\Device\\HarddiskVolume".Length);
            string strDevicePath = procImagePath.ToString().Substring(0, iDeviceIndex);
            int iStartDisk = (int)'A';
            while (iStartDisk <= (int)'Z')
            {
                StringBuilder sbWindowImagePath = new StringBuilder(256);
                int iRet = Win32API.QueryDosDevice(((char)iStartDisk).ToString() + ":", sbWindowImagePath, sbWindowImagePath.Capacity);
                if (iRet != 0)
                {
                    if (sbWindowImagePath.ToString() == strDevicePath)
                    {
                        strImageFilePath = ((char)iStartDisk).ToString() + ":" + procImagePath.ToString().Replace(strDevicePath, "");
                        break;
                    }
                }
                iStartDisk++;
            }
            return strImageFilePath;
        }

        /// <summary>
        /// 获得字符串中开始和结束字符串中间得值
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="s">开始</param>
        /// <param name="e">结束</param>
        /// <returns></returns>
        public static string GetValue(string str, string s, string e)
        {
            Regex rg = new Regex("(?<=(" + s + "))[.\\s\\S]*?(?=(" + e + "))", RegexOptions.Multiline | RegexOptions.Singleline);
            return rg.Match(str).Value;
        }
    }
}
