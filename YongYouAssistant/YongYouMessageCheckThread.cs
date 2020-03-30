using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YongYouAssistant
{
    /// <summary>
    /// 用友消息检查主线程
    /// </summary>
    class YongYouMessageCheckThread
    {
        private List<ToDoTask> oldToDoTasks = new List<ToDoTask>();
        

        private void mainFunction()
        {

            //获取消息
            string html = YongYouHandler.getToDoTaskHtmlUl();
            //显示到主页面中
            //获取任务列表
            List<ToDoTask> toDoTasks = YongYouHandler.getToDoTasks(html);
            //找出有其中新增的任务列表
            List<ToDoTask> newToDoTasks = YongYouHandler.getNewToDoTaskList(toDoTasks, oldToDoTasks);
            //提示新增的任务

            oldToDoTasks = toDoTasks;
        }

    }
}
