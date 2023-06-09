namespace BlaX.CryptoAutoTrading.Application.DTOs.BinanceDTOs.BaseDTOs
{
    public class RecentTradesBaseDto
    {
        public int Id { get; set; }
        public string Price { get; set; }
        public string Qty { get; set; }
        public string QuoteQty { get; set; }
        public object Time { get; set; }
        public bool IsBuyerMaker { get; set; }
        public bool IsBestMatch { get; set; }
    }
}
