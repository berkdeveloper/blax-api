using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BlaX.CryptoAutoTrading.API.Filters
{
    public sealed class RemoveAuthorizedUserObjectFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var requestModelSchemas = swaggerDoc.Components.Schemas
             .Where(s => s.Value.Properties.ContainsKey("authorizedUserObject"))
             .Select(s => s.Value).ToList();

            foreach (var schema in requestModelSchemas)
            {
                var propertiesToRemove = schema.Properties
                    .Where(p => p.Key == "authorizedUserObject")
                    .ToList();

                foreach (var propertyToRemove in propertiesToRemove)
                    schema.Properties.Remove(propertyToRemove.Key);
            }
        }
    }
}
