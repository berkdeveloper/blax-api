using BlaX.CryptoAutoTrading.Application.Abstractions;
using BlaX.CryptoAutoTrading.Application.Abstractions.Services.CommonServices;
using BlaX.CryptoAutoTrading.Application.DTOs.CommonDTOs.BaseObjectDto;
using BlaX.CryptoAutoTrading.Application.DTOs.CommonDTOs.UserWalletDto.Request;
using BlaX.CryptoAutoTrading.Application.DTOs.CommonDTOs.UserWalletDto.Response;
using BlaX.CryptoAutoTrading.Application.Exceptions.Common;
using BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases;
using BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.Concrete;
using BlaX.CryptoAutoTrading.Application.Utilities.Helpers;
using BlaX.CryptoAutoTrading.Domain.Core.Constants;
using Serilog;

namespace BlaX.CryptoAutoTrading.Persistence.Services
{
    public class UserWalletService : IUserWalletService
    {
        readonly IUnitOfWork _unitOfWork;
        readonly ILogger _logger;

        public UserWalletService(IUnitOfWork unitOfWork, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ResponseBase> CreateUserWallet(CreateUserWalletDto request)
        {
            request.AuthorizedUserObject = new AuthorizedUserObject();
            #region Validate Entity
            if (request is null || ObjectPropertiesHelper.CheckIsNullObjectProperties(request)) throw new BadRequestErrorException(ExceptionTypes.BadRequest);

            var isExisting = await _unitOfWork.UserWalletReadRepository.AnyAsync(uw => uw.UserId == request.UserId);

            if (isExisting is true) return new ResponseBase(System.Net.HttpStatusCode.Conflict, "Daha önce yatırma işlemi yaptınız."); // TO DO -> (Bir sonraki kayıtlar patlatır burayı)
            #endregion

            var userWallet = new Domain.Entities.UserWallet(request.UserId, request.AmountMoneyDeposited, request.ProfitRate, request.Earning, request.Loss);

            #region Save Point
            await _unitOfWork.UserWalletWriteRepository.CreateAsync(userWallet, request.UserId);

            await _unitOfWork.CommitAsync();
            #endregion

            _logger.Information("Kullanıcı para yatırma işlemi başarıyla gerçekleştirildi", userWallet);

            return new ResponseBase(System.Net.HttpStatusCode.Created);
        }

        public async Task<ObjectResponseBase<GetUserWalletDto>> GetUserWallet(BaseRequestById request)
        {
            var userWallet = await _unitOfWork.UserWalletReadRepository.FindByIdAsync(request.Id);

            if (userWallet is null) return new ObjectResponseBase<GetUserWalletDto>(System.Net.HttpStatusCode.NotFound, "Böyle bir işlem bulunamadı.");

            var responseDto = new GetUserWalletDto(userWallet.AmountMoneyDeposited, userWallet.ProfitRate, userWallet.Earning, userWallet.Loss);

            return new ObjectResponseBase<GetUserWalletDto>(responseDto, System.Net.HttpStatusCode.OK);
        }

        public async Task<ResponseBase> UpdateUserWallet(UpdateUserWalletDto request)
        {
            var userWallet = await _unitOfWork.UserWalletReadRepository.FindByIdAsync(request.Id);

            if (userWallet is null) return new ObjectResponseBase<GetUserWalletDto>(System.Net.HttpStatusCode.NotFound, "Böyle bir işlem bulunamadı.");

            userWallet.Earning = request.Earning;
            userWallet.Loss = request.Loss;
            userWallet.AmountMoneyDeposited = request.AmountMoneyDeposited;
            userWallet.ProfitRate = request.ProfitRate;



            _unitOfWork.UserWalletWriteRepository.Update(userWallet, request.UserId);
            await _unitOfWork.CommitAsync();

            return new ResponseBase(System.Net.HttpStatusCode.NoContent);
        }
    }
}
