using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Util.JsonData
{
    /// <summary>
    /// Json数据基类
    /// </summary>
    [DataContract]
    public class BaseJsonData
    {
        /// <summary>
        /// 返回状态：0异常，1正常
        /// </summary>
        [DataMember(Name="Status")]
        public string Status { get; set; }
        /// <summary>
        /// 异常结果说明
        /// </summary>
        [DataMember(Name = "Reason")]
        public string Reason { get; set; }
    }
}
