using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YongYouAssistant
{
    class YongYouHandler
    {
        public static String userLogin(HTTPRequests requests)
        {
            String url = "http://10.0.15.16:7001/console/login.action";
            String account = "8d8c8e60bf9bbf4fd79e9e70fe8505a5e135777ed2ae79acbadb09bdb8d8d6fef4791323433ac6d405cd73cc8e6c6a9d29a516c49244f46bcd48d1a5561d6c5668b1abbbfa71a2bb70e714e146d6dd4c31ac6a146a5ae3a7c33f8e86671ba1e1dc8267c2127eea17b7f8d96586548810ed0e1d11271873be91e3432474818ef4";
            String password = "867d8180f4d6bea2c8bb4e440828c01c10f49634a081b81d247f14a1f3a3031998fdd7685e95e7895a198751a7fff6f6917fffe924f828e66b62fb3fdb691696c12b0d875ec28a98784e7635091f2caec93488bcae17e5cd11972575543a8a1de4e990504a709fb41e9e450db82edc8e656e14b2ff20f4f15367ed4ade6b9bae";
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
        public static string  getToDoListHtml(HTTPRequests requests)
        {
            String url = "http://10.0.15.16:7001/console/remindWorkSpace.action";
            string respone = requests.get(url);
            return respone;

        }
        public List<ToDoTask> GetToDoTasks(string html)
        {
            return null;
        }
    }
}
