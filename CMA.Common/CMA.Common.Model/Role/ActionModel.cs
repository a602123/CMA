using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMA.Common.Model
{
    
    public class ActionModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string cFree { get; set; }

        public long? iFree { get; set; }
    }
}
