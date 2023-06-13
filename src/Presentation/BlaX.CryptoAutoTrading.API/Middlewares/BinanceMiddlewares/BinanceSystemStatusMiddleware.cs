using Binance.Net.Clients;
using Binance.Net.Objects;
using Binance.Net.Objects.Models.Spot;
using BlaX.CryptoAutoTrading.Application.Exceptions.BinanceExceptions;
using BlaX.CryptoAutoTrading.Domain.AppSettings.BinanceAppSetting;
using BlaX.CryptoAutoTrading.Domain.Core.Constants;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Options;

namespace BlaX.CryptoAutoTrading.API.Middlewares.BinanceMiddlewares
{
    public sealed class BinanceSystemStatusMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly BinanceClient _binanceClient;
        private readonly ILogger<BinanceSystemStatusMiddleware> _logger;
        private readonly BinanceAuthorization _binanceAuthorization;

        public BinanceSystemStatusMiddleware(RequestDelegate next, IOptions<BinanceAuthorization> binanceAuthorizationOptions, ILogger<BinanceSystemStatusMiddleware> logger)
        {
            _next = next;

            _binanceAuthorization = binanceAuthorizationOptions.Value;
            _logger = logger;

            _binanceClient = new BinanceClient(new BinanceClientOptions
            {
                ApiCredentials = new BinanceApiCredentials(_binanceAuthorization.ApiKey, _binanceAuthorization.ApiSecretKey),
                LogLevel = LogLevel.Information,
            });
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var endpoint = context?.Request?.Path.Value;

            if (endpoint.Contains(EndpointNameConst.BinanceController))
            {
                await Task.Delay(TimeSpan.FromSeconds(1.5));
                if (await _binanceClient.SpotApi.ExchangeData.GetSystemStatusAsync() is WebCallResult<BinanceSystemStatus> response && response.Data.Status == Binance.Net.Enums.SystemStatus.Maintenance) throw new BinanceSystemInternalServerErrorException();
            }

            // Middleware zincirinin sonraki adımına geçiş yapılır
            await _next(context);
        }
    }
}
