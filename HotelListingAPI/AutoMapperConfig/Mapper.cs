using AutoMapper;
using HotelListingAPI.Data.Model;
using HotelListingAPI.DTO.CountryDTO;
using HotelListingAPI.DTO.HotelDTO;
using HotelListingAPI.Users;

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
             
                CreateMap<Hotel, HotelDTO>().ReverseMap();
                CreateMap<Hotel, CreatHotelDTO>().ReverseMap();


                CreateMap<APIUsersDTO, APIUser>().ReverseMap();
            }
        }
    }
}
 