using Binance.Net.Clients;
using Binance.Net.Interfaces;
using Binance.Net.Objects.Models.Spot;
using BlaX.CryptoAutoTrading.Application.DTOs.BinanceDTOs.BinanceMarketDto.Request;
using BlaX.CryptoAutoTrading.Application.DTOs.BinanceDTOs.BinanceMarketDto.Response;
using BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.Concrete;
using BlaX.CryptoAutoTrading.Domain.Core.Constants;

namespace BlaX.CryptoAutoTrading.Application.Utilities.Helpers.ServiceHelpers.BinanceServiceHelpers
{
    public static class BinanceMarketServiceHelper
    {
        public static async Task<ListBaseResponse<BinanceTradeResponseDto>> GetUserTradesResult(BinanceClient binanceClient, string symbol)
        {
            var response = await binanceClient.SpotApi.Trading.GetUserTradesAsync(symbol);

            if (response.Data.Any() is false) return new ListBaseResponse<BinanceTradeResponseDto>(System.Net.HttpStatusCode.NotFound, ResponseErrorMessageConst.NoTradingHistoryFoundForUser);

            var userTrades = response.Data.ToList();

            var dataList = TypeConversion.ConversionList<BinanceTrade, BinanceTradeResponseDto>(userTrades);

            return new ListBaseResponse<BinanceTradeResponseDto>(dataList, System.Net.HttpStatusCode.OK);
        }

        public static async Task<ListBaseResponse<RecentTradeResponseDto>> GetRecentTradesListResult(BinanceClient binanceClient, RecentTradeRequestDto requestDto)
        {
            var response = await binanceClient.SpotApi.ExchangeData.GetRecentTradesAsync(requestDto.Symbol, requestDto.Limit);

            var recentTrades = response.Data.ToList();

            if (response.Data.Any() is false) return new ListBaseResponse<RecentTradeResponseDto>(System.Net.HttpStatusCode.NotFound, ResponseErrorMessageConst.RecentTradesNotFound);

            var dataList = TypeConversion.ConversionList<IBinanceRecentTrade, RecentTradeResponseDto>(recentTrades);

            return new ListBaseResponse<RecentTradeResponseDto>(dataList, System.Net.HttpStatusCode.OK);
        }


        public static async Task<SymbolPriceTickerResponseDto> GetSymbolPriceTickerResult(BinanceClient binanceClient, string symbol)
        {
            var response = await binanceClient.SpotApi.ExchangeData.GetPriceAsync(symbol);

            var data = TypeConversion.Conversion<BinancePrice, SymbolPriceTickerResponseDto>(response.Data);

            return data;
        }
    }
}
