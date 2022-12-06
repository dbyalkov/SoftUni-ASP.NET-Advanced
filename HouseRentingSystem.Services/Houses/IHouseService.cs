using HouseRentingSystem.Services.Houses.Models;

namespace HouseRentingSystem.Services.Houses
{
	public interface IHouseService
	{
		Task<IEnumerable<HouseIndexServiceModel>> LastThreeHousesAsync();

		Task<IEnumerable<HouseCategoryServiceModel>> AllCategoriesAsync();

		Task<bool> CategoryExistsAsync(int categoryId);

		Task<int> CreateAsync(
			string title,
			string address,
			string description,
			string imageUrl,
			decimal price,
			int categoryId,
			int agentId);

		Task<HouseQueryServiceModel> AllAsync(
			string? category = null,
			string? searchTerm = null,
			HouseSorting sorting = HouseSorting.Newest,
			int currPage = 1,
			int housesPerPage = 1);

		Task<IEnumerable<string>> AllCategoriesNamesAsync();

		Task<IEnumerable<HouseServiceModel>> AllHousesByAgentIdAsync(int agentId);

		Task<IEnumerable<HouseServiceModel>> AllHousesByUserIdAsync(string userId);

		Task<bool> ExistsAsync(int id);

		Task<HouseDetailsServiceModel> HouseDetailsByIdAsync(int id);

		Task EditAsync(
			int houseId,
			string title,
			string address,
			string description,
			string imageUrl,
			decimal price,
			int categoryId);

		Task<bool> HasAgentWithIdAsync(int houseId, string currUserId);

		Task<int> GetHouseCategoryIdAsync(int houseId);

		Task DeleteAsync(int houseId);

        Task<bool> IsRentedAsync(int id);

        Task<bool> IsRentedByUserWithIdAsync(int houseId, string userId);

		Task RentAsync(int houseId, string userId);

		Task LeaveAsync(int houseId);
    }
}