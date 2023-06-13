using BlaX.CryptoAutoTrading.Application.Abstractions.Services.BinanceServices;
using BlaX.CryptoAutoTrading.Application.DTOs.BinanceDTOs.BinanceAccountTradeDto.Request;
using BlaX.CryptoAutoTrading.Application.DTOs.BinanceDTOs.BinanceAccountTradeDto.Response;
using BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.Concrete;
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
        readonly IBinanceAccountTradeHandler _accountTradeHandler;

        public BinanceAccountTradesController(IBinanceAccountTradeService accountTradeService, IBinanceAccountTradeHandler accountTradeHandler)
        {
            _accountTradeService = accountTradeService;
            _accountTradeHandler = accountTradeHandler;
        }

        [HttpPost("create-new-order")]
        [ProducesResponseType(typeof(ObjectResponseBase<CreateNewOrderResponseDto>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ObjectResponseBase<CreateNewOrderResponseDto>), (int)HttpStatusCode.UnprocessableEntity)]
        public async Task<IActionResult> Create([FromQuery] CreateNewOrderRequestDto request)
        {
            var response = await _accountTradeService.CreateNewOrder(request);
            return ActionResponse(response);
        }

        [HttpGet("get-order")]
        [ProducesResponseType(typeof(ObjectResponseBase<OrderResponseDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get([FromQuery] GetOrderRequestDto request)
        {
            var response = await _accountTradeService.GetOrder(request);
            return ActionResponse(response);
        }

        [HttpGet("get-all-orders")]
        [ProducesResponseType(typeof(ListBaseResponse<OrderResponseDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll([FromQuery] AllOrdersRequestDto request)
        {
            var response = await _accountTradeService.GetAllOrders(request);
            return ActionResponse(response);
        }
    }
}
