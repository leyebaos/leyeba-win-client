using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Util
{
    public class TBoolResult<T>
    {
        public bool Result { get; set; }

        public T Data { get; set; }
    }
}
