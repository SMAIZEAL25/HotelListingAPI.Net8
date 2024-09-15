using System.ComponentModel.DataAnnotations;

namespace HotelListingAPI.CountryDTO
{
    public class CreateCountryDTO
    {

        [Required]
        public string Name { get; set; }
        public string ShortName { get; set; }
    }
}
