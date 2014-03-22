using System;
using System.Collections.Specialized;
using System.Text;
using System.Net;
using Util.ConfigManage;

namespace Util
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
                WebClientEx webClient = initWebClient();
                if (c != null && c.Count > 0)
                    webClient.Headers.Add(c);
                return webClient.DownloadData(address);
            }
            catch (Exception exp)
            {
                Log.error(typeof(WebHelper), (exp.InnerException == null ? "" : exp.InnerException.ToString()) + exp.Message);
                return null;
            }
        }

        public static void GetWebResponseDataAsync(string address, NameValueCollection c, DownloadDataCompletedEventHandler handler)
        {
            try
            {
                WebClientEx webClient = initWebClient();                
                if (c != null && c.Count > 0)
                    webClient.Headers.Add(c);
                webClient.DownloadDataAsync(new Uri(address));
                webClient.DownloadDataCompleted += handler;
            }
            catch (Exception exp)
            {
                Log.error(typeof(WebHelper), (exp.InnerException == null ? "" : exp.InnerException.ToString()) + exp.Message);
                return;
            }
        }

        private static WebClientEx initWebClient()
        {
            WebClientEx webClient = new WebClientEx();
            SystemSetting setting = ConfigHelper.SystemSettingConfig;
            if (setting != null)
            {
                if (setting.Proxy != null &&
                    setting.Proxy.Enabled)
                {
                    ProxyInfo proxyInfo = setting.Proxy;
                    if (!string.IsNullOrWhiteSpace(proxyInfo.Host) &&
                        proxyInfo.Port != 0 &&
                        !string.IsNullOrWhiteSpace(proxyInfo.UserName) &&
                        !string.IsNullOrWhiteSpace(proxyInfo.Password) &&
                        !string.IsNullOrWhiteSpace(proxyInfo.Domain))
                    {
                        WebProxy proxy = new WebProxy(proxyInfo.Host, proxyInfo.Port);
                        proxy.Credentials = new NetworkCredential(proxyInfo.UserName, proxyInfo.Password, proxyInfo.Domain);
                        webClient.Proxy = proxy;
                    }
                }
            }
            webClient.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
            webClient.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8");
            webClient.Headers.Add("Cache-Control", "max-age=0");
            webClient.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; InfoPath.2; .NET4.0C; .NET4.0E; Zune 4.7)");
            webClient.Headers.Add("Content-Type", "text/plain;charset=UTF-8");

            return webClient;
        }

        public static bool ValidateProxy(ProxyInfo proxyInfo)
        {
            try
            {
                WebClientEx webClient = new WebClientEx();
                if (proxyInfo == null ||
                    !proxyInfo.Enabled)
                    return false;
                WebProxy proxy = new WebProxy(proxyInfo.Host, proxyInfo.Port);
                proxy.Credentials =
                    new NetworkCredential(proxyInfo.UserName, proxyInfo.Password, proxyInfo.Domain);
                webClient.Proxy = proxy;
                byte[] bts = webClient.DownloadData("http://www.leyeba.net/");
                if (bts == null ||
                    bts.Length == 0)
                    return false;
                return true;
            }
            catch (Exception exp)
            {
                Log.error(typeof(WebHelper), (exp.InnerException == null ? "" : exp.InnerException.ToString()) + exp.Message);
                return false;
            }
        }
    }
}
