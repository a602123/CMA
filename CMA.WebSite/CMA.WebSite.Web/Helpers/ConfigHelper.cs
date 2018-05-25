using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace CMA.WebSite.Web
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
            _siteName = ConfigurationManager.AppSettings["SiteName"];
        }

        private string _siteName;
        /// <summary>
        /// 站点名称
        /// </summary>
        public string SiteName
        {
            get
            {
                return _siteName;
            }
        }
    }
}