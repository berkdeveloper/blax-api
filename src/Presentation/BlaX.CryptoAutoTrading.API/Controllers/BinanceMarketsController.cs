using BlaX.CryptoAutoTrading.Application.Abstractions.Services.BinanceServices;
using BlaX.CryptoAutoTrading.Application.Utilities.Common.RequestBases;
using BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.Concrete;
using BlaX.CryptoAutoTrading.Application.ViewModels.BinanceViewModels.MarketViewModels;
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
        [ProducesResponseType(typeof(ObjectResponseBase<SymbolPriceTickerViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ObjectResponseBase<SymbolPriceTickerViewModel>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ObjectResponseBase<SymbolPriceTickerViewModel>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetSymbolPrice([FromQuery] SymbolRequestBase request)
        {
            var response = await _binanceMarketService.GetSymbolPrice(request);
            return ActionResponse(response);
        }

        [HttpGet("get-old-trades")]
        [ProducesResponseType(typeof(ListBaseResponse<OldTradeLookupViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ListBaseResponse<OldTradeLookupViewModel>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ListBaseResponse<OldTradeLookupViewModel>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetOldTradeLookup([FromQuery] SymbolRequestBase request)
        {
            var response = await _binanceMarketService.GetOldTradeLookup(request);
            return ActionResponse(response);
        }

        [HttpGet("get-recent-trades")]
        [ProducesResponseType(typeof(ListBaseResponse<RecentTradesViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ListBaseResponse<RecentTradesViewModel>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ListBaseResponse<RecentTradesViewModel>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetRecentTradesList([FromQuery] SymbolRequestBase request)
        {
            var response = await _binanceMarketService.GetRecentTradesList(request);
            return ActionResponse(response);
        }
    }
}
