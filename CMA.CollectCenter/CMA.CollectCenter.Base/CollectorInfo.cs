using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMA.CollectCenter.Base
{
    public class CollectorInfo
    {
        public DateTime LastTime { get; set; }

        //public int Interval { get; set; }

        public BaseCollector Collector { get; set; }
    }
}
