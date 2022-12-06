using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;

using static HouseRentingSystem.Services.Data.DataConstants.User;

namespace HouseRentingSystem.Services.Data.Entities
{
    public class User : IdentityUser
    {
        [MaxLength(UserFirstNameMaxLength)]
        public string? FirstName { get; init; } = null;

        [MaxLength(UserLastNameMaxLength)]
        public string? LastName { get; init; } = null;
    }
}