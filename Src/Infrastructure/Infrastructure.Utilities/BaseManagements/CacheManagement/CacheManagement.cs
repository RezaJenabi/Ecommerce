using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Utilities.BaseManagements.CacheManagement
{
    public interface ICacheManagement
    {
        T GetValue<T>(object key, T defaultValue = default);
        void SetValue<T>(object key, T value, int timeoutInSeconds);
        void SetSlidingValue<T>(object key, T value, int timeoutInSeconds);
        void Remove(object key);
        void Remove(List<object> keys);
        void ClearTableCache(string tableName);
        void ClearTableCache(List<string> tableName);
        void ClearAllCaches();

    }
    public class CacheManagement : ICacheManagement
    {
        private readonly IMemoryCache _memoryCache;
        public CacheManagement(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public T GetValue<T>(object key, T defaultValue = default)
            => _memoryCache.TryGetValue<T>(key, out var cacheValue) ? cacheValue : defaultValue;

        public void SetValue<T>(object key, T value, int timeoutInSeconds) =>
            _memoryCache.Set<T>(key, value, new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(timeoutInSeconds)));

        public void SetSlidingValue<T>(object key, T value, int timeoutInSeconds) =>
            _memoryCache.Set<T>(key, value, new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(timeoutInSeconds)));

        public void Remove(object key) => _memoryCache.Remove(key);

        public void Remove(List<object> keys) => keys.ForEach(Remove);

        public void ClearTableCache(string tableName)
        {
            var field = typeof(MemoryCache).GetProperty("EntriesCollection", BindingFlags.NonPublic | BindingFlags.Instance);
            if (field != null && field.GetValue(_memoryCache) is ICollection collection)
                foreach (var item in collection)
                {
                    var methodInfo = item.GetType().GetProperty("Key");
                    var key = methodInfo.GetValue(item).ToString();
                    if (key.StartsWith(tableName + "_"))
                        _memoryCache.Remove(key);
                }
        }

        public void ClearTableCache(List<string> tableNames) => tableNames.ForEach(ClearTableCache);

        public void ClearAllCaches()
        {
            var field = typeof(MemoryCache).GetProperty("EntriesCollection", BindingFlags.NonPublic | BindingFlags.Instance);
            if (field != null && field.GetValue(_memoryCache) is ICollection collection)
                foreach (var item in collection)
                {
                    var methodInfo = item.GetType().GetProperty("Key");
                    var key = methodInfo.GetValue(item).ToString();
                    _memoryCache.Remove(key);
                }
        }
    }
}
