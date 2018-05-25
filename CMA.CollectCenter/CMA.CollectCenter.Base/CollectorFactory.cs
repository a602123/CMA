using CMA.Common.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CMA.CollectCenter.Base
{
    public static class CollectorFactory
    {
        public static BaseCollector CreateInstance(string configKey, BaseParamter paramter)
        {
            string configValue = configKey;
            var array = configValue.Split(',');
            string assblyName = array[0];
            string typeName = array[1];
            Type t = Assembly.Load(assblyName).GetType(typeName);
            return (BaseCollector)Activator.CreateInstance(t, paramter);
        }
    }


}
