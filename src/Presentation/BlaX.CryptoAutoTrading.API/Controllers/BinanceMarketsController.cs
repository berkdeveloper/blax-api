using BlaX.CryptoAutoTrading.Application.Abstractions.Services.BinanceServices;
using BlaX.CryptoAutoTrading.Application.DTOs.BinanceDTOs.BinanceMarketDto.Request;
using BlaX.CryptoAutoTrading.Application.DTOs.BinanceDTOs.BinanceMarketDto.Response;
using BlaX.CryptoAutoTrading.Application.Utilities.Common.RequestBases;
using BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BlaX.CryptoAutoTrading.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BinanceMarketsController : BaseController
    {
        readonly IBinanceMarketService _binanceMarketService;

        public BinanceMarketsController(IBinanceMarketService binanceMarketService) => _binanceMarketService = binanceMarketService;

        [HttpGet("get-latest-price")]
        [ProducesResponseType(typeof(ObjectResponseBase<SymbolPriceTickerResponseDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ObjectResponseBase<SymbolPriceTickerResponseDto>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ObjectResponseBase<SymbolPriceTickerResponseDto>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetSymbolPrice([FromQuery] SymbolRequestBase request)
        {
            var response = await _binanceMarketService.GetSymbolPrice(request);
            return ActionResponse(response);
        }

        [HttpGet("get-old-trades")]
        [ProducesResponseType(typeof(ListBaseResponse<BinanceTradeResponseDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ListBaseResponse<BinanceTradeResponseDto>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ListBaseResponse<BinanceTradeResponseDto>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetOldTradeLookup([FromQuery] SymbolRequestBase request)
        {
            var response = await _binanceMarketService.GetUserTrades(request);
            return ActionResponse(response);
        }

        [HttpGet("get-recent-trades")]
        [ProducesResponseType(typeof(ListBaseResponse<RecentTradeResponseDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ListBaseResponse<RecentTradeResponseDto>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ListBaseResponse<RecentTradeResponseDto>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetRecentTradesList([FromQuery] RecentTradeRequestDto request)
        {
            var response = await _binanceMarketService.GetRecentTradesList(request);
            return ActionResponse(response);
        }
    }
}
