using BlaX.CryptoAutoTrading.Domain.Core.Entities.Base;
using BlaX.CryptoAutoTrading.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlaX.CryptoAutoTrading.Persistence.Data.DbContexts
{
    public class Context : DbContext
    {
        public DbSet<TradingLog> TradingLog { get; set; }
        public DbSet<UserWallet> UserWallet { get; set; }

        public Context(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            //SentimentModelBuilder.Builder(modelBuilder);

            //modelBuilder.UseSnakeCaseNames();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseLazyLoadingProxies();

        public void DetachAll() => ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetEntityState();

            return await base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            SetEntityState();

            return base.SaveChanges();
        }

        private void SetEntityState()
        {
            var entities = ChangeTracker
                .Entries<EntityBase>();

            foreach (var entity in entities)
            {
                switch (entity.State)
                {
                    case EntityState.Deleted:
                        break;
                    case EntityState.Modified:
                        entity.Entity.SetUpdatedAt(DateTime.UtcNow);
                        break;
                    case EntityState.Added:
                        break;
                    default:
                        break;
                }
            }
        }
    }
}