using System.ComponentModel.DataAnnotations;

namespace HotelListingAPI.DTO.HotelDTO
{
    public abstract class BaseHotelDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string address { get; set; }

        // this is nullable 
        public double? Rating { get; set; }

        [Required,Range(1,int.MaxValue)]
        public int CountryID { get; set; }
    }
    
}