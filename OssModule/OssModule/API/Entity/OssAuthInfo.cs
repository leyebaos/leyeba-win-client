using System;
using System.Runtime.Serialization;

namespace Com.Jk.Leyeba.OssModule.API.Entity
{
    /// <summary>
    ///     OSS验证信息
    /// </summary>
    [DataContract]
    internal class OssAuthInfo
    {
        /// <summary>
        ///     OSS ID
        /// </summary>
        [DataMember]
        public String Id { get; set; }

        /// <summary>
        ///     OSS 授权令牌
        /// </summary>
        [DataMember]
        public String Token { get; set; }

        /// <summary>
        ///     OSS 存储路径
        /// </summary>
        [DataMember]
        public String Path { get; set; }

        /// <summary>
        ///     Bucket 名称
        /// </summary>
        [DataMember]
        public String Name { get; set; }

        /// <summary>
        ///     请求状态
        /// </summary>
        [DataMember]
        public String Status { get; set; }

        /// <summary>
        ///     失败原因
        /// </summary>
        [DataMember]
        public String Reason { get; set; }
    }
}