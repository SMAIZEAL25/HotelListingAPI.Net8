//using HotelListingAPI.DTO.HotelDTO;
//using HotelListingAPI.Model;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Security.Policy;
//using static HotelListingAPI.DTO.HotelDTO.HotelDTO;

using HotelListingAPI.DTO.HotelDTO;

namespace HotelListingAPI.DTO.CountryDTO
{
    public class CountryDTO
    {
        //public List<HotelDTO> Hotel { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Shortname { get; set; }
        public List<BaseHotelDTO> Hotels { get; set; }
    }
}