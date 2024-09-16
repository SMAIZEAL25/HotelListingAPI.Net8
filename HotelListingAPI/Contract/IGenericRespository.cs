using HotelListingAPI.Model;

namespace HotelListingAPI.Contract
{
    public interface IGenericRespository<T> where T : class
    {
        // this is the overral rule that need to be carry out 
        Task<T> GetAsync(int? id);

        Task<List<T>> GetAllAsync();

        Task <T> AddAsync (T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync (int Id);

        Task<bool> Exists(int id);
        
    }
  
}


  
