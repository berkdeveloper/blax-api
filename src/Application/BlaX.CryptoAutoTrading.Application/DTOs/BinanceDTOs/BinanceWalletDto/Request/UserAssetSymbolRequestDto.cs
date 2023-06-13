using System.ComponentModel.DataAnnotations;

namespace BlaX.CryptoAutoTrading.Application.DTOs.BinanceDTOs.BinanceWalletDto.Request
{
    public class UserAssetSymbolRequestDto
    {
        [MaxLength(10)]
        public string AssetSymbol { get; set; }
    }
}
