namespace BlaX.CryptoAutoTrading.ExternalClients.Binance.Common
{
    /// <summary>
    /// Interface for signing payloads.
    /// </summary>
    public interface IBinanceSignatureService
    {
        string Sign(string payload);
    }
}