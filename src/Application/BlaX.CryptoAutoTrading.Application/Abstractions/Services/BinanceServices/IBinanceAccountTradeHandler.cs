using BlaX.CryptoAutoTrading.Application.DTOs.BinanceDTOs.BinanceAccountTradeDto.Response;
using BlaX.CryptoAutoTrading.Application.Utilities.Common.RequestBases;
using BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.Concrete;

namespace BlaX.CryptoAutoTrading.Application.Abstractions.Services.BinanceServices
{
    public interface IBinanceAccountTradeHandler
    {
        Task<bool> AnyOrders(SymbolRequestBase request);
        Task<ListBaseResponse<GetAllOrderIdResponseDto>> GetAllOrderId(SymbolRequestBase request);
    }
}
