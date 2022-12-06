using HouseRentingSystem.Services.Data.Entities;
using HouseRentingSystem.Services.Data.Common;
using HouseRentingSystem.Services.Statistics.Models;

using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem.Services.Statistics
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IRepository repo;

        public StatisticsService(IRepository _repo)
        {
            this.repo = _repo;
        }

        public async Task<StatisticsServiceModel> TotalAsync()
        {
            var totalHouses = await this.repo.AllReadonly<House>()
                .CountAsync(h => h.IsActive);
            var totalRents = await this.repo.AllReadonly<House>()
                .Where(h => h.RenterId != null)
                .CountAsync(h => h.IsActive && h.RenterId != null);

            return new StatisticsServiceModel()
            {
                TotalHouses = totalHouses,
                TotalRents = totalRents
            };
        }
    }
}