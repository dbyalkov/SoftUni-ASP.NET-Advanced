using HouseRentingSystem.Services.Statistics;
using HouseRentingSystem.Services.Statistics.Models;

using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Web.Controllers.Api
{
    [ApiController]
    [Route("api/statistics")]
    public class StatisticsApiController : ControllerBase
    {
        private readonly IStatisticsService statistics;

        public StatisticsApiController(IStatisticsService _statistics)
            => this.statistics = _statistics;

        [HttpGet]
        public async Task<StatisticsServiceModel> GetStatistics()
            => await this.statistics.TotalAsync();
    }
}