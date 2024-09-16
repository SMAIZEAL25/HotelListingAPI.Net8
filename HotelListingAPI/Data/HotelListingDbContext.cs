using HotelListingAPI.Model;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Security.Cryptography.X509Certificates;

namespace HotelListingAPI.Data
{
    public class HotelListingDbContext : DbContext
    {

        public HotelListingDbContext (DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Hotel> Hotels { get; set; }

        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasData(
                new Country
                {
                    Id = 1,
                    Name = "Henle",
                    ShortName = "HNL",
                },
                new Country
                {
                    Id = 2,
                    Name = "Segun",
                    ShortName = "SGN",
                },
                new Country
                {
                    Id = 3,
                    Name = "Aforke",
                    ShortName = "AFK",
                }

                );
            modelBuilder.Entity<Hotel>().HasData(
                new Hotel
                {
                    Id = 1,
                    Name = "Temitope Oluwafemi",
                    Address = "No 23, Hossanah thift class, off lagos stree ikeja ",
                    CountryID = 2,
                    Rating = 4.3,
                },
                new Hotel
                {
                    Id = 2,
                    Name = "Solomon Chika",
                    Address = "No 22, elesegun street",
                    CountryID = 3,
                    Rating = 22.3,
                }

                ) ;
            
        }


    }
}
