using ACA.DeliverySystem.Business.Models;
using ACA.DeliverySystem_Api.Models;
using AutoMapper;

namespace ACA.DeliverySystem.Business.MappingProfiles
{
    public class OrderItemProfileDTO : Profile
    {
        public OrderItemProfileDTO()
        {
            CreateMap<OrderItemViewModel, OrderItemViewModelDTO>()
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
