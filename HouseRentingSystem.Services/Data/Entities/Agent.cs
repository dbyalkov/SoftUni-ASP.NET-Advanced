using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static HouseRentingSystem.Services.Data.DataConstants.Agent;

namespace HouseRentingSystem.Services.Data.Entities
{
    public class Agent
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public User User { get; init; } = null!;

        public bool IsActive { get; set; } = true;
    }
}