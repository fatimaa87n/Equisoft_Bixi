namespace Equisoft_Bixi.WebApi.Dtos
{

    public class StationStatusListDto
    {
        public StationStatusDataDto data { get; set; }
    }
    public class StationStatusDataDto
    {
        public List<StationStatusDto> stations { get; set; }
    }
    public class StationStatusDto
    {
        public string station_id { get; set; }
        public int num_bikes_available { get; set; }
        public int num_ebikes_available { get; set; }
        public int num_docks_available { get; set; }
        public long last_reported { get; set; }
    }
}
