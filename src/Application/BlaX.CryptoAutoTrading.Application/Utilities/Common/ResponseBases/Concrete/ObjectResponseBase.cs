using BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.Abstract;
using System.Net;

namespace BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.Concrete
{
    public class ObjectResponseBase<T> : ResponseBase, IObjectResponse<T> where T : class
    {
        private T _data;

        public virtual T Data
        {
            get => _data;
            set => _data = value;
        }

        public ObjectResponseBase() { }
        public ObjectResponseBase(T data = null) => Data = data;
        public ObjectResponseBase(T data, HttpStatusCode statusCode) : this(data) => StatusCode = statusCode;
        public ObjectResponseBase(HttpStatusCode statusCode, string errorMessage, T data = null) : this(data, statusCode) => Message = errorMessage;
    }
}
