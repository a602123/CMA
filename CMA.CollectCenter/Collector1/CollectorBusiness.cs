using CMA.Common.MySQLHelper;
using CMA.Common.RedisHelper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector1
{
    public class CollectorBusiness
    {
        private MysqlOperator _mySQLHelper;
        private RedisOperator _redisHelper;

        internal CollectorBusiness(string connStr)
        {
            _mySQLHelper = new MysqlOperator(connStr);
            _redisHelper = RedisOperator.GetInstance();
        }

        internal void Collect()
        {
            try
            {
                List<string> collectResult = new List<string>();
                collectResult = GetData();
                string queue = ConfigurationManager.AppSettings["RedisQueue"];
                if (_redisHelper.RPush<string>(queue, collectResult))
                {
                    string str = "";
                    //Console.WriteLine(DateTime.Now.ToLongTimeString() + "WebCollector采集数据一次");
                }
                //Task.Factory.StartNew(() =>
                //{
                //    List<string> collectResult = GetData();
                //    string queue = ConfigurationManager.AppSettings["RedisQueue"];
                //    if (_redisHelper.RPush<string>(queue, collectResult))
                //    {
                //        string str = "";
                //        //Console.WriteLine(DateTime.Now.ToLongTimeString() + "WebCollector采集数据一次");
                //    }
                //});
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        internal List<string> GetData()
        {
            List<string> collectResult = new List<string>();

            MySqlParameter[] parameters = null;

            DataSet dataSet = _mySQLHelper.ExecuteDataSet("select * from t_user", parameters);
            if (dataSet.Tables.Count > 0)
            {
                if (dataSet.Tables[0].Rows.Count > 0 && dataSet.Tables[0].Columns.Count > 0)
                {
                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        collectResult.Add(dataSet.Tables[0].Rows[i][0].ToString());
                    }
                }
            }
            return collectResult;
        }

        //public bool PushToRedis(List<string> collectResult)
        //{
        //    try
        //    {
        //        string queue = ConfigurationManager.AppSettings["RedisQueue"];
        //        if (_redisHelper.RPush<string>(queue, collectResult))
        //        {
        //            string str = "";
        //            //Console.WriteLine(DateTime.Now.ToLongTimeString() + "WebCollector采集数据一次");
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
    }
}
