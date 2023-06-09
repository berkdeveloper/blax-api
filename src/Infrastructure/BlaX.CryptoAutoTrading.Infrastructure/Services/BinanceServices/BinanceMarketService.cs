using BlaX.CryptoAutoTrading.Application.Abstractions.Services.BinanceServices;
using BlaX.CryptoAutoTrading.Application.DTOs.BinanceDTOs.BinanceMarketDto.Response;
using BlaX.CryptoAutoTrading.Application.Exceptions.Common;
using BlaX.CryptoAutoTrading.Application.Utilities.Common.RequestBases;
using BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.Concrete;
using BlaX.CryptoAutoTrading.Application.Utilities.Helpers;
using BlaX.CryptoAutoTrading.Application.ViewModels.BinanceViewModels.MarketViewModels;
using BlaX.CryptoAutoTrading.Domain.AppSettings.BinanceAppSetting;
using BlaX.CryptoAutoTrading.Domain.Core.Constants;
using BlaX.CryptoAutoTrading.ExternalClients.Binance.Common;
using BlaX.CryptoAutoTrading.ExternalClients.Binance.Spot;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;

namespace BlaX.CryptoAutoTrading.Infrastructure.Services.BinanceServices
{
    public class BinanceMarketService : IBinanceMarketService
    {
        readonly ILogger _logger;
        readonly BinanceAuthorization _binanceAuthorization;
        private readonly Market _market;

        public BinanceMarketService(IOptions<BinanceAuthorization> binanceAuthorizationOptions, ILogger<BinanceMarketService> logger)
        {
            _binanceAuthorization = binanceAuthorizationOptions.Value;
            _logger = logger;

            HttpMessageHandler loggingHandler = new BinanceLoggingHandler(logger: _logger);
            HttpClient httpClient = new(handler: loggingHandler);

            _market = new Market(httpClient, apiKey: _binanceAuthorization.ApiKey, apiSecret: _binanceAuthorization.ApiSecretKey);
        }

        /// <summary>
        /// Get older market trades.<para />
        /// Weight(IP): 5.
        /// </summary>
        /// <param name="symbol">Trading symbol, e.g. BNBUSDT.</param>
        /// <param name="limit">Default 500; max 1000.</param>
        /// <param name="fromId">Trade id to fetch from. Default gets most recent trades.</param>
        /// <returns>Trade list.</returns>
        public async Task<ListBaseResponse<OldTradeLookupViewModel>> GetOldTradeLookup(SymbolRequestBase request)
        {
            if (request is null || ObjectPropertiesHelper.CheckIsNullObjectProperties(request) is true)
                throw new BadRequestErrorException(ExceptionTypes.BadRequest);

            var data = await GetOldTradeLookupResult(request.Symbol);

            return data;
        }

        public async Task<ListBaseResponse<OldTradeLookupViewModel>> GetOldTradeLookupResult(string symbol)
        {
            if (TestConnectivityHelper.BinanceMarketSystemConnectionCheck() is false) throw new Exception(ExceptionTypes.InternalServerError);

            await Task.Delay(TimeSpan.FromSeconds(1));

            var oldTradeResultsString = await _market.OldTradeLookup(symbol);

            var oldTradesResult = JsonConvert.DeserializeObject<List<OldTradeLookupViewModel>>(oldTradeResultsString);

            return new ListBaseResponse<OldTradeLookupViewModel>(oldTradesResult, System.Net.HttpStatusCode.OK);
        }

        /// <summary>
        /// Get recent trades.<para />
        /// Weight(IP): 1.
        /// </summary>
        /// <param name="symbol">Trading symbol, e.g. BNBUSDT.</param>
        /// <param name="limit">Default 500; max 1000.</param>
        /// <returns>Trade list.</returns>
        public async Task<ListBaseResponse<RecentTradesViewModel>> GetRecentTradesList(SymbolRequestBase request)
        {
            if (request is null || ObjectPropertiesHelper.IsArgumentsNullOrEmptyCheck(request, r => r.Symbol) is true) throw new BadRequestErrorException(ExceptionTypes.BadRequest);

            var data = await GetRecentTradesListResult(request.Symbol);

            return data;
        }

        public async Task<ListBaseResponse<RecentTradesViewModel>> GetRecentTradesListResult(string symbol)
        {
            if (TestConnectivityHelper.BinanceMarketSystemConnectionCheck() is false) throw new Exception(ExceptionTypes.InternalServerError);

            await Task.Delay(TimeSpan.FromSeconds(1));

            var recentTradesString = await _market.RecentTradesList(symbol);

            var recentTradesResult = JsonConvert.DeserializeObject<List<RecentTradesViewModel>>(recentTradesString);

            return new ListBaseResponse<RecentTradesViewModel>(recentTradesResult, System.Net.HttpStatusCode.OK);
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
        public async Task<ObjectResponseBase<SymbolPriceTickerViewModel>> GetSymbolPrice(SymbolRequestBase request)
        {
            if (request is null || ObjectPropertiesHelper.CheckIsNullObjectProperties(request) is true)
                throw new BadRequestErrorException(ExceptionTypes.BadRequest);

            var symbolPriceTicker = await GetSymbolPriceTickerResult(request.Symbol);

            var data = TypeConversion.Conversion<SymbolPriceTickerResponseDto, SymbolPriceTickerViewModel>(symbolPriceTicker);

            return new ObjectResponseBase<SymbolPriceTickerViewModel>(data, System.Net.HttpStatusCode.OK);
        }

        private async Task<SymbolPriceTickerResponseDto> GetSymbolPriceTickerResult(string symbol)
        {
            if (TestConnectivityHelper.BinanceMarketSystemConnectionCheck() is false) throw new Exception(ExceptionTypes.InternalServerError);

            await Task.Delay(TimeSpan.FromSeconds(1));

            var symbolPriceTickerString = await _market.SymbolPriceTicker(symbol);

            var symbolPriceTicker = JsonConvert.DeserializeObject<SymbolPriceTickerResponseDto>(symbolPriceTickerString);

            if (ObjectPropertiesHelper.CheckIsNullObjectProperties(symbolPriceTicker) is true)
                throw new NotFoundErrorException(ExceptionTypes.NotFound);

            _logger.LogInformation($"{symbolPriceTicker.Symbol}:{symbolPriceTicker.Price}");

            return symbolPriceTicker;
        }
    }
}