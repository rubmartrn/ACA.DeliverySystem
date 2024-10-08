﻿using ACA.DeliverySystem.Api.Models;
using ACA.DeliverySystem.Business.Models;
using AutoMapper;

namespace ACA.DeliverySystem.Business.MappingProfiles
{
    public class ItemProfileDTO : Profile
    {
        public ItemProfileDTO()
        {
            CreateMap<ItemViewModel, ItemViewModelDTO>()
                .ForMember(d => d.Id, d => d.MapFrom(s => s.Id))
                .ForMember(d => d.Name, d => d.MapFrom(s => s.Name))
                .ForMember(d => d.Price, d => d.MapFrom(s => s.Price))
                .ForMember(d => d.Description, d => d.MapFrom(s => s.Description))
                .ForMember(d => d.ImageUrl, d => d.MapFrom(s => s.ImageUrl))
                .PreserveReferences();


            CreateMap<ItemAddModelDTO, ItemAddModel>()
                .ForMember(d => d.Name, d => d.MapFrom(s => s.Name))
                .ForMember(d => d.Description, d => d.MapFrom(s => s.Description))
                .ForMember(d => d.Price, d => d.MapFrom(s => s.Price))
                .ForMember(d => d.ImageUrl, d => d.MapFrom(s => s.ImageUrl))
                .PreserveReferences();

            CreateMap<ItemUpdateModelDTO, ItemUpdateModel>()
                .ForMember(d => d.Name, d => d.MapFrom(s => s.Name))
                .ForMember(d => d.Price, d => d.MapFrom(s => s.Price))
                .ForMember(d => d.Description, d => d.MapFrom(s => s.Description))
                .ForMember(d => d.ImageUrl, d => d.MapFrom(s => s.ImageUrl))
                .PreserveReferences();

        }
    }
}
