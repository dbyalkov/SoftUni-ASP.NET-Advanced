using System.ComponentModel.DataAnnotations;

using HouseRentingSystem.Services.Houses.Models;

using static HouseRentingSystem.Services.Data.DataConstants.House;

namespace HouseRentingSystem.Web.Models.Houses
{
    public class HouseFormModel : IHouseModel
	{
        [Required]
        [MaxLength(TitleMaxLength)]
        [MinLength(TitleMinLength)]
        public string Title { get; init; } = null!;

        [Required]
        [MaxLength(AddressMaxLength)]
        [MinLength(AddressMinLength)]
        public string Address { get; init; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        [MinLength(DescriptionMinLength)]
        public string Description { get; init; } = null!;

        [Required]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; init; } = null!;

        [Required]
        [Range(0.00, 20000.00, ErrorMessage = "Price Per Month must be a positive number and less than {2} lv.")]
        [Display(Name = "Price Per Month")]
        public decimal PricePerMonth { get; init; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<HouseCategoryServiceModel> Categories { get; set; } = new List<HouseCategoryServiceModel>();
    }
}