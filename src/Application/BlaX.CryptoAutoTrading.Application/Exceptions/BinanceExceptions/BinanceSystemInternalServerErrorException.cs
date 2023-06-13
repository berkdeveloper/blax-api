namespace BlaX.CryptoAutoTrading.Application.Exceptions.BinanceExceptions
{
    public class BinanceSystemInternalServerErrorException : Exception
    {
        public BinanceSystemInternalServerErrorException() : base("Binance Borsa Hizmeti Devre Dışı!")
        {
        }

        public BinanceSystemInternalServerErrorException(string message) : base(message)
        {
        }

        public BinanceSystemInternalServerErrorException(string message, object exception) : base(message, (Exception)exception)
        {
        }
    }
}
