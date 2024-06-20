using Microsoft.EntityFrameworkCore;
using Task10.Test.Core.Models;

namespace Task10.Infrastructure
{
    public sealed class CoursesDbContext : DbContext
    {
        public CoursesDbContext(DbContextOptions<CoursesDbContext> options) : base(options)
        {
            MigrateNotAppliedMigrations();

            SeedData();
        }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Student> Students { get; set; }


        private void MigrateNotAppliedMigrations()
        {
            if (Database.GetPendingMigrations().Any())
            {
                Database.Migrate();
            }
        }

        private void SeedData()
        {
            if (Courses.Any())
            {
                return;
            }

            Courses.AddRange(
                new Course
                {
                    Name = "SQL"
                },
                new Course
                {
                    Name = "C#"
                },
                new Course
                {
                    Name = "Java"
                });

            SaveChanges();
        }
    }
}
