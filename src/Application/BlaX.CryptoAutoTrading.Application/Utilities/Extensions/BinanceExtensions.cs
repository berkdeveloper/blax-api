using Binance.Net.Enums;
using BlaX.CryptoAutoTrading.Domain.Enums;

namespace BlaX.CryptoAutoTrading.Application.Utilities.Extensions
{
    public static class BinanceExtensions
    {
        public static OrderSide? GetTradingType(this TradingTypes tradingTypes) =>
        tradingTypes switch
        {
            TradingTypes.Buy => OrderSide.Buy,
            TradingTypes.Sell => OrderSide.Sell,
            _ => null,
        };

        public static SpotOrderType? GetOrderType(this OrderTypes orderTypes)
        => orderTypes switch
        {
            OrderTypes.Limit => SpotOrderType.Limit,
            OrderTypes.Market => SpotOrderType.Market,
            OrderTypes.LimitMaker => SpotOrderType.LimitMaker,
            OrderTypes.StopLoss => SpotOrderType.StopLoss,
            OrderTypes.StopLossLimit => SpotOrderType.StopLossLimit,
            OrderTypes.TakeProfit => SpotOrderType.TakeProfit,
            OrderTypes.TakeProfitLimit => SpotOrderType.TakeProfitLimit,
            _ => null,
        };
    }
}
