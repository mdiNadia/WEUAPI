
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Application.Helpers
{


    public static class Cookie
    {
        public static void SetCookie<T>(this HttpResponse response, T value, string key, int? expireDate)
        {
            CookieOptions options = new CookieOptions();
            string valueStr = value == null ? null : JsonSerializer.Serialize<T>(value);
            if (expireDate.HasValue)
                options.Expires = DateTime.Now.AddSeconds(expireDate.Value);
            else
                options.Expires = DateTime.Now.AddYears(1);
            options.Path = "/";
            response.Cookies.Append(key, valueStr, options);
        }
        public static T GetCookie<T>(this HttpRequest request, string key)
        {
            var cookieVal = request.Cookies[key];

            if (typeof(T) == typeof(string))
                return (T)(object)cookieVal;
            return cookieVal == null ? default(T) : JsonSerializer.Deserialize<T>(cookieVal);
        }
        public static void Delete(this HttpResponse response, string key)
        {
            response.Cookies.Delete(key);
        }
    }
}