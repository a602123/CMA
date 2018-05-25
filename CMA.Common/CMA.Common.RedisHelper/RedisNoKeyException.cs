using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMA.Common.RedisHelper
{
    /// <summary>
    /// Redis 异常，返回异常的redisKey
    /// </summary>
    public class RedisNoKeyException : Exception
    {
        public RedisNoKeyException(string message, string key) : base(message)
        {
            _key = key;
        }

        private string _key;
        public string Key { get { return _key; } }
    }
}
