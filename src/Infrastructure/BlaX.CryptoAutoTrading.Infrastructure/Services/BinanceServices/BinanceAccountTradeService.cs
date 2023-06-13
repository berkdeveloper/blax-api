using Binance.Net.Clients;
using Binance.Net.Objects;
using BlaX.CryptoAutoTrading.Application.Abstractions.Services.BinanceServices;
using BlaX.CryptoAutoTrading.Application.DTOs;
using BlaX.CryptoAutoTrading.Application.DTOs.BinanceDTOs.BinanceAccountTradeDto.Request;
using BlaX.CryptoAutoTrading.Application.DTOs.BinanceDTOs.BinanceAccountTradeDto.Response;
using BlaX.CryptoAutoTrading.Application.DTOs.CommonDTOs.BaseObjectDto;
using BlaX.CryptoAutoTrading.Application.DTOs.CommonDTOs.UserDto;
using BlaX.CryptoAutoTrading.Application.Exceptions.Common;
using BlaX.CryptoAutoTrading.Application.Utilities.Common.RequestBases;
using BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.Concrete;
using BlaX.CryptoAutoTrading.Application.Utilities.Helpers;
using BlaX.CryptoAutoTrading.Application.Utilities.Helpers.ServiceHelpers.BinanceServiceHelpers;
using BlaX.CryptoAutoTrading.Domain.AppSettings.BinanceAppSetting;
using BlaX.CryptoAutoTrading.Domain.Core.Constants;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;

namespace BlaX.CryptoAutoTrading.Infrastructure.Services.BinanceServices
{
    public class BinanceAccountTradeService : IBinanceAccountTradeService, IBinanceAccountTradeHandler
    {
        private readonly BinanceClient _binanceClient;
        readonly ILogger _logger;
        readonly BinanceAuthorization _binanceAuthorization;
        //private readonly SpotAccountTrade _spotAccountTrade;

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

            ////HttpMessageHandler loggingHandler = new BinanceLoggingHandler(logger: _logger);
            ////HttpClient httpClient = new(handler: loggingHandler);

            ////_spotAccountTrade = new SpotAccountTrade(httpClient, apiKey: _binanceAuthorization.ApiKey, apiSecret: _binanceAuthorization.ApiSecretKey);
            #endregion
        }

        public async Task<ObjectResponseBase<CreateNewOrderResponseDto>> CreateNewOrder(CreateNewOrderRequestDto request)
        {
            if (request is null || ObjectPropertiesHelper.CheckIsNullObjectProperties(request)) throw new BadRequestErrorException(ExceptionTypes.BadRequest);

            var result = await BinanceAccountTradeServiceHelper.GetNewOrderResult(_binanceClient, request);

            return result;
        }

        public async Task<ListBaseResponse<OrderResponseDto>> GetAllOrders(AllOrdersRequestDto request)
        {
            if (request is null || ObjectPropertiesHelper.CheckIsNullObjectProperties(request)) throw new BadRequestErrorException(ExceptionTypes.BadRequest);

            var result = await BinanceAccountTradeServiceHelper.GetAllOrderResult(_binanceClient, request.Symbol);

            return result;
        }

        public async Task<ObjectResponseBase<OrderResponseDto>> GetOrder(GetOrderRequestDto request)
        {
            request.OrderId = request.OrderId is null or (long)default ? await GetAllOrders(new AllOrdersRequestDto(request.Symbol)) is ListBaseResponse<OrderResponseDto> orders && orders.StatusCode == HttpStatusCode.OK ? orders.DataList.LastOrDefault().Id : default : request.OrderId;

            if (request is null || ObjectPropertiesHelper.CheckIsNullObjectProperties(request)) throw new BadRequestErrorException(ExceptionTypes.BadRequest);

            var result = await BinanceAccountTradeServiceHelper.GetOrderResult(_binanceClient, request);

            return result;
        }


        public async Task<AuthorizedUserDto> TestService(TestDto request)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(100));
            var testModel = request.AuthorizedUserObject;
            var data = TypeConversion.Conversion<AuthorizedUserObject, AuthorizedUserDto>(testModel);

            return data;
        }

        public async Task<ListBaseResponse<GetAllOrderIdResponseDto>> GetAllOrderId(SymbolRequestBase request)
        {
            if (request is null || ObjectPropertiesHelper.CheckIsNullObjectProperties(request)) throw new BadRequestErrorException(ExceptionTypes.BadRequest);

            var result = await BinanceAccountTradeServiceHelper.GetAllOrderIdResult(_binanceClient, request.Symbol);

            return result;
        }

        public async Task<bool> AnyOrders(SymbolRequestBase request)
        {
            var result = await BinanceAccountTradeServiceHelper.AnyOrdersResult(_binanceClient, request.Symbol);
            return result;
        }
    }
}