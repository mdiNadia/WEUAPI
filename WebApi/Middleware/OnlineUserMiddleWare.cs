using Microsoft.Extensions.Caching.Memory;

namespace WebApi.Middleware
{
    public class OnlineUserMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly IMemoryCache _memoryCache;

        public OnlineUserMiddleWare(RequestDelegate next, IMemoryCache memoryCache)
        {
            _next = next;
            _memoryCache = memoryCache;
        }

        public async Task Invoke(HttpContext context)
        {

            if (!_memoryCache.TryGetValue("OnlineUsers", out Dictionary<string, DateTime> onlineUsers))
            {
                onlineUsers = new Dictionary<string, DateTime>();
                _memoryCache.Set("OnlineUsers", onlineUsers, new MemoryCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10) });
            }
            if (context.User.Identity.IsAuthenticated)
            {
                var name = context.User.Identity.Name;
                if (name != null)
                {

                    if (onlineUsers.ContainsKey(name))
                        onlineUsers[name] = DateTime.Now;
                    else
                        onlineUsers.Add(name, DateTime.Now);
                }

            }
            await _next(context);
        }

    }
}
