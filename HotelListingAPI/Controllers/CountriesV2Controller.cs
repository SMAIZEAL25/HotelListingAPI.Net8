using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListingAPI.Data;
using AutoMapper;
using HotelListingAPI.DTO.CountryDTO;
using Microsoft.AspNetCore.Authorization;
using HotelListingAPI.Exceptions;
using Microsoft.AspNetCore.OData.Query;
using HotelListingAPI.Data.Model;
using HotelListingAPI.Respository.Contract;

namespace HotelListingAPI.Controllers
{
    //[Route("api/v{version:apiVersion}/countries")]
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]

    public class CountriesV2Controller2 : ControllerBase 
    {
        // private fields that makes refference to the class that i'm using here 
        private readonly IMapper _mapper;
        private readonly ICountriesRespository _countriesRespository;
        private readonly ILogger _logger;



        // Constructor class
        public CountriesV2Controller2(IMapper mapper, ICountriesRespository countriesRespository, ILogger<CountriesController> logger)
        {
            
            this._mapper = mapper;
            this._countriesRespository = countriesRespository;
            this._logger = logger;
        }




        // GET: api/Countries
        [HttpGet]
        [EnableQuery] //using Odata
        public async Task<ActionResult<IEnumerable<GetCountryDTO>>> GetCountries()
        {
            var countries = await _countriesRespository.GetAllAsync();
            var records = _mapper.Map<List<GetCountryDTO>>(countries);
            return Ok(records);
        } 



        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDTO>> GetCountry(int id)
        {

            // include the list of hotels and while you are fetching me the first record with the matching entity that has the Id 
            var country = await _countriesRespository.GetDetails(id);


            if (country == null)
            {
                //_logger.LogWarning($"Record Not Found in {nameof(GetCountry)} with the id: {id}.");
                //return NotFound();

                // implementation of the custom logger and try catch blosk here 

                throw new NotfoundException(nameof(GetCountry), id);
            }

            var countryDto = _mapper.Map<CountryDTO>(country); 

            return Ok(countryDto);
        }


        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutCountry(int id, UpdateCountryDTO updateCountryDTO)
        {
            if (id != updateCountryDTO.Id)
            {
                return BadRequest("Invalid record Id");
            }

            var country = await _countriesRespository.GetAsync(id);

            if (country == null)
            {
                return NotFound();
            }
            // Take the incoming data from the user and update it in the database 

            _mapper.Map(updateCountryDTO, country);

             try
            {
                await _countriesRespository.UpdateAsync(country);
            }
            catch (DbUpdateConcurrencyException)
            {
                if ( !await CountryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }



        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Administrator")] // only admininstrator can carryout this delete operation NOTE: user can be assign admin role is the database 
        public async Task<ActionResult<Country>> PostCountry(CreateCountryDTO createCountryDTO)
        {
            // manual mapping of automapper 
            //var country = new Country
            //{
            //    Name = createCountryDTO.Name,
            //    ShortName = createCountryDTO.ShortName,
            //};

            var country = _mapper.Map<Country>(createCountryDTO);

            await _countriesRespository.AddAsync(country);
         

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }




        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _countriesRespository.GetAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            await _countriesRespository.DeleteAsync(id);
            return NoContent();
        }

        private async Task <bool> CountryExists(int id)
        {
            return await _countriesRespository.Exists(id);
        }
    }
}
