using CryptoExchange.Net.Objects;

namespace BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.Abstract
{
    public interface IApiResponse<T>
    {
        bool Success { get; }
        T Data { get; }
    }
}
