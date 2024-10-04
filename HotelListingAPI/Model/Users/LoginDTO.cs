using System.ComponentModel.DataAnnotations;

namespace HotelListingAPI.Model.Users
{
    public class LoginDTO
    {
        [Required, EmailAddress]
        public string Email { get; set; }


        [Required, StringLength(15, ErrorMessage = 
            "Your Password is Limited to {2} to {i} characters",
            MinimumLength = 6)] // {2} to {i} Placeholder for binding MinimumLength  and Maxminimum Lenght 
        public string Password { get; set; }
    }
}
