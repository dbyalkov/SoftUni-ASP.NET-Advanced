using AutoMapper;
using AutoMapper.QueryableExtensions;

using HouseRentingSystem.Services.Data.Common;
using HouseRentingSystem.Services.Data.Entities;
using HouseRentingSystem.Services.Users.Models;

using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IRepository repo;
        private readonly IMapper mapper;

        public UserService(
            IRepository _repo,
            IMapper _mapper)
        {
            this.repo = _repo;
            this.mapper = _mapper;
        }

        public async Task<IEnumerable<UserServiceModel>> All()
        {
            List<UserServiceModel> result;

            result = await repo.AllReadonly<Agent>()
                .Include(a => a.User)
                .ProjectTo<UserServiceModel>(this.mapper.ConfigurationProvider)
                .ToListAsync();

            string[] agentIds = result.Select(a => a.UserId).ToArray();

            result.AddRange(await repo.AllReadonly<User>()
                .Where(u => !agentIds.Contains(u.Id))
                .ProjectTo<UserServiceModel>(this.mapper.ConfigurationProvider)
                .ToListAsync());

            return result;
        }

        public async Task<string> UserFullName(string userId)
        {
            var user = await repo.GetByIdAsync<User>(userId);

            return $"{user?.FirstName} {user?.LastName}".Trim();
        }
    }
}