using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace YongYouAssistant
{
    /// <summary>
    /// 用友操作主线程
    /// </summary>
    class YongYouHandlerThread
    {
        private List<ToDoTask> oldToDoTasks ;
        Thread checkThread;
        bool threadFlag = false;
        private int threadInterval = 5000;
        private static readonly object padlock = new object();//只读线程锁  
        MainForm.UpdateUiStringDelegate updateStatusStripLeftDelegate;
        MainForm.UpdateUiStringDelegate updateStatusStripCenterDelegate;
        MainForm.UpdateUiStringDelegate updateStatusStripRightDelegate;
        MainForm.UpdateUiStringDelegate updateWebBrowserDelegate;
        MainForm.AlertTodoTaskDelegate alertTodoTaskDelegate;
        private NetContectionStastion netContectionStastion=new NetContectionStastion();
        public YongYouHandlerThread(
            MainForm.UpdateUiStringDelegate updateStatusStripLeftDelegate,
            MainForm.UpdateUiStringDelegate updateStatusStripCenterDelegate,
            MainForm.UpdateUiStringDelegate updateStatusStripRightDelegate,
            MainForm.UpdateUiStringDelegate updateWebBrowserDelegate,
            MainForm.AlertTodoTaskDelegate alertTodoTaskDelegate
            )
        {
            this.updateStatusStripLeftDelegate = updateStatusStripLeftDelegate;
            this.updateStatusStripCenterDelegate = updateStatusStripCenterDelegate;
            this.updateStatusStripRightDelegate = updateStatusStripRightDelegate;
            this.updateWebBrowserDelegate = updateWebBrowserDelegate;
            this.alertTodoTaskDelegate = alertTodoTaskDelegate;
        }
        public void startThread()
        {

            lock (padlock)
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
            
            
        }
        public void stopThread()
        {

            lock (padlock)
            {
                threadFlag = false;
            }
        }
        /// <summary>
        /// 线程初始化，进行登陆，判断是否连接外网，是个递归函数
        /// </summary>
        private void init()
        {
            updateStatusStripLeftDelegate("正在登陆中……");
            string loginResult = loginYongYou();
            if (loginResult != null)
            {
                updateStatusStripRightDelegate("欢迎您，" + loginResult);
                updateStatusStripCenterDelegate("网络状态：办公网");
                netContectionStastion.isConnectInternet = NetContectionStastion.testIsConnectInternet();
                if (netContectionStastion.isConnectInternet)
                {
                    updateStatusStripCenterDelegate("网络状态：双网");
                }               

            }
            else
            {
                updateStatusStripLeftDelegate("登录失败,正在重试……");
                netContectionStastion.isConnectIntranet = false;
                Thread.Sleep(threadInterval);
                init();
            }
        }
        /// <summary>
        /// 成功返回登陆用户名，失败返回null
        /// </summary>
        private string loginYongYou()
        {
            Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            String userKey = cfa.AppSettings.Settings["userKey"].Value;
            String pswKey = cfa.AppSettings.Settings["pswKey"].Value;
            string result = YongYouHandler.userLogin(userKey, pswKey);
            return result;
        }
        /// <summary>
        /// 主方法
        /// </summary>
        private void mainFunction(object arg)
        {
            init();
            while (threadFlag)
            {
                updateStatusStripLeftDelegate("正在获取最新任务……");
                //获取消息
                string html = YongYouHandler.getToDoTaskHtmlUl();
                if (html == HTTPRequests.CONN_ERR)
                {
                    lock (padlock)
                    {
                        updateStatusStripLeftDelegate("连接内网失败，正在重连……");
                        updateStatusStripCenterDelegate("网络状态：无法连接办公网");
                        threadFlag = false;
                        checkThread = null;
                        startThread();
                        return;
                    }
                }
                else
                {
                    updateStatusStripLeftDelegate("已获取最新任务");
                    //显示到主页面中
                    updateWebBrowserDelegate(html);
                    //获取任务列表
                    List<ToDoTask> toDoTasks = YongYouHandler.getToDoTasks(html);
                    if (oldToDoTasks != null)
                    {
                        //找出有其中新增的任务列表
                        List<ToDoTask> newToDoTasks = YongYouHandler.getNewToDoTaskList(toDoTasks, oldToDoTasks);
                        //提示新增的任务
                        bool wechatSuccess=alertToDoTaskList(newToDoTasks);
                        if (!wechatSuccess)
                        {
                            netContectionStastion.isConnectInternet = false;
                            updateStatusStripCenterDelegate("网络状态：办公网");
                        }
                        else
                        {
                            netContectionStastion.isConnectInternet = true;
                            updateStatusStripCenterDelegate("网络状态：双网");

                        }
                    }
                    oldToDoTasks = toDoTasks;
                    updateStatusStripLeftDelegate("获取最新任务成功！");
                    
                    Thread.Sleep(threadInterval);
                }
            }
            checkThread = null;
        }
        /// <summary>
        /// 将待办任务提醒，返回微信提醒是否成功
        /// </summary>
        /// <param name="toDoTasks"></param>
        /// <returns></returns>
        private bool alertToDoTaskList(List<ToDoTask> toDoTasks)
        {
            bool wechatSuccess = true;
            foreach (ToDoTask item in toDoTasks)
            {
                alertTodoTaskDelegate(item);
                string result=alertToWeChat(item);
                if (result == HTTPRequests.CONN_ERR) wechatSuccess = false;

            }
            return wechatSuccess;
        }

        private string alertToWeChat(ToDoTask toDoTask)
        {
            HTTPRequests h = HTTPRequests.Instance;
            string url = string.Format("https://sc.ftqq.com/SCU90224T551e5c3bdd349d4aadf48237a851983a5e7324a83be11.send?text={0}&desp={1}", toDoTask.content, toDoTask.ToString());
            string result = h.get(url);
            return result;
        }

    }
}
