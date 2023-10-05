using CryptoExchange.Net.Objects;

namespace BlaX.CryptoAutoTrading.Application.Utilities.Handlers
{
    public static class ApiResponseHandler
    {
        public static bool Handle<T>(WebCallResult<T> response) => response.Success is true && response.Data is not null;

        public static bool HandleList<T>(WebCallResult<IEnumerable<T>> response) => response.Success is true && response.Data is not null;
    }
}
