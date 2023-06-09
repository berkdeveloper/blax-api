using BlaX.CryptoAutoTrading.Application.Abstractions.Repositories.BaseRepository;
using BlaX.CryptoAutoTrading.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace BlaX.CryptoAutoTrading.Application.Abstractions
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
        void Commit();
        Task<IDbContextTransaction> BeginTransactionAsync();
        int SaveChanges();
        Task<int> SaveChangesAsync();

        #region Repositories
        // TradingLog
        IWriteRepository<TradingLog> TradingLogWriteRepository { get; }
        IReadRepository<TradingLog> TradingLogReadRepository { get; }

        // UserWallet
        IWriteRepository<UserWallet> UserWalletWriteRepository { get; }
        IReadRepository<UserWallet> UserWalletReadRepository { get; }
        #endregion

    }
}
