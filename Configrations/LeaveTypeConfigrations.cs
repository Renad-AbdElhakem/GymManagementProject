using GymManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagement.Configrations
{
    public class LeaveTypeConfigrations : IEntityTypeConfiguration<LeaveType>
    {
        public void Configure(EntityTypeBuilder<LeaveType> builder)
        {
            builder.HasData(
                new LeaveType
                {
                    Id = 1,
                    Name = "Sick Leave",
                    Description = "Sick leave for health-related reasons.",
                    MaxDaysPerYear = 10,
                    RequiresApproval = true,
                    IsActive = true
                },
                new LeaveType
                {
                    Id = 2,
                    Name = "Vacation",
                    Description = "Annual vacation leave.",
                    MaxDaysPerYear = 21,
                    RequiresApproval = true,
                    IsActive = true
                },
                new LeaveType
                {
                    Id = 3,
                    Name = "Personal Leave",
                    Description = "Personal leave for private matters.",
                    MaxDaysPerYear = 5,
                    RequiresApproval = true,
                    IsActive = true
                },
                new LeaveType
                {
                    Id = 4,
                    Name = "Emergency Leave",
                    Description = "Emergency leave that does not require approval.",
                    MaxDaysPerYear = 3,
                    RequiresApproval = false,
                    IsActive = true
                }
            );
        }
    }
}
