using Newtonsoft.Json;

namespace BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.Abstract
{
    public interface IResponseStatusCode
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; }
    }
}
