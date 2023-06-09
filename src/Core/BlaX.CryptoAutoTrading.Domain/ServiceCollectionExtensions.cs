using BlaX.CryptoAutoTrading.Domain.AppSettings.BinanceAppSetting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlaX.CryptoAutoTrading.Domain
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAppSettingsServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<BinanceAuthorization>(configuration.GetSection(nameof(BinanceAuthorization)));
            return services;
        }
    }
}
