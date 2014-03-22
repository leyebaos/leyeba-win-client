using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Util.JsonData;

namespace Util
{
    [Serializable]
    public class AutologsTiming
    {
        public DateTime Date { get; set; }
        public List<AutoLogs> AutoLogList { get; set; }
    }
}
