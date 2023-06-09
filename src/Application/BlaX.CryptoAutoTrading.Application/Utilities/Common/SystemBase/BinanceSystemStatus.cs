using Newtonsoft.Json;

namespace BlaX.CryptoAutoTrading.Application.Utilities.Common
{
    public class BinanceSystemStatus
    {
        [JsonProperty("status")]
        public int Status { get; set; }
        [JsonProperty("msg")]
        public string Message { get; set; }
    }
}
