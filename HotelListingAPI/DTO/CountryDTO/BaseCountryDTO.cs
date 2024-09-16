using System.ComponentModel.DataAnnotations;

namespace HotelListingAPI.DTO.CountryDTO
{
    public class BaseCountryDTO
    {
        [Required]
        public string Name { get; set; }
        public string ShortName { get; set; }
    }
}
