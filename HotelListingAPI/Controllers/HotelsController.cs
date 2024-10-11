using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListingAPI.Data;
using AutoMapper;
using HotelListingAPI.Contract;
using HotelListingAPI.DTO.HotelDTO;
using Microsoft.AspNetCore.Authorization;
using HotelListingAPI.Data.Model;

namespace HotelListingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelRepository _hotelRepository1;
        private readonly IMapper _mapper;

        public HotelsController(IHotelRepository hotelRepository, IMapper mapper)
        {
            this._hotelRepository1 = hotelRepository;
            this._mapper = mapper;

        }

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelDTO>>> GetHotels()
        {
            var hotels = await _hotelRepository1.GetAllAsync();
            return Ok (_mapper.Map<List<HotelDTO>>(hotels));
        }
            


        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelDTO>> GetHotel(int id)
        {
            var hotel = await _hotelRepository1.GetAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            return Ok (_mapper.Map<HotelDTO>(hotel));
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, HotelDTO hotelDTO)
        {
            if (id != hotelDTO.Id)
            {
                return BadRequest();     
            }


            var hotel = await _hotelRepository1.GetAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            _mapper.Map(hotelDTO, hotel);

            //_context.Entry(hotel).State = EntityState.Modified;

            try
            {
                await _hotelRepository1.UpdateAsync(hotel);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await HotelExists(id))
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

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(CreatHotelDTO creatHotelDTO)
        {
            var hotel = _mapper.Map<Hotel>(creatHotelDTO);
           _hotelRepository1.AddAsync(hotel);

            return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
        }


        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _hotelRepository1.GetAsync (id);
            if (hotel == null)
            {
                return NotFound();
            }

           await _hotelRepository1.DeleteAsync(id);
           
            return NoContent();
        }

        private async Task <bool> HotelExists(int id)
        {
            return await _hotelRepository1.Exists(id) ;
        }
    }
}
