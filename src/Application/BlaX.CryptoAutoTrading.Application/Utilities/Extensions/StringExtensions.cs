using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.ComplexTypes;

namespace BlaX.CryptoAutoTrading.Application.Utilities.Extensions
{
    public static class StringExtensions
    {
        public static string ToCamelCase(this string str) => string.IsNullOrEmpty(str) is false && str is { Length: > 1 } ? char.ToLowerInvariant(str[0]) + str[1..] : str;

        public static string ToJsonCamelCase(this object data)
        {
            JsonSerializerSettings settings = new()
            {
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy { ProcessDictionaryKeys = true }
                },
                Formatting = Formatting.Indented
            };
            return JsonConvert.SerializeObject(data, settings);
        }

        public static BaseRequest ToAuthorizedBase(this object requestModel) => (BaseRequest)requestModel;
    }
}
