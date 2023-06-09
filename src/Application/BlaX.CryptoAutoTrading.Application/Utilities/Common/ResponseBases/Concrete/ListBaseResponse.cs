using System.Net;

namespace BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.Concrete
{
    public class ListBaseResponse<T> : ResponseBase
    {
        public List<T> DataList { get; set; }

        public ListBaseResponse() { }
        public ListBaseResponse(List<T> data = null) => DataList = data;
        public ListBaseResponse(List<T> data, HttpStatusCode statusCode) : this(data) => StatusCode = statusCode;
        public ListBaseResponse(HttpStatusCode statusCode, string errorMessage, List<T> data = null) : this(data, statusCode) => Message = errorMessage;
    }
}
