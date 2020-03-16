using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/**
 *待办事项的任务类 
 */
namespace YongYouAssistant
{
    class ToDoTask
    {
        public enum Urgency :uint
        {
            无=0,
            紧急=1,
            特急=2
        }

        private string content;
        private string createTime;
        private Urgency urgency;

        public ToDoTask(Urgency urgency,string content,string createTime)
        {
            this.urgency = urgency;
            this.content = content;
            this.createTime = createTime;
        }

        override
        public string ToString()
        {
            StringBuilder sb = new StringBuilder();
            switch (this.urgency)
            {
                case Urgency.无:
                    break;
                case Urgency.紧急:
                    sb.Append("紧急");
                    break;
                case Urgency.特急:
                    sb.Append("特急");
                    break;

            }
            sb.Append(this.content);
            sb.Append("\t");
            sb.Append(this.createTime);
            return sb.ToString();
        }
    }
}
