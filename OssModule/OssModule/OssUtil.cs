using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Com.Jk.Leyeba.OssModule.API;
using Com.Jk.Leyeba.OssModule.API.Entity;
using Aliyun.OpenServices.OpenStorageService;

namespace Com.jk.Leyeba.OssModule
{
    /// <summary>
    ///     OSS辅助类
    /// </summary>
    public class OssUtil
    {
        private static IOssAuthProvider _ossAuthProvider;

        private static OssAuthInfo _ossAuthInfo;

        /// <summary>
        ///     初始化OSS辅助类
        /// </summary>
        /// <param name="token">OSS访问令牌</param>
        public static bool Init(String token)
        {
            if (_ossAuthProvider == null)
            {
                _ossAuthProvider = new OssAuthProvider();
            }
            _ossAuthInfo = _ossAuthProvider.GetOssAuthInfo(token);
            return _ossAuthInfo != null;
        }

        /// <summary>
        ///     上传图像到OSS
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件上传到OSS后的Key值</returns>
        public static string UploadImage(string filePath)
        {
            try
            {
                if (_ossAuthInfo == null)
                {
                    return null;
                }
                OssClient client = new OssClient(_ossAuthInfo.Id, _ossAuthInfo.Token);
                using (FileStream fileStream = new FileStream(@filePath, FileMode.Open, FileAccess.Read))
                {
                    ObjectMetadata oMetaData = new ObjectMetadata();
                    string key = string.Format("{0}/{1}", _ossAuthInfo.Path, Path.GetFileName(filePath));
                    PutObjectResult result = client.PutObject(_ossAuthInfo.Name, key, fileStream, oMetaData);
                    return key;
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                try
                {
                    if (File.Exists(filePath))
                        File.Delete(filePath);
                }
                catch (Exception exp)
                {
                    throw exp;
                }
            }
        }
    }
}