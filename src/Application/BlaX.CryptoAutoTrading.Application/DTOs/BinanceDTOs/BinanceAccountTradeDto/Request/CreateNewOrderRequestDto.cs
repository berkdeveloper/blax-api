using BlaX.CryptoAutoTrading.Domain.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace BlaX.CryptoAutoTrading.Application.DTOs.BinanceDTOs.BinanceAccountTradeDto.Request
{
    public class CreateNewOrderRequestDto
    {
        [Required(ErrorMessage = $"Sembol bilgisi zorunludur!")]
        public string Symbol { get; set; }

        [Required(ErrorMessage = $"Ticaret türü zorunludur!")]
        [JsonConverter(typeof(StringEnumConverter))]
        public TradingTypes TradingType { get; set; }

        [Required(ErrorMessage = $"Sipariş türü zorunludur!")]
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderTypes OrderType { get; set; }

        [Required(ErrorMessage = $"Miktar bilgisi zorunludur!")]
        public decimal Quantity { get; set; }
    }
}
