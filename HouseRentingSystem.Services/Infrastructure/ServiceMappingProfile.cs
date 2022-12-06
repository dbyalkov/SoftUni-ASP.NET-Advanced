using AutoMapper;

using HouseRentingSystem.Services.Agents.Models;
using HouseRentingSystem.Services.Data.Entities;
using HouseRentingSystem.Services.Houses.Models;

namespace HouseRentingSystem.Services.Infrastructure
{
    public class ServiceMappingProfile : Profile
    {
        public ServiceMappingProfile()
        {
            this.CreateMap<House, HouseServiceModel>()
                .ForMember(h => h.IsRented, config => config.MapFrom(h => h.RenterId != null));

            this.CreateMap<House, HouseDetailsServiceModel>()
                .ForMember(h => h.IsRented, config => config.MapFrom(h => h.RenterId != null))
                .ForMember(h => h.Category, config => config.MapFrom(h => h.Category.Name));

            this.CreateMap<Agent, AgentServiceModel>()
                .ForMember(a => a.Email, config => config.MapFrom(a => a.User.Email));

            this.CreateMap<House, HouseIndexServiceModel>();

            this.CreateMap<House, HouseCategoryServiceModel>();
        }
    }
}