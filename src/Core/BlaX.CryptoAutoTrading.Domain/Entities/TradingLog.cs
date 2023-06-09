using BlaX.CryptoAutoTrading.Domain.Core.Entities.Base;
using BlaX.CryptoAutoTrading.Domain.Core.Extensions;

namespace BlaX.CryptoAutoTrading.Domain.Entities
{
    public class TradingLog : EntityBase
    {
        /// <summary>
        /// Satınalma fiyatı
        /// </summary>
        public double PurchasePrice { get; set; }
        public DateTime? PurchaseDate { get; set; }

        /// <summary>
        /// Satış fiyatı
        /// </summary>
        public double SalePrice { get; set; }
        public DateTime? SaleDate { get; set; }

        /// <summary>
        /// Kar oranı
        /// </summary>
        public double ProfitRate { get; set; }

        public TradingLog()
        {
            PurchaseDate = DateExtensions.GetDateNow();
            SaleDate = DateExtensions.GetDateNow();
        }

        public TradingLog(double purchasePrice, double salePrice, double profitRate)
        {
            PurchasePrice = purchasePrice;
            SalePrice = salePrice;
            ProfitRate = profitRate;
        }
    }
}
