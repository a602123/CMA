using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using static Newtonsoft.Json.JsonConvert;

namespace CMA.Common.RedisHelper
{
    public class RedisOperator
    {
        private IDatabase _db;

        private static RedisOperator _operatorInstance;

        public static RedisOperator GetInstance()
        {
            if (_operatorInstance == null)
            {
                string connStr = System.Configuration.ConfigurationManager.AppSettings["RedisConnectString"];
                _operatorInstance = new RedisOperator(connStr);
            }
            return _operatorInstance;
        }

        private RedisOperator(string connStr)
        {
            try
            {
                ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(connStr);
                _db = redis.GetDatabase();
            }
            catch (Exception ex)
            {
                throw new Exception("初始化RedisDB失败：" + ex.Message);
            }
        }

        public static RedisOperator NewInstance(string connStr)
        {
            return new RedisOperator(connStr);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="expireTime">续约时间（为空不续约）</param>
        /// <returns></returns>
        public T GetAndExpire<T>(string key, TimeSpan? expireTime = null)
        {
            if (_db.KeyExists(key))
            {
                if (expireTime != null)
                {
                    _db.KeyExpire(key, expireTime, flags: CommandFlags.FireAndForget);
                }
                var dataStr = _db.StringGet(key);
                if (dataStr.IsNull)
                {
                    return default(T);
                }
                return DeserializeObject<T>(dataStr);
            }
            else
            {
                throw new RedisNoKeyException("Key不存在或已过期", key);
            }
        }

        /// <summary>
        /// 设置
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="data">值</param>
        /// <param name="expireTime">有效期（空为一直有效）</param>
        /// <returns></returns>
        public bool Set<T>(string key, T data, TimeSpan? expireTime = null)
        {
            if (expireTime == null)
            {
                return _db.StringSet(key,SerializeObject(data));
            }
            else
            {
                return _db.StringSet(key, SerializeObject(data), expireTime, flags: CommandFlags.FireAndForget);
            }
        }

        /// <summary>
        /// 向队列尾部插入值
        /// </summary>
        /// <typeparam name="T">值类型（必须为Serializable）</typeparam>
        /// <param name="listId">队列Id</param>
        /// <param name="data">值</param>
        /// <returns></returns>
        public bool RPush<T>(string listId, IEnumerable<T> data)
        {
            RedisValue[] dataStr = data.Select(n =>(RedisValue)SerializeObject(n)).ToArray();
            return _db.ListRightPush(listId, dataStr, flags: CommandFlags.FireAndForget) == data.Count();
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listId"></param>
        /// <returns></returns>
        public T LPop<T>(string listId)
        {
            var dataStr = _db.ListLeftPop(listId);
            if (dataStr.IsNull)
            {
                return default(T);
            }
            return DeserializeObject<T>(dataStr);
        }


        /// <summary>
        /// 设置哈希表的一个字段的值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">哈希表的Key</param>
        /// <param name="field">哈希表字段的Id</param>
        /// <param name="data">值</param>
        /// <returns></returns>
        public bool HashSet<T>(string key, string field, T data)
        {
            return _db.HashSet(key, field, SerializeObject(data),flags: CommandFlags.FireAndForget);
        }

        /// <summary>
        /// 获取哈希表中的一个值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">哈希表的Key</param>
        /// <param name="field">哈希表的字段Id</param>
        /// <returns></returns>
        public T HashGet<T>(string key, string field)
        {
            var dataStr = _db.HashGet(key, field);
            if (dataStr.IsNull)
            {
                return default(T);
            }
            return DeserializeObject<T>(dataStr);
        }

        /// <summary>
        /// 获取哈希表里所有的值
        /// </summary>
        /// <param name="key">哈希表的Key</param>
        /// <returns></returns>
        public Dictionary<string, T> HashGetAll<T>(string key)
        {
            var result = _db.HashGetAll(key);
            Dictionary<string, T> dic = new Dictionary<string, T>();
            for (int i = 0; i < result.Length; i++)
            {
                dic.Add(result[i].Name, DeserializeObject<T>(result[i].Value));
            }
            return dic;
        }

        /// <summary>
        /// 添加一批数据进入哈希表
        /// </summary>
        /// <typeparam name="T">数据的类型</typeparam>
        /// <param name="key">哈希表的Key</param>
        /// <param name="fileds">数据（Key：哈希表的字段Id，Value：哈希表的值）</param>
        public void HashSet<T>(string key, Dictionary<string, T> fileds)
        {
            HashEntry[] entrys = fileds.Select(n => new HashEntry(n.Key,SerializeObject(n.Value))).ToArray();

            _db.HashSet(key, entrys, flags: CommandFlags.FireAndForget);
        }

        /// <summary>
        /// 添加一批数据进入哈希表
        /// </summary>
        /// <param name="key">哈希表的Key</param>
        /// <param name="entrys">数据</param>
        public void HashSet<T>(string key, HashEntry[] entrys)
        {
            _db.HashSet(key, entrys, flags: CommandFlags.FireAndForget);
        }
    }
}