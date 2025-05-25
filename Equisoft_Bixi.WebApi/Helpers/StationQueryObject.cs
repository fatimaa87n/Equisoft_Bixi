namespace Equisoft_Bixi.WebApi.Helpers
{
    public class StationQueryObject
    {
        public string? Name { get; set; } = null;
        public int? MinAvailableBikes { get; set; } = null;
        public int? MaxAvailableBikes { get; set; } = null;
        public int? MinAvailableDocks { get; set; } = null;
        public int? MaxAvailableDocks { get; set; } = null;
        public DateTime? FromLastReported { get; set; } = null;
        public DateTime? ToLastReported { get; set; } = null;
        public bool hasEbike { get; set; } = false;
        public string SortBy { get; set; } 
        public string SortOrder { get; set; }
    }

    public static class StationSortByValues {
        public const string Name = "stationname";
        public const string AvailableBikesNum = "availablebikes";
        public const string AvailableDockNum = "availabledocks";
        public const string AvailabilityPercentage = "availabilitypercentage";
        public const string ReportDate = "lastupdatedtime";
    }
}
