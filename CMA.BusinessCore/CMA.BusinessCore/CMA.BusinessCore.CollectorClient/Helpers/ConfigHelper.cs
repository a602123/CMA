﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace CMA.BusinessCore.CollectorClient
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
            
        }
        
    }
}