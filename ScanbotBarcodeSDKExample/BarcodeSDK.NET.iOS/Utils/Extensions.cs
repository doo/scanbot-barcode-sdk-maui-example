using System;
namespace BarcodeScannerExample.iOS
{
    public static class Extensions
    {
        public static DateTime ToDateTime(this Foundation.NSDate date)
        {
            DateTime reference = new DateTime(2001, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var utcDateTime = reference.AddSeconds(date.SecondsSinceReferenceDate);
            return utcDateTime.ToLocalTime();
        }

        public static Foundation.NSDate ToNSDate(this DateTime date)
        {
            DateTime reference = new DateTime(2001, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var utcDateTime = date.ToUniversalTime();
            return Foundation.NSDate.FromTimeIntervalSinceReferenceDate((utcDateTime - reference).TotalSeconds);
        }
    }
}
