using AutoMapper;

using HouseRentingSystem.Services.Agents;
using HouseRentingSystem.Services.Houses;
using HouseRentingSystem.Services.Houses.Models;
using HouseRentingSystem.Web.Infrastructure;
using HouseRentingSystem.Web.Models.Houses;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

using static HouseRentingSystem.Web.Areas.Admin.AdminConstants;

namespace HouseRentingSystem.Web.Controllers
{
    public class HousesController : Controller
    {
        private readonly IHouseService houses;
        private readonly IAgentService agents;
        private readonly IMapper mapper;
        private readonly IMemoryCache cache;

        public HousesController(
            IHouseService _houses,
            IAgentService _agents,
            IMapper _mapper,
            IMemoryCache _cache)
        {
            this.houses = _houses;
            this.agents = _agents;
            this.mapper = _mapper;
            this.cache = _cache;
        }

        [HttpGet]
        public async Task<IActionResult> All([FromQuery] AllHousesQueryModel query)
        {
            var result = await this.houses.AllAsync(
                query.Category,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllHousesQueryModel.HousesPerPage);

            query.TotalHousesCount = result.TotalHousesCount;
            query.Categories = await this.houses.AllCategoriesNamesAsync();
            query.Houses = result.Houses;

            return View(query);
        }

        [Authorize]
        public async Task<IActionResult> Mine()
        {
            if (this.User.IsInRole(AdminRoleName))
            {
                return RedirectToAction("Mine", "Houses", new { area = "Admin" });
            }

            IEnumerable<HouseServiceModel> myHouses;

            var userId = this.User.Id();

            if (await this.agents.ExistsByIdAsync(userId))
            {
                var currAgentId = await this.agents.GetAgentIdAsync(userId);

                myHouses = await this.houses.AllHousesByAgentIdAsync(currAgentId);
            }
            else
            {
                myHouses = await this.houses.AllHousesByUserIdAsync(userId);
            }

            return View(myHouses);
        }

        public async Task<IActionResult> Details(int id, string information)
        {
            if ((await this.houses.ExistsAsync(id)) == false)
            {
                return BadRequest();
            }

            var houseModel = await this.houses.HouseDetailsByIdAsync(id);

            if (information != houseModel.GetInformation())
            {
                return BadRequest();
            }

            return View(houseModel);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            if ((await this.agents.ExistsByIdAsync(this.User.Id())) == false)
            {
                return RedirectToAction(nameof(AgentsController.Become), "Agents");
            }

            return View(new HouseFormModel()
            {
                Categories = await this.houses.AllCategoriesAsync()
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(HouseFormModel house)
        {
            if ((await this.agents.ExistsByIdAsync(this.User.Id())) == false)
            {
                return RedirectToAction(nameof(AgentsController.Become), "Agents");
            }

            if ((await this.houses.CategoryExistsAsync(house.CategoryId)) == false)
            {
                ModelState.AddModelError(nameof(house.CategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                house.Categories = await this.houses.AllCategoriesAsync();

                return View(house);
            }

            var agentId = await this.agents.GetAgentIdAsync(this.User.Id());

            var newHouseId = await this.houses.CreateAsync(
                house.Title,
                house.Address,
                house.Description,
                house.ImageUrl,
                house.PricePerMonth,
                house.CategoryId,
                agentId);

            return RedirectToAction(nameof(Details), new { id = newHouseId, information = house.GetInformation() });
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (!(await this.houses.ExistsAsync(id)))
            {
                return BadRequest();
            }

            if (!(await this.houses.HasAgentWithIdAsync(id, this.User.Id())) && !this.User.IsAdmin())
            {
                return Unauthorized();
            }

            var house = await this.houses.HouseDetailsByIdAsync(id);

            var houseCategoryId = await this.houses.GetHouseCategoryIdAsync(house.Id);

            var houseModel = this.mapper.Map<HouseFormModel>(house);
            houseModel.CategoryId = houseCategoryId;
            houseModel.Categories = await this.houses.AllCategoriesAsync();

            return View(houseModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, HouseFormModel house)
        {
            if (!(await this.houses.ExistsAsync(id)))
            {
                return View();
            }

            if (!(await this.houses.HasAgentWithIdAsync(id, this.User.Id())) && !this.User.IsAdmin())
            {
                return Unauthorized();
            }

            if ((await this.houses.CategoryExistsAsync(house.CategoryId)) == false)
            {
                ModelState.AddModelError(nameof(house.CategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                house.Categories = await this.houses.AllCategoriesAsync();

                return View(house);
            }

            await houses.EditAsync(
                id,
                house.Title,
                house.Address,
                house.Description,
                house.ImageUrl,
                house.PricePerMonth,
                house.CategoryId);

            return RedirectToAction(nameof(Details), new { id = id, information = house.GetInformation() });
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (!(await this.houses.ExistsAsync(id)))
            {
                return View();
            }

            if (!(await this.houses.HasAgentWithIdAsync(id, this.User.Id())) && !this.User.IsAdmin())
            {
                return Unauthorized();
            }

            var house = await this.houses.HouseDetailsByIdAsync(id);

            var model = this.mapper.Map<HouseDetailsViewModel>(house);

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Delete(HouseDetailsViewModel model)
        {
            if ((await this.houses.ExistsAsync(model.Id)) == false)
            {
                return View();
            }

            if (!(await this.houses.HasAgentWithIdAsync(model.Id, this.User.Id())) && !this.User.IsAdmin())
            {
                return Unauthorized();
            }

            await houses.DeleteAsync(model.Id);

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Rent(int id)
        {
            if (!(await this.houses.ExistsAsync(id)))
            {
                return BadRequest();
            }

            if (await this.agents.ExistsByIdAsync(this.User.Id()) && !this.User.IsAdmin())
            {
                return Unauthorized();
            }

            if (await this.houses.IsRentedAsync(id))
            {
                return BadRequest();
            }

            await houses.RentAsync(id, this.User.Id());

            this.cache.Remove(RentsCacheKey);

            return RedirectToAction(nameof(Mine));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            if ((await this.houses.ExistsAsync(id)) == false || (await this.houses.IsRentedAsync(id) == false))
            {
                return BadRequest();
            }

            if ((await this.houses.IsRentedByUserWithIdAsync(id, this.User.Id())) == false)
            {
                return Unauthorized();
            }

            await this.houses.LeaveAsync(id);

            this.cache.Remove(RentsCacheKey);

            return RedirectToAction(nameof(Mine));
        }
    }
}