using HouseRentingSystem.Services.Data.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseRentingSystem.Services.Data.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder
                .HasData(SeedCategories());
        }

        private List<Category> SeedCategories()
        {
            var categories = new List<Category>()
            {
                new Category()
                {
                    Id = 1,
                    Name = "Cottage"
                },
                new Category()
                {
                    Id = 2,
                    Name = "Single-Family"
                },
                new Category()
                {
                    Id = 3,
                    Name = "Duplex"
                }
            };

            return categories;
        }
    }
}