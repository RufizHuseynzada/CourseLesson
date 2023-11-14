using System.ComponentModel.DataAnnotations;

namespace CourseLesson.Models.Country
{
    public abstract class BaseCountryMod
    {
        [Required]
        public string Name { get; set; }
        public string ShortName { get; set; }
    }
}
