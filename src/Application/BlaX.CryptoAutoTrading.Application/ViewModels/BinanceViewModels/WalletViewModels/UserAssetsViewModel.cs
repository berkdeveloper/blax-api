using Newtonsoft.Json;

namespace BlaX.CryptoAutoTrading.Application.ViewModels.BinanceViewModels.WalletViewModels
{
    public class UserAssetsViewModel
    {
        [JsonProperty("asset")]
        public string Asset { get; set; }
        [JsonProperty("free")]
        public string Free { get; set; }
        [JsonProperty("locked")]
        public string Locked { get; set; }
        [JsonProperty("freeze")]
        public string Freeze { get; set; }
        [JsonProperty("withdrawing")]
        public string Withdrawing { get; set; }
        [JsonProperty("ipoable")]
        public string Ipoable { get; set; }
        [JsonProperty("btcValuation")]
        public string BtcValuation { get; set; }
    }
}
