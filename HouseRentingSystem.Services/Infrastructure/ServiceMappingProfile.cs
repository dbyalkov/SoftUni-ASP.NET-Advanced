using AutoMapper;

using HouseRentingSystem.Services.Agents.Models;
using HouseRentingSystem.Services.Data.Entities;
using HouseRentingSystem.Services.Houses.Models;
using HouseRentingSystem.Services.Rents.Models;
using HouseRentingSystem.Services.Users.Models;

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

            this.CreateMap<Category, HouseCategoryServiceModel>();

            this.CreateMap<Agent, UserServiceModel>()
                .ForMember(u => u.UserId, config => config.MapFrom(a => a.UserId))
                .ForMember(u => u.Email, config => config.MapFrom(a => a.User.Email))
                .ForMember(u => u.FullName, config => config.MapFrom(a => $"{a.User.FirstName} {a.User.LastName}"));

            this.CreateMap<User, UserServiceModel>()
                .ForMember(us => us.UserId, config => config.MapFrom(u => u.Id))
                .ForMember(us => us.PhoneNumber, config => config.MapFrom(u => string.Empty))
                .ForMember(us => us.FullName, config => config.MapFrom(u => $"{u.FirstName} {u.LastName}"));

            this.CreateMap<House, RentServiceModel>()
                .ForMember(rs => rs.HouseTitle, config => config.MapFrom(h => h.Title))
                .ForMember(rs => rs.HouseImageUrl, config => config.MapFrom(h => h.ImageUrl))
                .ForMember(rs => rs.AgentFullName, config => config.MapFrom(h => $"{h.Agent.User.FirstName} {h.Agent.User.LastName}"))
                .ForMember(rs => rs.AgentEmail, config => config.MapFrom(h => h.Agent.User.Email))
                .ForMember(rs => rs.RenterFullName, config => config.MapFrom(h => $"{h.Renter.FirstName} {h.Renter.LastName}"))
                .ForMember(rs => rs.RenterEmail, config => config.MapFrom(h => h.Renter.Email));
        }
    }
}