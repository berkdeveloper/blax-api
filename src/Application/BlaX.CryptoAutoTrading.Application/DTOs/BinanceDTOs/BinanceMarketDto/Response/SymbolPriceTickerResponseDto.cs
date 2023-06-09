using Newtonsoft.Json;

namespace BlaX.CryptoAutoTrading.Application.DTOs.BinanceDTOs.BinanceMarketDto.Response
{
    public class SymbolPriceTickerResponseDto
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
        [JsonProperty("price")]
        public string Price { get; set; }

        public SymbolPriceTickerResponseDto() { }
        public SymbolPriceTickerResponseDto(string price) => Price = price;
        public SymbolPriceTickerResponseDto(string price, string symbol) : this(price) => Symbol = symbol;
    }
}
