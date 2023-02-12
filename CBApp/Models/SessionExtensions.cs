using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CBApp.Models
{
    public static class SessionExtensions
    {
        public static void SetObject<T>(this ISession session,
            string key, T value) where T : class
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObject<T> (this ISession session, string key)
        {
            var valueJson = session.GetString(key);
            if (string.IsNullOrEmpty(valueJson))
            {
                return default(T);
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(valueJson);
            }
        }
    }
}
