namespace BlaX.CryptoAutoTrading.ExternalClients.Binance.Common
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// Binance exception class for any errors throw as a result of the misuse of the API or the library.
    /// </summary>
    public class BinanceClientException : BinanceHttpException
    {
        public BinanceClientException()
        : base()
        {
        }

        public BinanceClientException(string message, int code)
        : base(message)
        {
            this.Code = code;
            this.ErrorMessage = message;
        }

        public BinanceClientException(string message, int code, Exception innerException)
        : base(message, innerException)
        {
            this.Code = code;
            this.ErrorMessage = message;
        }

        [JsonPropertyAttribute("code")]
        public int Code { get; set; }

        [JsonPropertyAttribute("msg")]
        public new string ErrorMessage { get; protected set; }
    }
}