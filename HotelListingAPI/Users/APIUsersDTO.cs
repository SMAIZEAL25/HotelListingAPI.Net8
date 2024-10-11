using System.ComponentModel.DataAnnotations;

namespace HotelListingAPI.Users
{
    public class APIUsersDTO : LoginDTO
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }


    }
}
