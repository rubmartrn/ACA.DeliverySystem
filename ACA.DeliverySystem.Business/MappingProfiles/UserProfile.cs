using ACA.DeliverySystem.Business.Models;
using ACA.DeliverySystem.Data.Models;
using AutoMapper;

namespace ACA.DeliverySystem.Business.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserViewModel>()
                .ForMember(d => d.Name, d => d.MapFrom(s => s.Name))
                .ForMember(d => d.SureName, d => d.MapFrom(s => s.SureName))
                .ForMember(d => d.Email, d => d.MapFrom(s => s.Email))
                .ForMember(d => d.OrderId, d => d.MapFrom(s => s.OrderId))
                .ForMember(d => d.Order, d => d.MapFrom(s => s.Order))
                .ForMember(d => d.Id, d => d.MapFrom(s => s.Id))
                .PreserveReferences();

            CreateMap<UserAddModel, User>()
                .ForMember(d => d.Name, d => d.MapFrom(s => s.Name))
                .ForMember(d => d.SureName, d => d.MapFrom(s => s.SureName))
                .ForMember(d => d.Email, d => d.MapFrom(s => s.Email))
                .PreserveReferences();

        }
    }
}
