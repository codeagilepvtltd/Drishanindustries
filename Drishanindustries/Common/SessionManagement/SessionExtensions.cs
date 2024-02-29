﻿using Newtonsoft.Json;

namespace Drishanindustries.Common
{
    public static class SessionExtensions
    {
        public static void SetData<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        public static T GetData<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) :
                                  JsonConvert.DeserializeObject<T>(value);
        }
    }
}