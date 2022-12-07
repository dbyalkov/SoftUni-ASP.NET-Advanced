using AutoMapper;
using AutoMapper.QueryableExtensions;

using HouseRentingSystem.Services.Data.Common;
using HouseRentingSystem.Services.Data.Entities;
using HouseRentingSystem.Services.Rents.Models;

using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem.Services.Rents
{
    public class RentService : IRentService
    {
        private readonly IRepository repo;
        private readonly IMapper mapper;

        public RentService(
            IRepository _repo,
            IMapper _mapper)
        {
            this.repo = _repo;
            this.mapper = _mapper;
        }

        public async Task<IEnumerable<RentServiceModel>> All()
            => await this.repo.AllReadonly<House>()
            .Include(h => h.Agent.User)
            .Include(h => h.Renter)
            .Where(h => h.RenterId != null)
            .ProjectTo<RentServiceModel>(this.mapper.ConfigurationProvider)
            .ToListAsync();
    }
}