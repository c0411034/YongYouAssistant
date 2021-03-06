﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YongYouAssistant
{
    class YongYouHandler
    {
        public static String userLogin(string account, String password)
        {
            HTTPRequests requests=HTTPRequests.Instance;
            String url = "http://10.0.15.16:7001/console/login.action";
            StringBuilder buffer= new StringBuilder();
            buffer.Append(url);
            buffer.AppendFormat("?{0}={1}", "account", account);
            buffer.AppendFormat("&{0}={1}", "password", password);
            url = buffer.ToString();
            String respone=requests.post(url);
            if (respone.Contains("欢迎您!"))
            {
                string startMark = "</span> ，";
                string endMark = "欢迎您!";
                int startPosition = respone.IndexOf(startMark);
                int endPosition=respone.IndexOf(endMark);
                string userName = respone.Substring(startPosition+startMark.Length, endPosition-startPosition- startMark.Length);
                return userName;
            }
            return null;
        }
        public static string  getToDoListHtml()
        {

            HTTPRequests requests = HTTPRequests.Instance;
            string timestamp = GetTimeStamp(DateTime.Now);
            String url = string.Format("http://10.0.15.16:7001/console/remindWorkSpace.action?timestamp={0}&ajax=y", timestamp);
            string respone = requests.get(url);
            //TODO判断是否能连接内网
            return respone;

        }
        /// <summary>
        /// 传入整个页面的html，返回只有任务列表那部分html
        /// 即<ul></ul>之间的html
        /// 并把所有点击的链接都去除
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string getToDoTaskHtmlUl(string html)
        {
            NSoup.Nodes.Document doc = NSoup.NSoupClient.Parse(html);
            NSoup.Select.Elements ele = doc.GetElementsByClass("con_left");
            if (ele.IsEmpty)
            {
                NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
                logger.Error("无法解析html:items_ul");
                return null;
            }
            ele[0].GetElementsByTag("a").RemoveAttr("href");//把所有点击的链接都去除
            StringBuilder sb = new StringBuilder("<div class=\"con_left\">");
            sb.Append(ele[0].Html());
            sb.Append("</div>");
            string ulHtml = sb.ToString();
            return (ulHtml);
        }
        public static string getToDoTaskHtmlUl()
        {
            string result = getToDoListHtml();
            if (result != HTTPRequests.CONN_ERR)
            {
                return getToDoTaskHtmlUl(result);
            }
            else
            {
                return HTTPRequests.CONN_ERR;
            }
        }
        public static List<ToDoTask> getToDoTasks(string html)
        {
            NSoup.Nodes.Document doc = NSoup.NSoupClient.Parse(html);
            NSoup.Select.Elements ele = doc.GetElementsByTag("span");
            List<ToDoTask> result = new List<ToDoTask>();
            foreach (var i in ele)
            {
                if (i.Attr("style") == "padding:1px 0 0 2px")
                {
                    var title = i.GetElementsByTag("a")[0];
                    string content = title.Attr("title");
                    if (content.Equals(""))
                    {
                        content = title.Text();                        
                    }
                    var urgencyElement = title.GetElementsByTag("b")[0];
                    string urgency = urgencyElement.Html();
                    var nextSpan = i.NextElementSibling;
                    string time = nextSpan.Html();
                    ToDoTask toDoTask = new ToDoTask(urgency, content, time);
                    result.Add(toDoTask);

                }
            }
            return result;
        }
        /// <summary>
        /// 比对两个list，取出新增的todo任务，并返回
        /// </summary>
        /// <param name="newToDOTaskList"></param>
        /// <param name="oldToDoTasList"></param>
        /// <returns></returns>
        public static List<ToDoTask> getNewToDoTaskList(List<ToDoTask> newToDOTaskList, List<ToDoTask> oldToDoTasList)
        {
            List<ToDoTask> result = new List<ToDoTask>();
            foreach (var newItem in newToDOTaskList)
            {
                bool isFind = false;
                foreach (var oldItem in oldToDoTasList)
                {
                    if (String.Equals(newItem.ToString(), oldItem.ToString()))
                    {
                        isFind = true;
                    }
                }
                if (!isFind)
                {
                    result.Add(newItem);
                }
            }
            return result;
        }
        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStamp(System.DateTime time, int length = 13)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (time.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位
            return t.ToString().Substring(0, length);
        }
    }
}
