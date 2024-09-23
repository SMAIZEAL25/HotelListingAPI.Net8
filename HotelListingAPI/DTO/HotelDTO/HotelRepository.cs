using HotelListingAPI.Contract;
using HotelListingAPI.Data;
using HotelListingAPI.Model;
using HotelListingAPI.Respository;

namespace HotelListingAPI.DTO.HotelDTO
{
    public class HotelRepository : GenericRepository<Hotel>, IHotelRepository
    {
        public HotelRepository(HotelListingDbContext context) : base(context)
        {
        }
    }

}

