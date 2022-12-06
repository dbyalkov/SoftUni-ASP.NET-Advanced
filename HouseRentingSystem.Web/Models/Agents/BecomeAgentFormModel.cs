using System.ComponentModel.DataAnnotations;

using static HouseRentingSystem.Services.Data.DataConstants.Agent;

namespace HouseRentingSystem.Web.Models.Agents
{
	public class BecomeAgentFormModel
	{
		[Required]
		[MaxLength(PhoneNumberMaxLength)]
		[MinLength(PhoneNumberMinLength)]
		[Display(Name = "Phone Number")]
		[Phone]
		public string PhoneNumber { get; init; } = null!;
	}
}