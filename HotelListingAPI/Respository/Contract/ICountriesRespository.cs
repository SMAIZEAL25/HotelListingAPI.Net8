using HotelListingAPI.Data.Model;

namespace HotelListingAPI.Respository.Contract
{
    public interface ICountriesRespository : IGenericRespository<Country>
    {
        Task<Country> GetDetails(int id);
    }
}
