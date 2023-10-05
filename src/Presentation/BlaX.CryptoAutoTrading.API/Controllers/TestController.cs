using BlaX.CryptoAutoTrading.Application.Abstractions.Services.BinanceServices;
using BlaX.CryptoAutoTrading.Application.Abstractions.Services.CommonServices;
using BlaX.CryptoAutoTrading.Application.DTOs;
using BlaX.CryptoAutoTrading.Application.DTOs.CommonDTOs.UserWalletDto.Request;
using BlaX.CryptoAutoTrading.Application.Utilities.Attributes;
using BlaX.CryptoAutoTrading.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlaX.CryptoAutoTrading.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        readonly IBinanceAccountTradeService _spotAccountTrade;
        readonly IUserWalletService _userWalletService;

        public TestController(IBinanceAccountTradeService spotAccountTrade, IUserWalletService userWalletService)
        {
            _spotAccountTrade = spotAccountTrade;
            _userWalletService = userWalletService;
        }

        [UserRoleAuthorization(UserRoleEnum.User)]
        [HttpGet("[action]")]
        public async Task<ActionResult<string>> Test([FromQuery] TestDto request)
        {
            var result = await _spotAccountTrade.TestService(request);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserWalletDto request)
        {
            var result = await _userWalletService.CreateUserWallet(request);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateUserWalletDto request)
        {
            var result = await _userWalletService.UpdateUserWallet(request);
            return Ok(result);
        }
    }
}
