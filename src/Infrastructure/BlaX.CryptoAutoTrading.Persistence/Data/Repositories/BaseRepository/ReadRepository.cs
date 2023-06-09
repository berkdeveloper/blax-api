using BlaX.CryptoAutoTrading.Application.Abstractions.Repositories.BaseRepository;
using BlaX.CryptoAutoTrading.Domain.Core.Entities.Base;
using BlaX.CryptoAutoTrading.Domain.Core.Enums;
using BlaX.CryptoAutoTrading.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BlaX.CryptoAutoTrading.Persistence.Data.Repositories.BaseRepository
{
    public class ReadRepository<T> : IReadRepository<T> where T : EntityBase
    {
        protected readonly Context _context;

        public ReadRepository(Context context) => _context = context;

        public DbSet<T> Table => _context.Set<T>();

        public Task<List<T>> FindAllByConditionAsync(Expression<Func<T, bool>> predicate) => Table.AsNoTracking().Where(x => x.StatusType != StatusTypeEnum.Deleted).Where(predicate).ToListAsync();

        public async Task<T> FindByIdAsync(Guid id) => await Table.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.StatusType != StatusTypeEnum.Deleted);

        public async Task<T> FindAsync(Expression<Func<T, bool>> predicate) => await Table.AsNoTracking().Where(x => x.StatusType != StatusTypeEnum.Deleted).FirstOrDefaultAsync(predicate);

        public IQueryable<T> FindAllByCondition(Expression<Func<T, bool>> predicate) => Table.AsNoTracking().Where(x => x.StatusType != StatusTypeEnum.Deleted).Where(predicate);

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate) => await Table.AsNoTracking().Where(x => x.StatusType != StatusTypeEnum.Deleted).FirstOrDefaultAsync(predicate) != null;

        public IQueryable<T> FindAllAsQueryable() => Table.AsNoTracking().Where(x => x.StatusType != StatusTypeEnum.Deleted);

        public int Count(Expression<Func<T, bool>> predicate) => Table.AsNoTracking().Where(x => x.StatusType != StatusTypeEnum.Deleted).Count(predicate);

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate) => await Table.AsNoTracking().Where(x => x.StatusType != StatusTypeEnum.Deleted).CountAsync(predicate);

        public List<T> FindByLimitSkip(int skipCount, int limit) => Table.AsNoTracking().Where(x => x.StatusType != StatusTypeEnum.Deleted).Skip(skipCount).Take(limit).ToList();

        public List<T> FindByLimit(int limit) => Table.AsNoTracking().Where(x => x.StatusType != StatusTypeEnum.Deleted).Take(limit).ToList();
        public async Task<List<T>> FindByLimitAsync(int limit) => limit != default ? await Table.AsNoTracking().Where(x => x.StatusType != StatusTypeEnum.Deleted).Take(limit).ToListAsync() : await Table.AsNoTracking().Where(x => x.StatusType != StatusTypeEnum.Deleted).ToListAsync();

        public List<T> FindByLimit(Expression<Func<T, bool>> predicate, int limit) => Table.AsNoTracking().Where(x => x.StatusType != StatusTypeEnum.Deleted).Where(predicate).Take(limit).ToList();
    }
}