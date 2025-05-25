namespace Equisoft_Bixi.WebApi.Dtos
{

    public class StationInfoListDto
    {
        public StationInfoDataDto data { get; set; }
    }

    public class StationInfoDataDto
    {
        public List<StationInfoDto> stations { get; set; }
    }
    public class StationInfoDto
    {
        public string station_id { get; set; }
        public string name { get; set; }
        public Double lat { get; set; }
        public Double lon { get; set; }
        public int capacity{ get; set; }
    }
}
