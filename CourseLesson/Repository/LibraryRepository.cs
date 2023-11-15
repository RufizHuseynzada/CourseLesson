using CourseLesson.Contract;
using CourseLesson.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseLesson.Repository
{
    public class LibraryRepository : GenericRepository<Library>, ILibraryRepository
    {
        private readonly Course _context;

        public LibraryRepository(Course context) : base(context)
        {
            _context = context;
        }

        public async Task<Library> GetDetails(int id)
        {
            return await _context.Libraries.Include(q => q.CountryName).FirstOrDefaultAsync(q => q.Id == id);
        }
    }
}
