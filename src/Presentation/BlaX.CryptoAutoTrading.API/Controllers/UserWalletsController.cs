using BlaX.CryptoAutoTrading.Application.Abstractions.Services.CommonServices;
using BlaX.CryptoAutoTrading.Application.DTOs.CommonDTOs.BaseObjectDto;
using BlaX.CryptoAutoTrading.Application.DTOs.CommonDTOs.UserWalletDto.Response;
using BlaX.CryptoAutoTrading.Application.Utilities.Attributes;
using BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.Concrete;
using BlaX.CryptoAutoTrading.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BlaX.CryptoAutoTrading.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserWalletsController : BaseController
    {
        readonly IUserWalletService _userWallet;

        public UserWalletsController(IUserWalletService userWallet) => _userWallet = userWallet;

        [UserRoleAuthorization(UserRoleEnum.Admin, UserRoleEnum.User)]
        [HttpGet]
        [ProducesResponseType(typeof(ObjectResponseBase<GetUserWalletDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ObjectResponseBase<GetUserWalletDto>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get([FromQuery] BaseRequestById request)
        {
            var response = await _userWallet.GetUserWallet(request);
            return ActionResponse(response);
        }
    }
}
