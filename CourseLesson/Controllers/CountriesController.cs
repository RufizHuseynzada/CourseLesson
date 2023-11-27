using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CourseLesson.Data;
using CourseLesson.Models.Country;
using AutoMapper;
using CourseLesson.Contract;
using Microsoft.AspNetCore.Authorization;

namespace CourseLesson.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryRepository _context;
        private readonly IMapper _mapper;

        public CountriesController(ICountryRepository context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCountriesMod>>> GetCountries()
        {
            var countries = await _context.GetAllAsync();

            var getCountry = _mapper.Map<IList<GetCountriesMod>>(countries);

            return Ok(getCountry);
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCountryMod>> GetCountry(int id)
        {
            var country = await _context.GetDetails(id);

            if (country == null)
            {
                return NotFound();
            }

            var countryGet = _mapper.Map<GetCountryMod>(country);

            return countryGet;
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Underwriting,Seller")]
        public async Task<IActionResult> PutCountry(int id, UpdateCountryMod countryUpdate)
        {
            var country = await _context.GetAsync(id);

            if (country == null)
            {
                return NotFound();
            }
            _mapper.Map(countryUpdate, country);
            try
            {
                await _context.UpdateAsync(country);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Exists(id))
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
        [Authorize]
        public async Task<ActionResult<Country>> PostCountry(CreateCountryMod countryCreate)
        {
            var country = _mapper.Map<Country>(countryCreate);
            await _context.AddAsync(country);

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            if (await _context.Exists(id))
            {
                return NotFound();
            }
            await _context.DeleteAsync(id);
            return NoContent();
        }

        //private bool CountryExists(int id)
        //{
        //    return _context.Countries.Any(e => e.Id == id);
        //}
    }
}
