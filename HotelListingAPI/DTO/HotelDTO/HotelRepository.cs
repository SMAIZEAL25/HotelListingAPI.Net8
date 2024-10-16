using AutoMapper;
using HotelListingAPI.Data;
using HotelListingAPI.Data.Model;
using HotelListingAPI.Respository;
using HotelListingAPI.Respository.Contract;

namespace HotelListingAPI.DTO.HotelDTO
{
    public class HotelRepository : GenericRepository<Hotel>, IHotelRepository
    {
        public HotelRepository(HotelListingDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }

}

