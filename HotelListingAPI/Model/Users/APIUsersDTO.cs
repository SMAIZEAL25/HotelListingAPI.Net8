using System.ComponentModel.DataAnnotations;

namespace HotelListingAPI.Model.Users
{
    public class APIUsersDTO
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        [Required,EmailAddress]
        public string Email { get; set; }
        [Required, StringLength(15, ErrorMessage = "Your Password is Limited to {2} to {i} characters", MinimumLength = 6)] // {2} to {i} Placeholder for binding MinimumLength  and Maxminimum Lenght 
        public string Password { get; set; }

    }
}
