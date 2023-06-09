namespace BlaX.CryptoAutoTrading.Application.Abstractions.Repositories.BaseRepository
{
    using BlaX.CryptoAutoTrading.Domain.Core.Entities.Base;
    using Microsoft.EntityFrameworkCore;

    public interface IRepository<T> where T : EntityBase
    {
        DbSet<T> Table { get; }
    }
}
