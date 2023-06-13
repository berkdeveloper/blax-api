using BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BlaX.CryptoAutoTrading.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public IActionResult ActionResponse<T>(T value) where T : ResponseBase, new()
        {
            value ??= new T();
            return ActionObject(value.StatusCode, value);
        }

        protected IActionResult ActionObject<T>(HttpStatusCode statusCode, T value) where T : ResponseBase, new()
            => statusCode switch
            {
                HttpStatusCode.Created => StatusCode((int)HttpStatusCode.Created),
                HttpStatusCode.NoContent => StatusCode((int)HttpStatusCode.NoContent),
                HttpStatusCode.Accepted => StatusCode((int)HttpStatusCode.Accepted),
                _ => StatusCode((int)value.StatusCode, value),
            };
    }
}
