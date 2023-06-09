using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BlaX.CryptoAutoTrading.API.Extensions
{
    public static class AuthenticationExtension
    {
        public static IServiceCollection AddAuthenticationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true, // Oluşturulacak token değerini kimlerin hangi originlerin/domainlerin kullanacağını belirlediğimiz değerdir.
                    ValidateIssuer = true, // oluşturulan token değerini kimin oluşturduğunu yani kimin dağıttığını ifade edeceğimiz alandır.
                    ValidateLifetime = true, // oluşturulan token değerinin süresini kontrol edecek olan doğrulamadır.
                    ValidateIssuerSigningKey = true, // Üretilecek token değerinin uygulamamıza ait bir değer olduğunu ifade eden security key
                    ValidAudience = configuration["Token:Audience"],
                    ValidIssuer = configuration["Token:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"])),
                    LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires is not null && expires > DateTime.UtcNow,
                };
            });

            return services;
        }
    }
}
