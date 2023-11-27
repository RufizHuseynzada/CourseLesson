
using System.ComponentModel.DataAnnotations;

namespace CourseLesson.Models.Users
{
    public class CreateUserMod : LogInMod
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
