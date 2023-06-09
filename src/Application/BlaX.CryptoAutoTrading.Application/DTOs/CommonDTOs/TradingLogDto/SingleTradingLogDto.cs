namespace BlaX.CryptoAutoTrading.Application.DTOs.CommonDTOs.TradingLogDto
{
    public class SingleTradingLogDto
    {
        /// <summary>
        /// Satınalma fiyatı
        /// </summary>
        public double PurchasePrice { get; set; }
        /// <summary>
        /// Satış fiyatı
        /// </summary>
        public double SalePrice { get; set; }
        /// <summary>
        /// Kar oranı
        /// </summary>
        public double ProfitRate { get; set; }
    }
}
