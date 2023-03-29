using System;
using System.Threading;
using System.Threading.Tasks;

namespace Yu3zx.Util
{
    public class TaskManager
    {
        /// <summary>
        /// 创建一个超时任务
        /// </summary>
        /// <param name="action">执行的函数</param>
        /// <param name="timeOut">超时时间</param>
        public static bool Wait(Action action,int timeOut)
        {
            Task taskWait = new Task(()=> 
            {
                action();
            });

            Task taskTime = new Task(() =>
            {
                Thread.Sleep(timeOut);
            });

            taskWait.Start();
            taskTime.Start();

            if (Task.WaitAny(taskWait, taskTime)==0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 延迟执行
        /// </summary>
        /// <param name="action">执行的函数</param>
        /// <param name="timeOut">超时时间</param>
        public static void Delay(Action action, int time)
        {
            Task taskWait = new Task(() =>
            {
                Thread.Sleep(time);
                action();
            });
            taskWait.Start();
        }

        /// <summary>
        /// 开始异步任务
        /// </summary>
        /// <param name="action"></param>
        public static void StartTask(Action action)
        {
            try
            {
                Action newAction = () =>
                { };
                newAction += action;
                Task task = new Task(newAction);
                task.Start();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
