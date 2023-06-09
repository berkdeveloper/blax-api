namespace BlaX.CryptoAutoTrading.Application.Exceptions.Common
{
    public class BadRequestErrorException : Exception
    {
        public BadRequestErrorException() : base("Lütfen ilgili alanları boş bırakmayınız ve doğru bir biçimde doldurunuz.") { }

        public BadRequestErrorException(string message) : base(message) { }

        public BadRequestErrorException(string message, object exception) : base(message, (Exception)exception) { }
    }
}
