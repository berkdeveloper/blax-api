using BlaX.CryptoAutoTrading.API.Middlewares;

namespace BlaX.CryptoAutoTrading.API.Extensions
{
    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app) => app.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}
