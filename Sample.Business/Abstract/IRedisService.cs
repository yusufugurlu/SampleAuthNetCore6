using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Business.Abstract
{
    public interface IRedisService
    {
        List<T> GetList<T>(string key) where T : class, new();
        T Get<T>(string key) where T : class, new();
        void Set<T>(string key,T value,int cacheTime=15) where T : class, new();
        void Set<T>(string key, List<T> values, int cacheTime = 15) where T : class, new();
        bool IsSetKey(string key);
        void Clear(string key);
    }
}
