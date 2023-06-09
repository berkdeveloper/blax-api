namespace BlaX.CryptoAutoTrading.Application.Exceptions.Common
{
    public class NotFoundErrorException : Exception
    {
        public NotFoundErrorException() : base("Herhangi bir veri bulunamadı!") { }

        public NotFoundErrorException(string message) : base(message) { }

        public NotFoundErrorException(string message, object exception) : base(message, (Exception)exception) { }
    }
}
