using BlaX.CryptoAutoTrading.Application.Abstractions.Repositories.BaseRepository;
using BlaX.CryptoAutoTrading.Domain.Core.Entities.Base;
using BlaX.CryptoAutoTrading.Domain.Core.Enums;
using BlaX.CryptoAutoTrading.Domain.Core.Extensions;
using BlaX.CryptoAutoTrading.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace BlaX.CryptoAutoTrading.Persistence.Data.Repositories.BaseRepository
{
    public class WriteRepository<T> : IWriteRepository<T> where T : EntityBase
    {
        protected readonly Context _context;

        public WriteRepository(Context context) => _context = context;

        public DbSet<T> Table => _context.Set<T>();

        public void Create(T entity, Guid createdBy)
        {
            entity.SetCreatedBy(createdBy);
            Table.Add(entity);
        }

        public async Task CreateAsync(T entity, Guid createdBy)
        {
            entity.SetCreatedBy(createdBy);
            await Table.AddAsync(entity);
        }

        public async Task CreateMany(List<T> entities, Guid createdBy)
        {
            entities.ForEach(e => e.SetCreatedBy(createdBy));
            await _context.AddRangeAsync(entities);
        }

        public void HardDelete(T entity) => Table.Remove(entity);

        public void HardDeleteMany(List<T> entity) => Table.RemoveRange(entity);

        public void SoftDelete(T entity, Guid? updatedBy)
        {
            entity.SetStatusType(StatusTypeEnum.Deleted);
            Update(entity, updatedBy);
        }

        public void SoftDeleteMany(List<T> entities, Guid updatedBy)
        {
            entities.ForEach(e => SoftDelete(e, updatedBy));

            Table.UpdateRange(entities);
        }

        public void Update(T entity, Guid? updatedBy)
        {
            entity.SetUpdatedAt(DateExtensions.GetDateNow());
            entity.SetUpdatedBy((Guid)updatedBy);

            Table.Update(entity);
        }

        public void UpdateMany(List<T> entities, Guid updatedBy)
        {
            entities.ForEach(e => Update(e, updatedBy));

            Table.UpdateRange(entities);
        }

        //public int SaveChanges() => _context.SaveChanges();

        //public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
