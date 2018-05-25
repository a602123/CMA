using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMA.Common.Model
{
    public class MonitorItemDBModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Host { get; set; }

        public string Note { get; set; }

        public MonitorItemType ItemType { get; set; }

        public string CollectorHost { get; set; }

        public string Paramter { get; set; }
    }
}
