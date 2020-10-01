using AutoMapper;
using BusinessManager.Models.DtoModels;
using BusinessManager.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessManagerApi.MappingConfiguration
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Products, ProductsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.Barcode, opt => opt.MapFrom(src => src.Barcode))
                .ForMember(dest => dest.SerialNumber, opt => opt.MapFrom(src => src.SerialNumber))
                .ForMember(dest => dest.PurchasePrice, opt => opt.MapFrom(src => src.PurchasePrice))
                .ForMember(dest => dest.SalePrice, opt => opt.MapFrom(src => src.SalePrice))
                .ForMember(dest => dest.Warranty, opt => opt.MapFrom(src => src.Warranty))
                .ForMember(dest => dest.WarrantyExpirationDate, opt => opt.MapFrom(src => src.WarrantyExpirationDate))
                .ForMember(dest => dest.PurchaseDate, opt => opt.MapFrom(src => src.PurchaseDate))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.Others, opt => opt.MapFrom(src => src.Others))
                .ReverseMap();
        }
    }
}
