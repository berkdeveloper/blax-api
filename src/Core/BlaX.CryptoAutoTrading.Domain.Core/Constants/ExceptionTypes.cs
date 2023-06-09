namespace BlaX.CryptoAutoTrading.Domain.Core.Constants
{
    public static class ExceptionTypes
    {
        public const string NotFound = "Veri Bulunamadı!";
        public const string BadRequest = "Eksik veya hatalı istek!";
        public const string InternalServerError = "Sunucu hatası!";
        public const string Conflict = "Sunucudaki bir çakışma nedeniyle istek gerçekleştirilemedi.";
        public const string UnprocessableEntity = "İstek İşlenemedi.";
    }
}