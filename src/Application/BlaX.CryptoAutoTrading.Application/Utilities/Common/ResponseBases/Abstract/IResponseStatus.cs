using Newtonsoft.Json;
using System.Net;

namespace BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.Abstract
{
    public interface IResponseStatus
    {
        [JsonProperty("statusCode")]
        public HttpStatusCode StatusCode { get; }
    }
}
