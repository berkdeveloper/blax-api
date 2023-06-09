using BlaX.CryptoAutoTrading.Application.DTOs.CommonDTOs.TradingLogDto;
using BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.Concrete;
using BlaX.CryptoAutoTrading.Domain.Entities;

namespace BlaX.CryptoAutoTrading.Application.Abstractions.Services.CommonServices
{
    public interface ITradingLogService
    {
        Task<TradingLog> GetLogAsync(Guid id);
        Task<ListBaseResponse<TradingLog>> GetAllLogAsync(int limit);
        Task CreateLogAsync(CreateTradingLogDto request);
    }
}
