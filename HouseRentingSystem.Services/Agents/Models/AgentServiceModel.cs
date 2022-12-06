using System.ComponentModel.DataAnnotations;

using static HouseRentingSystem.Services.Data.DataConstants.Agent;

namespace HouseRentingSystem.Services.Agents.Models
{
    public class AgentServiceModel
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        [MinLength(PhoneNumberMinLength)]
        [Display(Name = "Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [MaxLength(EmailMaxLength)]
        [MinLength(EmailMinLength)]
        [EmailAddress]
        public string Email { get; set; } = null!;

        public string? FullName { get; set; } = null;
    }
}