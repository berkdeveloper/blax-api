using BlaX.CryptoAutoTrading.Application.Exceptions.BinanceExceptions;
using BlaX.CryptoAutoTrading.Application.Exceptions.Common;
using BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.Concrete;
using BlaX.CryptoAutoTrading.ExternalClients.Binance.Common;
using Newtonsoft.Json;
using System.Net;

namespace BlaX.CryptoAutoTrading.API.Middlewares
{
    public sealed class ExceptionHandlerMiddleware
    {
        readonly RequestDelegate _next;
        readonly ILogger<ExceptionHandlerMiddleware> _logger;
        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            { await _next(httpContext); }
            catch (Exception ex)
            {
                _logger.LogError(ex, message: ex.Message);

                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            int statusCode = exception switch
            {
                BinanceWalletSystemConnectionErrorException => GetHttpStatusCodeAsInt(HttpStatusCode.InternalServerError),
                BadRequestErrorException => GetHttpStatusCodeAsInt(HttpStatusCode.BadRequest),
                NotFoundErrorException => GetHttpStatusCodeAsInt(HttpStatusCode.NotFound),
                UnauthorizedAccessException => GetHttpStatusCodeAsInt(HttpStatusCode.Unauthorized),
                _ => context.Response.StatusCode
            };

            string message = exception.Message;

            try
            {
                if (string.IsNullOrEmpty(((BinanceClientException)exception)?.ErrorMessage) is false)
                    message = ((BinanceClientException)exception)?.ErrorMessage;
                else
                    message = exception.Message;
            }
            catch { }

            var errorResponse = new ErrorResponseBaseDto(statusCode: statusCode, message);
            var errorObjectJsonFormat = JsonConvert.SerializeObject(errorResponse);

            await context.Response.WriteAsync(errorObjectJsonFormat);
        }

        private static int GetHttpStatusCodeAsInt(HttpStatusCode statusCode) => (int)statusCode;
    }
}
