using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/**
 *待办事项的任务类 
 */
namespace YongYouAssistant
{
    public class ToDoTask
    {
        public enum Urgency :uint
        {
            无=0,
            急件=1,
            紧急=2,
            特急=3
        }

        public string content;
        public string createTime;
        public string urgency;

        public ToDoTask(string urgency,string content,string createTime)
        {
            this.urgency = urgency;
            this.content = content;
            this.createTime = createTime;
        }

        override
        public string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.urgency);
            sb.Append(this.content);
            sb.Append("\t");
            sb.Append(this.createTime);
            return sb.ToString();
        }
    }
}
