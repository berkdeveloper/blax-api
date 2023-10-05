using Binance.Net.Clients;
using Binance.Net.Objects.Models.Spot;
using BlaX.CryptoAutoTrading.Application.DTOs.BinanceDTOs.BinanceAccountTradeDto.Request;
using BlaX.CryptoAutoTrading.Application.DTOs.BinanceDTOs.BinanceAccountTradeDto.Response;
using BlaX.CryptoAutoTrading.Application.Exceptions.Common;
using BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.Concrete;
using BlaX.CryptoAutoTrading.Application.Utilities.Extensions;
using BlaX.CryptoAutoTrading.Application.Utilities.Handlers;
using BlaX.CryptoAutoTrading.Domain.Core.Constants;
using CryptoExchange.Net.Objects;
using System.Net;

namespace BlaX.CryptoAutoTrading.Application.Utilities.Helpers.ServiceHelpers.BinanceServiceHelpers
{
    public static class BinanceAccountTradeServiceHelper
    {
        public static async Task<ListBaseResponse<OrderResponseDto>> GetAllOrderResult(BinanceClient binanceClient, string symbol)
        {
            var response = await binanceClient.SpotApi.Trading.GetOrdersAsync(symbol);

            #region Response Validate
            if (ApiResponseHandler.HandleList(response) is false) return new ListBaseResponse<OrderResponseDto>(System.Net.HttpStatusCode.BadRequest, ResponseErrorMessageConst.RequestFailed);

            var orders = new List<BinanceOrder>();

            var binanceOrdersResult = ObjectValidationHandler.HandleList(response, dataList =>
            {
                orders = dataList.ToList();

                return true;
            });

            if (binanceOrdersResult is false) return new ListBaseResponse<OrderResponseDto>(HttpStatusCode.NotFound, ResponseErrorMessageConst.OrderNotFound);
            #endregion

            var dataList = TypeConversion.ConversionList<BinanceOrder, OrderResponseDto>(orders);

            return new ListBaseResponse<OrderResponseDto>(dataList, HttpStatusCode.OK);
        }

        public static async Task<ObjectResponseBase<OrderResponseDto>> GetOrderResult(BinanceClient binanceClient, GetOrderRequestDto request)
        {
            var response = await binanceClient.SpotApi.Trading.GetOrderAsync(request.Symbol, request.OrderId);

            #region Response Validate
            if (ApiResponseHandler.Handle(response) is false) return new ObjectResponseBase<OrderResponseDto>(HttpStatusCode.BadRequest, ResponseErrorMessageConst.RequestFailed);

            var binanceOrder = new BinanceOrder();

            var binanceOrderResult = ObjectValidationHandler.Handle(response, data =>
            {
                binanceOrder = data;

                return true;
            });

            if (binanceOrderResult is false) return new ObjectResponseBase<OrderResponseDto>(HttpStatusCode.NotFound, ResponseErrorMessageConst.OrderNotFound);
            #endregion

            var data = TypeConversion.Conversion<BinanceOrder, OrderResponseDto>(binanceOrder);

            return new ObjectResponseBase<OrderResponseDto>(data, HttpStatusCode.OK);
        }

        public static async Task<ObjectResponseBase<CreateNewOrderResponseDto>> GetNewOrderResult(BinanceClient binanceClient, CreateNewOrderRequestDto request)
        {
            if (request is null || ObjectPropertiesHelper.CheckIsNullObjectProperties(request)) throw new BadRequestErrorException(ExceptionTypes.BadRequest);

            var tradingType = request.TradingType.GetTradingType();
            var orderType = request.OrderType.GetOrderType();

            //var response = await binanceClient.SpotApi.Trading.PlaceOrderAsync(request.Symbol, tradingType, orderType, request.Quantity);
            var response = await binanceClient.SpotApi.Trading.PlaceTestOrderAsync(request.Symbol, tradingType, orderType, request.Quantity);

            #region Response Validate
            if (ApiResponseHandler.Handle(response) is false) return new ObjectResponseBase<CreateNewOrderResponseDto>(HttpStatusCode.BadRequest, ResponseErrorMessageConst.RequestFailed);

            var placedOrder = new BinancePlacedOrder();

            var binancePlaceOrderResult = ObjectValidationHandler.Handle(response, data =>
            {
                placedOrder = data;

                return true;
            });

            if (binancePlaceOrderResult is false) return new ObjectResponseBase<CreateNewOrderResponseDto>(statusCode: response.ResponseStatusCode.Value, errorMessage: response.Error.Message);
            #endregion

            if (response.Data.Status == Binance.Net.Enums.OrderStatus.Filled)
            {
                var data = TypeConversion.Conversion<BinancePlacedOrder, CreateNewOrderResponseDto>(placedOrder);

                return new ObjectResponseBase<CreateNewOrderResponseDto>(data, HttpStatusCode.OK);
            }

            var orderStatus = response.Data.Status.ToString();
            var message = $"Order status: {orderStatus}";

            return new ObjectResponseBase<CreateNewOrderResponseDto>(HttpStatusCode.NotAcceptable, message);
        }

        public static async Task<ListBaseResponse<GetAllOrderIdResponseDto>> GetAllOrderIdResult(BinanceClient binanceClient, string symbol)
        {
            var response = await binanceClient.SpotApi.Trading.GetOrdersAsync(symbol);

            #region Response Validate
            if (ApiResponseHandler.HandleList(response) is false) return new ListBaseResponse<GetAllOrderIdResponseDto>(HttpStatusCode.BadRequest, ResponseErrorMessageConst.RequestFailed);

            var orders = new List<BinanceOrder>();

            var ordersResult = ObjectValidationHandler.HandleList(response, dataList =>
            {
                orders = dataList.ToList();

                return true;
            });

            if (ordersResult is false) return new ListBaseResponse<GetAllOrderIdResponseDto>(System.Net.HttpStatusCode.NotFound, ResponseErrorMessageConst.OrderNotFound);
            #endregion

            var result = orders.Select(o => new GetAllOrderIdResponseDto() { OrderId = o.Id, Symbol = o.Symbol }).ToList();

            return new ListBaseResponse<GetAllOrderIdResponseDto>(result, HttpStatusCode.OK);
        }

        public static async Task<bool> AnyOrdersResult(BinanceClient binanceClient, string symbol) => await binanceClient.SpotApi.Trading.GetOrdersAsync(symbol) is WebCallResult<IEnumerable<BinanceOrder>> response && response.Success is true && response is not null && response.Data.Any() is true;
    }
}
