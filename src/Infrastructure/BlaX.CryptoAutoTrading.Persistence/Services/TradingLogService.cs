using BlaX.CryptoAutoTrading.Application.Abstractions;
using BlaX.CryptoAutoTrading.Application.Abstractions.Services.CommonServices;
using BlaX.CryptoAutoTrading.Application.DTOs.CommonDTOs.TradingLogDto;
using BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.Concrete;
using BlaX.CryptoAutoTrading.Domain.Core.Constants;
using BlaX.CryptoAutoTrading.Domain.Entities;

namespace BlaX.CryptoAutoTrading.Persistence.Services
{
    public class TradingLogService : ITradingLogService
    {
        readonly IUnitOfWork _unitOfWork;

        public TradingLogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateLogAsync(CreateTradingLogDto request)
        {
            if (request is not null)
            {
                TradingLog tradingLog = new(request.PurchasePrice, request.SalePrice, request.ProfitRate);
                await _unitOfWork.TradingLogWriteRepository.CreateAsync(tradingLog, AdminRoleConst.BerkUserId);
                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<ListBaseResponse<TradingLog>> GetAllLogAsync(int limit = default)
        {
            var tradingLogList = await _unitOfWork.TradingLogReadRepository.FindByLimitAsync(limit);

            return new ListBaseResponse<TradingLog>(tradingLogList, System.Net.HttpStatusCode.OK);
        }

        public async Task<TradingLog> GetLogAsync(Guid id) => await _unitOfWork.TradingLogReadRepository.FindByIdAsync(id);

    }
}
