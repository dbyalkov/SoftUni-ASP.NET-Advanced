using HouseRentingSystem.Services.Agents;
using HouseRentingSystem.Services.Houses;
using HouseRentingSystem.Web.Areas.Admin.Models;
using HouseRentingSystem.Web.Infrastructure;

using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Web.Areas.Admin.Controllers
{
    public class HousesController : AdminController
    {
        private readonly IHouseService houses;
        private readonly IAgentService agents;

        public HousesController(
            IHouseService _houses,
            IAgentService _agents)
        {
            this.houses = _houses;
            this.agents = _agents;
        }

        public async Task<IActionResult> Mine()
        {
            var myHouses = new MyHousesViewModel();

            var adminUserId = this.User.Id();
            myHouses.RentedHouses = await this.houses.AllHousesByUserIdAsync(adminUserId);

            var adminAgentId = await this.agents.GetAgentIdAsync(adminUserId);
            myHouses.AddedHouses = await this.houses.AllHousesByAgentIdAsync(adminAgentId);

            return View(myHouses);
        }
    }
}