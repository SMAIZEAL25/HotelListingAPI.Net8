using HotelListingAPI.Contract;
using HotelListingAPI.Data;
using HotelListingAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace HotelListingAPI.Respository
{
    public class CountriesRepository : GenericRepository<Country>, ICountriesRespository
    {
        private readonly HotelListingDbContext _context;
        public CountriesRepository(HotelListingDbContext context) : base (context)
        { 
            this._context = context;
        }

        public async Task<Country> GetDetails(int id)
        {
             return await _context.Countries.Include(q => q.Hotels)
                .FirstOrDefaultAsync(q => q.Id == id);
        }
    }
}
