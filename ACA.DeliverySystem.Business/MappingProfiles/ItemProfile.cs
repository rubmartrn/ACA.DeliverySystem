using ACA.DeliverySystem.Business.Models;
using ACA.DeliverySystem.Data.Models;
using AutoMapper;

namespace ACA.DeliverySystem.Business.MappingProfiles
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<Item, ItemViewModel>()
                .ForMember(d => d.Id, d => d.MapFrom(s => s.Id))
                .ForMember(d => d.Name, d => d.MapFrom(s => s.Name))
                .ForMember(d => d.Price, d => d.MapFrom(s => s.Price))
                .ForMember(d => d.Description, d => d.MapFrom(s => s.Description))
                .ForMember(d => d.ImageUrl, d => d.MapFrom(s => s.ImageUrl))
                .PreserveReferences();



            CreateMap<ItemAddModel, Item>()
                .ForMember(d => d.Name, d => d.MapFrom(s => s.Name))
                .ForMember(d => d.Description, d => d.MapFrom(s => s.Description))
                .ForMember(d => d.Price, d => d.MapFrom(s => s.Price))
                .ForMember(d => d.ImageUrl, d => d.MapFrom(s => s.ImageUrl))
                .PreserveReferences();





        }
    }
}
