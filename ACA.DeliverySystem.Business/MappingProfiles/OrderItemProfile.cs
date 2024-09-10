using ACA.DeliverySystem.Business.Models;
using ACA.DeliverySystem.Data.Models;
using AutoMapper;

namespace ACA.DeliverySystem.Business.MappingProfiles
{
    public class OrderItemProfile : Profile
    {
        public OrderItemProfile()
        {
            CreateMap<OrderItem, OrderItemViewModel>()
                 .ForMember(d => d.OrderId, d => d.MapFrom(s => s.OrderId))
                 .ForMember(d => d.Quantity, d => d.MapFrom(s => s.Quantity))
                 .ForMember(d => d.Price, d => d.MapFrom(s => s.Price))
                 .ForMember(d => d.Item, d => d.MapFrom(s => s.Item))
                 .ForMember(d => d.ItemId, d => d.MapFrom(s => s.ItemId))
                 .ForMember(d => d.ItemName, d => d.MapFrom(s => s.ItemName))
                 .PreserveReferences();

        }
    }
}
