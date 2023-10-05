using Binance.Net.Enums;
using BlaX.CryptoAutoTrading.Application.Utilities.Common.RequestBases;

namespace BlaX.CryptoAutoTrading.Application.DTOs.BinanceDTOs.BinanceMarketDto.Request
{
    public class CandlestickDataRequestDto : SymbolRequestBase
    {
        public KlineInterval Interval { get; set; }
        public int? Limit { get; set; } = 1;
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
