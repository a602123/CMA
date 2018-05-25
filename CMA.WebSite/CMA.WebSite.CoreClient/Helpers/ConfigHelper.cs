using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace CMA.WebSite.CoreClient
{
    public class ConfigHelper
    {
        private static ConfigHelper _instance;

        private static object _lock = new object();
        public static ConfigHelper GetInstance()
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new ConfigHelper();
                }
            }
            return _instance;
        }

        private ConfigHelper()
        {
            _dbHost = ConfigurationManager.AppSettings["BusinessHost"];
        }

        private string _dbHost;
        /// <summary>
        /// 站点名称
        /// </summary>
        public string DBHost
        {
            get
            {
                return _dbHost;
            }
        }
    }
}