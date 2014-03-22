using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Util;

namespace leyeba
{
    internal class KeyMouseHook
    {
        public enum WM_MOUSE
        {
            /// <summary>
            /// 鼠标开始
            /// </summary>
            WM_MOUSEFIRST = 0x200,

            /// <summary>
            /// 鼠标移动
            /// </summary>
            WM_MOUSEMOVE = 0x200,

            /// <summary>
            /// 左键按下
            /// </summary>
            WM_LBUTTONDOWN = 0x201,

            /// <summary>
            /// 左键释放
            /// </summary>
            WM_LBUTTONUP = 0x202,

            /// <summary>
            /// 左键双击
            /// </summary>
            WM_LBUTTONDBLCLK = 0x203,

            /// <summary>
            /// 右键按下
            /// </summary>
            WM_RBUTTONDOWN = 0x204,

            /// <summary>
            /// 右键释放
            /// </summary>
            WM_RBUTTONUP = 0x205,

            /// <summary>
            /// 右键双击
            /// </summary>
            WM_RBUTTONDBLCLK = 0x206,

            /// <summary>
            /// 中键按下
            /// </summary>
            WM_MBUTTONDOWN = 0x207,

            /// <summary>
            /// 中键释放
            /// </summary>
            WM_MBUTTONUP = 0x208,

            /// <summary>
            /// 中键双击
            /// </summary>
            WM_MBUTTONDBLCLK = 0x209,

            /// <summary>
            /// 滚轮滚动
            /// </summary>
            /// <remarks>WINNT4.0以上才支持此消息</remarks>
            WM_MOUSEWHEEL = 0x020A
        }

        public enum WM_KEYBOARD
        {
            /// <summary>
            /// 非系统按键按下
            /// </summary>
            WM_KEYDOWN = 0x100,

            /// <summary>
            /// 非系统按键释放
            /// </summary>
            WM_KEYUP = 0x101,

            /// <summary>
            /// 系统按键按下
            /// </summary>
            WM_SYSKEYDOWN = 0x104,

            /// <summary>
            /// 系统按键释放
            /// </summary>
            WM_SYSKEYUP = 0x105
        }
        
        /// <summary>
        /// 鼠标钩子句柄
        /// </summary>
        private static IntPtr m_pMouseHook = IntPtr.Zero;

        /// <summary>
        /// 键盘钩子句柄
        /// </summary>
        private static IntPtr m_pKeyboardHook = IntPtr.Zero;

        /// <summary>
        /// 鼠标钩子委托实例
        /// </summary>
        /// <remarks>
        /// 不要试图省略此变量,否则将会导致
        /// 激活 CallbackOnCollectedDelegate 托管调试助手 (MDA)。 
        /// 详细请参见MSDN中关于 CallbackOnCollectedDelegate 的描述
        /// </remarks>
        private static Win32API.HookProc m_MouseHookProcedure;

        /// <summary>
        /// 键盘钩子委托实例
        /// </summary>
        /// <remarks>
        /// 不要试图省略此变量,否则将会导致
        /// 激活 CallbackOnCollectedDelegate 托管调试助手 (MDA)。 
        /// 详细请参见MSDN中关于 CallbackOnCollectedDelegate 的描述
        /// </remarks>
        private static Win32API.HookProc m_KeyboardHookProcedure;

        private static bool paused = true;
        private static int keyCounter = 0; //键盘无作业计数
        private static int mouseCounter = 0; //鼠标无作业计数
        private const int KMNoOpInterval = 10; //鼠标键盘无作业时间间隔10秒
        private static System.Timers.Timer keyTimer = null;
        private static System.Timers.Timer mouseTimer = null;

        private static Stopwatch keyWatcher = new Stopwatch();

        private static Stopwatch mouseWatcher = new Stopwatch();
        /// <summary>
        /// 鼠标在设定时间间隔内使用的时间（只读）
        /// </summary>
        public static Stopwatch MouseWatcher
        {
            get
            {
                return mouseWatcher;
            }
        }
        /// <summary>
        /// 键盘在设定时间间隔内使用时间（只读）
        /// </summary>
        public static Stopwatch KeyWatcher
        {
            get
            {
                return keyWatcher;
            }
        }
        public static int MouseWorkSeconds 
        {
            get {
                return (int)Math.Round(mouseWatcher.Elapsed.TotalSeconds, MidpointRounding.AwayFromZero);
            }
        }
        public static int KeyWorkSeconds
        {
            get
            {
                return (int)Math.Round(keyWatcher.Elapsed.TotalSeconds, MidpointRounding.AwayFromZero);
            }
        }
        /// <summary>
        /// 初始化KeyTimer
        /// </summary>
        private static void initKeyTimer()
        {
            keyTimer = new System.Timers.Timer();
            keyTimer.Interval = 1000;
            keyTimer.Elapsed += keyTimer_Elapsed;
        }

        /// <summary>
        /// 初始化MouseTimer
        /// </summary>
        private static void initMouseTimer()
        {
            mouseTimer = new System.Timers.Timer();
            mouseTimer.Interval = 1000;
            mouseTimer.Elapsed += mouseTimer_Elapsed;
        }

        static void keyTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //如果键盘无作业时间间隔大于设定时间，停止计时器运行
            if (++keyCounter > KMNoOpInterval)
            {
                //停止无作业计时
                if (keyTimer.Enabled)
                    keyTimer.Stop();
                //停止作业计时
                if (keyWatcher.IsRunning)
                    keyWatcher.Stop();
                //重置无作业计数
                keyCounter = 0;
            }
        }

