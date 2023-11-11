using System.ComponentModel.DataAnnotations.Schema;

namespace CourseLesson.Data
{
    public class Library
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public decimal Rating { get; set; }

        [ForeignKey(nameof(CountryId))]
        public int CountryId { get; set; }
        public Country CountryName { get; set; }

    }
}
