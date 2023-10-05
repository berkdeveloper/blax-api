using Binance.Net.Clients;
using Binance.Net.Objects.Models.Spot;
using BlaX.CryptoAutoTrading.Application.DTOs.BinanceDTOs.BinanceWalletDto.Response;
using BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.Concrete;
using BlaX.CryptoAutoTrading.Application.Utilities.Handlers;
using BlaX.CryptoAutoTrading.Domain.Core.Constants;

namespace BlaX.CryptoAutoTrading.Application.Utilities.Helpers.ServiceHelpers.BinanceServiceHelpers
{
    public static class BinanceWalletServiceHelper
    {
        public static async Task<ListBaseResponse<UserAssetResponseDto>> GetUserAssetsResult(BinanceClient binanceClient, string assetSymbol)
        {
            if (string.IsNullOrEmpty(assetSymbol) is false && SymbolHelper.IsValidSymbol(assetSymbol) is false)
                return new ListBaseResponse<UserAssetResponseDto>(System.Net.HttpStatusCode.NotFound, ResponseErrorMessageConst.InvalidAssetSymbol);

            var response = await binanceClient.SpotApi.Account.GetBalancesAsync(assetSymbol);

            #region Response Validate
            if (ApiResponseHandler.HandleList(response) is false) return new ListBaseResponse<UserAssetResponseDto>(System.Net.HttpStatusCode.BadRequest, ResponseErrorMessageConst.RequestFailed);

            var userBalances = new List<BinanceUserBalance>();

            var userBalancesResult = ObjectValidationHandler.HandleList(response, dataList =>
            {
                userBalances = dataList.ToList();

                return true;
            });

            if (userBalancesResult is false) return new ListBaseResponse<UserAssetResponseDto>(System.Net.HttpStatusCode.NotFound, ResponseErrorMessageConst.UserAssetNotFound);
            #endregion

            var dataList = TypeConversion.ConversionList<BinanceUserBalance, UserAssetResponseDto>(userBalances);

            return new ListBaseResponse<UserAssetResponseDto>(dataList, System.Net.HttpStatusCode.OK);
        }
    }
}
