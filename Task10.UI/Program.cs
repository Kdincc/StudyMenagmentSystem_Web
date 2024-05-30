using Task10.Core;
using Task10.Infrastructure;

namespace Task10.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddJsonFile(@"E:\C#\Task10\Task10.UI\appsettings.json");

            builder.Services.AddControllersWithViews();

            builder.Services.AddInfrastructure(builder.Configuration).AddCore();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Groups}/{action=Index}/{id?}"
                );

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Students}/{action=Index}/{id?}"
                );

            app.Run();
        }
    }
}
