using BlaX.CryptoAutoTrading.Application.DTOs.BinanceDTOs.BinanceWalletDto.Request;
using BlaX.CryptoAutoTrading.Application.DTOs.BinanceDTOs.BinanceWalletDto.Response;
using BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.Concrete;

namespace BlaX.CryptoAutoTrading.Application.Abstractions.Services.BinanceServices
{
    public interface IBinanceWalletService
    {
        Task<ListBaseResponse<UserAssetResponseDto>> GetUserAsset(UserAssetSymbolRequestDto request);
    }
}
