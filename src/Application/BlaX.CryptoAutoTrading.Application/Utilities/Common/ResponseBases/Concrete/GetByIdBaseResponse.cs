using BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.Abstract;

namespace BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.Concrete
{
    public class GetByIdBaseResponse<T> : ResponseBase, IObjectResponse<T>
    {
        public T Data { get; }
    }
}
