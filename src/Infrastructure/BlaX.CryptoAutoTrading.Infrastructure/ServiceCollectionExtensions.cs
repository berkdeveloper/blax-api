using BlaX.CryptoAutoTrading.Application.Abstractions.Services.BinanceServices;
using BlaX.CryptoAutoTrading.Infrastructure.Services.BinanceServices;
using Microsoft.Extensions.DependencyInjection;

namespace BlaX.CryptoAutoTrading.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services)
        {
            services.AddScoped<IBinanceMarketService, BinanceMarketService>();
            services.AddScoped<IBinanceWalletService, BinanceWalletService>();
            services.AddScoped<IBinanceAccountTradeService, BinanceAccountTradeService>();
            services.AddScoped<IBinanceAccountTradeHandler, BinanceAccountTradeService>();
            return services;
        }
    }
}
