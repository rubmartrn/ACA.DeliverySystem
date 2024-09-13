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
                .ForMember(d => d.SurName, d => d.MapFrom(s => s.SurName))
                .ForMember(d => d.Email, d => d.MapFrom(s => s.Email))
                .ForMember(d => d.Id, d => d.MapFrom(s => s.Id))
                .PreserveReferences();

            CreateMap<UserAddModel, User>()
                .ForMember(d => d.Name, d => d.MapFrom(s => s.Name))
                .ForMember(d => d.SurName, d => d.MapFrom(s => s.SurName))
                .ForMember(d => d.Email, d => d.MapFrom(s => s.Email))
                .ForMember(d => d.PasswordHash, d => d.MapFrom(s => s.PasswordHash))
                .PreserveReferences();

            CreateMap<User, ResponseForSignIn>()
            .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(d => d.PasswordHash, opt => opt.MapFrom(src => src.PasswordHash))
            .PreserveReferences();


        }
    }
}
