using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Util;

namespace leyeba
{
    static class Program
    {
        static bool canCreateNew;
        static System.Threading.Mutex mutex = new System.Threading.Mutex(true, "leyebaclient", out canCreateNew);
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FormLeyebaMsg());
            //return;
            if (canCreateNew)
            {
                try
                {
                    string tempUpdatePath =
                        Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "temp"), "update");
                    if (Directory.Exists(tempUpdatePath))
                    {
                        moveFile(tempUpdatePath, AppDomain.CurrentDomain.BaseDirectory);
                    }
                    string autoUpdateFile =
                        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AutoUpdate.exe");
                    if (File.Exists(autoUpdateFile) &&
                        args.Length == 0)
                    {
                        string autoUpdate = "1";
                        Util.ConfigManage.SystemSetting sysSetting =
                            Util.ConfigManage.ConfigHelper.SystemSettingConfig;
                        autoUpdate = sysSetting.AutoUpdate ? "1" : "0";
                        Process proce =
                            Process.Start(
                            autoUpdateFile,
                            string.Format(
                            "{0} {1} {2}",
                            AppDomain.CurrentDomain.FriendlyName,
                            Application.ProductVersion,
                            autoUpdate));
                        proce.WaitForExit();
                    }
                }
                catch (Exception exp)
                {
                    Log.error(typeof(Program), (exp.InnerException == null ? "" : exp.InnerException.ToString()) + exp.Message);
                }
                Application.Run(FormLogin.Instance);
            }
            else
            {                
                IntPtr hWnd = Win32API.FindWindowEx(IntPtr.Zero, IntPtr.Zero, null, "乐业吧");
                if (hWnd == IntPtr.Zero)
                {
                    hWnd = Win32API.FindWindowEx(IntPtr.Zero, IntPtr.Zero, null, "登录");
                    if (hWnd == IntPtr.Zero)
                        return;
                }
                Win32API.ShowWindow(hWnd, Win32API.SW_SHOWNOACTIVATE);
                Win32API.SetForegroundWindow(hWnd);
            }
        }

        //复制文件;
        private static void moveFile(string sourcePath, string destPath)
        {
            if (!Directory.Exists(destPath))
                Directory.CreateDirectory(destPath);

            string[] files = Directory.GetFiles(sourcePath);
            foreach (string file in files)
            {
                if (Path.GetFileName(file).Equals(
                    AppDomain.CurrentDomain.FriendlyName,
                    StringComparison.OrdinalIgnoreCase))
                    continue;
                File.Copy(file, Path.Combine(destPath, Path.GetFileName(file)), true);
                File.Delete(file);
            }
            string[] dirs = Directory.GetDirectories(sourcePath);
            foreach (string dir in dirs)
                moveFile(dir, Path.Combine(destPath, Path.GetFileName(dir)));
        }
    }
}
