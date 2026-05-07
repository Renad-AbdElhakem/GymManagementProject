using GymManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagement.Configrations
{
    public class EmployeeConfigration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasMany(e => e.EmployeeAttendancecs)
                   .WithOne(a => a.Employee);

            builder.HasMany(e => e.Course)
                   .WithOne(c => c.Employee);

            builder.HasData(new Employee
            {
                Id = 1,
                UserName = "RenadAbdelhakem",
                Address="Cairo",
                PhoneNumber="010555454545",
                RoleId = 1,
                

            });
        }
    }
}
