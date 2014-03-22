using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using Com.Jk.Leyeba.OssModule.API.Entity;

namespace Com.Jk.Leyeba.OssModule.API
{
    /// <summary>
    ///     OSS验证信息获取
    /// </summary>
    internal class OssAuthProvider : IOssAuthProvider
    {
        private OssAuthInfo _ossAuthInfo;

        public OssAuthInfo GetOssAuthInfo(String token)
        {
            if (_ossAuthInfo != null) return _ossAuthInfo;
            string requestUri = String.Format("{0}{1}", Constant.LeyebaApiEndpoint, Constant.LeyebaOssAuthInfo);
            var uri = new Uri(string.Format(@requestUri));
            var request = WebRequest.Create(requestUri) as HttpWebRequest;
            if (request == null) return _ossAuthInfo;
            request.Headers.Add("Token", token);
            using (var response = request.GetResponse() as HttpWebResponse)
            {
                if (response == null) return _ossAuthInfo;
                var reader = new StreamReader(response.GetResponseStream());
                string result = reader.ReadToEnd();
                var json = new DataContractJsonSerializer(typeof (OssAuthInfo));
                using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(result)))
                {
                    return _ossAuthInfo = (OssAuthInfo) json.ReadObject(stream);
                }
            }
        }
    }
}