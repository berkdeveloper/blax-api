using System.ComponentModel;

namespace BlaX.CryptoAutoTrading.Domain.Core.Enums
{
    public enum PaymentStatusTypeEnum
    {
        [Description("Ödeme Yapılmadı")]
        PaymentNotMade,
        [Description("Ödeme Tamamlandı/Yapıldı")]
        PaymentCompleted,
        [Description("Onay Bekleniyor")]
        PaymentPendingApproval,
        [Description("Ödeme İptal Edildi")]
        PaymentCancelled,
        [Description("Ödeme Başarısız")]
        PaymentFailed,
        [Description("İade Edildi")]
        PaymentRefunded
    }
}
