using HotelListingAPI.Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListingAPI.Data.Configuration
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {

 

        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasData(
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
        }
    }
}
