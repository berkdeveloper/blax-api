using Binance.Net.Clients;
using Binance.Net.Objects;
using BlaX.CryptoAutoTrading.Application.Abstractions.Services.BinanceServices;
using BlaX.CryptoAutoTrading.Application.DTOs.BinanceDTOs.BinanceMarketDto.Request;
using BlaX.CryptoAutoTrading.Application.DTOs.BinanceDTOs.BinanceMarketDto.Response;
using BlaX.CryptoAutoTrading.Application.Exceptions.Common;
using BlaX.CryptoAutoTrading.Application.Utilities.Common.RequestBases;
using BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.Concrete;
using BlaX.CryptoAutoTrading.Application.Utilities.Helpers;
using BlaX.CryptoAutoTrading.Application.Utilities.Helpers.ServiceHelpers.BinanceServiceHelpers;
using BlaX.CryptoAutoTrading.Domain.AppSettings.BinanceAppSetting;
using BlaX.CryptoAutoTrading.Domain.Core.Constants;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BlaX.CryptoAutoTrading.Infrastructure.Services.BinanceServices
{
    public class BinanceMarketService : IBinanceMarketService
    {
        private readonly BinanceClient _binanceClient;
        readonly ILogger _logger;
        readonly BinanceAuthorization _binanceAuthorization;

        public BinanceMarketService(IOptions<BinanceAuthorization> binanceAuthorizationOptions, ILogger<BinanceMarketService> logger)
        {
            _binanceAuthorization = binanceAuthorizationOptions.Value;
            _logger = logger;

            _binanceClient = new BinanceClient(new BinanceClientOptions
            {
                ApiCredentials = new BinanceApiCredentials(_binanceAuthorization.ApiKey, _binanceAuthorization.ApiSecretKey),
                LogLevel = LogLevel.Information,
            });
        }

        /// <summary>
        /// Get older market trades.<para />
        /// Weight(IP): 5.
        /// </summary>
        /// <param name="symbol">Trading symbol, e.g. BNBUSDT.</param>
        /// <param name="limit">Default 500; max 1000.</param>
        /// <param name="fromId">Trade id to fetch from. Default gets most recent trades.</param>
        /// <returns>Trade list.</returns>
        public async Task<ListBaseResponse<BinanceTradeResponseDto>> GetUserTrades(SymbolRequestBase request)
        {
            if (request is null || ObjectPropertiesHelper.CheckIsNullObjectProperties(request) is true) throw new BadRequestErrorException(ExceptionTypes.BadRequest);

            var result = await BinanceMarketServiceHelper.GetUserTradesResult(_binanceClient, request.Symbol);

            return result;
        }

        /// <summary>
        /// Get recent trades.<para />
        /// Weight(IP): 1.
        /// </summary>
        /// <param name="symbol">Trading symbol, e.g. BNBUSDT.</param>
        /// <param name="limit">Default 500; max 1000.</param>
        /// <returns>Trade list.</returns>
        public async Task<ListBaseResponse<RecentTradeResponseDto>> GetRecentTradesList(RecentTradeRequestDto request)
        {
            if (request is null || ObjectPropertiesHelper.IsArgumentsNullOrEmptyCheck(request, r => r.Symbol) is true) throw new BadRequestErrorException(ExceptionTypes.BadRequest);

            var result = await BinanceMarketServiceHelper.GetRecentTradesListResult(_binanceClient, request);

            return result;
        }

        /// <summary>
        /// Latest price for a symbol or symbols.<para />
        /// - If the symbol is not sent, prices for all symbols will be returned in an array.<para />
        /// Weight(IP):.<para />
        /// - `1` for a single symbol;.<para />
        /// - `2` when the symbol parameter is omitted;.
        /// </summary>
        /// <param name="symbol">Trading symbol, e.g. BNBUSDT.</param>
        /// <param name="symbols"></param>
        /// <returns>Price ticker.</returns>
        public async Task<ObjectResponseBase<SymbolPriceTickerResponseDto>> GetSymbolPrice(SymbolRequestBase request)
        {
            if (request is null || ObjectPropertiesHelper.CheckIsNullObjectProperties(request) is true) throw new BadRequestErrorException(ExceptionTypes.BadRequest);

            var result = await BinanceMarketServiceHelper.GetSymbolPriceTickerResult(_binanceClient, request.Symbol);

            _logger.LogInformation($"{result.Symbol}:{result.Price}");

            return new ObjectResponseBase<SymbolPriceTickerResponseDto>(result, System.Net.HttpStatusCode.OK);
        }
    }
}