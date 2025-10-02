using BusinessEntities;
using AutoMapper;

namespace BusinessLogics
{
    public class BreweryMappingProfile : Profile
    {
        public BreweryMappingProfile()
        {
            CreateMap<BreweryExternalModel, BreweryBusinessModel>()
                .ForMember(dest => dest.BreweryName, opt => opt.MapFrom(src => src.Name))                
                .ForMember(dest => dest.BreweryAddress, opt => opt.MapFrom(src => src.Street + (string.IsNullOrEmpty(src.Address1) ? "" : ", " + src.Address1) + (string.IsNullOrEmpty(src.Address2) ? "" : ", " + src.Address2) + (string.IsNullOrEmpty(src.Address3) ? "" : ", " + src.Address3) ))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))       
                .ForMember(dest => dest.ContactNumber, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.WebsiteUrl, opt => opt.MapFrom(src => src.WebsiteUrl));
                //.ForMember(dest => dest.BreweryType, opt => opt.MapFrom(src => src.BreweryType))
                //.ForMember(dest => dest.StateProvince, opt => opt.MapFrom(src => src.StateProvince))
                //.ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Longitude))
                //.ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Latitude))
        }
    }
}
