using AutoMapper;
using HotelListingAPI.DTO.CountryDTO;
using HotelListingAPI.DTO.HotelDTO;
using HotelListingAPI.Model;


namespace HotelListingAPI.AutoMapperConfig
{
    public class Mapper
    {
        public class MappingConfig : Profile
        {
            public MappingConfig()
            {
                CreateMap<Country, CreateCountryDTO>().ReverseMap();
                CreateMap<Country, GetCountryDTO>().ReverseMap();
                CreateMap<Country, UpdateCountryDTO>().ReverseMap();
                CreateMap<Country, CountryDTO>().ReverseMap();


                // Hotel mapping 
                CreateMap<Country, HotelDTO>().ReverseMap();
            }
        }
    }
}
