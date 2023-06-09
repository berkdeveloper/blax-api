using BlaX.CryptoAutoTrading.Application.Abstractions;
using BlaX.CryptoAutoTrading.Application.Abstractions.Services.CommonServices;
using BlaX.CryptoAutoTrading.Persistence.Data.DbContexts;
using BlaX.CryptoAutoTrading.Persistence.Data.Seed;
using BlaX.CryptoAutoTrading.Persistence.Data.UnitOfWork;
using BlaX.CryptoAutoTrading.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlaX.CryptoAutoTrading.Persistence
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Context>(options => options.UseNpgsql(configuration.GetConnectionString("PostgreSQL"))); // ConnectionStrings:PostgreSQL
            services.AddTransient<Seeder>();
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped<ITradingLogService, TradingLogService>();
            services.AddScoped<IUserWalletService, UserWalletService>();
            return services;
        }
    }
}
