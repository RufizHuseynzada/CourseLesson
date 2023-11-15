using CourseLesson.Data;

namespace CourseLesson.Contract
{
    public interface ILibraryRepository : IGenericRepository<Library>
    {
        Task<Library> GetDetails(int id);
    }
}
