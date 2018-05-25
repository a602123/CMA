using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMA.WebSite.DBClient
{
    public class UrlHelper
    {
        ConfigHelper _config;

        private static UrlHelper _instance;

        private static object _lock = new object();
        public static UrlHelper GetInstance()
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new UrlHelper();
                }
            }
            return _instance;
        }

        private UrlHelper()
        {
            _config = ConfigHelper.GetInstance();
        }

        public string GetDBUrl(string url)
        {
            return $"{_config.DBHost}/data/{url}";
        }
    }
}
