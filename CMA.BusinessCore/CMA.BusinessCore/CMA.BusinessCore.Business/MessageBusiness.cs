using CMA.Common.Model;
using CMA.Common.RedisHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CMA.BusinessCore.Business
{
    public class MessageBusiness
    {
        private static MessageBusiness _instance;

        public static MessageBusiness GetInstance()
        {
            if (_instance == null)
            {
                _instance = new MessageBusiness();
            }
            return _instance;
        }


        RedisOperator _redis;
        private bool _workState;

        private MessageBusiness()
        {
            string connStr = System.Configuration.ConfigurationManager.AppSettings["RedisConnectString"];

            _redis = RedisOperator.NewInstance(connStr);
            _workState = false;
        }


        public void Start()
        {
            _workState = true;
            Task.Factory.StartNew(ReceMessage);
        }

        private void ReceMessage()
        {
            while (_workState)
            {
                //var message = _redis.LPop<>(Keys.UserCookieKey);
                Thread.Sleep(500);
            }
        }

    }
}
