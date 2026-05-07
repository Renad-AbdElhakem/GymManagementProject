using GymManagement.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GymManagement.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<EmployeeAttendance> EmployeeAttendance { get; set; }
        public DbSet<MemberAttendance>MemberAttendances { get; set; }
        public DbSet<SubscriptionType>  SubscriptionTypes { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<WeekDays> WeekDays { get; set; }
        public DbSet<Scheduling> Schedulings { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BaseUser>().UseTpcMappingStrategy();

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
          

        }
    }
}
