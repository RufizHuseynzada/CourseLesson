using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace CourseLesson.Data
{
    public class Course : DbContext
    {
        public Course(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Library> Libraries { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasData(
                new Country
                {
                    Id = 1,
                    Name = "Azerbaijan",
                    ShortName = "AJ"
                },
                new Country
                {
                    Id = 2,
                    Name = "America",
                    ShortName = "USA"
                });

            modelBuilder.Entity<Library>().HasData(

                new Library
                {
                    Id = 1,
                    Name = "Nizami Library",
                    Rating = 5M,
                    Address = "Narimanov",
                    CountryId = 1,
                }, new Library
                {
                    Id = 2,
                    Name = "Huston Library",
                    Rating = 4.7M,
                    Address = "Los Angeles",
                    CountryId = 2,
                });
        }
    }
}
