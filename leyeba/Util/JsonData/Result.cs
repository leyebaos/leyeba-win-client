using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Util.JsonData
{
    public class Result : BaseJsonData
    {
        public int ID { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 是否超时
        /// </summary>
        public bool Timeout { get; set; }

        public Result() { }
    }
}
