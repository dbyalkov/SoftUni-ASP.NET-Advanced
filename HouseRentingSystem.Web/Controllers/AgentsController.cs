using HouseRentingSystem.Web.Infrastructure;
using HouseRentingSystem.Web.Models.Agents;
using HouseRentingSystem.Services.Agents;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Web.Controllers
{
	public class AgentsController : Controller
	{
		private readonly IAgentService agents;

		public AgentsController(IAgentService _agents)
		{
			agents = _agents;
		}

		[Authorize]
		[HttpGet]
		public async Task<IActionResult> Become()
		{
			if (await agents.ExistsByIdAsync(this.User.Id()))
			{
				return BadRequest();
			}

			return View();
		}

		[Authorize]
		[HttpPost]
		public async Task<IActionResult> Become(BecomeAgentFormModel agent)
		{
			var userId = this.User.Id();

			if (await this.agents.ExistsByIdAsync(userId))
			{
				return BadRequest();
			}

			if (await this.agents.UserWithPhoneNumberExistsAsync(agent.PhoneNumber))
			{
				ModelState.AddModelError(nameof(agent.PhoneNumber), "Phone number already exists. Enter another one.");
			}

			if (await this.agents.UserHasRentsAsync(userId))
			{
				ModelState.AddModelError("Error", "You should have no rents to become an agent!");
			}

			if (!ModelState.IsValid)
			{
				return View(agent);
			}

			await this.agents.CreateAsync(userId, agent.PhoneNumber);

			TempData["message"] = "You have successfully become an agent!";

			return RedirectToAction(nameof(HousesController.All), "Houses");
		}
    }
}