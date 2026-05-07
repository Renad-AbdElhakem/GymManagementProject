
using GymManagement.AutoMapper;
using GymManagement.Data;
using GymManagement.IRepositories;
using GymManagement.IRepositories.Repositories;
using GymManagement.IServices;
using GymManagement.IServices.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace GymManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();


            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<EmployeeProfile>();
            });

            builder.Services.AddDbContext<ApplicationDbContext>(option=>
            option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeService, EmployeesService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
