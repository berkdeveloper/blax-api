using BlaX.CryptoAutoTrading.Application.DTOs.CommonDTOs.BaseObjectDto;
using BlaX.CryptoAutoTrading.Application.DTOs.CommonDTOs.UserWalletDto.Request;
using BlaX.CryptoAutoTrading.Application.DTOs.CommonDTOs.UserWalletDto.Response;
using BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases;
using BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.Concrete;

namespace BlaX.CryptoAutoTrading.Application.Abstractions.Services.CommonServices
{
    public interface IUserWalletService
    {
        Task<ResponseBase> CreateUserWallet(CreateUserWalletDto request);
        Task<ResponseBase> UpdateUserWallet(UpdateUserWalletDto request);
        Task<ObjectResponseBase<GetUserWalletDto>> GetUserWallet(BaseRequestById request);
    }
}
