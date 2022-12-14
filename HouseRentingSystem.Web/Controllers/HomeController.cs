using HouseRentingSystem.Services.Houses;

using Microsoft.AspNetCore.Mvc;

using static HouseRentingSystem.Web.Areas.Admin.AdminConstants;

namespace HouseRentingSystem.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHouseService houses;

        public HomeController(IHouseService _houses)
        {
            this.houses = _houses;
        }

        public async Task<IActionResult> Index()
        {
            if (this.User.IsInRole(AdminRoleName))
            {
                return RedirectToAction("Index", "Home", new { area = "Admin"});
            }

            var model = await this.houses.LastThreeHousesAsync();

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
            if (statusCode == 400)
            {
                return View("Error400");
            }

            if (statusCode == 401)
            {
                return View("Error401");
            }

            return View();
        }
    }
}