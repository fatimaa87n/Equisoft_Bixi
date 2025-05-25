using Equisoft_Bixi.WebApi.Helpers;
using Equisoft_Bixi.WebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Equisoft_Bixi.WebApi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class StationController : ControllerBase
    {
        private readonly StationService _stationService;
        public StationController(StationService stationService)
        {
            _stationService = stationService;
        }
        [HttpGet("Stations")]
        public async Task<IActionResult> Get([FromQuery] StationQueryObject query)
        {
            try
            {
                var data = await _stationService.GetStationData();
                var queryableData = data.AsQueryable();
                
                // Apply filters based on the query object
                if (!string.IsNullOrEmpty(query.Name))
                {
                    queryableData = queryableData.Where(x => x.Name.Contains(query.Name, StringComparison.OrdinalIgnoreCase));
                }
                if (query.MinAvailableBikes.HasValue)
                {
                    queryableData = queryableData.Where(x => x.AvailableBikesNum >= query.MinAvailableBikes.Value);
                }
                if (query.MaxAvailableBikes.HasValue)
                {
                    queryableData = queryableData.Where(x => x.AvailableBikesNum <= query.MaxAvailableBikes.Value);
                }
                if (query.MinAvailableDocks.HasValue)
                {
                    queryableData = queryableData.Where(x => x.AvailableDocksNum >= query.MinAvailableDocks.Value);
                }
                if (query.MaxAvailableDocks.HasValue)
                {
                    queryableData = queryableData.Where(x => x.AvailableDocksNum <= query.MaxAvailableDocks.Value);
                }
                if (query.FromLastReported.HasValue)
                {
                    queryableData = queryableData.Where(x => x.LastReported >= query.FromLastReported.Value);
                }
                if (query.ToLastReported.HasValue)
                {
                    queryableData = queryableData.Where(x => x.LastReported <= query.ToLastReported.Value);
                }
                if (query.hasEbike)
                {
                    queryableData = queryableData.Where(x => x.AvailableEBikesNum > 0);
                }
                // Sorting logic
                queryableData = query.SortBy.ToLower() switch
                {
                    StationSortByValues.Name => query.SortOrder == "desc"
                        ? queryableData.OrderByDescending(s => s.Name)
                        : queryableData.OrderBy(s => s.Name),

                    StationSortByValues.AvailableDockNum => query.SortOrder == "desc"
                        ? queryableData.OrderByDescending(s => s.AvailableDocksNum)
                        : queryableData.OrderBy(s => s.AvailableDocksNum),
                    StationSortByValues.AvailableBikesNum => query.SortOrder == "desc"
                   ? queryableData.OrderByDescending(s => s.AvailableBikesNum)
                   : queryableData.OrderBy(s => s.AvailableBikesNum),
                    StationSortByValues.AvailabilityPercentage => query.SortOrder == "desc"
                        ? queryableData.OrderByDescending(s => s.BikeAvailabilityPercentage)
                        : queryableData.OrderBy(s => s.BikeAvailabilityPercentage),
                    StationSortByValues.ReportDate => query.SortOrder == "desc"
                   ? queryableData.OrderByDescending(s => s.LastReported)
                   : queryableData.OrderBy(s => s.LastReported),

                    _ => queryableData.OrderBy(s => s.Name)  // sorting default by station name
                };

                return Ok(queryableData.ToList());
            }
            catch (Exception ex)
            {
                // Log the exception 
                return StatusCode(StatusCodes.Status500InternalServerError, "Error fetching station data");
            }
        }
    }
}
