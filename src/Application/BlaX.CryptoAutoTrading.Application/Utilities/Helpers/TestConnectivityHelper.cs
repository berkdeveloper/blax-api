using BlaX.CryptoAutoTrading.Application.DTOs.BinanceDTOs.BinanceMarketDto.Response;
using BlaX.CryptoAutoTrading.Application.Utilities.Common;
using BlaX.CryptoAutoTrading.ExternalClients.Binance.Spot;
using Newtonsoft.Json;

namespace BlaX.CryptoAutoTrading.Application.Utilities.Helpers
{
    public class TestConnectivityHelper
    {
        public static bool BinanceMarketSystemConnectionCheck()
        {
            Market market = new();

            var connectionResponse = market.TestConnectivity().Result;

            if (string.IsNullOrEmpty(connectionResponse)) connectionResponse = string.Empty;

            var result = connectionResponse.Equals("{}");

            return result;
        }

        public static bool BinanceWalletSystemConnectionCheck()
        {
            Wallet wallet = new();

            var connectionResponse = wallet.SystemStatus().Result;

            var result = JsonConvert.DeserializeObject<BinanceSystemStatus>(connectionResponse);

            if (result.Status == 0 && result.Message == "normal") return true;

            return default;
        }
    }
}