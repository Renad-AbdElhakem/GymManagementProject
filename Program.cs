
using FluentValidation;
using GymManagement.AutoMapper;
using GymManagement.Data;
using GymManagement.Domain;
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
                cfg.AddProfile<RoleProfile>();
                cfg.AddProfile<EmployeeProfile>();
                cfg.AddProfile<CourseProfile>();
                cfg.AddProfile<WeekDaysProfile>();
                cfg.AddProfile<SchedulingProfile>();
                cfg.AddProfile<MemberProfile>();
                cfg.AddProfile<SubscriptionTypeMappingProfile>();
                cfg.AddProfile<AttendanceMemberProfile>();
                cfg.AddProfile<EmployeeAttendanceProfile>();
              
            });

            builder.Services.AddDbContext<ApplicationDbContext>(option=>
            option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddValidatorsFromAssemblyContaining<Program>();


            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<ICourseRepository, CourseRepository>();
            builder.Services.AddScoped<IWeekDaysRepository, WeekDaysRepository>();
            builder.Services.AddScoped<ISchedulingRepository,SchedulingRepository>();
            builder.Services.AddScoped<IMemberRepository, MemberRepository>();
            builder.Services.AddScoped<ISubscriptionTypeRepository, SubscriptionTypeRepository>();
            builder.Services.AddScoped<IMemberAttendanceRepository, MemberAttendanceRepository>();
            builder.Services.AddScoped<IEmployeeAttendanceRepository, EmployeeAttendanceRepository>();





            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<IEmployeeService, EmployeesService>();
            builder.Services.AddScoped<ICourseService,CourseService>();
            builder.Services.AddScoped<IWeekDaysService, WeekDaysService>();
            builder.Services.AddScoped<ISchedulingService,SchedulingService>();
            builder.Services.AddScoped<IMemberService, MemberService>();
            builder.Services.AddScoped<ISubscriptionTypeService, SubscriptionTypeService>();
            builder.Services.AddScoped<IMemberAttendanceService, MemberAttendanceService>();
            builder.Services.AddScoped<IEmployeeAttendanceService, EmployeeAttendanceService>();
           
          
            
            
            
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
