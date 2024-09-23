using Microsoft.AspNetCore.Identity;

namespace HotelListingAPI.Model
{
    public class APIUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; } // = string.Empty;
    }
}
