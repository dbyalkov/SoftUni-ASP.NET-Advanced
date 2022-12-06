using HouseRentingSystem.Services.Data.Configuration;
using HouseRentingSystem.Services.Data.Entities;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem.Services.Data
{
    public class HouseRentingDbContext : IdentityDbContext<User>
    {
        public HouseRentingDbContext(DbContextOptions<HouseRentingDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new AgentConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new HouseConfiguration());

            base.OnModelCreating(builder);
        }

        public DbSet<House> Houses { get; init; } = null!;

        public DbSet<Category> Categories { get; init; } = null!;

        public DbSet<Agent> Agents { get; init; } = null!;
    }
}