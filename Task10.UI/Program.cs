using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;
using Task10.Core;
using Task10.Infrastructure;

namespace Task10.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //builder.Services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Task10 API", Version = "v1" });
            //});

            builder.Services.AddControllersWithViews();

            builder.Services.AddInfrastructure(builder.Configuration).AddCore();

            var app = builder.Build();

            //if (app.Environment.IsDevelopment()) 
            //{
            //    app.UseSwaggerUI(c =>
            //    {
            //        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            //        c.RoutePrefix = string.Empty; // Это сделает Swagger UI доступным на корне URL (http://localhost:<port>/)
            //    });
            //}

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
