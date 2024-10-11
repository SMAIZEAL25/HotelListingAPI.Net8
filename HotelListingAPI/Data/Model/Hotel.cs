using System.ComponentModel.DataAnnotations.Schema;
using HotelListingAPI.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;

namespace HotelListingAPI.Data.Model
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Rating { get; set; }


        [ForeignKey(nameof(CountryID))]
        public int CountryID { get; set; }
        public Country Country { get; set; }

    }


}
