using CryptoExchange.Net.Objects;

namespace BlaX.CryptoAutoTrading.Application.Utilities.Handlers
{
    public static class ObjectValidationHandler
    {
        public static T Handle<T, U>(WebCallResult<U> response, Func<U, T> onSuccess)
        {
            if (response.Data is not null) return onSuccess(response.Data);

            return default;
        }

        public static T HandleList<T, U>(WebCallResult<IEnumerable<U>> response, Func<IEnumerable<U>, T> onSuccess)
        {
            if (response.Data is not null && response.Data.Any() is true) return onSuccess(response.Data);

            return default;
        }
    }
}
