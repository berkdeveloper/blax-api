using BlaX.CryptoAutoTrading.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlaX.CryptoAutoTrading.Persistence.Data.Builders.Binance
{
    class TradingLogModelBuilder
    {
        internal static void Builder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TradingLog>(entity =>
            {
                //entity.ToTable(nameof(TradingLog));

                //entity.HasKey(e => e.Id);

                //entity.Property(e => e.CreatedAt)
                ////.HasColumnName("CREATED_AT")
                //.HasColumnType("TIMESTAMP(6)");

                //entity.Property(e => e.UpdatedAt)
                //.HasColumnType("TIMESTAMP(6)");

                //entity.Property(e => e.CreatedBy)
                //.HasMaxLength(30);

                //entity.Ignore(x => x.UpdatedUserName);
            });
        }
    }
}
