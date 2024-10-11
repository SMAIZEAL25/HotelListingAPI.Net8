using HotelListingAPI.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListingAPI.Data.Configuration
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData(

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

                );
        }
    }
}
