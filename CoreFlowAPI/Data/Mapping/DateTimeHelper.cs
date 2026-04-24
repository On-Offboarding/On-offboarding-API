namespace CoreFlowAPI.Data.Mapping
{
    internal static class DateTimeHelper
    {
        private static readonly TimeZoneInfo SwedenTz =
            TimeZoneInfo.FindSystemTimeZoneById("Europe/Stockholm");

        public static DateTime ToSwedishTime(DateTime utc) =>
            TimeZoneInfo.ConvertTimeFromUtc(DateTime.SpecifyKind(utc, DateTimeKind.Utc), SwedenTz);

        public static DateTime? ToSwedishTime(DateTime? utc) =>
            utc.HasValue ? ToSwedishTime(utc.Value) : null;
    }
}
