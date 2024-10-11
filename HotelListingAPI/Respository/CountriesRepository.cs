using AutoMapper;
using HotelListingAPI.Contract;
using HotelListingAPI.Data;
using HotelListingAPI.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace HotelListingAPI.Respository
{
    public class CountriesRepository : GenericRepository<Country>, ICountriesRespository
    {
        private readonly HotelListingDbContext _context;
        public CountriesRepository(HotelListingDbContext context, IMapper mapper) : base(context, mapper)
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
