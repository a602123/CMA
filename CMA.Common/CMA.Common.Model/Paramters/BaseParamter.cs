using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMA.Common.Model
{
    [Serializable]
    public class BaseParamter
    {
        public string Host { get; set; }
        public int Interval { get; set; }

        public int DeviceId { get; set; }
    }
}
