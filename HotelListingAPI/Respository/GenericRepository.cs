using HotelListingAPI.Contract;
using HotelListingAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelListingAPI.Respository
{
    public class GenericRepository<T> : IGenericRespository<T> where T : class
    {

        private readonly HotelListingDbContext _context;


        public GenericRepository(HotelListingDbContext context)
        {
            this._context = context;
        }


        public async Task<T> AddAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }


        public async Task DeleteAsync(int Id)
        {
            var entity = await GetAsync(Id);
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }


        public async Task<bool> Exists(int id)
        {
            var entity = await GetAsync(id);
            return entity != null;
        }

     

        public async Task<List<T>> GetAllAsync()
        {
            // sotot the database _context use the Dbset<T> T is a generic Type that represent any type eg in the dbset.entity in te dbcontext  
            return await _context.Set<T>().ToListAsync();

        }

        public async Task<T> GetAsync(int? id)
        {
            if (id is null)
            {
                return null;
            }

            return await _context.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
