using BlaX.CryptoAutoTrading.Application.Abstractions.Repositories.TradingLogRepositories;
using BlaX.CryptoAutoTrading.Domain.Entities;
using BlaX.CryptoAutoTrading.Persistence.Data.DbContexts;
using BlaX.CryptoAutoTrading.Persistence.Data.Repositories.BaseRepository;

namespace BlaX.CryptoAutoTrading.Persistence.Data.Repositories.TradingLogRepositories
{
    public class TradingLogWriteRepository : WriteRepository<TradingLog>, ITradingLogWriteRepository
    {
        public TradingLogWriteRepository(Context context) : base(context)
        {
        }
    }
}
