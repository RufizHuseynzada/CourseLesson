using CourseLesson.Models.Library;

namespace CourseLesson.Models.Country
{
    public class GetCountryMod : BaseCountryMod
    {
        public int Id { get; set; }
        public IList<GetLibrariesMod> Libraries { get; set; }
    }
}
