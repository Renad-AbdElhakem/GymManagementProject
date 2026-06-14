
using FluentValidation;
using GymManagement.AutoMapper;
using GymManagement.Data;
using GymManagement.Domain;
using GymManagement.Event;
using GymManagement.Helper;
using GymManagement.IRepositories;
using GymManagement.IRepositories.Repositories;
using GymManagement.IServices;
using GymManagement.IServices.Services;
using GymManagement.IServices.SMS;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using System;

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
                cfg.AddProfile<LeaveRequestProfile>();
              
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
            builder.Services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
            builder.Services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();





            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<IEmployeeService, EmployeesService>();
            builder.Services.AddScoped<ICourseService,CourseService>();
            builder.Services.AddScoped<IWeekDaysService, WeekDaysService>();
            builder.Services.AddScoped<ISchedulingService,SchedulingService>();
            builder.Services.AddScoped<IMemberService, MemberService>();
            builder.Services.AddScoped<ISubscriptionTypeService, SubscriptionTypeService>();
            builder.Services.AddScoped<IMemberAttendanceService, MemberAttendanceService>();
            builder.Services.AddScoped<IEmployeeAttendanceService, EmployeeAttendanceService>();
            builder.Services.AddScoped<ILeaveRequestService, LeaveRequestService>();


            //builder.Services.AddHostedService<SubscriptionExpiryWorker>();
            //builder.Services.AddSingleton<IEventPublisher, EventPublisher>();
            //builder.Services.AddScoped<IEventHandler<SubscriptionAboutToExpireEvent>, SubscriptionSmsHandler>();

            //builder.Services.Configure<TwilioSettings>(builder.Configuration.GetSection("Twilio"));
            //builder.Services.AddScoped<ISmsService, SmsService>();


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
