using BlaX.CryptoAutoTrading.Domain.Core.Entities.Base;

namespace BlaX.CryptoAutoTrading.Application.Abstractions.Repositories.BaseRepository
{
    public interface IWriteRepository<T> : IRepository<T> where T : EntityBase
    {
        void Create(T entity, Guid createdBy);
        Task CreateAsync(T entity, Guid createdBy);
        Task CreateMany(List<T> entities, Guid createdBy);
        void Update(T entity, Guid? updatedBy);
        void UpdateMany(List<T> entities, Guid updatedBy);
        void SoftDelete(T entity, Guid? updatedBy);
        void SoftDeleteMany(List<T> entities, Guid updatedBy);
        void HardDelete(T entity);
        void HardDeleteMany(List<T> entity);
        //int SaveChanges();
        //Task<int> SaveChangesAsync();
    }
}
