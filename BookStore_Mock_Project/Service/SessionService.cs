using Newtonsoft.Json;

namespace BookStore_Mock_Project.Service
{
    public class SessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public T Get<T>(string key)
        {
            var session = _httpContextAccessor.HttpContext.Session.GetString(key);
            return session == null ? default(T) : JsonConvert.DeserializeObject<T>(session);
        }

        public void Set<T>(string key, T value)
        {
            _httpContextAccessor.HttpContext.Session.SetString(key, JsonConvert.SerializeObject(value));
        }
        public void RemoveSession(string key)
        {
            _httpContextAccessor.HttpContext.Session.Remove(key);
        }
    }
}
