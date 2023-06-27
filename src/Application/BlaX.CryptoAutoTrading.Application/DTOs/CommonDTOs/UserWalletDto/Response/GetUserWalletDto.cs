using BlaX.CryptoAutoTrading.Domain.Core.Enums;

namespace BlaX.CryptoAutoTrading.Application.DTOs.CommonDTOs.UserWalletDto.Response
{
    public class GetUserWalletDto
    {
        public double AmountMoneyDeposited { get; set; }
        public double ProfitRate { get; set; }
        public double Earning { get; set; }
        public double Loss { get; set; }
        public PaymentStatusTypeEnum PaymentStatusType { get; set; }

        public GetUserWalletDto() { }

        public GetUserWalletDto(double amountMoneyDeposited, double profitRate, double earning, double loss, PaymentStatusTypeEnum paymentStatusType)
        {
            AmountMoneyDeposited = amountMoneyDeposited;
            ProfitRate = profitRate;
            Earning = earning;
            Loss = loss;
            PaymentStatusType = paymentStatusType;
        }
    }
}
