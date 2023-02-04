using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelApi.Data;
using HotelApi.ModelDtos.countryDtos;
using AutoMapper;
using HotelApi.Contracts;

namespace HotelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly HotelDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICountriesRepository _countriesRepository;

        public CountriesController(IMapper mapper, ICountriesRepository countriesRepository)
        {
            this._mapper = mapper;
            this._countriesRepository = countriesRepository;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCountryDto>>> Getcountries()
        {
            if (_countriesRepository == null)
            {
                return NotFound();
            }

            // var countries = await _context.countries.ToListAsync();
            var countries = await _countriesRepository.GetAllAsync();

            var getCountryDtos = _mapper.Map<List<GetCountryDto>>(countries);
            return Ok(getCountryDtos);
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDto>> GetCountry(int id)
        {
            if (_countriesRepository == null)
            {
                return NotFound();
            }
            // var country = await _context.countries.Include(q => q.hotels).FirstOrDefaultAsync(q => q.Id == id);
            var country = await _countriesRepository.GetDetails(id);

            if (country == null)
            {
                return NotFound();
            }

            var countryDto = _mapper.Map<CountryDto>(country);

            return Ok(countryDto);
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, UpdateCountryDto updateCountryDto)
        {
            if (id != updateCountryDto.Id)
            {
                return BadRequest();
            }
            // _context.Entry(country).State = EntityState.Modified;
            // var country = await _context.countries.FindAsync(id);
            var country = await _countriesRepository.GetAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            var mappercountry = _mapper.Map(updateCountryDto, country);
            try
            {
                await _countriesRepository.UpdateAsync(mappercountry);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CountryExists(id))
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
        public async Task<ActionResult<Country>> PostCountry(CreateCountryDto createCountryDto)
        {
            // var country = new Country
            // {
            //     Name = createCountry.Name,
            //     shortName = createCountry.ShortName
            // };
            // !convering from one data type to another
            var country = _mapper.Map<Country>(createCountryDto);

            if (_countriesRepository == null)
            {
                return Problem("Entity set 'HotelDbContext.countries'  is null.");
            }
            // _context.countries.Add(country);
            // await _context.SaveChangesAsync();
            await _countriesRepository.AddAsync(country);

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            if (_countriesRepository == null)
            {
                return NotFound();
            }
            // var country = await _context.countries.FindAsync(id);
            var country = await _countriesRepository.GetAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            // _context.countries.Remove(country);
            // await _context.SaveChangesAsync();
            await _countriesRepository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> CountryExists(int id)
        {
            // return (_context.countries?.Any(e => e.Id == id)).GetValueOrDefault();
            return await _countriesRepository.Exists(id);
        }
    }
}
