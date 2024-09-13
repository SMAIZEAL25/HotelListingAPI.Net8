using HotelListingAPI.Model;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace HotelListingAPI.Data
{
    public class HotelListingDbContext : DbContext
    {

        public HotelListingDbContext (DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Hotel> Hotels { get; set; }

        public DbSet<Country> Countries { get; set; }

    }
}
