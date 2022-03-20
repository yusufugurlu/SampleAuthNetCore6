using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Sample.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Business.Concrete
{
    public class RedisManager : IRedisService
    {
        private readonly IDistributedCache _redisDistributedCache;
        public RedisManager(IDistributedCache distributedCache)
        {
            _redisDistributedCache = distributedCache;
        }

        public void Clear(string key)
        {
           _redisDistributedCache.Remove(key);
        }


        public T Get<T>(string key) where T : class, new()
        {
            string cacheJsonItem;
            T obj = new T();
            var val = _redisDistributedCache.Get(key);
            if (val != null)
            {
                cacheJsonItem = Encoding.UTF8.GetString(val);
                obj = JsonConvert.DeserializeObject<T>(cacheJsonItem);
            }
            return obj;
        }

        public List<T> GetList<T>(string key) where T : class, new()
        {
            string cacheJsonItem;
            List<T> obj = null;
            var val = _redisDistributedCache.Get(key);
            if (val != null)
            {
                cacheJsonItem = Encoding.UTF8.GetString(val);
                obj = JsonConvert.DeserializeObject<List<T>>(cacheJsonItem);
            }
            return obj;
        }

        public bool IsSetKey(string key)
        {
            var val = _redisDistributedCache.Get(key);
            if (val != null)
            {
                return true;
            }
            return false;
        }

        public void Set<T>(string key, T value, int cacheTime = 15) where T : class, new()
        {
            string cacheJsonItem;
            cacheJsonItem = JsonConvert.SerializeObject(value);
            var stringToBytes = Encoding.UTF8.GetBytes(cacheJsonItem);
            var isHaveKey = _redisDistributedCache.Get(key);
            if (isHaveKey != null)
            {
                var options = new DistributedCacheEntryOptions()
                       .SetSlidingExpiration(TimeSpan.FromMinutes(cacheTime))
                       .SetAbsoluteExpiration(DateTime.Now.AddMinutes(cacheTime));
                _redisDistributedCache.Set(key, stringToBytes,options);
            }
            else
            {
                _redisDistributedCache.Set(key, stringToBytes);
            }
        }

        public void Set<T>(string key, List<T> values, int cacheTime = 15) where T : class, new()
        {
            string cacheJsonItem;
            cacheJsonItem = JsonConvert.SerializeObject(values);
            var stringToBytes = Encoding.UTF8.GetBytes(cacheJsonItem);
            var isHaveKey = _redisDistributedCache.Get(key);
            if (isHaveKey != null)
            {
                var options = new DistributedCacheEntryOptions()
                       .SetSlidingExpiration(TimeSpan.FromMinutes(cacheTime))
                       .SetAbsoluteExpiration(DateTime.Now.AddMinutes(cacheTime));
                _redisDistributedCache.Set(key, stringToBytes, options);
            }
            else
            {
                _redisDistributedCache.Set(key, stringToBytes);
            }
        }
    }
}
