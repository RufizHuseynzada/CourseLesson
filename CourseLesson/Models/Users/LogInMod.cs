
using System.ComponentModel.DataAnnotations;

namespace CourseLesson.Models.Users
{
    public class LogInMod
    {
        [Required]
        [StringLength(15, ErrorMessage = "Кол-во символов в вашем пороле должен быть в диапозоне от {2} до {1}", MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
