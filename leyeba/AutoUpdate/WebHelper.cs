using System;
using System.Collections.Specialized;
using System.Net;
using System.IO;
using System.Text;

namespace AutoUpdate
{
    public class WebHelper
    {
        public static string GetWebResponseString(string address, NameValueCollection c, Encoding e)
        {
            if (string.IsNullOrWhiteSpace(address)) return null;
            byte[] bts = GetWebResponseData(address, c);
            if (bts == null || bts.Length == 0) return null;
            return e.GetString(bts);
        }

        public static byte[] GetWebResponseData(string address, NameValueCollection c)
        {
            try
            {
                WebClient webClient = InitWebClient();
                if (c != null && c.Count > 0)
                    webClient.Headers.Add(c);
                return webClient.DownloadData(address);
            }
            catch (Exception exp)
            {
                File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AutoUpdate.WebHelper.error.txt"), exp.Message);
                return null;
            }
        }

        private static WebClient InitWebClient()
        {
            WebClient webClient = new WebClient();
            webClient.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
            webClient.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8");
            webClient.Headers.Add("Cache-Control", "max-age=0");
            webClient.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; InfoPath.2; .NET4.0C; .NET4.0E; Zune 4.7)");
            webClient.Headers.Add("Content-Type", "text/plain;charset=UTF-8");

            return webClient;
        }
    }
}
