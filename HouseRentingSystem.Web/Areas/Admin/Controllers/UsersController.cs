using HouseRentingSystem.Services.Users;

using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Web.Areas.Admin.Controllers
{
    public class UsersController : AdminController
    {
        private readonly IUserService users;

        public UsersController(IUserService _users)
        {
            this.users = _users;
        }

        [Route("Users/All")]
        public async Task<IActionResult> All()
        {
            var users = await this.users.All();
            return View(users);
        }
    }
}