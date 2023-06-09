using BlaX.CryptoAutoTrading.Application.Abstractions.Services.BinanceServices;
using BlaX.CryptoAutoTrading.Application.DTOs.BinanceDTOs.BinanceWalletDto.Request;
using BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.Concrete;
using BlaX.CryptoAutoTrading.Application.ViewModels.BinanceViewModels.MarketViewModels;
using BlaX.CryptoAutoTrading.Application.ViewModels.BinanceViewModels.WalletViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BlaX.CryptoAutoTrading.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BinanceWalletsController : BaseController
    {
        readonly IBinanceWalletService _binanceWalletService;

        public BinanceWalletsController(IBinanceWalletService spotAccountTradeService) => _binanceWalletService = spotAccountTradeService;

        [HttpGet("get-user-assets")]
        [ProducesResponseType(typeof(ObjectResponseBase<SymbolPriceTickerViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get([FromQuery] UserAssetSymbolRequestDto userAssetSymbolRequestDto)
        {
            var response = await _binanceWalletService.GetUserAsset(userAssetSymbolRequestDto);
            return ActionResponse(response);
        }
    }
}
