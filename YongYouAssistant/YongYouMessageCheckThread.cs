using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace YongYouAssistant
{
    /// <summary>
    /// 用友消息检查主线程
    /// </summary>
    class YongYouMessageCheckThread
    {
        private List<ToDoTask> oldToDoTasks ;
        Thread checkThread;
        bool threadFlag = false;
        private int threadInterval = 5000;
        MainForm.UpdateUiStringDelegate updateStatusStripDelegate;
        MainForm.UpdateUiStringDelegate updateWebBrowserDelegate;
        MainForm.AlertTodoTaskDelegate alertTodoTaskDelegate;
        public YongYouMessageCheckThread(MainForm.UpdateUiStringDelegate updateStatusStripDelegate,
            MainForm.UpdateUiStringDelegate updateWebBrowserDelegate,
            MainForm.AlertTodoTaskDelegate alertTodoTaskDelegate
            )
        {
            this.updateStatusStripDelegate = updateStatusStripDelegate;
            this.updateWebBrowserDelegate = updateWebBrowserDelegate;
            this.alertTodoTaskDelegate = alertTodoTaskDelegate;
        }
        public void startThread()
        {
            threadFlag = true;
            if (checkThread == null)
            {
                checkThread = new Thread(new ParameterizedThreadStart(mainFunction));
            }
            if (!checkThread.IsAlive)
            {
                checkThread.Start("flag");
            }
        }
        public void stopThread()
        {
            threadFlag = false;
        }
        /// <summary>
        /// 主方法
        /// </summary>
        private void mainFunction(object arg)
        {
            while (threadFlag)
            {
                updateStatusStripDelegate("正在获取最新任务……");
                //获取消息
                string html = YongYouHandler.getToDoTaskHtmlUl();
                //显示到主页面中
                updateWebBrowserDelegate(html);
                //获取任务列表
                List<ToDoTask> toDoTasks = YongYouHandler.getToDoTasks(html);
                if (oldToDoTasks != null)
                {
                    //找出有其中新增的任务列表
                    List<ToDoTask> newToDoTasks = YongYouHandler.getNewToDoTaskList(toDoTasks, oldToDoTasks);
                    //提示新增的任务
                    alertToDoTaskList(newToDoTasks);
                }
                oldToDoTasks = toDoTasks;
                updateStatusStripDelegate("获取最新任务成功！");
                Thread.Sleep(threadInterval);
                //this.
            }
            checkThread = null;
        }
        private void  alertToDoTaskList(List<ToDoTask> toDoTasks)
        {
            foreach (ToDoTask item in toDoTasks)
            {
                alertTodoTaskDelegate(item);
            }   
        }

    }
}
