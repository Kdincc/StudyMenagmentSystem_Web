using Microsoft.OpenApi.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using Task10.Core;
using Task10.Infrastructure;

namespace Task10.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Task10 API",
                    Version = "v1"
                });
            });

            builder.Services.AddControllersWithViews();

            builder.Services.AddInfrastructure(builder.Configuration).AddCore();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            });

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.Use(async (context, next) =>
            {
                if (context.Request.Method == "POST" && context.Request.Form["_method"] == "PUT")
                {
                    context.Request.Method = "PUT";
                }

                if (context.Request.Method == "POST" && context.Request.Form["_method"] == "DELETE")
                {
                    context.Request.Method = "DELETE";
                }

                await next();

            });

            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
