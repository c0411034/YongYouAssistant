using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YongYouAssistant
{
    /// <summary>
    /// 网络状态
    /// </summary>
    class NetContectionStastion
    {
        public const uint ONLY_INTERNET_CONNECT = 0;
        public const uint ONLY_INTARNET_CONNECT = 1;
        public const uint DOUBLE_NET_CONNECT = 2;
        public const uint NONE_NET_CONNECT = 3;
        public bool isConnectInternet = false;
        public bool isConnectIntranet = false;
        public uint getConnectStation()
        {
            if (isConnectIntranet)
            {
                if (isConnectInternet)
                {
                    return DOUBLE_NET_CONNECT;
                }
                else
                {
                    return ONLY_INTARNET_CONNECT;
                }
            }
            else if (isConnectInternet)
            {
                return ONLY_INTERNET_CONNECT;
            }
            else
            {
                return NONE_NET_CONNECT;
            }
        }
        public static bool testIsConnectInternet()
        {
            string result = HTTPRequests.Instance.get("http://www.baidu.com");
            return result == HTTPRequests.CONN_ERR ? false : true;
        }
        public static bool testIsConnectIntranet()
        {
            string result = HTTPRequests.Instance.get("http://10.0.15.16:7001");
            return result == HTTPRequests.CONN_ERR ? false : true;
        }
        
        
    }
}
