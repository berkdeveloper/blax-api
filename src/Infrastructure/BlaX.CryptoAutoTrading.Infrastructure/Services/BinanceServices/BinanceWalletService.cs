using BlaX.CryptoAutoTrading.Application.Abstractions.Services.BinanceServices;
using BlaX.CryptoAutoTrading.Application.DTOs.BinanceDTOs.BinanceWalletDto.Request;
using BlaX.CryptoAutoTrading.Application.Exceptions.BinanceExceptions;
using BlaX.CryptoAutoTrading.Application.Exceptions.Common;
using BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.Concrete;
using BlaX.CryptoAutoTrading.Application.Utilities.Helpers;
using BlaX.CryptoAutoTrading.Application.ViewModels.BinanceViewModels.WalletViewModels;
using BlaX.CryptoAutoTrading.Domain.AppSettings.BinanceAppSetting;
using BlaX.CryptoAutoTrading.ExternalClients.Binance.Common;
using BlaX.CryptoAutoTrading.ExternalClients.Binance.Spot;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;

namespace BlaX.CryptoAutoTrading.Infrastructure.Services.BinanceServices
{
    public class BinanceWalletService : IBinanceWalletService
    {
        private readonly Serilog.ILogger _seriLogger;
        readonly ILogger _logger;
        readonly BinanceAuthorization _binanceAuthorization;
        private readonly Wallet _wallet;

        public BinanceWalletService(IOptions<BinanceAuthorization> binanceAuthorizationOptions, ILogger<BinanceWalletService> logger, Serilog.ILogger seriLogger)
        {
            _logger = logger;
            _binanceAuthorization = binanceAuthorizationOptions.Value;

            HttpMessageHandler loggingHandler = new BinanceLoggingHandler(logger: _logger);
            HttpClient httpClient = new(handler: loggingHandler);

            _wallet = new Wallet(httpClient, apiKey: _binanceAuthorization.ApiKey, apiSecret: _binanceAuthorization.ApiSecretKey);

            _seriLogger = seriLogger;
        }

        public async Task<ObjectResponseBase<UserAssetListViewModel>> GetUserAsset(UserAssetSymbolRequestDto request)
        {
            var data = await GetUserAssetsResult(assetSymbol: request.AssetSymbol);

            return new ObjectResponseBase<UserAssetListViewModel>(data, HttpStatusCode.OK);
        }

        public async Task<UserAssetListViewModel> GetUserAssetsResult(string assetSymbol)
        {
            if (TestConnectivityHelper.BinanceWalletSystemConnectionCheck() is false) throw new BinanceWalletSystemConnectionErrorException();

            await Task.Delay(TimeSpan.FromSeconds(1));

            var userAssetsString = await _wallet.UserAsset(assetSymbol);

            if (string.IsNullOrEmpty(userAssetsString) is true) throw new NotFoundErrorException("Hiçbir veri bulunamadı!");

            _logger.LogInformation($"User Assets: {userAssetsString}");

            #region Dataset
            //userAssetsString = "[\r\n  {\r\n    \"asset\": \"USDT\",\r\n    \"free\": \"10.983\",\r\n    \"locked\": \"0\",\r\n    \"freeze\": \"0\",\r\n    \"withdrawing\": \"0\",\r\n    \"ipoable\": \"0\",\r\n    \"btcValuation\": \"0.00050426\"\r\n  },\r\n  {\r\n    \"asset\": \"BTC\",\r\n    \"free\": \"15.983\",\r\n    \"locked\": \"0\",\r\n    \"freeze\": \"0\",\r\n    \"withdrawing\": \"0\",\r\n    \"ipoable\": \"0\",\r\n    \"btcValuation\": \"0.00070426\"\r\n  }\r\n]";
            #endregion

            return new UserAssetListViewModel { UserAssetsViewModels = JsonConvert.DeserializeObject<List<UserAssetsViewModel>>(userAssetsString) };
        }
    }
}