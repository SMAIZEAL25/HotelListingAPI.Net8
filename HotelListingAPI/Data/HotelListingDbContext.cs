using HotelListingAPI.Data.Configuration;
using HotelListingAPI.Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Security.Cryptography.X509Certificates;

namespace HotelListingAPI.Data
{
    public class HotelListingDbContext : IdentityDbContext <APIUser>
    {

        public HotelListingDbContext (DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Hotel> Hotels { get; set; }

        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleConfiguration()); // this implemnt the Roleconfiguration class which is relative to IEntityTypeConfiguration <IdentityRole> to seed roles for JWT 
            modelBuilder.ApplyConfiguration(new CountryConfiguration());
            modelBuilder.ApplyConfiguration(new HotelConfiguration());
          
            
        }



    }
}
