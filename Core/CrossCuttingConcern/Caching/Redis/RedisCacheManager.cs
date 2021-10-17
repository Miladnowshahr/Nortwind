using Core.IOC;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcern.Caching.Redis
{
    public class RedisCacheManager:ICacheManager
    {
        private IDistributedCache _cache;

        public RedisCacheManager()
        {
            _cache = ServiceTool.Resolve<IDistributedCache>();
        }

        public void Add(string key, object data, int duration)
        {
            _cache.Set(key, ToByeObject(data), new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow= TimeSpan.FromMinutes(duration) });
        }

        public T Get<T>(string key)
        {
            var getData= _cache.Get(key);
             return ConvertToObject<T>(ToStringObject(getData));
        }

        public object Get(string key)
        {
            return ConvertToObject(ToStringObject(_cache.Get(key)));
        }

        public bool IsAdd(string key)
        {
            return TryGetValue(key, out _);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
        //    var cacheEntriesCollectionDefinition = typeof(MemoryDistributedCache)
        //                .GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic);

        //    var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_cache) as dynamic;

        //    List<IDistrobutedCacheEntry> cacheCollectValues = new List<ICacheEntry>();

        //    foreach (var item in cacheEntriesCollection)
        //    {
        //        ICacheEntry cacheItemValue =
        //            item.GetType().GetProperty("Value").GetValue(item, null);
        //        cacheCollectValues.Add(cacheItemValue);
        //    }

        //    var regext = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
        //    var keysToRemove = cacheCollectValues.Where(d => regext.IsMatch(d.Key.ToString())).Select(d => d.Key);

        //    foreach (var key in keysToRemove)
        //    {
        //        _cache.Remove(key);
        //    }
        }

        private byte[] ToByeObject(object data)
        {
            var toJson = ConvertToJson(data);
            return Encoding.UTF8.GetBytes(toJson);
        }
        private string ToStringObject(byte[] data)
        {
            return Encoding.UTF8.GetString(data);
            
        }
        private string ConvertToJson(object data)
        {
            return JsonConvert.SerializeObject(data);
        }
        private object ConvertToObject(string data)
        {
            return JsonConvert.DeserializeObject(data);
        }
        private T ConvertToObject<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }
        private bool TryGetValue(string key,out object x)
        {
            if (Get(key) == null)
            {
                x = Get(key);
                return true;
            }
            x = null;
            return false;
        }
    }
}
