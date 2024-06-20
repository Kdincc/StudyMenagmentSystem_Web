using Microsoft.Extensions.DependencyInjection;
using Task10.Core.Interfaces;
using Task10.Core.Services;

namespace Task10.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddScoped<IHomeService, HomeService>();
            services.AddScoped<IGroupsService, GroupsService>();
            services.AddScoped<IStudentsService, StudentsService>();

            return services;
        }
    }
}
