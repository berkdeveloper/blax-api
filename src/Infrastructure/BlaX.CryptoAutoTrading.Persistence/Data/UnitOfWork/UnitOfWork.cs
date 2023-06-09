using BlaX.CryptoAutoTrading.Application.Abstractions;
using BlaX.CryptoAutoTrading.Application.Abstractions.Repositories.BaseRepository;
using BlaX.CryptoAutoTrading.Domain.Core.Entities.Base;
using BlaX.CryptoAutoTrading.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using BlaX.CryptoAutoTrading.Persistence.Data.DbContexts;
using BlaX.CryptoAutoTrading.Persistence.Data.Repositories.BaseRepository;

namespace BlaX.CryptoAutoTrading.Persistence.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly Context _context;
        readonly ILogger _logger;
        bool _disposed;

        #region Dependency Injection
        // TradingLog
        readonly (Lazy<IReadRepository<TradingLog>> tradingLogReadRepository, Lazy<IWriteRepository<TradingLog>> tradingLogWriteRepository)
        _tradingLogRepository;


        // UserWallet
        readonly (Lazy<IReadRepository<UserWallet>> userWalletReadRepository, Lazy<IWriteRepository<UserWallet>> userWalletWriteRepository)
        _userWalletRepository;
        #endregion


        private static Lazy<IWriteRepository<TEntity>> CreateWriteRepository<TEntity>(Context context) where TEntity : EntityBase =>
            new Lazy<IWriteRepository<TEntity>>(() => new WriteRepository<TEntity>(context));

        private static Lazy<IReadRepository<TEntity>> CreateReadRepository<TEntity>(Context context) where TEntity : EntityBase =>
            new Lazy<IReadRepository<TEntity>>(() => new ReadRepository<TEntity>(context));


        public UnitOfWork(Context context, ILogger<IUnitOfWork> logger)
        {
            _context = context;
            _logger = logger;

            _tradingLogRepository = (CreateReadRepository<TradingLog>(_context), CreateWriteRepository<TradingLog>(_context));


            _userWalletRepository = (CreateReadRepository<UserWallet>(_context), CreateWriteRepository<UserWallet>(_context));
        }

        #region Properties Configuration

        #region TradingLog Property
        public IReadRepository<TradingLog> TradingLogReadRepository { get { return _tradingLogRepository.tradingLogReadRepository.Value; } }

        public IWriteRepository<TradingLog> TradingLogWriteRepository { get { return _tradingLogRepository.tradingLogWriteRepository.Value; } }
        #endregion

        #region UserWallet Property
        public IWriteRepository<UserWallet> UserWalletWriteRepository { get { return _userWalletRepository.userWalletWriteRepository.Value; } }

        public IReadRepository<UserWallet> UserWalletReadRepository { get { return _userWalletRepository.userWalletReadRepository.Value; } }
        #endregion

        #endregion

        public async Task<IDbContextTransaction> BeginTransactionAsync() => await _context.Database.BeginTransactionAsync();

        public async Task CommitAsync()
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.SaveChangesAsync();
                    _context.DetachAll();

                    await transaction.CommitAsync();
                }
                catch (Exception e)
                {
                    _logger.LogError($"UnitOfWork -> Commit Catch Error {Environment.NewLine}" +
                        $"EXCEPTION DETAILS: {JsonConvert.SerializeObject(e)} {Environment.NewLine}"
                    );
                    await transaction.RollbackAsync();
                    throw new Exception(e.Message);
                }
            }
        }

        public void Commit()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.SaveChanges();
                    _context.DetachAll();

                    transaction.Commit();

                }
                catch (Exception e)
                {
                    _logger.LogError($"UnitOfWork -> Commit Catch Error {Environment.NewLine}" +
                        $"EXCEPTION DETAILS: {JsonConvert.SerializeObject(e)} {Environment.NewLine}"
                    );
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }
        }

        public int SaveChanges() => _context.SaveChanges();
        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed is false && disposing is true) _context.Dispose();

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
