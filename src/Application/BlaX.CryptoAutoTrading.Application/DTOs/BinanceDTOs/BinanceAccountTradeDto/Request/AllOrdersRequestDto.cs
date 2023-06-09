using System.ComponentModel.DataAnnotations;

namespace BlaX.CryptoAutoTrading.Application.DTOs.BinanceDTOs.BinanceAccountTradeDto.Request
{
    public class AllOrdersRequestDto
    {
        [Required(ErrorMessage = $"{nameof(Symbol)} is required!")]
        public string Symbol { get; set; }
    }
}