using Equisoft_Bixi.WebApi.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Equisoft_Bixi.WebApi.Services
{
    public class StationService
    {
        private readonly HttpClient _httpClient;
        private readonly string _stationApiUrl;
        private readonly ILogger<StationService> _logger;

        public StationService(HttpClient httpClient, IConfiguration configuration, ILogger<StationService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _stationApiUrl = configuration.GetValue<string>("StationApiUrl");
        }

        public async Task<List<StationStatusUIDto>> GetStationData()
        {
            try
            {
                // Fetching data from the API endpoints
                var infoApiData = await _httpClient.GetStringAsync($"{_stationApiUrl}/station_information.json");
                var statusApiData = await _httpClient.GetStringAsync($"{_stationApiUrl}/station_status.json");
                _logger.LogInformation("Successfully fetched stations");
                var infoList = JsonConvert.DeserializeObject<StationInfoListDto>(infoApiData);
                var statusList = JsonConvert.DeserializeObject<StationStatusListDto>(statusApiData);
                if (infoList?.data?.stations == null || statusList?.data?.stations == null)
                {
                    _logger.LogError("Invalid data received from the API");
                    throw new Exception("Invalid data received from the API");
                }
                else
                {

                    var stations = from info in infoList.data.stations
                                   join status in statusList.data.stations on info.station_id equals status.station_id
                                   select new StationStatusUIDto
                                   {

                                       Id = info.station_id,
                                       Name = info.name,
                                       AvailableBikesNum = status.num_bikes_available,
                                       AvailableEBikesNum = status.num_ebikes_available,
                                       AvailableDocksNum = status.num_docks_available,
                                       LastReported = DateTimeOffset.FromUnixTimeSeconds(status.last_reported).LocalDateTime,
                                       BikeAvailabilityPercentage = CalculatePercentage(status, info.capacity)
                                   };

                    return stations.ToList();
                }
            }
            catch (Exception ex)
            {
                // Log the exception 
                    _logger.LogError("Invalid data received from the API");
                throw new Exception("Error fetching station data", ex);
            }
        }

        private double CalculatePercentage(StationStatusDto status , int capacity)
        {
            // calculated based on station capacity. we can also calcuate percentage according sum of available bikes and avalable docks
            return capacity == 0 ? 0 : Math.Round(100.0 * status.num_bikes_available / capacity, 2);
        }

    }
}
