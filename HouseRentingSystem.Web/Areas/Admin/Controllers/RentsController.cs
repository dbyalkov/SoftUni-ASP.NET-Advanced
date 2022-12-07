using HouseRentingSystem.Services.Rents;

using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Web.Areas.Admin.Controllers
{
    public class RentsController : AdminController
    {
        private readonly IRentService rents;

        public RentsController(IRentService _rents)
        {
            this.rents = _rents;
        }

        [Route("Rents/All")]
        public async Task<IActionResult> All()
        {
            var rents = await this.rents.All();

            return View(rents);
        }
    }
}