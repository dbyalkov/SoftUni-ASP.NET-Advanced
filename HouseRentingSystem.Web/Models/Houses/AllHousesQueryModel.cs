﻿using System.ComponentModel.DataAnnotations;

using HouseRentingSystem.Services;
using HouseRentingSystem.Services.Houses.Models;

namespace HouseRentingSystem.Web.Models.Houses
{
    public class AllHousesQueryModel
    {
        public const int HousesPerPage = 3;

        public string? Category { get; init; }

        [Display(Name = "Search by text")]
        public string? SearchTerm { get; init; }

        public HouseSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalHousesCount { get; set; }

        public IEnumerable<string> Categories { get; set; } = null!;

        public IEnumerable<HouseServiceModel> Houses { get; set; } = new List<HouseServiceModel>();
    }
}