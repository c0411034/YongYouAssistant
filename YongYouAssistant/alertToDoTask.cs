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
    public partial class AlertToDoTask : Form
    {
        public AlertToDoTask(ToDoTask toDoTask)
        {
            InitializeComponent();
            this.labelUrgency.Text = toDoTask.urgency;
            this.labelContent.Text = toDoTask.content;
            this.labelTime.Text = toDoTask.createTime;
        }

        private void AlertToDoTask_Load(object sender, EventArgs e)
        {
            //设置窗口在右下角
            int left = SystemInformation.WorkingArea.Width - this.Width;
            int top = SystemInformation.WorkingArea.Height - this.Height;
            this.Location = new Point(left, top);
        }
    }
}
