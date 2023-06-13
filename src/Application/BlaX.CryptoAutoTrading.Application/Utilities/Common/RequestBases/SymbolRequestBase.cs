namespace BlaX.CryptoAutoTrading.Application.Utilities.Common.RequestBases
{
    public class SymbolRequestBase
    {
        /// <summary>
        /// Satın alınan Coin her daim Sol kısma yazılır.
        /// </summary>
        public string Symbol { get; set; }

        public SymbolRequestBase() { Symbol = "USDTTRY"; }

        public SymbolRequestBase(string symbol) => Symbol = symbol;
    }
}
