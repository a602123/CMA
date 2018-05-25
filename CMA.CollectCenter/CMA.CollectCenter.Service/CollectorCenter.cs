using CMA.CollectCenter.Base;
using CMA.Common.MySQLHelper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis.Support;
using System.Threading;
using CMA.Common.Model;
using System.Data.Common;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;

namespace CMA.CollectCenter.Service
{
    public class CollectorCenter
    {
        private static CollectorCenter _instance;

        private static object _lock = new object();
        public static CollectorCenter GetInstance()
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new CollectorCenter();
                }
                return _instance;
            }
        }

        private List<CollectorInfo> _collectorList;
        private int _centerInterval;
        private bool _state;

        private CollectorCenter()
        {
            _collectorList = new List<CollectorInfo>();
            _centerInterval = Convert.ToInt32(ConfigurationManager.AppSettings["CenterInterval"]);
        }


        //public CollectorCenter(CollectorType itemtype, BaseParamter baseParamter)
        //{
        //    _collectorList = new List<CollectorInfo>();
        //    _centerInterval = Convert.ToInt32(ConfigurationManager.AppSettings["CenterInterval"]);

        //    InitCollectors(itemtype, baseParamter);
        //}

        //public CollectorCenter(CollectorType itemtype, byte[] bytesParamter)
        //{
        //    _collectorList = new List<CollectorInfo>();
        //    _centerInterval = Convert.ToInt32(ConfigurationManager.AppSettings["CenterInterval"]);

        //    InitCollectors(itemtype, bytesParamter);
        //}


        public void StartMonitor()
        {
            _state = true;
            Task.Factory.StartNew(Monitor);
        }

        public void Stop()
        {
            _collectorList.Clear();
            _state = false;
        }

       
        private object _listlock = new object();
        public void Add(CollectorType type,BaseParamter paramter)
        {
            lock (_listlock)
            {
                try
                {
                    if (_collectorList.Where(n => n.Collector.Paramter.DeviceId == paramter.DeviceId).FirstOrDefault() == null)
                    {
                        _collectorList.Add(new CollectorInfo()
                        {
                            Collector = CollectorFactory.CreateInstance(type.GetCollectorType(), paramter),
                            LastTime = DateTime.MinValue
                        });
                    } 
                }
                catch (Exception)
                {

                    throw;
                }
            }
            
        }

        public void Remove(long id)
        {
            lock (_listlock)
            {
                try
                {
                    var collector = _collectorList.Where(n => n.Collector.Paramter.DeviceId == id).FirstOrDefault();
                    if (collector != null)
                    {
                        _collectorList.Remove(collector);
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public void Updata(long id, CollectorType type, BaseParamter paramter)
        {
            Remove(id);
            Add(type, paramter);
        }
        private void Monitor()
        {
            try
            {
                while (_state)
                {
                    Parallel.For(0, _collectorList.Count, (i) => {
                        var timeSpan = (DateTime.Now - _collectorList[i].LastTime).TotalMilliseconds;
                        if (timeSpan > Convert.ToDouble(_collectorList[i].Collector.Paramter.Interval))
                        {
                            _collectorList[i].Collector.Collect();
                            _collectorList[i].LastTime = DateTime.Now;
                        }
                    });
                    Thread.Sleep(_centerInterval);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //private void InitCollectors(CollectorType itemtype, byte[] bytesParamter)
        //{
        //    //_collectorList = collectorProvider.GetList("");
        //    BaseCollector baseCollector = new CollectorInfoDataProvider().GetCollectorByBytes(itemtype, bytesParamter);
        //    CollectorInfo infoModel = new CollectorInfo();
        //    infoModel.Collector = baseCollector;
        //    infoModel.LastTime = DateTime.MinValue;
        //    _collectorList.Add(infoModel);
        //}

        //private void InitCollectors(CollectorType itemtype, BaseParamter baseParamter)
        //{
        //    //_collectorList = collectorProvider.GetList("");
        //    BaseCollector baseCollector = new CollectorInfoDataProvider().GetCollectorByParam(itemtype, baseParamter);
        //    CollectorInfo infoModel = new CollectorInfo();
        //    infoModel.Collector = baseCollector;
        //    infoModel.LastTime = DateTime.MinValue;
        //    _collectorList.Add(infoModel);
        //}
    }
}
