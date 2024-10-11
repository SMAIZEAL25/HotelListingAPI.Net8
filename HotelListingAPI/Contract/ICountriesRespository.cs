using HotelListingAPI.Data.Model;

namespace HotelListingAPI.Contract
{
    public interface ICountriesRespository : IGenericRespository<Country>
    {
        Task<Country> GetDetails(int id);
    }
}
