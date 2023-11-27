using Microsoft.AspNetCore.Identity;

namespace CourseLesson.Data
{
    public class ApiUser : IdentityUser
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth  { get; set; }

    }
}
