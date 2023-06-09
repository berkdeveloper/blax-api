using BlaX.CryptoAutoTrading.Application.DTOs.BinanceDTOs.BinanceWalletDto.Request;
using BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.Concrete;
using BlaX.CryptoAutoTrading.Application.ViewModels.BinanceViewModels.WalletViewModels;

namespace BlaX.CryptoAutoTrading.Application.Abstractions.Services.BinanceServices
{
    public interface IBinanceWalletService
    {
        Task<ObjectResponseBase<UserAssetListViewModel>> GetUserAsset(UserAssetSymbolRequestDto request);
    }
}
