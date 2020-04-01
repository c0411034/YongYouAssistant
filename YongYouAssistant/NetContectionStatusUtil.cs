using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YongYouAssistant
{
    /// <summary>
    /// 网络状态判断
    /// </summary>
    class NetContectionStatusUtil
    {
        public const uint ONLY_INTERNET_CONNECT = 0;
        public const uint ONLY_INTARNET_CONNECT = 1;
        public const uint DOUBLE_NET_CONNECT = 2;
        public const uint NONE_NET_CONNECT = 3;
        public static uint getNetConnection()
        {
            bool isConnetInternet = false;
            bool isConnetIntarnet = false;
            HTTPRequests h = HTTPRequests.Instance;
            string result=h.get("http://10.0.15.16:7001");
            if (result != HTTPRequests.CONN_ERR)
            {
                isConnetIntarnet = true;
            }

            result = h.get("http://www.baidu.com");
            if (result != HTTPRequests.CONN_ERR)
            {
                isConnetInternet = true;
            }
            if (isConnetIntarnet)
            {
                if (isConnetInternet)
                {
                    return DOUBLE_NET_CONNECT;
                }
                else
                {
                    return ONLY_INTARNET_CONNECT;
                }
            }
            else if (isConnetInternet)
            {
                return ONLY_INTERNET_CONNECT;
            }
            else
            {
                return NONE_NET_CONNECT;
            }

        }
    }
}
