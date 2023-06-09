using BlaX.CryptoAutoTrading.Application.Exceptions.Common;
using BlaX.CryptoAutoTrading.Domain.Core.Constants;
using Newtonsoft.Json;

namespace BlaX.CryptoAutoTrading.Application.Utilities.Extensions
{
    public static class JsonSerializeExtensions
    {
        public static async Task<string> CallAsynchronousFunction<T>(Task<T> asyncValue)
        {
            await Task.Delay(TimeSpan.FromSeconds(5));
            var jsonObject = await asyncValue;
            var result = jsonObject as string;
            return result;
        }

        public static string GetResultFromAsyncFunction<T>(this Task<T> asyncValue) => CallAsynchronousFunction(asyncValue).GetAwaiter().GetResult();

        public static TResult DeserializeObjectSingle<TResult>(this string jsonObject) where TResult : class, new()
        {
            if (string.IsNullOrEmpty(jsonObject) || jsonObject == "{}" || jsonObject == "[]") throw new BadRequestErrorException(ExceptionTypes.BadRequest);

            if (jsonObject[..1] is string jsonStringList && jsonStringList.Equals("["))
                return JsonConvert.DeserializeObject<List<TResult>>(jsonObject).FirstOrDefault();
            else
                return JsonConvert.DeserializeObject<TResult>(jsonObject);
        }

        public static List<TResult> DeserializeObjectList<TResult>(this string jsonObject) => JsonConvert.DeserializeObject<List<TResult>>(jsonObject);
    }
}