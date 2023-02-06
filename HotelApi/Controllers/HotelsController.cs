using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelApi.Data;
using HotelApi.Contracts;
using AutoMapper;
using HotelApi.ModelDtos.hotelDtos;

namespace HotelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelsRepository _iHotelsRepository;
        private readonly IMapper _mapper;

        public HotelsController(IHotelsRepository iHotelsRepository, IMapper mapper)
        {
            this._iHotelsRepository = iHotelsRepository;
            this._mapper = mapper;
        }

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelDto>>> Gethotels()
        {
            var hotels = await _iHotelsRepository.GetAllAsync();
            if (_iHotelsRepository == null)
            {
                return NotFound();
            }
            // return await _context.hotels.ToListAsync();
            return _mapper.Map<List<HotelDto>>(hotels);
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelDto>> GetHotel(int id)
        {
            if (_iHotelsRepository == null)
            {
                return NotFound();
            }
            // var hotel = await _context.hotels.FindAsync(id);
            var hotel = await _iHotelsRepository.GetAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<HotelDto>(hotel));
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, HotelDto hotelDto)
        {
            if (id != hotelDto.Id)
            {
                return BadRequest();
            }

            // _context.Entry(hotel).State = EntityState.Modified;
            var hotel = await _iHotelsRepository.GetAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            _mapper.Map(hotelDto, hotel);

            try
            {
                // await _context.SaveChangesAsync();
                await _iHotelsRepository.UpdateAsync(hotel);
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
        public async Task<ActionResult<Hotel>> PostHotel(CreateHotelDto createHotelDto)
        {
            var hotel = _mapper.Map<Hotel>(createHotelDto);
            if (_iHotelsRepository == null)
            {
                return Problem("Entity set 'HotelDbContext.hotels'  is null.");
            }
            // _context.hotels.Add(hotel);
            // await _context.SaveChangesAsync();
            await _iHotelsRepository.AddAsync(hotel);

            return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            if (_iHotelsRepository == null)
            {
                return NotFound();
            }
            // var hotel = await _context.hotels.FindAsync(id);
            var hotel = _iHotelsRepository.GetAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            // _context.hotels.Remove(hotel);
            // await _context.SaveChangesAsync();
            await _iHotelsRepository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> HotelExists(int id)
        {
            // return (_context.hotels?.Any(e => e.Id == id)).GetValueOrDefault();
            return _iHotelsRepository.Equals(id);
        }
    }
}
