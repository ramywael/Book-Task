
using Book_Task.Data;
using Book_Task.Repositories;
using Book_Task.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace Book_Task
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string myAllowedCorsOptions = "myAllowedCorsOptions";

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options => options.AddPolicy(
                name: myAllowedCorsOptions,
                policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                }
                ));
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();
            app.UseCors(myAllowedCorsOptions);
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
