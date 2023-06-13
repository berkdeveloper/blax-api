using BlaX.CryptoAutoTrading.Application.DTOs;
using BlaX.CryptoAutoTrading.Application.DTOs.BinanceDTOs.BinanceAccountTradeDto.Request;
using BlaX.CryptoAutoTrading.Application.DTOs.BinanceDTOs.BinanceAccountTradeDto.Response;
using BlaX.CryptoAutoTrading.Application.DTOs.CommonDTOs.UserDto;
using BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.Concrete;

namespace BlaX.CryptoAutoTrading.Application.Abstractions.Services.BinanceServices
{
    public interface IBinanceAccountTradeService
    {
        Task<ObjectResponseBase<CreateNewOrderResponseDto>> CreateNewOrder(CreateNewOrderRequestDto request);
        Task<ListBaseResponse<OrderResponseDto>> GetAllOrders(AllOrdersRequestDto request);
        Task<ObjectResponseBase<OrderResponseDto>> GetOrder(GetOrderRequestDto request);
        Task<AuthorizedUserDto> TestService(TestDto request);
    }
}
