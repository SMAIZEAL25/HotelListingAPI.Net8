using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelListingAPI.Data;
using HotelListingAPI.QueriableParameters;
using HotelListingAPI.Respository.Contract;
using Microsoft.EntityFrameworkCore;

namespace HotelListingAPI.Respository
{
    public class GenericRepository<T> : IGenericRespository<T> where T : class
    {

        private readonly HotelListingDbContext _context;
        private readonly IMapper _mapper;


        public GenericRepository(HotelListingDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
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

        // pagining method 
        public async Task<PageResult<TResult>> GetAllAsync<TResult>(QueriableParameter queryParameters)
        {
            var totalSize = await _context.Set<T>().CountAsync();
            var items = await _context.Set<T>()
                .Skip(queryParameters.StartIndex)
                .Take(queryParameters.PageSize)

                // first inject the mapper 
                .ProjectTo<TResult>(_mapper.ConfigurationProvider) 
                .ToListAsync();
            return new PageResult<TResult> 
            { 
                Items = items,
                PageNumber = queryParameters.StartIndex,
                RecordNumber = queryParameters.PageSize,
                TotalCount = totalSize
            
            };

           
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
