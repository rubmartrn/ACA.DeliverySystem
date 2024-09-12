using ACA.DeliverySystem.Api.Models;
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
                 .ForMember(d => d.Name, d => d.MapFrom(s => s.Name))
                 .ForMember(d => d.Date, d => d.MapFrom(s => s.Date))
                 .ForMember(d => d.ProgressEnum, d => d.MapFrom(s => s.ProgressEnum))
                 .ForMember(d => d.PaidAmount, d => d.MapFrom(s => s.PaidAmount))
                 .ForMember(d => d.AmountToPay, d => d.MapFrom(s => s.AmountToPay))
                 .ForMember(d => d.UserId, d => d.MapFrom(s => s.UserId))
                 .ForMember(d => d.Items, d => d.MapFrom(s => s.Items))
                 .PreserveReferences();


            CreateMap<OrderAddModelDTO, OrderAddModel>()
                .ForMember(d => d.Name, d => d.MapFrom(s => s.Name))
                .PreserveReferences();



        }
    }
}
