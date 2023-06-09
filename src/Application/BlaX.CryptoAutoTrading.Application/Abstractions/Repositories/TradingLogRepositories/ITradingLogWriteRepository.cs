using BlaX.CryptoAutoTrading.Application.Abstractions.Repositories.BaseRepository;
using BlaX.CryptoAutoTrading.Domain.Entities;

namespace BlaX.CryptoAutoTrading.Application.Abstractions.Repositories.TradingLogRepositories
{
    public interface ITradingLogWriteRepository : IWriteRepository<TradingLog>
    {
    }
}
