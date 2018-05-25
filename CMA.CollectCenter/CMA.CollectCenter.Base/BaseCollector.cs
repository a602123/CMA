using CMA.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMA.CollectCenter.Base
{
    public abstract class BaseCollector
    {
        protected string _host;

        public BaseParamter Paramter { get; set;}

        public BaseCollector(BaseParamter paramter)
        {
            _host = paramter.Host;
            Paramter = paramter;
        }

        public virtual void Collect()
        {

        } 
    }
}
