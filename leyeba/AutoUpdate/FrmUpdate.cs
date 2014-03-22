using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Xml;
using System.Net;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace AutoUpdate
{
    public partial class FrmUpdate : BaseSubForm
    {
        private string mainAppName = string.Empty;
        private string mainVer = string.Empty;
        private bool autoUpdate = true;
        private bool promptUpdate = false;

        private string updateUrl = string.Empty;
        private string tempUpdatePath = string.Empty;

		public FrmUpdate(
            string mainName = "", 
            string version = "",
            bool auto = true, 
            bool prompt = false)
		{
            InitializeComponent();
            this.mainAppName = mainName;
            this.mainVer = version;
            autoUpdate = auto;
            promptUpdate = prompt;
		}

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Application.DoEvents();
            try
            {
                tempUpdatePath =
                    Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "temp"), "update");
                if (Directory.Exists(tempUpdatePath) &&
                    hasFile(tempUpdatePath))
                {
                    updateFile();
                    exit();
                }

                AutoUpdate.Update update =
                    AutoUpdate.Update.GetUpdate(mainVer);
                if (update == null ||
                    string.IsNullOrEmpty(update.Status) ||
                    update.Status.Equals("0"))
                {
                    if (promptUpdate)
                    {
                        PromptBox.Alert(
                            "������ҵ�ɿͻ��������µģ�����Ҫ���£�",
                            "����");
                    }
                    exit();
                    return;
                }
                if (!autoUpdate)
                {
                    DialogResult result =
                        PromptBox.Question(
                        "��⵽��ҵ�����µ������ļ����Ƿ�������",
                        "����");
                    if (DialogResult.No == result)
                    {
                        exit();
                        return;
                    }
                }
                Thread threadDown = new Thread(new ParameterizedThreadStart(downUpdateFile));
                threadDown.IsBackground = true;
                threadDown.Start(update.Path);
            }
            catch (Exception exp)
            {
                File.WriteAllText(
                    Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "AutoUpdate.error.txt"),
                    exp.Message);
            }
        }

        private void exit()
        {
            this.Close();
            this.Dispose();
        }

        private void downUpdateFile(object obj)
		{
            string updateFileUrl = obj as string;
            if (string.IsNullOrEmpty(updateFileUrl))
                return;
            try
            {
                long fileLength = 0;
                WebRequest webReq = WebRequest.Create(updateFileUrl);
                WebResponse webRes = webReq.GetResponse();
                fileLength = webRes.ContentLength;

                while (!this.IsHandleCreated) { }
                if (this.IsHandleCreated)
                    this.Invoke(new MethodInvoker(() => {
                        lblFinishProcess.Text = "�������ظ����ļ�,���Ժ�...";
                        pbCurrentProgress.Value = 0;
                    }));

                Stream srm = webRes.GetResponseStream();
                StreamReader srmReader = new StreamReader(srm);
                byte[] bufferbyte = new byte[fileLength];
                int count = 1024;
                int startByte = 0;
                while (fileLength > 0 && startByte < fileLength)
                {
                    int downByte = 
                        srm.Read(
                        bufferbyte,
                        startByte, 
                        fileLength - startByte > count ? 
                        count : (int)(fileLength - startByte));

                    if (downByte == 0) break;
                    startByte += downByte;
                    int percent = Convert.ToInt32((startByte / (fileLength * 1m)) * 100);

                    if (this.IsHandleCreated)
                        Invoke(new MethodInvoker(() => {
                            pbCurrentProgress.Value = percent;
                            lblFinishProcess.Text = "�������ظ����ļ�,���Ժ�..." + percent.ToString() + "%";
                        }));
                }
                srm.Close();
                srmReader.Close();

                if (!Directory.Exists(tempUpdatePath))
                    Directory.CreateDirectory(tempUpdatePath);
                string zipFileName = Path.Combine(tempUpdatePath, "update.zip");
                FileStream fs = 
                    new FileStream(
                        zipFileName, 
                        FileMode.OpenOrCreate, 
                        FileAccess.Write);
                fs.Write(bufferbyte, 0, bufferbyte.Length);
                fs.Close();
                //��ѹ��ɾ��ѹ���ļ�
                unZip(zipFileName);

                if (promptUpdate)
                {
                    PromptBox.Alert(
                        "���³ɹ�������������Ӧ�ó�����и��¡�",
                        "����");
                    return;
                }
                updateFile();
            }
            catch (Exception exp)
            {
                File.WriteAllText(
                    Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory, 
                    "AutoUpdate.error.txt"), 
                    exp.Message);
            }
            finally
            {
                if (this.IsHandleCreated)
                    Invoke(new MethodInvoker(() => {
                        exit();
                    }));
            }
		}
        /// <summary>
        /// ��ѹ��ɾ��ѹ���ļ�
        /// </summary>
        /// <param name="zipFileName">ѹ���ļ�</param>
        private void unZip(string zipFileName)
        {
            if (!File.Exists(zipFileName))
                return;
            string sevenzFile = 
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "7z.exe");
            if(!File.Exists(sevenzFile))
                File.WriteAllText(
                    Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "AutoUpdate.error.txt"),
                    "��ѹ�ļ���ɾ����");
            Process process = new Process();
            process.StartInfo.FileName = sevenzFile;
            string args = string.Format("x \"{0}\" -o\"{1}\" -y", zipFileName, Path.GetDirectoryName(zipFileName));
            process.StartInfo.Arguments = args;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            process.WaitForExit();
            process.Close();
            File.Delete(zipFileName);
        }

        //���������ļ�
        private void updateFile()
        {
            killMainProgram();
            moveFile(tempUpdatePath, AppDomain.CurrentDomain.BaseDirectory);
            Process.Start(
                Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                mainAppName),
                "0");
        }

        private bool hasFileFlag = false;
        private bool hasFile(string path)
        {            
            if (!Directory.Exists(path))
                return hasFileFlag;
            if (Directory.GetFiles(path).Length > 0)
                hasFileFlag = true;
            string[] dirs = Directory.GetDirectories(path);
            foreach (string dir in dirs)
                hasFile(dir);

            return hasFileFlag;
        }

		//�����ļ�;
		private void moveFile(string sourcePath, string destPath)
		{
            if (!Directory.Exists(destPath))
                Directory.CreateDirectory(destPath);
			string[] files = Directory.GetFiles(sourcePath);
            foreach (string file in files)
            {
                if (AppDomain.CurrentDomain.FriendlyName.Equals(
                    Path.GetFileName(file),
                    StringComparison.OrdinalIgnoreCase))
                    continue;
                if (Path.GetFileName(file).Equals(
                    mainAppName,
                    StringComparison.OrdinalIgnoreCase))
                {
                    FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(file);
                    this.mainVer = fvi.ProductVersion;
                }
                File.Copy(file, Path.Combine(destPath, Path.GetFileName(file)), true);
                File.Delete(file);
            }
			string[] dirs = Directory.GetDirectories(sourcePath);
            foreach (string dir in dirs)
                moveFile(dir, Path.Combine(destPath, Path.GetFileName(dir)));
		}

        private bool killMainProgram()
        {
            Process[] allProcess = Process.GetProcesses();
            foreach (Process p in allProcess)
            {
                if (string.Equals(
                    p.ProcessName + ".exe",
                    mainAppName,
                    StringComparison.OrdinalIgnoreCase))
                {
                    p.Kill();
                    p.WaitForExit();
                    return true;
                }
            }
            return false;
        }
	}
}
