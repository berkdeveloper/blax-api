using System.Reflection;

namespace BlaX.CryptoAutoTrading.Application.Utilities.Helpers
{
    public static class TypeConversion
    {
        public static TResult Conversion<T, TResult>(T model) where TResult : class, new()
        {
            TResult result = new();
            var properties = typeof(T).GetProperties().ToList();
            foreach (var p in properties)
            {
                PropertyInfo? property = typeof(TResult)?.GetProperty(p.Name);
                if (property is not null && property.CanWrite is true)
                    property.SetValue(result, p.GetValue(model));
            }
            return result;
        }

        public static List<TResult> ConversionList<T, TResult>(List<T> models) where TResult : class, new()
        {
            List<TResult> resultList = new(models.Count);
            List<T> modelsList = models;
            foreach (var model in modelsList)
            {
                TResult result = Conversion<T, TResult>(model);
                resultList.Add(result);
            }
            return resultList;
        }
    }
}
