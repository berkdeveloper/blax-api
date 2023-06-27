using BlaX.CryptoAutoTrading.Domain.Core.Entities.Base;
using BlaX.CryptoAutoTrading.Domain.Core.Enums;

namespace BlaX.CryptoAutoTrading.Domain.Entities
{
    public class UserWallet : EntityBase
    {
        /// <summary>
        /// Yatıran kullanıcının Id bilgisi
        /// </summary>
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

        public UserWallet() { PaymentStatusType = PaymentStatusTypeEnum.PaymentNotMade; }

        public UserWallet(Guid userId, double amountMoneyDeposited, double profitRate, double earning, double loss, PaymentStatusTypeEnum paymentStatusType)
        {
            UserId = userId;
            AmountMoneyDeposited = amountMoneyDeposited;
            ProfitRate = profitRate;
            Earning = earning;
            Loss = loss;
            PaymentStatusType = paymentStatusType;
        }
    }
}
