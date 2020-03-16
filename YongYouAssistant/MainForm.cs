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
            HTTPRequests requests = new HTTPRequests();
            //requests.get("http://www.baidu.com");
            //requests.get("http://10.0.15.16:7001/console/account.action");
            HTTPRequests hTTP = new HTTPRequests();
            YongYouHandler.userLogin(hTTP);
            YongYouHandler.getToDoListHtml(hTTP);
        }
    }
}
