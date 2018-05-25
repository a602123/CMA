using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMA.Common.Model
{
    public abstract class BaseMonitorItemModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Note { get; set; }

        public abstract MonitorItemType ItemType { get; }

        public BaseParamter Paramter { get; set; }

        public string CollectorHost { get; set; }
    }
}
