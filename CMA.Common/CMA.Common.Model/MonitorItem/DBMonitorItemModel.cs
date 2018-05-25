using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMA.Common.Model
{
    public class DBMonitorItemModel:BaseMonitorItemModel
    {
        public override MonitorItemType ItemType
        {
            get
            {
                return MonitorItemType.Web;
            }
        }

        public DBParamter GetPatamter()
        {
            return Paramter as DBParamter;
        }
    }
}
