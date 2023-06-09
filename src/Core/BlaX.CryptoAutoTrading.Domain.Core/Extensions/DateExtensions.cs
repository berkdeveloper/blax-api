using System.Runtime.InteropServices;

namespace BlaX.CryptoAutoTrading.Domain.Core.Extensions
{
    public static class DateExtensions
    {
        public static DateTime GetDateNow()
        {
            try
            {
                var timeZone = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Europe/Istanbul" : "Turkey Standard Time";

                var info = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
                DateTimeOffset localServerTime = DateTimeOffset.Now;
                DateTimeOffset localTime = TimeZoneInfo.ConvertTime(localServerTime, info);

                return localTime.DateTime;
            }
            catch { return DateTime.UtcNow; }
        }
    }
}
