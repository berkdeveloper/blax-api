using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace BlaX.CryptoAutoTrading.Application.Utilities.Extensions
{
    public static class JsonExtensions
    {
        public static string ToJson(this object data)
        {
            JsonSerializerSettings settings = new()
            {
                NullValueHandling = NullValueHandling.Include,
                TypeNameHandling = TypeNameHandling.None,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            return JsonConvert.SerializeObject(data, settings);
        }

        public static string ToJson(this object data, bool includeTypes = true)
        {
            JsonSerializerSettings settings = new()
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            if (includeTypes)
            {
                settings.TypeNameHandling = TypeNameHandling.All;
                settings.TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple;
            }
            return JsonConvert.SerializeObject(data, settings);
        }

        public static T ToObject<T>(this string jsonString)
        {
            try
            {
                JsonSerializerSettings settings = new()
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    TypeNameHandling = TypeNameHandling.All
                };
                return !string.IsNullOrEmpty(jsonString) ? JsonConvert.DeserializeObject<T>(jsonString, settings) : default(T);
            }
            catch
            {
                try
                {
                    JsonSerializerSettings settings = new()
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                    };
                    return !string.IsNullOrEmpty(jsonString) ? JsonConvert.DeserializeObject<T>(jsonString, settings) : default(T);
                }
                catch { return default; }
            }
        }

        public static object ToObject(this string jsonString, Type typeToDeserialize)
        {
            JsonSerializerSettings settings = new()
            {
                NullValueHandling = NullValueHandling.Ignore,
                TypeNameHandling = TypeNameHandling.All
            };
            if (string.IsNullOrEmpty(jsonString))
                return null;
            return JsonConvert.DeserializeObject(jsonString, typeToDeserialize, settings);
        }

        public static T DeepClone<T>(this T obj)
        {
            var serialized = JsonConvert.SerializeObject(obj, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return JsonConvert.DeserializeObject<T>(serialized);
        }
    }
}
