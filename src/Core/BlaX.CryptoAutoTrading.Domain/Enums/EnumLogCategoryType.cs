using System.ComponentModel;

namespace BlaX.CryptoAutoTrading.Domain.Enums
{
    public enum EnumLogCategoryType
    {
        [Description("Binance Market")]
        BinanceMarket = 1,
        [Description("Trading Log")]
        TradingLog = 2,
    }
}
