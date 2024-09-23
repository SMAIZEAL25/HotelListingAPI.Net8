using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListingAPI.Data.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {

        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            //builder.ToTable(""); // this can be used to rename the table
            builder.HasData(
                new IdentityRole
                {
                    Name = "Administartor",
                    NormalizedName = "ADMINISTARTOR",
                },

                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER",
                }
                );
        }
    }
}
