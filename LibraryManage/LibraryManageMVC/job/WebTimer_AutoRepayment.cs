using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using System.Web;

namespace LibraryManageMVC.job
{
    public class WebTimer_AutoRepayment
    {
        static WebTimer_AutoRepayment()
        {
            _WebTimerTask = new WebTimer_AutoRepayment();
        }
        /// <summary>
        /// 实例化
        /// </summary>
        /// <returns></returns>
        public static WebTimer_AutoRepayment Instance()
        {
            return _WebTimerTask;
        }
        /// <summary>
        /// 实际执行的方法
        /// </summary>
        private void ExecuteMain()
        {
            //定义你自己要执行的Job
            
        }
        #region Timer 计时器定义
        /// <summary>
        /// 调用 callback 的时间间隔（以毫秒为单位）。指定 Timeout.Infinite 可以禁用定期终止。
        /// </summary>
        private static int Period = 1 * 60 * 60 * 1000;
        /// <summary>
        /// 调用 callback 之前延迟的时间量（以毫秒为单位）。指定 Timeout.Infinite 以防止计时器开始计时。指定零 (0) 以立即启动计时器。
        /// </summary>
        private static int dueTime = 3 * 1000;//三分钟后启动
                                              /// <summary>
                                              ///第几次执行
                                              /// </summary>
        private long Times = 0;
        /// <summary>
        /// 实例化一个对象
        /// </summary>
        private static readonly WebTimer_AutoRepayment _WebTimerTask = null;
        private Timer WebTimerObj = null;
        /// <summary>
        /// 是否正在执行中
        /// </summary>
        private int _IsRunning;
        /// <summary>
        /// 开始
        /// </summary>
        public void Start()
        {
            if (WebTimerObj == null)
            {
                DateTime now = DateTime.Now;
                int minutes = now.Minute;
                if (minutes >= 55)
                {
                    dueTime = 0;//立即启动
                }
                else
                {
                    dueTime = (55 - minutes) * 60 * 1000;//到某个时间点的55分钟启动
                }
                WebTimerObj = new Timer(new TimerCallback(WebTimer_Callback), null, dueTime, Period);
            }        
        }
        /// <summary>
        /// WebTimer的主函数
        /// </summary>
        /// <param name="sender"></param>
        private void WebTimer_Callback(object sender)
        {
            try
            {
                if (Interlocked.Exchange(ref _IsRunning, 1) == 0)
                {
                    ExecuteMain();
                    Times++;
                    Times = (Times % 100000);
                }
            }
            catch
            {
            }
            finally
            {
                Interlocked.Exchange(ref _IsRunning, 0);
            }
        }
        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            if (WebTimerObj != null)
            {
                WebTimerObj.Dispose();
                WebTimerObj = null;
            }
        }
        #endregion
    }
}