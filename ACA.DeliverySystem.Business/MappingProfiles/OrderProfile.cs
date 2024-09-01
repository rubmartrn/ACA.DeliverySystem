using ACA.DeliverySystem.Business.Models;
using ACA.DeliverySystem.Data.Models;
using AutoMapper;

namespace ACA.DeliverySystem.Business.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderViewModel>()
                 .ForMember(d => d.UserId, d => d.MapFrom(s => s.UserId))
                 .ForMember(d => d.Name, d => d.MapFrom(s => s.Name))
                 .ForMember(d => d.Date, d => d.MapFrom(s => s.Date))
                 .ForMember(d => d.ProgressEnum, d => d.MapFrom(s => s.ProgressEnum))
                 .ForMember(d => d.PaidAmount, d => d.MapFrom(s => s.PaidAmount))
                 .ForMember(d => d.AmountToPay, d => d.MapFrom(s => s.AmountToPay))
                 .ForMember(d => d.UserId, d => d.MapFrom(s => s.UserId))
                 .ForMember(d => d.Items, d => d.MapFrom(s => s.Items))
                 .PreserveReferences();

            CreateMap<OrderAddModel, Order>()
                .ForMember(d => d.Name, d => d.MapFrom(s => s.Name))
                .PreserveReferences();



        }
    }
}
