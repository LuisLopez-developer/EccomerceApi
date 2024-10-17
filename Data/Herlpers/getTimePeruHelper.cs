namespace Data.Herlpers
{
    public static class getTimePeruHelper
    {
        public static DateTime GetCurrentTimeInPeru()
        {
            var peruTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
            var currentTimePeru = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, peruTimeZone);
            return currentTimePeru;
        }
    }
}
