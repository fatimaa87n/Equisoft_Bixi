namespace Equisoft_Bixi.WebApi.Dtos
{
    public class StationStatusUIDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int AvailableBikesNum { get; set; }
        public int AvailableEBikesNum { get; set; }
        public int AvailableDocksNum { get; set; }
        public DateTime LastReported { get; set; }  
        public double BikeAvailabilityPercentage { get; set; }
    }
}
