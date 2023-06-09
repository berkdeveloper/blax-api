using BlaX.CryptoAutoTrading.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlaX.CryptoAutoTrading.Persistence.Data.Seed
{
    public class Seeder
    {
        public async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<Context>();

                #region Add TradingLog

                if (db.TradingLog.Any() is false)
                {
                    db.TradingLog.Add(new Domain.Entities.TradingLog()
                    {
                        PurchasePrice = default,
                        PurchaseDate = DateTime.UtcNow,
                        SalePrice = default,
                        SaleDate = DateTime.UtcNow,
                        ProfitRate = default,
                    });

                    await db.SaveChangesAsync();
                }

                #endregion

                await db.Database.MigrateAsync();

            }
        }
    }
}
