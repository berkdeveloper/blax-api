using BlaX.CryptoAutoTrading.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlaX.CryptoAutoTrading.Persistence.Data.Seed
{
    public class DatabaseInitializer
    {
        public static class AutoMigration
        {
            public static void Initialize(IServiceProvider serviceProvider)
            {
                using (var serviceScope = serviceProvider.CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetService<Context>();

                    context?.Database.Migrate();
                }
            }
        }
    }
}
