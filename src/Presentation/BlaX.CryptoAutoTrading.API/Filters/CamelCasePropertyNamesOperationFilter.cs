using BlaX.CryptoAutoTrading.Application.Utilities.Extensions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BlaX.CryptoAutoTrading.API.Filters
{
    public class CamelCasePropertyNamesOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            context.ApiDescription.ParameterDescriptions
                    .ToList()
                    .ForEach(param =>
                    {
                        var openApiParameter = operation.Parameters
                            .SingleOrDefault(p => p.Name == param.Name);

                        if (openApiParameter is not null)
                            openApiParameter.Name = openApiParameter.Name.ToCamelCase();
                    });
        }
    }
}
