using HouseRentingSystem.Services.Users.Models;

namespace HouseRentingSystem.Services.Users
{
    public interface IUserService
    {
        Task<string> UserFullName(string userId);

        Task<IEnumerable<UserServiceModel>> All();
    }
}