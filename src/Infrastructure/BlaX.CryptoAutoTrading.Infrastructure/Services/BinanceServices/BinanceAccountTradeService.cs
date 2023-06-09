using Binance.Net.Clients;
using Binance.Net.Enums;
using Binance.Net.Objects;
using BlaX.CryptoAutoTrading.Application.Abstractions.Services.BinanceServices;
using BlaX.CryptoAutoTrading.Application.DTOs;
using BlaX.CryptoAutoTrading.Application.DTOs.BinanceDTOs.BinanceAccountTradeDto.Request;
using BlaX.CryptoAutoTrading.Application.DTOs.CommonDTOs.BaseObjectDto;
using BlaX.CryptoAutoTrading.Application.DTOs.CommonDTOs.UserDto;
using BlaX.CryptoAutoTrading.Application.Exceptions.Common;
using BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases;
using BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.Concrete;
using BlaX.CryptoAutoTrading.Application.Utilities.Extensions;
using BlaX.CryptoAutoTrading.Application.Utilities.Helpers;
using BlaX.CryptoAutoTrading.Application.ViewModels.BinanceViewModels.AccountTradeViewModels;
using BlaX.CryptoAutoTrading.Domain.AppSettings.BinanceAppSetting;
using BlaX.CryptoAutoTrading.Domain.Core.Constants;
using BlaX.CryptoAutoTrading.ExternalClients.Binance.Common;
using BlaX.CryptoAutoTrading.ExternalClients.Binance.Spot;
using BlaX.CryptoAutoTrading.ExternalClients.Binance.Spot.Models;
using CryptoExchange.Net.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BlaX.CryptoAutoTrading.Infrastructure.Services.BinanceServices
{
    public class BinanceAccountTradeService : IBinanceAccountTradeService
    {
        private readonly BinanceClient _binanceClient;
        readonly ILogger _logger;
        readonly BinanceAuthorization _binanceAuthorization;
        private readonly SpotAccountTrade _spotAccountTrade;

        public BinanceAccountTradeService(IOptions<BinanceAuthorization> binanceAuthorizationOptions, ILogger<BinanceAccountTradeService> logger)
        {
            _binanceAuthorization = binanceAuthorizationOptions.Value;
            _logger = logger;

            _binanceClient = new BinanceClient(new BinanceClientOptions
            {
                ApiCredentials = new BinanceApiCredentials(_binanceAuthorization.ApiKey, _binanceAuthorization.ApiSecretKey),
                LogLevel = LogLevel.Information,
            });

            #region Old-Use
            // Eski kullanımı:

            HttpMessageHandler loggingHandler = new BinanceLoggingHandler(logger: _logger);
            HttpClient httpClient = new(handler: loggingHandler);

            _spotAccountTrade = new SpotAccountTrade(httpClient, apiKey: _binanceAuthorization.ApiKey, apiSecret: _binanceAuthorization.ApiSecretKey);
            #endregion
        }

        public async Task<ResponseBase> CreateNewOrder(CreateNewOrderRequestDto request) =>
            await GetOrderResponseResult(request) is true ? new ResponseBase(System.Net.HttpStatusCode.Created, "The order was successfully!") :
            new ResponseBase(System.Net.HttpStatusCode.UnprocessableEntity, "The order was unsuccessful!");

        public async Task<ObjectResponseBase<AllOrdersViewModel>> GetAllOrders(AllOrdersRequestDto request)
        {
            if (request is null || ObjectPropertiesHelper.CheckIsNullObjectProperties(request)) throw new BadRequestErrorException(ExceptionTypes.BadRequest);

            var response = await _binanceClient.SpotApi.Trading.GetOrdersAsync(request.Symbol);

            var data = response.Data.ToList();

            return new ObjectResponseBase<AllOrdersViewModel>(null, System.Net.HttpStatusCode.OK);
        }

        public async Task<bool> GetOrderResponseResult(CreateNewOrderRequestDto request)
        {
            if (request is null || ObjectPropertiesHelper.CheckIsNullObjectProperties(request)) throw new BadRequestErrorException(ExceptionTypes.BadRequest);

            var tradingType = (OrderSide)request.TradingType.GetTradingType(); // cast edilmesinin sebebi, fonksiyondan dönen nesnenin nullable olmasıdır.
            var orderType = (SpotOrderType)request.OrderType.GetOrderType();

            //var deneme = await _binanceClient.SpotApi.Trading.PlaceOrderAsync(request.Symbol, tradingType, orderType, request.Quantity);
            var result = await _binanceClient.SpotApi.Trading.PlaceTestOrderAsync(request.Symbol, tradingType, orderType, request.Quantity);

            return result.Success;
        }

        public async Task<AuthorizedUserDto> TestService(TestDto request)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(100));
            var testModel = request.AuthorizedUserObject;
            var data = TypeConversion.Conversion<AuthorizedUserObject, AuthorizedUserDto>(testModel);

            return data;
        }
    }
}