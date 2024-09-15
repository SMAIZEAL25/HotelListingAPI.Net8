using AutoMapper;
using HotelListingAPI.CountryDTO;
using HotelListingAPI.Model;

namespace HotelListingAPI.AutoMapperConfig
{
    public class Mapper
    {
        public class MapperConfig : Profile
        {
            public MapperConfig() 
            {
                CreateMap<Country,CreateCountryDTO>().ReverseMap();
            }
        }
    }
}
