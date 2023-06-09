using BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.Abstract;
using System.Text.Json;

namespace BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.Concrete
{
    public class ErrorResponseBaseDto : IResponseException, IResponseStatusCode, IResponseMessage
    {
        public int StatusCode { get; set; }
        public object Exception { get; set; }
        public string Message { get; set; }

        public ErrorResponseBaseDto(object exception = null) => Exception = exception;
        public ErrorResponseBaseDto(object exception, string message = null) : this(exception) => Message = message;
        public ErrorResponseBaseDto(int statusCode, string message = null, object exception = null) : this(exception, message) => StatusCode = statusCode;

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}