using System;
using System.Collections.Generic;
using Util.JsonData;

namespace Util
{
    [Serializable]
    public class TaskTiming
    {
        public bool EndWork { get; set; }

        public DateTime WorkDate { get; set; }

        public int WorkSeconds { get; set; }

        public List<LogData> WorkLogList { get; set; }
    }
}
