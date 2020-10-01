using AutoMapper;
using BusinessManager.Models.DtoModels;
using BusinessManager.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessManagerApi.MappingConfiguration
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            CreateMap<Sale, SaleDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Buyer, opt => opt.MapFrom(src => src.Buyer))
                .ForMember(dest => dest.Catalog, opt => opt.MapFrom(src => src.Catalog))
                .ForMember(dest => dest.SaleDate, opt => opt.MapFrom(src => src.SaleDate))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
                .ReverseMap();
        }
    }
}
