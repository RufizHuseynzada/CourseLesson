using CourseLesson.Data;

namespace CourseLesson.Contract
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
        Task<Country> GetDetails(int id);
    }
}
