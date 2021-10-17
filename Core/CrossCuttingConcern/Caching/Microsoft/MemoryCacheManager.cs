using Core.IOC;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcern.Caching.Microsoft
{
    public class MemoryCacheManager : ICacheManager
    {
        private IMemoryCache _cache;
        public MemoryCacheManager()
        {
            _cache = ServiceTool.Resolve<IMemoryCache>();
        }
        public void Add(string key, object data, int duration)
        {
            _cache.Set(key, data, TimeSpan.FromMinutes(duration));
        }

        public T Get<T>(string key)
        {
            return _cache.Get<T>(key);
        }

        public object Get(string key)
        {
            return _cache.Get(key);
        }

        public bool IsAdd(string key)
        {
            return _cache.TryGetValue(key, out _);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            var cacheEntriesCollectionDefinition = typeof(MemoryCache)
                        .GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic);

            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_cache) as dynamic;

            List<ICacheEntry> cacheCollectValues = new List<ICacheEntry>();

            foreach (var item in cacheEntriesCollection)
            {
                ICacheEntry cacheItemValue =
                    item.GetType().GetProperty("Value").GetValue(item, null);
                cacheCollectValues.Add(cacheItemValue);
            }

            var regext = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = cacheCollectValues.Where(d => regext.IsMatch(d.Key.ToString())).Select(d => d.Key);

            foreach (var key in keysToRemove)
            {
                _cache.Remove(key);
            }
        }
    }
}
