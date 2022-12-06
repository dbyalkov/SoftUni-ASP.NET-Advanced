using HouseRentingSystem.Services.Data.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static HouseRentingSystem.Services.Data.Constants;

namespace HouseRentingSystem.Services.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasData(SeedUsers());
        }

        private List<User> SeedUsers()
        {
            var users = new List<User>();
            var hasher = new PasswordHasher<User>();

            var user = new User()
            {
                Id = "dea12856-c198-4129-b3f3-b893d8395082",
                UserName = "agent@mail.com",
                NormalizedUserName = "AGENT@MAIL.COM",
                Email = "agent@mail.com",
                NormalizedEmail = "AGENT@MAIL.COM",
                FirstName = "Linda",
                LastName = "Michaels"
            };

            user.PasswordHash = hasher.HashPassword(user, "agent123");
            users.Add(user);

            user = new User()
            {
                Id = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                UserName = "guest@mail.com",
                NormalizedUserName = "GUEST@MAIL.COM",
                Email = "guest@mail.com",
                NormalizedEmail = "GUEST@MAIL.COM",
                FirstName = "Teodor",
                LastName = "Lesly"
            };

            user.PasswordHash = hasher.HashPassword(user, "guest123");
            users.Add(user);

            user= new User()
            {
                Id = "bcb4f072-ecca-43c9-ab26-c060c6f364e4",
                Email = AdminEmail,
                NormalizedEmail = AdminNormalizedEmail,
                UserName = AdminEmail,
                NormalizedUserName = AdminNormalizedEmail,
                FirstName = "Great",
                LastName = "Admin"
            };

            user.PasswordHash = hasher.HashPassword(user, "admin123");
            users.Add(user);

            return users;
        }
    }
}