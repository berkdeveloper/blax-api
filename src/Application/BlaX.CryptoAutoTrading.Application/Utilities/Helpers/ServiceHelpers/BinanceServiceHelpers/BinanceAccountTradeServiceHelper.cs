using Binance.Net.Clients;
using Binance.Net.Objects.Models.Spot;
using BlaX.CryptoAutoTrading.Application.DTOs.BinanceDTOs.BinanceAccountTradeDto.Request;
using BlaX.CryptoAutoTrading.Application.DTOs.BinanceDTOs.BinanceAccountTradeDto.Response;
using BlaX.CryptoAutoTrading.Application.Exceptions.Common;
using BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.Concrete;
using BlaX.CryptoAutoTrading.Application.Utilities.Extensions;
using BlaX.CryptoAutoTrading.Domain.Core.Constants;
using System.Net;

namespace BlaX.CryptoAutoTrading.Application.Utilities.Helpers.ServiceHelpers.BinanceServiceHelpers
{
    public static class BinanceAccountTradeServiceHelper
    {
        public static async Task<ListBaseResponse<OrderResponseDto>> GetAllOrderResult(BinanceClient binanceClient, string symbol)
        {
            var response = await binanceClient.SpotApi.Trading.GetOrdersAsync(symbol);

            var binanceOrders = response.Data.ToList();

            if (binanceOrders.Any() is false) return new ListBaseResponse<OrderResponseDto>(HttpStatusCode.NotFound, ResponseErrorMessageConst.OrderNotFound);

            var dataList = TypeConversion.ConversionList<BinanceOrder, OrderResponseDto>(binanceOrders);

            return new ListBaseResponse<OrderResponseDto>(dataList, HttpStatusCode.OK);
        }

        public static async Task<ObjectResponseBase<OrderResponseDto>> GetOrderResult(BinanceClient binanceClient, GetOrderRequestDto request)
        {
            var response = await binanceClient.SpotApi.Trading.GetOrderAsync(request.Symbol, request.OrderId);

            var binanceOrder = response.Data;

            if (binanceOrder is null) return new ObjectResponseBase<OrderResponseDto>(HttpStatusCode.NotFound, ResponseErrorMessageConst.OrderNotFound);

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

            var placedOrder = response.Data;

            if (placedOrder is null && response.Error is not null) return new ObjectResponseBase<CreateNewOrderResponseDto>(statusCode: response.ResponseStatusCode.Value, errorMessage: response.Error.Message);

            if (response.Data is not null && response.Data.Status == Binance.Net.Enums.OrderStatus.Filled)
            {
                var data = TypeConversion.Conversion<BinancePlacedOrder, CreateNewOrderResponseDto>(response.Data);

                return new ObjectResponseBase<CreateNewOrderResponseDto>(data, HttpStatusCode.OK);
            }

            var orderStatus = response.Data is not null ? response.Data.Status.ToString() : "Canceled";
            var message = $"Order status: {orderStatus}";

            return new ObjectResponseBase<CreateNewOrderResponseDto>(HttpStatusCode.NotAcceptable, message);
        }

        public static async Task<ListBaseResponse<GetAllOrderIdResponseDto>> GetAllOrderIdResult(BinanceClient binanceClient, string symbol)
        {
            var response = await binanceClient.SpotApi.Trading.GetOrdersAsync(symbol);

            var binanceOrders = response.Data.ToList();

            if (binanceOrders.Any() is false) return new ListBaseResponse<GetAllOrderIdResponseDto>(HttpStatusCode.NotFound, ResponseErrorMessageConst.OrderNotFound);

            var result = binanceOrders.Select(o => new GetAllOrderIdResponseDto() { OrderId = o.Id, Symbol = o.Symbol }).ToList();

            return new ListBaseResponse<GetAllOrderIdResponseDto>(result, HttpStatusCode.OK);
        }

        public static async Task<bool> AnyOrdersResult(BinanceClient binanceClient, string symbol)
        {
            var response = await binanceClient.SpotApi.Trading.GetOrdersAsync(symbol);
            return response.Data.Any();
        }

    }
}
