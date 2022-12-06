using HouseRentingSystem.Services.Data.Entities;
using HouseRentingSystem.Services.Data.Common;

using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem.Services.Agents
{
    public class AgentService : IAgentService
    {
        private readonly IRepository repo;

        public AgentService(IRepository _repo)
        {
            this.repo = _repo;
        }

        public async Task CreateAsync(string userId, string phoneNumber)
        {
            var agent = new Agent()
            {
                UserId = userId,
                PhoneNumber = phoneNumber
            };

            await this.repo.AddAsync(agent);
            await this.repo.SaveChangesAsync();
        }

        public async Task<bool> ExistsByIdAsync(string userId)
            => await this.repo.All<Agent>().AnyAsync(a => a.UserId == userId);

        public async Task<int> GetAgentIdAsync(string userId)
            => (await this.repo.AllReadonly<Agent>().FirstOrDefaultAsync(a => a.UserId == userId))?.Id ?? 0;

        public async Task<bool> UserHasRentsAsync(string userId)
            => await this.repo.All<House>().AnyAsync(h => h.RenterId == userId);

        public async Task<bool> UserWithPhoneNumberExistsAsync(string phoneNumber)
            => await this.repo.All<Agent>().AnyAsync(a => a.PhoneNumber == phoneNumber);
    }
}