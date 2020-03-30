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
        private string todoTaskHtmlModel = Resource1.todoTaskList;//主界面浏览器显示任务页面的模板html；
        public MainForm()
        {
            InitializeComponent();
            test();


        }
        
        public void test()
        {
            
            YongYouHandler.userLogin();
            string ulHtml = YongYouHandler.getToDoTaskHtmlUl();
            //string html = System.IO.File.ReadAllText(@"D:\YongYouAssistant\remindWorkSpace.html");
            //string ul=YongYouHandler.getToDoTaskHtmlUl(html);
            showToDoTaskInWebBrowse(ulHtml);
            //System.Console.WriteLine("Contents of WriteText.txt = {0}", ul);
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            //设置窗口在右下角
            int left = SystemInformation.WorkingArea.Width - this.Width;
            int top = SystemInformation.WorkingArea.Height - this.Height;
            this.Location = new Point(left, top);

            notifyIcon.Visible = true;
        }

        private void showToDoTaskInWebBrowse(string ulHtml)
        {
            webBrowser1.Navigate("about:blank");
            string showHtml = todoTaskHtmlModel.Replace("<div class=\"con_left\"></div>", ulHtml);
            while (webBrowser1.ReadyState != WebBrowserReadyState.Complete)  //报错“指定的转换无效”
                Application.DoEvents();
            webBrowser1.Document.Write(showHtml);

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //webBrowser1.ena
        }
        //private bool loginYongYou()
        //{
            
        //    requests.

        //}
    }
}
