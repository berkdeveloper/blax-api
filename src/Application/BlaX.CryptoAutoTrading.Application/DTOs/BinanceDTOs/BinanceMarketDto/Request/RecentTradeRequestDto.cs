﻿using BlaX.CryptoAutoTrading.Application.Utilities.Common.RequestBases;

namespace BlaX.CryptoAutoTrading.Application.DTOs.BinanceDTOs.BinanceMarketDto.Request
{
    public class RecentTradeRequestDto : SymbolRequestBase
    {
        public int? Limit { get; set; }
    }
}
