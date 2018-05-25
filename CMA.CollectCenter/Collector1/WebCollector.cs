using CMA.CollectCenter.Base;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CMA.Common.Model;
using CMA.Common.RedisHelper;
using CMA.Common.MySQLHelper;

namespace Collector1
{
    public class WebCollector : BaseCollector
    {
        private CollectorBusiness _business;
        private string _connStr;

        public WebCollector(DBParamter paramter) : base(paramter)
        {
            _connStr = $"Database={paramter.DBName};Data Source={paramter.Host};User Id={paramter.Username};Password={paramter.Password};pooling=false;CharSet=utf8;port={paramter.Port}";
            _business = new Collector1.CollectorBusiness(_connStr);

            Paramter = paramter;
        }

        public override void Collect()
        {
            _business.Collect();
        }
    }
}
