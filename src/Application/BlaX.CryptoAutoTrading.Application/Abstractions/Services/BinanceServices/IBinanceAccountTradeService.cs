using BlaX.CryptoAutoTrading.Application.DTOs;
using BlaX.CryptoAutoTrading.Application.DTOs.BinanceDTOs.BinanceAccountTradeDto.Request;
using BlaX.CryptoAutoTrading.Application.DTOs.CommonDTOs.UserDto;
using BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases;
using BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.Concrete;
using BlaX.CryptoAutoTrading.Application.ViewModels.BinanceViewModels.AccountTradeViewModels;

namespace BlaX.CryptoAutoTrading.Application.Abstractions.Services.BinanceServices
{
    public interface IBinanceAccountTradeService
    {
        Task<ResponseBase> CreateNewOrder(CreateNewOrderRequestDto request);
        Task<ObjectResponseBase<AllOrdersViewModel>> GetAllOrders(AllOrdersRequestDto request);
        Task<AuthorizedUserDto> TestService(TestDto request);
    }
}
