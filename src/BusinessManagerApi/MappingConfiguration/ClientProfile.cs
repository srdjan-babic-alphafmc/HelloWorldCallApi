using AutoMapper;
using BusinessManager.Models.DtoModels;
using BusinessManager.Models.Models;

namespace BusinessManagerApi.MappingConfiguration
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<Clients, ClientsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PIB, opt => opt.MapFrom(src => src.PIB))
                .ForMember(dest => dest.Note, opt => opt.MapFrom(src => src.Note))
                .ReverseMap();
        }
    }
}
