using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace YongYouAssistant
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Console.WriteLine("hello");
            test();


        }
        public void  test()
        {
            //HTTPRequests requests = new HTTPRequests();
            ////requests.get("http://www.baidu.com");
            ////requests.get("http://10.0.15.16:7001/console/account.action");
            //YongYouHandler.userLogin(requests);
            //string html=YongYouHandler.getToDoListHtml(requests);
            ////html = System.IO.File.ReadAllText(@"D:\YongYouAssistant\remindWorkSpace.html");
            ////System.Console.WriteLine("Contents of WriteText.txt = {0}", html);
            //YongYouHandler.GetToDoTasks(html);
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            //设置窗口在右下角
            int left = SystemInformation.WorkingArea.Width - this.Width;
            int top = SystemInformation.WorkingArea.Height - this.Height;
            this.Location = new Point(left,top);

            notifyIcon.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //AlertToDoTask alertToDoTask = new AlertToDoTask(new ToDoTask("紧急", "关于某某的通知", "2020/03/03"));
            //alertToDoTask.Show();
            webBrowser1.Navigate("about:blank");
            string html = System.IO.File.ReadAllText(@"D:\YongYouAssistant\remindWorkSpace.only.html");
            while (webBrowser1.ReadyState != WebBrowserReadyState.Complete)  //报错“指定的转换无效”
                Application.DoEvents();
            webBrowser1.Document.Write(html);
        }

        private void showToDoBalloonTips(ToDoTask toDoTask)
        {
            notifyIcon.ShowBalloonTip(20000,"你有新任务了", toDoTask.urgency+" "+toDoTask.content, ToolTipIcon.Info);

        }
    }
}
