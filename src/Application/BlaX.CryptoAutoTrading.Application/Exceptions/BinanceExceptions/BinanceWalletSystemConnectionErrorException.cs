namespace BlaX.CryptoAutoTrading.Application.Exceptions.BinanceExceptions
{
    public class BinanceWalletSystemConnectionErrorException : Exception
    {
        public BinanceWalletSystemConnectionErrorException() : base("Cüzdan hizmeti devre dışı!")
        {
        }

        public BinanceWalletSystemConnectionErrorException(string message) : base(message)
        {
        }

        public BinanceWalletSystemConnectionErrorException(string message, object exception) : base(message, (Exception)exception)
        {
        }
    }
}
