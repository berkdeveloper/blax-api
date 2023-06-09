using System.ComponentModel;

namespace BlaX.CryptoAutoTrading.Domain.Core.Enums
{
    public enum StatusTypeEnum
    {
        [Description("Bir varlığın/kullanıcının pasif haldeki durumunu temsil eder")]
        Deleted = 0,
        [Description("Bir varlığın/kullanıcının herhangi bir etkileşimi olduğunu belirtir.")]
        Active = 1,
        [Description("Bir varlığın/kullanıcının herhangi bir etkileşimi olmadığını belirtir. (daha önce para yatırma ve son olarak çekme işlemi gerçekleştirenler)")]
        Passive = 2,
    }
}
