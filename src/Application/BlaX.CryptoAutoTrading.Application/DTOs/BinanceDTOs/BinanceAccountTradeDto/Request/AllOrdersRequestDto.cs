﻿using BlaX.CryptoAutoTrading.Application.Utilities.Common.RequestBases;

namespace BlaX.CryptoAutoTrading.Application.DTOs.BinanceDTOs.BinanceAccountTradeDto.Request
{
    public class AllOrdersRequestDto : SymbolRequestBase
    {
        public AllOrdersRequestDto() { }

        public AllOrdersRequestDto(string symbol) : base(symbol) { }
    }
}