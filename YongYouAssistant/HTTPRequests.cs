using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;

namespace YongYouAssistant
{
    class HTTPRequests
    {

        public static String CONN_ERR = "CONN_ERR";//网络连接失败

        private static CookieContainer _cookie = new CookieContainer();
        private NLog.Logger logger;
        public HTTPRequests()
        {

            logger = NLog.LogManager.GetCurrentClassLogger();
        }

        public String get(String url)
        {
            HttpWebRequest Web_Request = createHttpRequest(url);
            Web_Request.Method = "GET";
            return getRespones(Web_Request);

        }
        public String post(String url)
        {
            HttpWebRequest Web_Request = createHttpRequest(url);
            Web_Request.Method = "POST";
            return getRespones(Web_Request);

        }
        public String getRespones(HttpWebRequest Web_Request)
        {
            HttpWebResponse Web_Response;
            string html;
            try
            {
                Web_Response = (HttpWebResponse)Web_Request.GetResponse();
            }
            catch (Exception)
            {
                return CONN_ERR;
            }

            logger.Info("本次获取的Cookie：" + GetCookieIndexCookieKey(_cookie, ".Xauth"));
            if (Web_Response.ContentEncoding.ToLower() == "gzip")  // 如果使用了GZip则先解压
            {
                using (Stream Stream_Receive = Web_Response.GetResponseStream())
                {
                    using (var Zip_Stream = new GZipStream(Stream_Receive, CompressionMode.Decompress))
                    {
                        using (StreamReader Stream_Reader = new StreamReader(Zip_Stream, Encoding.UTF8))
                        {
                            html = Stream_Reader.ReadToEnd();
                        }
                    }
                }
            }
            else
            {
                using (Stream Stream_Receive = Web_Response.GetResponseStream())
                {
                    using (StreamReader Stream_Reader = new StreamReader(Stream_Receive, Encoding.UTF8))
                    {
                        html = Stream_Reader.ReadToEnd();
                    }
                }
            }
            logger.Info("本次获取的html：" + html);
            return html;
        }
        private HttpWebRequest createHttpRequest(string url)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.CookieContainer = _cookie;
            //request.Timeout = 90000;
            //request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            //request.Headers.Add("Accept-Encoding", "gzip,deflate,sdch");
            //request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8");
            //request.UserAgent = @"Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36";
            //request.KeepAlive = true;
            return request;

        }
        public static string GetCookieIndexCookieKey(CookieContainer cc, string cookieName)
        {

            List<Cookie> lstCookies = new List<Cookie>();

            Hashtable table = (Hashtable)cc.GetType().InvokeMember("m_domainTable",

                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField |

                System.Reflection.BindingFlags.Instance, null, cc, new object[] { });

            foreach (object pathList in table.Values)

            {
                SortedList lstCookieCol = (SortedList)pathList.GetType().InvokeMember("m_list",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField
                    | System.Reflection.BindingFlags.Instance, null, pathList, new object[] { });
                foreach (CookieCollection colCookies in lstCookieCol.Values)

                    foreach (Cookie c1 in colCookies) lstCookies.Add(c1);
            }
            var model = lstCookies.Find(p => p.Name == cookieName);

            if (model != null)
            {
                return model.Value;
            }
            return string.Empty;

        }

    }
}
