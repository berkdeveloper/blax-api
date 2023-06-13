namespace BlaX.CryptoAutoTrading.Application.DTOs.BinanceDTOs.BinanceMarketDto.Response
{
    public class RecentTradeResponseDto
    {
        /// <summary>
        /// The id of the trade
        /// </summary>
        public long OrderId { get; set; }

        /// <summary>
        /// The price of the trade
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// The base quantity of the trade
        /// </summary>
        public decimal BaseQuantity { get; set; }
        /// <summary>
        /// The quote quantity of the trade
        /// </summary>
        public decimal QuoteQuantity { get; set; }

        /// <summary>
        /// The timestamp of the trade
        /// </summary>
        public DateTime TradeTime { get; set; }

        /// <summary>
        /// Whether the buyer is maker
        /// </summary>
        public bool BuyerIsMaker { get; set; }

        /// <summary>
        /// Whether the trade was made at the best match
        /// </summary>
        public bool IsBestMatch { get; set; }
    }
}
