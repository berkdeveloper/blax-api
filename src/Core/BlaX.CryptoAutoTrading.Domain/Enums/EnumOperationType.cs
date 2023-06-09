using System.ComponentModel;

namespace BlaX.CryptoAutoTrading.Domain.Enums
{
    public enum EnumOperationType
    {
        [Description("Oluşturma")]
        Create = 1,
        [Description("Güncelleme")]
        Update = 2,
        [Description("Silme")]
        Delete = 3,
        [Description("Görüntüleme")]
        View = 4,
        [Description("Kullanıcı Girişi")]
        Login = 5,
        [Description("Doğrulama")]
        Validate = 6,
        [Description("Kayıt Olma")]
        Register = 7,
        [Description("Şifre Yenileme")]
        ResetPassword = 8,
        [Description("Şifre Değiştirme")]
        ChangePassword = 9
    }
}
