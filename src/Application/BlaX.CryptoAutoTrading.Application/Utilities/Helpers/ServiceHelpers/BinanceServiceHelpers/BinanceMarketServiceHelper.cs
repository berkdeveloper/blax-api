using Binance.Net.Clients;
using Binance.Net.Interfaces;
using Binance.Net.Objects.Models.Spot;
using BlaX.CryptoAutoTrading.Application.DTOs.BinanceDTOs.BinanceMarketDto.Request;
using BlaX.CryptoAutoTrading.Application.DTOs.BinanceDTOs.BinanceMarketDto.Response;
using BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.Concrete;
using BlaX.CryptoAutoTrading.Application.Utilities.Handlers;
using BlaX.CryptoAutoTrading.Domain.Core.Constants;
using CryptoExchange.Net.CommonObjects;

namespace BlaX.CryptoAutoTrading.Application.Utilities.Helpers.ServiceHelpers.BinanceServiceHelpers
{
    public static class BinanceMarketServiceHelper
    {
        public static async Task<ListBaseResponse<TradeResponseDto>> GetUserTradesResult(BinanceClient binanceClient, string symbol)
        {
            var response = await binanceClient.SpotApi.Trading.GetUserTradesAsync(symbol);

            #region Response Validate
            if (ApiResponseHandler.HandleList(response) is false) return new ListBaseResponse<TradeResponseDto>(System.Net.HttpStatusCode.BadRequest, ResponseErrorMessageConst.RequestFailed);

            var userTrades = new List<BinanceTrade>();

            var userTradesResult = ObjectValidationHandler.HandleList(response, dataList =>
            {
                userTrades = dataList.ToList();

                return true;
            });

            if (userTradesResult is false) return new ListBaseResponse<TradeResponseDto>(System.Net.HttpStatusCode.NotFound, ResponseErrorMessageConst.NoTradingHistoryFoundForUser);
            #endregion

            var dataList = TypeConversion.ConversionList<BinanceTrade, TradeResponseDto>(userTrades);

            return new ListBaseResponse<TradeResponseDto>(dataList, System.Net.HttpStatusCode.OK);
        }

        public static async Task<ListBaseResponse<RecentTradeResponseDto>> GetRecentTradesListResult(BinanceClient binanceClient, RecentTradeRequestDto requestDto)
        {
            var response = await binanceClient.SpotApi.ExchangeData.GetRecentTradesAsync(requestDto.Symbol, requestDto.Limit);

            #region Response Validate
            if (ApiResponseHandler.HandleList(response) is false) return new ListBaseResponse<RecentTradeResponseDto>(System.Net.HttpStatusCode.BadRequest, ResponseErrorMessageConst.RequestFailed);

            var recentTrades = new List<IBinanceRecentTrade>();

            var recentTradesResult = ObjectValidationHandler.HandleList(response, dataList =>
            {
                recentTrades = dataList.ToList();

                return true;
            });

            if (recentTradesResult is false) return new ListBaseResponse<RecentTradeResponseDto>(System.Net.HttpStatusCode.NotFound, ResponseErrorMessageConst.RecentTradesNotFound);
            #endregion

            var dataList = TypeConversion.ConversionList<IBinanceRecentTrade, RecentTradeResponseDto>(recentTrades);

            return new ListBaseResponse<RecentTradeResponseDto>(dataList, System.Net.HttpStatusCode.OK);
        }

        public static async Task<ObjectResponseBase<SymbolPriceTickerResponseDto>> GetSymbolPriceTickerResult(BinanceClient binanceClient, string symbol)
        {
            var response = await binanceClient.SpotApi.ExchangeData.GetPriceAsync(symbol);

            #region Response Validate
            if (ApiResponseHandler.Handle(response) is false) return new ObjectResponseBase<SymbolPriceTickerResponseDto>(System.Net.HttpStatusCode.BadRequest, ResponseErrorMessageConst.RequestFailed);

            var binancePrice = new BinancePrice();

            var binancePriceResult = ObjectValidationHandler.Handle(response, data =>
            {
                binancePrice = data;

                return true;
            });

            if (binancePriceResult is false) return new ObjectResponseBase<SymbolPriceTickerResponseDto>(System.Net.HttpStatusCode.NotFound, $"Price {ResponseErrorMessageConst.GeneralNotFound}");
            #endregion

            var data = TypeConversion.Conversion<BinancePrice, SymbolPriceTickerResponseDto>(binancePrice);

            return new ObjectResponseBase<SymbolPriceTickerResponseDto>(data, System.Net.HttpStatusCode.OK);
        }

        public static async Task<ListBaseResponse<CandlestickDataResponseDto>> GetCandlestickDataResult(BinanceClient binanceClient, CandlestickDataRequestDto requestDto)
        {
            var response = await binanceClient.SpotApi.ExchangeData.GetKlinesAsync(requestDto.Symbol, requestDto.Interval, limit: requestDto.Limit, startTime: requestDto.StartTime, endTime: requestDto.EndTime);

            #region Response Validate
            if (ApiResponseHandler.Handle(response) is false) return new ListBaseResponse<CandlestickDataResponseDto>(System.Net.HttpStatusCode.BadRequest, ResponseErrorMessageConst.RequestFailed);

            var candlestickDataList = new List<IBinanceKline>();

            var candlestickDataResult = ObjectValidationHandler.Handle(response, data =>
            {
                candlestickDataList = data.ToList();

                return true;
            });

            if (candlestickDataResult is false) return new ListBaseResponse<CandlestickDataResponseDto>(System.Net.HttpStatusCode.NotFound, $"Candlestick Data {ResponseErrorMessageConst.GeneralNotFound}");
            #endregion

            var dataList = TypeConversion.ConversionList<IBinanceKline, CandlestickDataResponseDto>(candlestickDataList);

            return new ListBaseResponse<CandlestickDataResponseDto>(dataList, System.Net.HttpStatusCode.OK);
        }

        public static async Task<ObjectResponseBase<TickerResponseDto>> Get24hTickerResult(BinanceClient binanceClient, string symbol)
        {
            var response = await binanceClient.SpotApi.CommonSpotClient.GetTickerAsync(symbol);

            #region Response Validate
            if (ApiResponseHandler.Handle(response) is false) return new ObjectResponseBase<TickerResponseDto>(System.Net.HttpStatusCode.BadRequest, ResponseErrorMessageConst.RequestFailed);

            var binanceTicker = new Ticker();

            var binanceTickerResult = ObjectValidationHandler.Handle(response, data =>
            {
                binanceTicker = data;

                return true;
            });

            if (binanceTickerResult is false) return new ObjectResponseBase<TickerResponseDto>(System.Net.HttpStatusCode.NotFound, $"Ticker {ResponseErrorMessageConst.GeneralNotFound}");
            #endregion

            var data = TypeConversion.Conversion<Ticker, TickerResponseDto>(binanceTicker);

            return new ObjectResponseBase<TickerResponseDto>(data, System.Net.HttpStatusCode.OK);
        }

    }
}
