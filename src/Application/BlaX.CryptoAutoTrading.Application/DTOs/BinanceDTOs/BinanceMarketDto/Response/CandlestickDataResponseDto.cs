namespace BlaX.CryptoAutoTrading.Application.DTOs.BinanceDTOs.BinanceMarketDto.Response
{
    public class CandlestickDataResponseDto
    {
        /// <summary>
        /// The time this candlestick opened
        /// (Açılış zamanı)
        /// </summary>
        public DateTime OpenTime { get; set; }

        /// <summary>
        /// The price at which this candlestick opened
        /// (Açılış fiyatı)
        /// </summary>
        public decimal OpenPrice { get; set; }

        /// <summary>
        /// The highest price in this candlestick
        /// (En yüksek fiyat)
        /// </summary>
        public decimal HighPrice { get; set; }

        /// <summary>
        /// The lowest price in this candlestick
        /// (En düşük fiyat)
        /// </summary>
        public decimal LowPrice { get; set; }

        /// <summary>
        /// The price at which this candlestick closed
        /// (Kapanış fiyatı)
        /// </summary>
        public decimal ClosePrice { get; set; }

        /// <summary>
        /// The volume traded during this candlestick
        /// (İşlem hacmi)
        /// </summary>
        public decimal Volume { get; set; }

        /// <summary>
        /// The close time of this candlestick
        /// (Kapanış zamanı)
        /// </summary>
        public DateTime CloseTime { get; set; }

        /// <summary>
        /// The volume traded during this candlestick in the asset form
        /// (Kote edilen işlem hacmi)
        /// </summary>
        public decimal QuoteVolume { get; set; }

        /// <summary>
        /// The amount of trades in this candlestick
        /// (İşlem sayısı)
        /// </summary>
        public int TradeCount { get; set; }

        /// <summary>
        /// Taker buy base asset volume
        /// (Alıcı tarafından yapılan işlem hacmi)
        /// </summary>
        public decimal TakerBuyBaseVolume { get; set; }

        /// <summary>
        /// Taker buy quote asset volume
        /// (Alıcı tarafından yapılan işlem kote edilen hacmi)
        /// </summary>
        public decimal TakerBuyQuoteVolume { get; set; }
    }
}
