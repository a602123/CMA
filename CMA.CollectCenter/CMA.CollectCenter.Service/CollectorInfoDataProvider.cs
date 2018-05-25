using CMA.CollectCenter.Base;
using CMA.Common.Model;
using ServiceStack.Redis.Support;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace CMA.CollectCenter.Service
{
    public class CollectorInfoDataProvider
    {
        private ObjectSerializer _ser;

        public CollectorInfoDataProvider()
        {
            _ser = new ObjectSerializer();
        }

        #region 没用了
        //public bool Insert(BaseCollector collector)
        //{
        //    try
        //    {
        //        BaseParamter paramter = collector.Paramter;

        //        //string typeStr =paramter.GetType().ToString();

        //        using (MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection())
        //        {
        //            conn.ConnectionString = ConfigurationManager.AppSettings["MySqlConnectionStr"];
        //            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand();
        //            cmd.CommandText = "insert into tb_monitoritem(name,ip,note,itemtype,paramter) values(@name,@ip,@note,@itemtype,@paramter)";
        //            cmd.CommandType = CommandType.Text;
        //            cmd.Parameters.Add("@name", MySql.Data.MySqlClient.MySqlDbType.VarChar);
        //            cmd.Parameters.Add("@ip", MySql.Data.MySqlClient.MySqlDbType.VarChar);
        //            cmd.Parameters.Add("@note", MySql.Data.MySqlClient.MySqlDbType.VarChar);
        //            cmd.Parameters.Add("@itemtype", MySql.Data.MySqlClient.MySqlDbType.Int32);
        //            cmd.Parameters.Add("@paramter", MySql.Data.MySqlClient.MySqlDbType.VarBinary);

        //            cmd.Parameters[0].Value = Guid.NewGuid();
        //            cmd.Parameters[1].Value = paramter.Host;
        //            cmd.Parameters[2].Value = cmd.Parameters[0].Value;
        //            cmd.Parameters[3].Value = 1;
        //            cmd.Parameters[4].Value = _ser.Serialize(paramter);
        //            cmd.Connection = conn;
        //            conn.Open();

        //            int affectedrows = cmd.ExecuteNonQuery();

        //            cmd.Dispose();//此处可以不用调用,  
        //            conn.Close();// 离开 using 块, connection 会自行关闭  
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //public List<CollectorInfo> GetList(string condition)
        //{
        //    List<CollectorInfo> list = new List<CollectorInfo>();

        //    using (MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection())
        //    {
        //        conn.ConnectionString = ConfigurationManager.AppSettings["MySqlConnectionStr"];
        //        conn.Open();

        //        MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand();
        //        cmd.CommandType = CommandType.Text;
        //        cmd.CommandText = "select name,ip,note,itemtype,paramter from tb_monitoritem";
        //        cmd.Connection = conn;

        //        System.Data.Common.DbDataReader reader = cmd.ExecuteReader();
        //        byte[] buffer = null;
        //        if (reader.HasRows)
        //        {
        //            while (reader.Read())
        //            {
        //                CollectorInfo info = new CollectorInfo();
        //                int itemtype = reader.GetInt32(3);

        //                long len = reader.GetBytes(4, 0, null, 0, 0);
        //                buffer = new byte[len];
        //                len = reader.GetBytes(4, 0, buffer, 0, (int)len);

        //                info.Collector = GetCollectorByBytes(itemtype, buffer);
        //                info.LastTime = DateTime.MinValue;
        //                list.Add(info);
        //            }
        //        }
        //    }

        //    return list;
        //}
        #endregion

        public BaseCollector GetCollectorByBytes(CollectorType itemtype, byte[] bytes)
        {
            BaseCollector collector = null;

            string collectType = EnumExtension.GetCollectorType(itemtype);
            string collectNameSpace = collectType.Substring(0, collectType.LastIndexOf("."));

            BaseParamter baseParamter = (BaseParamter)Activator.CreateInstance(EnumExtension.GetParamterType((CollectorType)itemtype));
            baseParamter = (DBParamter)_ser.Deserialize(bytes);

            collector = CollectorFactory.CreateInstance(collectNameSpace + "," + collectType, baseParamter);

            return collector;
        }

        public BaseCollector GetCollectorByParam(CollectorType itemtype, BaseParamter baseParamter)
        {
            BaseCollector collector = null;

            string collectType = EnumExtension.GetCollectorType(itemtype);
            string collectNameSpace = collectType.Substring(0, collectType.LastIndexOf("."));
            collector = CollectorFactory.CreateInstance(collectNameSpace + "," + collectType, baseParamter);
            return collector;
        }


    }
}
