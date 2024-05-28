using Microsoft.EntityFrameworkCore;
using Task10.Test.Core.Models;

namespace Task10.Infrastructure
{
    public sealed class CoursesDbContext : DbContext
    {
        public CoursesDbContext(DbContextOptions<CoursesDbContext> options) : base(options)
        {
            MigrateNotAppliedMigrations();
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
    }
}
