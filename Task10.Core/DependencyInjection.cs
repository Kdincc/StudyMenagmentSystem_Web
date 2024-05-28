using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            
            return services;
        }
    }
}
