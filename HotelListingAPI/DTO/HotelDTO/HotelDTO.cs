using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListingAPI.DTO.HotelDTO
{
    public class HotelDTO :BaseHotelDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string address { get; set; }
        public double Rating { get; set; }
        public int CountryID { get; set; }
    }
    
}