using Infrastructure.Utilities.Extensions;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Utilities.BaseManagements.SessionManagement
{
    public interface ISessionManagement
    {
        string GetSessionId();
        int GetInt(string key, int defaultValue = default);
        string GetString(string key, string defaultValue = default);
        T Get<T>(string key, T defaultValue = default);
        void SetInt(string key, int value);
        void SetString(string key, string value);
        void Set<T>(string key, T value);
        void Remove(string key);
    }
    public class SessionManagement : ISessionManagement
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ISession _session;
        public SessionManagement(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            _session = contextAccessor.HttpContext.Session;
        }
        public string GetSessionId()
        {
            return _contextAccessor?.HttpContext?.Session?.Id ?? string.Empty;
        }
        public int GetInt(string key, int defaultValue = default)
        {
            var cacheValue = _session.GetInt32(key);
            if (cacheValue != null)
                return (int)cacheValue;
            return defaultValue;
        }
        public string GetString(string key, string defaultValue = default)
        {
            var cacheValue = _session.GetString(key);
            return !string.IsNullOrEmpty(cacheValue) ? cacheValue : defaultValue;
        }
        public T Get<T>(string key, T defaultValue = default)
        {
            var cacheValue = _session.GetString(key);
            return !string.IsNullOrEmpty(cacheValue) ? cacheValue.DeserializeObject<T>() : defaultValue;
        }
        public void SetInt(string key, int value) => _session.SetInt32(key, value);

        public void SetString(string key, string value) => _session.SetString(key, value);

        public void Set<T>(string key, T value) => _session.SetString(key, value.SerializeObject());

        public void Remove(string key) => _session.Remove(key);
    }
}
