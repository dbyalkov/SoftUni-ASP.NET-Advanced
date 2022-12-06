using HouseRentingSystem.Services.Data.Common;
using HouseRentingSystem.Services.Data.Entities;
using HouseRentingSystem.Services.Users.Models;

namespace HouseRentingSystem.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IRepository repo;

        public UserService(IRepository _repo)
            => this.repo = _repo;

        public Task<IEnumerable<UserServiceModel>> All()
        {
            throw new NotImplementedException();
        }

        public async Task<string> UserFullName(string userId)
        {
            var user = await repo.GetByIdAsync<User>(userId);

            return $"{user?.FirstName} {user?.LastName}".Trim();
        }
    }
}