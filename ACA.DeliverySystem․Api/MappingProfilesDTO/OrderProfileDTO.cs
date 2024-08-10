using ACA.DeliverySystem.Business.Models;
using AutoMapper;

namespace ACA.DeliverySystem.Business.MappingProfiles
{
    public class OrderProfileDTO : Profile
    {
        public OrderProfileDTO()
        {
            CreateMap<OrderViewModel, OrderViewModelDTO>()
                 .ForMember(d => d.UserId, d => d.MapFrom(s => s.UserId))
                 .ForMember(d => d.ItemId, d => d.MapFrom(s => s.ItemId))
                 .ForMember(d => d.Date, d => d.MapFrom(s => s.Date))
                 .ForMember(d => d.ProgressEnum, d => d.MapFrom(s => s.ProgressEnum))
                 .ForMember(d => d.PaidAmount, d => d.MapFrom(s => s.PaidAmount))
                 .ForMember(d => d.UserId, d => d.MapFrom(s => s.UserId))
                 .PreserveReferences();

            CreateMap<OrderAddModelDTO, OrderAddModel>()
                .ForMember(d => d.UserId, d => d.MapFrom(s => s.UserId))
                .ForMember(d => d.ItemId, d => d.MapFrom(s => s.ItemId))
                .PreserveReferences();



        }
    }
}
