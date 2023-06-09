using BlaX.CryptoAutoTrading.API.Filters;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace BlaX.CryptoAutoTrading.API.Extensions
{
    public static class OpenApiAuthSchemeExtensions
    {
        public static IServiceCollection AddCustomizeSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo() { Title = Assembly.GetEntryAssembly().GetName().Name, Version = "v1" });

                c.OperationFilter<SwaggerJsonIgnoreOperationFilter>();
                c.OperationFilter<CamelCasePropertyNamesOperationFilter>();
                c.DocumentFilter<RemoveAuthorizedUserObjectFilter>();

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...')",
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            return services;
        }
    }
}
