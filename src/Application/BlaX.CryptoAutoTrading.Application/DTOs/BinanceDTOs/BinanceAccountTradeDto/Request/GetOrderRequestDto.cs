using BlaX.CryptoAutoTrading.Application.Utilities.Common.RequestBases;

namespace BlaX.CryptoAutoTrading.Application.DTOs.BinanceDTOs.BinanceAccountTradeDto.Request
{
    public class GetOrderRequestDto : SymbolRequestBase
    {
        public long? OrderId { get; set; }
        public GetOrderRequestDto() => OrderId ??= default;
    }
}
