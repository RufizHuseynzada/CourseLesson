using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CourseLesson.Data;
using CourseLesson.Contract;
using AutoMapper;
using CourseLesson.Models.Library;

namespace CourseLesson.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrariesController : ControllerBase
    {
        private readonly ILibraryRepository _repos;
        private readonly IMapper _mapper;

        public LibrariesController(ILibraryRepository repos, IMapper mapper)
        {
            _repos = repos;
            _mapper = mapper;
        }

        // GET: api/Libraries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetLibrariesMod>>> GetLibraries()
        {
            var libraries = await _repos.GetAllAsync();

            var getLibraries = _mapper.Map<IList<GetLibrariesMod>>(libraries);

            return Ok(getLibraries);
        }

        // GET: api/Libraries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetLibraryMod>> GetLibrary(int id)
        {
            var library = await _repos.GetDetails(id);


            if (library == null)
            {
                return NotFound();
            }

            var getLibrary = _mapper.Map<GetLibraryMod>(library);

            return getLibrary;
        }

        // PUT: api/Libraries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLibrary(int id, UpdateLibraryMod libraryUpdate)
        {
            var library = await _repos.GetAsync(id);

            _mapper.Map(libraryUpdate, library);

            try
            {
               await _repos.UpdateAsync(library);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _repos.Exists(id))
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

        // POST: api/Libraries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Library>> PostLibrary(CreateLibraryMod libraryUpdate)
        {
            var library = _mapper.Map<Library>(libraryUpdate);
            await _repos.AddAsync(library);

            return CreatedAtAction("GetLibrary", new { id = library.Id }, library);
        }

        // DELETE: api/Libraries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLibrary(int id)
        {
            var library = await _repos.GetAsync(id);
            if (library == null)
            {
                return NotFound();
            }

            await _repos.DeleteAsync(id);

            return NoContent();
        }


    }
}
