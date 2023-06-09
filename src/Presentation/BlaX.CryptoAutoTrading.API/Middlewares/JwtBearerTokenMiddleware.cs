using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net;

namespace BlaX.CryptoAutoTrading.API.Middlewares
{
    public sealed class JwtBearerTokenMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtBearerTokenMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            var authHeader = context.Request.Headers[nameof(Authorization)].FirstOrDefault();

            if (string.IsNullOrEmpty(authHeader) is false && authHeader.StartsWith($"{JwtBearerDefaults.AuthenticationScheme} "))
                context.Request.Headers[JwtBearerDefaults.AuthenticationScheme] = $"{JwtBearerDefaults.AuthenticationScheme} {authHeader[$"{JwtBearerDefaults.AuthenticationScheme} ".Length..]}";

            await _next(context);
        }
    }
}
