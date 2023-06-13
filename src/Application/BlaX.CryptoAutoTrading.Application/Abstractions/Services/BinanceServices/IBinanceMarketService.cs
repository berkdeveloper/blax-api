using BlaX.CryptoAutoTrading.Application.DTOs.BinanceDTOs.BinanceMarketDto.Request;
using BlaX.CryptoAutoTrading.Application.DTOs.BinanceDTOs.BinanceMarketDto.Response;
using BlaX.CryptoAutoTrading.Application.Utilities.Common.RequestBases;
using BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.Concrete;

namespace BlaX.CryptoAutoTrading.Application.Abstractions.Services.BinanceServices
{
    public interface IBinanceMarketService
    {
        Task<ObjectResponseBase<SymbolPriceTickerResponseDto>> GetSymbolPrice(SymbolRequestBase request);
        Task<ListBaseResponse<BinanceTradeResponseDto>> GetUserTrades(SymbolRequestBase request);
        Task<ListBaseResponse<RecentTradeResponseDto>> GetRecentTradesList(RecentTradeRequestDto request);
    }
}
