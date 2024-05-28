using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Task10.Infrastructure.Repos;
using Task10.Test.Core.Interfaces;

namespace Task10.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CoursesDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DbString"));
            });

            services.AddScoped<ICoursesRepository, CourseRepository>();
            services.AddScoped<IGroupsRepository, GroupsRepository>();
            services.AddScoped<IStudentsRepository, StudentsRepository>();

            return services;
        }
    }
}
