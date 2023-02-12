using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

// Attribution: Murach MVC ASP.net book, page 331

namespace CBApp.Models
{
    public static class SessionExtensions
    {   
        // Converts an object of type 'T' to a string using Newtonsoft.Json library so it can be stored in session as string
        public static void SetObject<T>(this ISession session,
            string key, T value) where T : class
        {   
            // Sets the object as a string in session under a specified key (input arg)
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        // Converts session string back to an instance of the 'T' object
        public static T GetObject<T> (this ISession session, string key)
        {   
            // Gets, and then converts session string back into object of type 'T' and returns the object
            var valueJson = session.GetString(key);
            if (string.IsNullOrEmpty(valueJson)) // No such session string under the inputted key!
            {
                return default(T)!;
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(valueJson)!;
            }
        }
    }
}
