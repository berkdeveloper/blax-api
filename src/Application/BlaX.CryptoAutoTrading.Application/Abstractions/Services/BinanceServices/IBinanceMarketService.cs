using BlaX.CryptoAutoTrading.Application.Utilities.Common.RequestBases;
using BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.Concrete;
using BlaX.CryptoAutoTrading.Application.ViewModels.BinanceViewModels.MarketViewModels;

namespace BlaX.CryptoAutoTrading.Application.Abstractions.Services.BinanceServices
{
    public interface IBinanceMarketService
    {
        Task<ObjectResponseBase<SymbolPriceTickerViewModel>> GetSymbolPrice(SymbolRequestBase request);
        Task<ListBaseResponse<OldTradeLookupViewModel>> GetOldTradeLookup(SymbolRequestBase request);
        Task<ListBaseResponse<RecentTradesViewModel>> GetRecentTradesList(SymbolRequestBase request);
    }
}
