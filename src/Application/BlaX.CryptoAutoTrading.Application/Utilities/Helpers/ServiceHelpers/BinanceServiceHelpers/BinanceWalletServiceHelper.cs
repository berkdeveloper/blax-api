using Binance.Net.Clients;
using Binance.Net.Objects.Models.Spot;
using BlaX.CryptoAutoTrading.Application.DTOs.BinanceDTOs.BinanceWalletDto.Response;
using BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.Concrete;
using BlaX.CryptoAutoTrading.Domain.Core.Constants;

namespace BlaX.CryptoAutoTrading.Application.Utilities.Helpers.ServiceHelpers.BinanceServiceHelpers
{
    public static class BinanceWalletServiceHelper
    {
        public static async Task<ListBaseResponse<UserAssetResponseDto>> GetUserAssetsResul(BinanceClient binanceClient, string assetSymbol)
        {
            var response = await binanceClient.SpotApi.Account.GetBalancesAsync(assetSymbol);

            var userBalances = response.Data.ToList();

            if (userBalances.Any() is false) return new ListBaseResponse<UserAssetResponseDto>(System.Net.HttpStatusCode.NotFound, ResponseErrorMessageConst.UserAssetNotFound);

            var dataList = TypeConversion.ConversionList<BinanceUserBalance, UserAssetResponseDto>(userBalances);

            return new ListBaseResponse<UserAssetResponseDto>(dataList, System.Net.HttpStatusCode.OK);
        }
    }
}