        static void mouseTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //如果鼠标无作业时间间隔大于设定时间，停止计时器运行
            if (++mouseCounter > KMNoOpInterval)
            {
                //停止无作业计时
                if (mouseTimer.Enabled)
                    mouseTimer.Stop();
                //停止作业计时
                if (mouseWatcher.IsRunning)
                    mouseWatcher.Stop();
                //重置无作业计数
                mouseCounter = 0;
            }
        }
        public static void StartWatcher()
        {
            if (m_pMouseHook == IntPtr.Zero &&
                m_pKeyboardHook == IntPtr.Zero)
                InstallHook();
            paused = false;
            InstallHook();
            StartKeyWatcher();
            StartMouseWatcher();
        }

        public static void StartMouseWatcher()
        {
            //作业计时开始
            if (!mouseWatcher.IsRunning)
                mouseWatcher.Start();
            if (mouseTimer == null)
                initMouseTimer();
            //无作业计时开始
            if (!mouseTimer.Enabled)
                mouseTimer.Start();
            //重置无作业计数
            mouseCounter = 0;
        }

        public static void StartKeyWatcher()
        {
            //作业计时开始
            if (!keyWatcher.IsRunning)
                keyWatcher.Start();
            if (keyTimer == null)
                initKeyTimer();
            //无作业计时开始
            if (!keyTimer.Enabled)
                keyTimer.Start();            
            //重置无作业计数
            keyCounter = 0;
        }

        public static void PauseWatcher()
        {
            paused = true;
            StopTimer();
        }

        public static void StopWatcher()
        {
            paused = true;
            UnInstallHook();
            StopTimer();
            if (keyWatcher.Elapsed.Seconds > 0)
                keyWatcher.Reset();
            if (MouseWatcher.Elapsed.Seconds > 0)
                MouseWatcher.Reset();
        }

        private static void StopTimer()
        {
            //停止键盘无作业计时
            if (keyTimer != null &&
                keyTimer.Enabled)
                keyTimer.Stop();
            //停止键盘作业计时
            if (keyWatcher.IsRunning)
                keyWatcher.Stop();
            //停止鼠标无作业计时
            if (mouseTimer != null &&
                mouseTimer.Enabled)
                mouseTimer.Stop();
            //停止鼠标作业计时
            if (mouseWatcher.IsRunning)
                mouseWatcher.Stop();
        }

        /// <summary>
        /// 鼠标钩子处理函数
        /// </summary>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        private static int MouseHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            try
            {
                if (nCode >= 0 &&
                    Enum.IsDefined(typeof(WM_MOUSE), wParam))
                {
                    if (paused)
                        return 0;
                    StartMouseWatcher();
                }

                return Win32API.CallNextHookEx(m_pMouseHook, nCode, wParam, lParam);
            }
            catch (Exception exp)
            {
                Log.error(typeof(KeyMouseHook), (exp.InnerException == null ? "" : exp.InnerException.ToString()) + exp.Message);
                return 0;
            }
        }

        /// <summary>
        /// 键盘钩子处理函数
        /// </summary>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        /// <remarks>此版本的键盘事件处理不是很好,还有待修正.</remarks>
        private static int KeyboardHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            try
            {
                if (nCode >= 0 &&
                    Enum.IsDefined(typeof(WM_KEYBOARD), wParam))
                {
                    if (paused)
                        return 0;
                    StartKeyWatcher();
                }

                return Win32API.CallNextHookEx(m_pKeyboardHook, nCode, wParam, lParam);
            }
            catch (Exception exp)
            {
                Log.error(typeof(KeyMouseHook), (exp.InnerException == null ? "" : exp.InnerException.ToString()) + exp.Message);
                return 0;
            }
        }

        #region 公共方法

        /// <summary>
        /// 安装钩子
        /// </summary>
        /// <returns></returns>
        public static bool InstallHook()
        {
            IntPtr pInstance = Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().ManifestModule);

            if (m_pMouseHook == IntPtr.Zero)
            {
                m_MouseHookProcedure = new Win32API.HookProc(MouseHookProc);
                m_pMouseHook = Win32API.SetWindowsHookEx(Win32API.WH_Codes.WH_MOUSE_LL, m_MouseHookProcedure, pInstance, 0);
                if (m_pMouseHook == IntPtr.Zero)
                {
                    UnInstallHook();
                    return false;
                }
            }
            if (m_pKeyboardHook == IntPtr.Zero)
            {
                m_KeyboardHookProcedure = new Win32API.HookProc(KeyboardHookProc);
                m_pKeyboardHook = Win32API.SetWindowsHookEx(Win32API.WH_Codes.WH_KEYBOARD_LL, m_KeyboardHookProcedure, pInstance, 0);
                if (m_pKeyboardHook == IntPtr.Zero)
                {
                    UnInstallHook();
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 卸载钩子
        /// </summary>
        /// <returns></returns>
        public static bool UnInstallHook()
        {
            bool result = true;
            if (m_pMouseHook != IntPtr.Zero)
            {
                result = (Win32API.UnhookWindowsHookEx(m_pMouseHook) && result);
                m_pMouseHook = IntPtr.Zero;
            }
            if (m_pKeyboardHook != IntPtr.Zero)
            {
                result = (Win32API.UnhookWindowsHookEx(m_pKeyboardHook) && result);
                m_pKeyboardHook = IntPtr.Zero;
            }

            return result;
        }

        #endregion 公共方法

        public static void Dispose()
        {
            if (keyTimer != null)
            {
                keyTimer.Stop();
                keyTimer.Close();
                keyTimer = null;
            }
            if (mouseTimer != null)
            {
                mouseTimer.Stop();
                mouseTimer.Close();
                mouseTimer = null;
            }
            GC.Collect();
        }
    }
}
