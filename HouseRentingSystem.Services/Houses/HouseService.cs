using AutoMapper;
using AutoMapper.QueryableExtensions;

using HouseRentingSystem.Services.Agents.Models;
using HouseRentingSystem.Services.Data.Common;
using HouseRentingSystem.Services.Data.Entities;
using HouseRentingSystem.Services.Houses.Models;

using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem.Services.Houses
{
	public class HouseService : IHouseService
	{
		private readonly IRepository repo;
		private readonly IMapper mapper;

		public HouseService(
			IRepository _repo,
            IMapper _mapper)
		{
			this.repo = _repo;
			this.mapper = _mapper;
		}

		public async Task<HouseQueryServiceModel> AllAsync(
			string? category = null,
			string? searchTerm = null,
			HouseSorting sorting = HouseSorting.Newest,
			int currPage = 1,
			int housesPerPage = 1)
		{
			var result = new HouseQueryServiceModel();
			var houses = repo.AllReadonly<House>()
				.Where(h => h.IsActive);

			if (string.IsNullOrEmpty(category) == false)
			{
				houses = houses
					.Where(h => h.Category.Name == category);
			}

			if (string.IsNullOrEmpty(searchTerm) == false)
			{
				searchTerm = $"%{searchTerm.ToLower()}%";

				houses = houses
					.Where(h => EF.Functions.Like(h.Title.ToLower(), searchTerm) ||
						EF.Functions.Like(h.Address.ToLower(), searchTerm) ||
						EF.Functions.Like(h.Description.ToLower(), searchTerm));
			}

			houses = sorting switch
			{
				HouseSorting.Price => houses
					.OrderBy(h => h.PricePerMonth),
				HouseSorting.NotRentedFirst => houses
					.OrderBy(h => h.RenterId),
				_ => houses.OrderByDescending(h => h.Id)
			};

			result.Houses = await houses
				.Skip((currPage - 1) * housesPerPage)
				.Take(housesPerPage)
				.ProjectTo<HouseServiceModel>(this.mapper.ConfigurationProvider)
				.ToListAsync();

			result.TotalHousesCount = await houses.CountAsync();

			return result;
		}

		public async Task<IEnumerable<HouseCategoryServiceModel>> AllCategoriesAsync()
			=> await this.repo.AllReadonly<Category>()
				.ProjectTo<HouseCategoryServiceModel>(this.mapper.ConfigurationProvider)
				.ToListAsync();

		public async Task<IEnumerable<string>> AllCategoriesNamesAsync()
			=> await this.repo.AllReadonly<Category>()
				.Select(c => c.Name)
				.Distinct()
				.ToListAsync();

		public async Task<IEnumerable<HouseServiceModel>> AllHousesByAgentIdAsync(int agentId)
			=> await this.repo.AllReadonly<House>()
				.Where(h => h.IsActive)
				.Where(h => h.AgentId == agentId)
				.ProjectTo<HouseServiceModel>(this.mapper.ConfigurationProvider)
				.ToListAsync();

		public async Task<IEnumerable<HouseServiceModel>> AllHousesByUserIdAsync(string userId)
			=> await this.repo.AllReadonly<House>()
				.Where(h => h.RenterId == userId)
				.Where(h => h.IsActive)
				.ProjectTo<HouseServiceModel>(this.mapper.ConfigurationProvider)
				.ToListAsync();

		public async Task<bool> CategoryExistsAsync(int categoryId)
			=> await this.repo.AllReadonly<Category>()
				.AnyAsync(c => c.Id == categoryId);

		public async Task<int> CreateAsync(
			string title,
			string address,
			string description,
			string imageUrl,
			decimal price,
			int categoryId,
			int agentId)
		{
			var house = new House()
			{
				Title = title,
				Address = address,
				Description = description,
				ImageUrl = imageUrl,
				PricePerMonth = price,
				CategoryId = categoryId,
				AgentId = agentId
			};

			await this.repo.AddAsync(house);
			await this.repo.SaveChangesAsync();

			return house.Id;
		}

		public async Task DeleteAsync(int houseId)
		{
			var house = await this.repo.GetByIdAsync<House>(houseId);
			house.IsActive = false;

			await this.repo.SaveChangesAsync();
		}

		public async Task EditAsync(
			int houseId,
			string title,
			string address,
			string description,
			string imageUrl,
			decimal price,
			int categoryId)
		{
			var house = await this.repo.GetByIdAsync<House>(houseId);

			house.Title = title;
			house.Address = address;
			house.Description = description;
			house.ImageUrl = imageUrl;
			house.PricePerMonth = price;
			house.CategoryId = categoryId;

			await this.repo.SaveChangesAsync();
		}

		public async Task<bool> ExistsAsync(int id)
			=> await this.repo.AllReadonly<House>()
				.AnyAsync(h => h.Id == id && h.IsActive);

		public async Task<int> GetHouseCategoryIdAsync(int houseId)
			=> (await this.repo.GetByIdAsync<House>(houseId)).CategoryId;

		public async Task<bool> HasAgentWithIdAsync(int houseId, string currUserId)
		{
			bool result = false;
			var house = await this.repo.AllReadonly<House>()
				.Where(h => h.IsActive)
				.Where(h => h.Id == houseId)
				.Include(h => h.Agent)
				.FirstOrDefaultAsync();

			if (house?.Agent != null && house.Agent.UserId == currUserId)
			{
				result = true;
			}

			return result;
		}

		public async Task<HouseDetailsServiceModel> HouseDetailsByIdAsync(int id)
			=> await this.repo.AllReadonly<House>()
				.Where(h => h.IsActive)
				.Where(h => h.Id == id)
				.Select(h => new HouseDetailsServiceModel()
				{
					Id = h.Id,
					Title = h.Title,
					Address = h.Address,
					Description = h.Description,
					ImageUrl = h.ImageUrl,
					PricePerMonth = h.PricePerMonth,
					IsRented = h.RenterId != null,
					Category = h.Category.Name,
					Agent = new AgentServiceModel()
					{
						FullName = $"{h.Agent.User.FirstName} {h.Agent.User.LastName}".Trim(),
						PhoneNumber = h.Agent.PhoneNumber,
						Email = h.Agent.User.Email
					}
				})
				.FirstAsync();

		public async Task<bool> IsRentedAsync(int id)
			=> (await this.repo.GetByIdAsync<House>(id)).RenterId != null;

		public async Task<bool> IsRentedByUserWithIdAsync(int houseId, string userId)
		{
			bool result = false;
			var house = await this.repo.AllReadonly<House>()
				.Where(h => h.IsActive)
				.Where(h => h.Id == houseId)
				.FirstOrDefaultAsync();

			if (house != null && house.RenterId == userId)
			{
				result = true;
			}

			return result;
		}

		public async Task<IEnumerable<HouseIndexServiceModel>> LastThreeHousesAsync()
			=> await this.repo.AllReadonly<House>()
				.Where(h => h.IsActive)
				.OrderByDescending(h => h.Id)
				.ProjectTo<HouseIndexServiceModel>(this.mapper.ConfigurationProvider)
				.Take(3)
				.ToListAsync();

		public async Task LeaveAsync(int houseId)
		{
			var house = await this.repo.GetByIdAsync<House>(houseId);

			house.RenterId = null;

			await this.repo.SaveChangesAsync();
		}

		public async Task RentAsync(int houseId, string userId)
		{
			var house = await this.repo.GetByIdAsync<House>(houseId);

			house.RenterId = userId;

			await this.repo.SaveChangesAsync();
		}
	}
}