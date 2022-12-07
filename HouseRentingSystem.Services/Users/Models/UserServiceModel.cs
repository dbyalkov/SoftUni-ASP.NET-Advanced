﻿namespace HouseRentingSystem.Services.Users.Models
{
    public class UserServiceModel
    {
        public string UserId { get; init; } = null!;

        public string? Email { get; init; } = null;

        public string? FullName { get; init; } = null;

        public string? PhoneNumber { get; init; } = null;
    }
}