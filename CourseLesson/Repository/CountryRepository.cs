using CourseLesson.Contract;
using CourseLesson.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseLesson.Repository
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        private readonly Course _context;

        public CountryRepository(Course context) : base(context)
        {
            _context = context;
        }

        public async Task<Country> GetDetails(int id)
        {
            return await _context.Countries.Include(q => q.Libraries).FirstOrDefaultAsync(q => q.Id == id);
        }
    }
}
