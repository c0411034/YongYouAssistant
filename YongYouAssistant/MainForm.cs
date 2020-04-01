using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace YongYouAssistant
{
    public partial class MainForm : Form
    {
        private string todoTaskHtmlModel = Resource1.todoTaskList;//主界面浏览器显示任务页面的模板html；
        public delegate void UpdateUiStringDelegate(string obj);//更新UI中string的委托类型
        public delegate void AlertTodoTaskDelegate(ToDoTask obj);//更新UI中string的委托类型
        public MainForm()
        {
            InitializeComponent();
            UpdateUiStringDelegate updateStatusStripDelegate = new UpdateUiStringDelegate(updateStatusStripLeft);
            //login();


        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            startInitThread();
        }
        public void startInitThread()
        {
            Thread initThread= new Thread(init);
            initThread.Start();
            
        }
        private void init()
        {
            updateStatusStripLeft("正在检测网络状态……");
            uint netConection = NetContectionStatusUtil.getNetConnection();
            updateStatusStripLeft("检测网络状态完毕");
            switch (netConection)
            {
                case NetContectionStatusUtil.DOUBLE_NET_CONNECT:
                    updateStatusStripCenter("网络状态：双网");
                    break;
                case NetContectionStatusUtil.ONLY_INTARNET_CONNECT:
                    updateStatusStripCenter("网络状态：已连接内网");
                    break;
                case NetContectionStatusUtil.ONLY_INTERNET_CONNECT:
                    updateStatusStripCenter("网络状态：只连接外网");
                    close("无法连接用友，请检查网络");
                    break;
                case NetContectionStatusUtil.NONE_NET_CONNECT:
                    updateStatusStripCenter("网络状态：无网络");
                    close("无法连接用友，请检查网络");
                    break;
                default:
                    break;
            }
            //if()
            //login();
        }
        public void close(string closeReason)
        {
            if (closeReason != "") MessageBox.Show(closeReason);
            this.Close();
        }
        
        /// <summary>
        /// 更新状态栏的文字，被其他线程委托调用
        /// </summary>
        /// <param name="text"></param>
        private void updateStatusStripLeft(string text)
        {
            if (this.statusStrip1.InvokeRequired)//如果调用控件的线程和创建创建控件的线程不是同一个则为True
            {
                while (!this.statusStrip1.IsHandleCreated)
                {
                    //解决窗体关闭时出现“访问已释放句柄“的异常
                    if (this.statusStrip1.Disposing || this.statusStrip1.IsDisposed)
                        return;
                }
                UpdateUiStringDelegate d = new UpdateUiStringDelegate(updateStatusStripLeft);
                this.statusStrip1.Invoke(d, new object[] { text });
            }
            else
            {
                this.toolStripStatusLabel1.Text = text;
            }        
        }

        /// <summary>
        /// 更新状态栏右侧的文字，被其他线程委托调用
        /// </summary>
        /// <param name="text"></param>
        private void updateStatusStripRight(string text)
        {
            if (this.statusStrip1.InvokeRequired)//如果调用控件的线程和创建创建控件的线程不是同一个则为True
            {
                while (!this.statusStrip1.IsHandleCreated)
                {
                    //解决窗体关闭时出现“访问已释放句柄“的异常
                    if (this.statusStrip1.Disposing || this.statusStrip1.IsDisposed)
                        return;
                }
                UpdateUiStringDelegate d = new UpdateUiStringDelegate(updateStatusStripRight);
                this.statusStrip1.Invoke(d, new object[] { text });
            }
            else
            {
                this.toolStripStatusLabel2.Text = text;
            }
        }
        /// <summary>
        /// 更新状态栏中央的文字，被其他线程委托调用
        /// </summary>
        /// <param name="text"></param>
        private void updateStatusStripCenter(string text)
        {
            if (this.statusStrip1.InvokeRequired)//如果调用控件的线程和创建创建控件的线程不是同一个则为True
            {
                while (!this.statusStrip1.IsHandleCreated)
                {
                    //解决窗体关闭时出现“访问已释放句柄“的异常
                    if (this.statusStrip1.Disposing || this.statusStrip1.IsDisposed)
                        return;
                }
                UpdateUiStringDelegate d = new UpdateUiStringDelegate(updateStatusStripCenter);
                this.statusStrip1.Invoke(d, new object[] { text });
            }
            else
            {
                this.toolStripStatusLabel3.Text = text;
            }
        }
        /// <summary>
        /// 更新主页面的html内容，被其他线程委托调用
        /// </summary>
        /// <param name="text"></param>
        private void showToDoTaskInWebBrowse(string html)
        {
            if (this.webBrowser1.InvokeRequired)//如果调用控件的线程和创建创建控件的线程不是同一个则为True
            {
                while (!this.webBrowser1.IsHandleCreated)
                {
                    //解决窗体关闭时出现“访问已释放句柄“的异常
                    if (this.webBrowser1.Disposing || this.webBrowser1.IsDisposed)
                        return;
                }
                UpdateUiStringDelegate d = new UpdateUiStringDelegate(showToDoTaskInWebBrowse);
                this.webBrowser1.Invoke(d, new object[] { html });
            }
            else
            {
                _showToDoTaskInWebBrowse(html);
            }
        }

        /// <summary>
        /// 弹出待办提醒窗口，被其他线程委托调用
        /// </summary>
        /// <param name="text"></param>
        private void showAlertToDoTask(ToDoTask toDoTask)
        {
            if (this.InvokeRequired)//如果调用控件的线程和创建创建控件的线程不是同一个则为True
            {
                while (!this.IsHandleCreated)
                {
                    //解决窗体关闭时出现“访问已释放句柄“的异常
                    if (this.Disposing || this.IsDisposed)
                        return;
                }
                AlertTodoTaskDelegate d = new AlertTodoTaskDelegate(showAlertToDoTask);
                this.Invoke(d, new object[] { toDoTask });
            }
            else
            {
                AlertToDoTask alertToDoTask = new AlertToDoTask(toDoTask);
                alertToDoTask.Show();
            }
        }
        private void  login()
        {
            updateStatusStripLeft("正在登陆……" );
            Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            String userKey = cfa.AppSettings.Settings["userKey"].Value;
            String pswKey = cfa.AppSettings.Settings["pswKey"].Value;
            string result= YongYouHandler.userLogin(userKey, pswKey);
            if (result != null)
            {
                updateStatusStripRight("欢迎您，" + result);
            }
            else
            {
                MessageBox.Show("登录失败,请检查");
                this.Close();
            }
        }
        public void test()
        {
            NetContectionStatusUtil.getNetConnection();
            //updateStatusStrip("测试多线程1");
            //YongYouHandler.userLogin();
            //string ulHtml = YongYouHandler.getToDoTaskHtmlUl();
            ////string html = System.IO.File.ReadAllText(@"D:\YongYouAssistant\remindWorkSpace.html");
            ////string ul=YongYouHandler.getToDoTaskHtmlUl(html);
            //showToDoTaskInWebBrowse(ulHtml);
            ////System.Console.WriteLine("Contents of WriteText.txt = {0}", ul);

        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            //设置窗口在右下角
            int left = SystemInformation.WorkingArea.Width - this.Width;
            int top = SystemInformation.WorkingArea.Height - this.Height;
            this.Location = new Point(left, top);

            notifyIcon.Visible = true;
        }

        private void _showToDoTaskInWebBrowse(string ulHtml)
        {
            webBrowser1.Navigate("about:blank");
            string showHtml = todoTaskHtmlModel.Replace("<div class=\"con_left\"></div>", ulHtml);
            while (webBrowser1.ReadyState != WebBrowserReadyState.Complete)  //报错“指定的转换无效”
                Application.DoEvents();
            webBrowser1.Document.Write(showHtml);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            YongYouMessageCheckThread t = new YongYouMessageCheckThread(updateStatusStripLeft, showToDoTaskInWebBrowse, showAlertToDoTask);
            t.startThread();
        }
    }
}
