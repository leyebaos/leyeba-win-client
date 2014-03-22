using System;
using Com.Jk.Leyeba.OssModule.API.Entity;

namespace Com.Jk.Leyeba.OssModule.API
{
    /// <summary>
    ///     OSS验证信息获取接口
    /// </summary>
    internal interface IOssAuthProvider
    {
        /// <summary>
        ///     从服务器获取OSS验证信息
        /// </summary>
        /// <returns></returns>
        OssAuthInfo GetOssAuthInfo(String token);
    }
}