using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace AutoUpdate
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(String[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            if (args != null && args.Length > 0)
            {
                string mainName = string.Empty;
                string mainVer = string.Empty;
                bool autoUpdate = true;
                bool promptUpdate = false;
                if (args.Length > 0)
                    mainName = args[0];

                if (args.Length > 1)
                    mainVer = args[1];

                if (args.Length > 2)
                {
                    if (args[2].Equals("0"))
                        autoUpdate = false;
                    else if (args[2].Equals("1"))
                        autoUpdate = true;
                }

                if (args.Length > 3)
                {
                    if (args[3].Equals("0"))
                        promptUpdate = false;
                    else if (args[3].Equals("1"))
                        promptUpdate = true;
                }

                Application.Run(new FrmUpdate(mainName, mainVer, autoUpdate, promptUpdate));
                return;
            }
            Application.Run(new FrmUpdate());
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            try
            {
                File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AutoUpdate.error.txt"), e.Exception.Message);
            }
            catch 
            { 
            }
        }
    }
}
