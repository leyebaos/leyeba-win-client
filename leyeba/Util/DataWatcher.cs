using System;
using System.Threading;

namespace Util
{
    public class DataWatcher : IDisposable
    {
        public Action DoAction;

        private Timer timer = null;
        private long interval = 5 * 60 * 1000;  //默认5分钟
        /// <summary>
        /// 时间间隔
        /// </summary>
        public long Interval
        {
            get { return interval; }
            set { interval = value; }
        }        

        public DataWatcher()
        {
            init();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="interval">时间间隔</param>
        public DataWatcher(long interval)
        {
            this.interval = interval;
            init();
        }
        /// <summary>
        /// 初始化Timer
        /// </summary>
        private void init()
        {
            TimerCallback callback = 
                (object state) => {
                    if (DoAction == null)
                        return;
                    DoAction();
                };
            timer = new Timer(callback, null, 0, interval);            
        }
        /// <summary>
        /// 重置为初始化时间
        /// </summary>
        /// <returns></returns>
        public bool Reset()
        {
            if (timer == null) 
                return false;
            return timer.Change(interval, interval);
        }
        /// <summary>
        /// 无限等待，直到做出改变
        /// </summary>
        /// <returns></returns>
        public bool Wait()
        {
            if (timer == null) 
                return false;
            return timer.Change(Timeout.Infinite, Timeout.Infinite);
        }
        /// <summary>
        /// 更改计时器的启动时间和方法调用之间的间隔，用 64 位有符号整数度量时间间隔。
        /// </summary>
        /// <param name="dueTime">在调用构造 System.Threading.Timer 时指定的回调方法之前的延迟时间量（以毫秒为单位）。</param>
        /// <param name="period">调用构造 System.Threading.Timer 时指定的回调方法的时间间隔（以毫秒为单位）。</param>
        /// <returns>如果尚未释放当前实例，则为 true；否则为 false。</returns>
        public bool Change(long dueTime, long period)
        {
            if (timer == null) 
                return false;
            return timer.Change(dueTime, period);
        }

        public void Dispose()
        {
            if (timer != null)
            {
                timer.Dispose();
                timer = null;
            }
        }
    }
}
