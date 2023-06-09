using BlaX.CryptoAutoTrading.Domain.Core.Entities.Base;

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

        public UserWallet() { }

        public UserWallet(Guid userId, double amountMoneyDeposited, double profitRate, double earning, double loss)
        {
            UserId = userId;
            AmountMoneyDeposited = amountMoneyDeposited;
            ProfitRate = profitRate;
            Earning = earning;
            Loss = loss;
        }
    }
}
