namespace BlaX.CryptoAutoTrading.Application.DTOs.CommonDTOs.TradingLogDto
{
    public class CreateTradingLogDto
    {
        /// <summary>
        /// Satınalma fiyatı
        /// </summary>
        public double PurchasePrice { get; set; }
        public DateTime PurchaseDate { get; set; }

        /// <summary>
        /// Satış fiyatı
        /// </summary>
        public double SalePrice { get; set; }
        public DateTime SaleDate { get; set; }

        /// <summary>
        /// Kar oranı
        /// </summary>
        public double ProfitRate { get; set; }

        public CreateTradingLogDto()
        {
        }

        public CreateTradingLogDto(double purchasePrice, double salePrice, double profitRate)
        {
            PurchasePrice = purchasePrice;
            SalePrice = salePrice;
            ProfitRate = profitRate;
        }
    }
}
