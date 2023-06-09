using BlaX.CryptoAutoTrading.Application.Abstractions.Services.BinanceServices;
using BlaX.CryptoAutoTrading.Application.DTOs.BinanceDTOs.BinanceAccountTradeDto.Request;
using BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases;
using BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.Concrete;
using BlaX.CryptoAutoTrading.Application.ViewModels.BinanceViewModels.AccountTradeViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BlaX.CryptoAutoTrading.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BinanceAccountTradesController : BaseController
    {
        readonly IBinanceAccountTradeService _accountTradeService;

        public BinanceAccountTradesController(IBinanceAccountTradeService accountTradeService) => _accountTradeService = accountTradeService;

        [HttpPost("create-new-order")]
        [ProducesResponseType(typeof(ResponseBase), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ResponseBase), (int)HttpStatusCode.UnprocessableEntity)]
        public async Task<IActionResult> Post([FromQuery] CreateNewOrderRequestDto request)
        {
            var response = await _accountTradeService.CreateNewOrder(request);
            return ActionResponse(response);
        }

        [HttpGet("get-all-orders")]
        [ProducesResponseType(typeof(ObjectResponseBase<AllOrdersViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get([FromQuery] AllOrdersRequestDto request)
        {
            var response = await _accountTradeService.GetAllOrders(request);
            return ActionResponse(response);
        }
    }
}
