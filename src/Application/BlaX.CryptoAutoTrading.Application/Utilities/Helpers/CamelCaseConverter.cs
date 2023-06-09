using BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.ComplexTypes;
using BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.Concrete;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BlaX.CryptoAutoTrading.Application.Utilities.Helpers
{
    public class CamelCaseConverter<T> : JsonConverter<T> where T : ObjectResponseBase<BaseRequestDto>, new()
    {
        public override T ReadJson(JsonReader reader, Type objectType, T existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject jsonObject = JObject.Load(reader);
            T target = existingValue ?? Activator.CreateInstance<T>();

            foreach (var property in target.GetType().GetProperties())
            {
                var camelCasePropertyName = char.ToLowerInvariant(property.Name.FirstOrDefault()) + property.Name[1..];
                var value = jsonObject.GetValue(camelCasePropertyName, StringComparison.OrdinalIgnoreCase);
                if (value is not null) property.SetValue(target, value.ToObject(property.PropertyType, serializer));
            }

            return target;
        }

        public override void WriteJson(JsonWriter writer, T value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
