using AutoMapper;
using HotelListingAPI.Contract;
using HotelListingAPI.Data;
using HotelListingAPI.Data.Model;
using HotelListingAPI.Respository;

namespace HotelListingAPI.DTO.HotelDTO
{
    public class HotelRepository : GenericRepository<Hotel>, IHotelRepository
    {
        public HotelRepository(HotelListingDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }

}

