

using System.ComponentModel.DataAnnotations;

namespace CourseLesson.Models.Library
{
    public class BaseLibrariesMod
    {
        public string Address { get; set; }
        [Required]
        public string Name { get; set; }
        public decimal Rating { get; set; }
    }
}