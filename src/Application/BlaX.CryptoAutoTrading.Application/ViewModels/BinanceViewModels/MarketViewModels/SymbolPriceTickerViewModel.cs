using Newtonsoft.Json;

namespace BlaX.CryptoAutoTrading.Application.ViewModels.BinanceViewModels.MarketViewModels
{
    public class SymbolPriceTickerViewModel
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
        [JsonProperty("price")]
        public string Price { get; set; }

        public SymbolPriceTickerViewModel() { }
        public SymbolPriceTickerViewModel(string price) => Price = price;
        public SymbolPriceTickerViewModel(string price, string symbol) : this(price) => Symbol = symbol;
    }
}
