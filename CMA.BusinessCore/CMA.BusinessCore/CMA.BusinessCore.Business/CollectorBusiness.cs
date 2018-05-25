using CMA.BusinessCore.CollectorClient;
using CMA.BusinessCore.DBClient;
using CMA.Common.Model;
using CMA.Common.RedisHelper;
using CMA.Common.WebApiClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CMA.BusinessCore.Business
{
    public class CollectorBusiness
    {
        private static CollectorBusiness _instance;
        private RedisOperator _redis;
        private List<string> _collectorList;   //采集器列表
        private int _checkCollectorStateInterval; // 采集器状态 频率
        private string _collectorStateKeyInRedis;  //采集器状态 的 redis key
        private object _listlock = new object(); //采集器列表 锁
        private bool _checkState = false;    //轮询监测的开关

        public static CollectorBusiness GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CollectorBusiness();
            }
            return _instance;
        }

        private CollectorBusiness()
        {
            _collectorList = new List<string>();
            string connStr = System.Configuration.ConfigurationManager.AppSettings["RedisConnectString"];
            _redis = RedisOperator.NewInstance(connStr);

            _checkCollectorStateInterval = Convert.ToInt32(ConfigurationManager.AppSettings["CheckCollectorStateInterval"]);
            _collectorStateKeyInRedis = ConfigurationManager.AppSettings["CollectorStateKeyInRedis"];
        }

        public void Init()
        {
            _collectorList.Clear();
            IEnumerable<CollectorDBModel> itemlist = new CollectorDBClient().GetList();
            var collector = new MonitorItemCollectorClient();

            foreach (var item in itemlist)
            {
                _collectorList.Add(item.Host);
            }
        }


        /// <summary>
        /// 从redis 中读取 存储的Collector State
        /// </summary>
        /// <returns></returns>
        public string GetCollectorState()
        {
            return _redis.GetAndExpire<string>(_collectorStateKeyInRedis);
        }

        /// <summary>
        /// 开始轮询监测
        /// </summary>
        public void StartCheckState()
        {
            Init();
            _checkState = true;
            Task.Factory.StartNew(CheckCollectorState);
        }

        /// <summary>
        /// 停止轮询监测
        /// </summary>
        public void StopCheckState()
        {
            _checkState = false;
            _collectorList.Clear();
        }

        /// <summary>
        /// 轮询采集 Collector State
        /// </summary>
        /// <returns></returns>
        private void CheckCollectorState()
        {
            Dictionary<string, bool> stateDic = new Dictionary<string, bool>();
            try
            {
                while (_checkState)
                {
                    stateDic.Clear();
                    //Parallel.For(0, _collectorList.Count, (i) =>
                    //{
                    //    stateDic.Add(_collectorList[i], CheckOneState(_collectorList[i]));
                    //});
                    lock (_listlock)
                    {
                        foreach (var item in _collectorList)
                        {
                            stateDic.Add(item, CheckOneState(item));
                        } 
                    }
                    Thread.Sleep(_checkCollectorStateInterval);

                    _redis.Set<string>(_collectorStateKeyInRedis, Newtonsoft.Json.JsonConvert.SerializeObject(stateDic));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 使用http head 监测 单个 采集器 是否联通
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public bool CheckOneState(string host)
        {
            bool result = false;

            WebApiClient client = new WebApiClient();
            try
            {
                return client.Head($"http://{host}");
            }
            #region 不用的WebRequest Head
            //WebRequest request = WebRequest.CreateDefault(new Uri($"http://{host}"));
            //request.Method = "HEAD";
            //request.Timeout = 5000;
            //WebResponse response;
            //try
            //{
            //    response = request.GetResponse();
            //    if (response.Headers.Count > 0)
            //    {
            //        response.Close();
            //        request.Abort();
            //        result = true;
            //    }
            //    else
            //    {
            //        response.Close();
            //        request.Abort();
            //        result = false;
            //    }
            //    return result;
            //} 
            #endregion
            catch (Exception ex)
            {
                //request.Abort();
                return result;
            }
        }


        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="host"></param>
        public bool AddHost(CollectorDBModel model)
        {
            bool result = false;
            lock (_listlock)
            {
                try
                {
                    var item = _collectorList.Find(n => n == model.Host);
                    if (item == null)
                    {
                        //同时修改数据库
                        if (!new CollectorDBClient().Add(model))
                        {
                            throw new Exception("同步添加采集器成功，但同步添加数据库失败，请重新检查！");
                        }
                        else
                        {
                            _collectorList.Add(model.Host);
                        }
                    }
                    else
                    {
                        throw new Exception("采集器已存在，请重新检查！");
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            result = true;

            return result;
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="host"></param>
        public bool RemoveHost(string host)
        {
            bool result = false;

            lock (_listlock)
            {
                try
                {
                    if (_collectorList.Find(n => n == host) != null)
                    {
                        //同时修改数据库
                        if (!new CollectorDBClient().Delete(host))
                        {
                            throw new Exception("同步删除采集器成功，但同步删除数据库失败，请重新检查！");
                        }
                        else
                        {
                            _collectorList.Remove(host);
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            result = true;

            return result;
        }

        /// <summary>
        /// 发现新的采集器
        /// </summary>
        /// <param name="startIP"></param>
        /// <param name="endIP"></param>
        /// <param name="port"></param>
        public void FindNewHost(string startIP, string endIP, int port)
        {

            UdpClient sendClient = new UdpClient();                                                 
            byte[] bytes = Encoding.Unicode.GetBytes("CheckHost");
            IPEndPoint sendEndPoint = new IPEndPoint(IPAddress.Parse(startIP), port);
            sendClient.Send(bytes, bytes.Length, sendEndPoint);

            System.Threading.Timer timer = new Timer(TimerCallbackFunc);

            if (sendClient.Available > 0)
            {

            }
            else
            {
                Thread.Sleep(5000);

                sendClient.BeginReceive(ReceiveCallback, new UdpState { Client = sendClient, EndPoint = sendEndPoint });

            }
        }

        private void TimerCallbackFunc(object state)
        {
            
        }

        private void ReceiveCallback(IAsyncResult iar)
        {
            UdpState udpReceiveState = iar.AsyncState as UdpState;
            if (iar.IsCompleted)
            {
                IPEndPoint endPoint = udpReceiveState.EndPoint;
                UdpClient udpclient = udpReceiveState.Client;
                byte[] receiveBytes = udpclient.EndReceive(iar, ref endPoint);
                string receiveString = Encoding.ASCII.GetString(receiveBytes);
                Console.WriteLine("{0} {1}\r\n", endPoint.Address, receiveString);

                udpclient.BeginReceive(ReceiveCallback, new UdpState { Client = udpclient, EndPoint = endPoint });
                udpclient.Close();
            }
        }


    }

    public class UdpState
    {  
        public UdpClient Client { get; set; }

        public IPEndPoint EndPoint { get; set; }
    }
}
