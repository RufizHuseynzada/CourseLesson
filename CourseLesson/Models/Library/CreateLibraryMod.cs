using Microsoft.Build.Framework;

namespace CourseLesson.Models.Library
{
    public class CreateLibraryMod : BaseLibrariesMod
    {
        [Required]
        public int CountryId { get; set; }
    }
}