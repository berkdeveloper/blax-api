using BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.ComplexTypes;
using BlaX.CryptoAutoTrading.Domain.Core.Enums;

namespace BlaX.CryptoAutoTrading.Application.DTOs.CommonDTOs.UserWalletDto.Request
{
    public class CreateUserWalletDto : BaseRequest
    {
        public Guid UserId { get; set; }

        /// <summary>
        /// Yatırılan Para Miktarı 
        /// </summary>
        public double AmountMoneyDeposited { get; set; }

        /// <summary>
        /// Kar oranı
        /// </summary>
        public double ProfitRate { get; set; }

        /// <summary>
        /// Kazanç
        /// </summary>
        public double Earning { get; set; }

        /// <summary>
        /// Kayıp/Zarar
        /// </summary>
        public double Loss { get; set; }
        public PaymentStatusTypeEnum PaymentStatusType { get; set; }
    }
}
