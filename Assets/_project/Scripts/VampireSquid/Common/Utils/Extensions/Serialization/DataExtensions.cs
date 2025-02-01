using System;
using Newtonsoft.Json;

namespace VampireSquid.Common.Utils.Extensions.Serialization
{
    public static class DataExtensions
    {
        public static T ToDeserialized<T>(this string json)
            => JsonConvert.DeserializeObject<T>(json);

        public static object ToDeserialized(this string json, string type)
            => JsonConvert.DeserializeObject(json, Type.GetType(type));

        public static string ToJson(this object obj)
            => JsonConvert.SerializeObject(obj, Formatting.Indented);
    }
}
