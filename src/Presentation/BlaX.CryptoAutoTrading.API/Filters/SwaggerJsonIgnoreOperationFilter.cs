using BlaX.CryptoAutoTrading.Application.DTOs.CommonDTOs.BaseObjectDto;
using BlaX.CryptoAutoTrading.Application.Utilities.Extensions;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace BlaX.CryptoAutoTrading.API.Filters
{
    public class SwaggerJsonIgnoreOperationFilter : IOperationFilter
    {
        readonly List<string> _attributesToIgnoreList = GetClassList();

        /// <summary>
        /// Apply fonksiyonu, IOperationFilter interface içerisinden gelen bir signature'nin implementation'udur.
        /// OpenApi (Swagger) şeması için gerekli değişiklikleri ve düzenlemeleri yapmamıza olanak sağlayan bir fonksiyondur.
        /// </summary>
        /// <param name="operation">OpenApi (Swagger) şeması için gerekli konfigürasyonları yapmamıza olanak sağlayan nesnedir.</param>
        /// <param name="operation.Parameters">
        /// It takes parameters in OpenApi schema as a list.
        /// (OpenApi şemasındaki parametreleri liste halinde alır.)
        /// </param>
        /// <param name="context">OpenApi (Swagger) şeması içinde gerekli filtreleme işlemlerini yapabildiğimiz bir nesnedir.</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var ingoredPropertyList = GetListOfAttributesToIgnore(context);

            string mainObjectName = string.Empty;
            string propertyName = string.Empty;
            if (ingoredPropertyList.Count > 0)
                foreach (var property in ingoredPropertyList)
                {
                    propertyName = property.Name;
                    if (_attributesToIgnoreList != null && _attributesToIgnoreList.Any(p => p == ((TypeInfo)property.DeclaringType).Name))
                        mainObjectName = $"{((TypeInfo)property.DeclaringType).Name}.";
                    else
                    {
                        mainObjectName = string.Empty;
                        propertyName = propertyName.ToCamelCase();
                    }
                    operation.Parameters = operation.Parameters
                        .Where(p => p.Name.Equals($"{mainObjectName}{propertyName}") is false && p.In.Equals("Route") is false).ToList();
                }
        }

        /// <summary>
        /// GetListOfAttributesToIgnore fonksiyonu, Ignore Attribute'na sahip tüm property'leri bir listeye toplar.
        /// </summary>
        /// <param name="context">Apply fonksiyonunda bulunan context işaretçi parametresini, argüman olarak alır.</param>
        /// <returns>Ignore Attribute'na sahip tüm property listesini döndürür.</returns>
        private List<PropertyInfo> GetListOfAttributesToIgnore(OperationFilterContext context)
        {
            try
            {
                var ingoredPropertyList = GetAttributesToIgnoreInsideObject(context);

                if (ingoredPropertyList.Count > 0)
                    ingoredPropertyList = ingoredPropertyList?.Where(prop => prop?.GetCustomAttribute<JsonIgnoreAttribute>() != null)?.ToList();

                var ignoredProperties = context.MethodInfo.GetParameters()
                    ?.SelectMany(p => p.ParameterType.GetProperties()
                                     ?.Where(prop => prop?.GetCustomAttribute<JsonIgnoreAttribute>() != null))?.ToList();

                if (ignoredProperties != null && ignoredProperties.Count > 0)
                    ingoredPropertyList.AddRange(ignoredProperties);

                return ingoredPropertyList;
            }
            catch { return new List<PropertyInfo>(); }
        }

        /// <summary>
        /// GetAttributesToIgnoreInsideObject fonksiyonu, inject edilmiş immutable Generic List (_attributesToIgnoreList) içerisinde bulunan, ignore edilecek object property isimlerini kullanarak, Apply fonksiyonundan parametre ile gelen context işaretçisi üzerinden, ignore attribute'na sahip property'leri karşılaştırır ve generic listeye toplar.
        /// </summary>
        /// <param name="context">Apply fonksiyonunda bulunan context işaretçi parametresini, argüman olarak alır.</param>
        /// <returns></returns>
        private List<PropertyInfo> GetAttributesToIgnoreInsideObject(OperationFilterContext context)
        {
            try
            {
                List<PropertyInfo> result = new List<PropertyInfo>();

                var attributesToIgnore = context.MethodInfo.GetParameters().SelectMany(p => p.ParameterType.GetProperties()
                        .Where(prop => _attributesToIgnoreList.Any(p => p == prop.Name))).FirstOrDefault();

                if (attributesToIgnore != null && attributesToIgnore.PropertyType.IsClass)
                    result.AddRange(GetProperty(attributesToIgnore));

                result.Add(attributesToIgnore);

                return result;
            }
            catch { return new List<PropertyInfo>(); }
        }

        /// <summary>
        /// GetProperty fonksiyonu, bir Class'ın içerisinde bulunan property'leri alır ve döndürür.
        /// </summary>
        /// <param name="propertyInfo">Class olan bir property'i argüman olarak alır.</param>
        /// <returns>Bir Class içerisindeki, Ignore Attribute'na sahip property'lerin listesini döndürür.</returns>
        private List<PropertyInfo> GetProperty(PropertyInfo propertyInfo) =>
            ((TypeInfo)propertyInfo.PropertyType).DeclaredProperties?.Where(prop =>
                prop?.GetCustomAttribute<JsonIgnoreAttribute>() != null)?.ToList();

        private static List<string> GetClassList() => new() { nameof(AuthorizedUserObject) };
    }
}
