using Binance.Net.Clients;
using Binance.Net.Objects;
using BlaX.CryptoAutoTrading.Application.Abstractions.Services.BinanceServices;
using BlaX.CryptoAutoTrading.Application.DTOs.BinanceDTOs.BinanceWalletDto.Request;
using BlaX.CryptoAutoTrading.Application.DTOs.BinanceDTOs.BinanceWalletDto.Response;
using BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.Concrete;
using BlaX.CryptoAutoTrading.Application.Utilities.Helpers.ServiceHelpers.BinanceServiceHelpers;
using BlaX.CryptoAutoTrading.Domain.AppSettings.BinanceAppSetting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BlaX.CryptoAutoTrading.Infrastructure.Services.BinanceServices
{
    public class BinanceWalletService : IBinanceWalletService
    {
        private readonly BinanceClient _binanceClient;
        private readonly Serilog.ILogger _seriLogger;
        readonly ILogger _logger;
        readonly BinanceAuthorization _binanceAuthorization;
        //private readonly Wallet _wallet;

        public BinanceWalletService(IOptions<BinanceAuthorization> binanceAuthorizationOptions, ILogger<BinanceWalletService> logger, Serilog.ILogger seriLogger)
        {
            _binanceAuthorization = binanceAuthorizationOptions.Value;
            _logger = logger;

            _binanceClient = new BinanceClient(new BinanceClientOptions
            {
                ApiCredentials = new BinanceApiCredentials(_binanceAuthorization.ApiKey, _binanceAuthorization.ApiSecretKey),
                LogLevel = LogLevel.Information,
            });

            _seriLogger = seriLogger;
        }

        public async Task<ListBaseResponse<UserAssetResponseDto>> GetUserAsset(UserAssetSymbolRequestDto request) => await BinanceWalletServiceHelper.GetUserAssetsResult(_binanceClient, request.AssetSymbol);
    }
}