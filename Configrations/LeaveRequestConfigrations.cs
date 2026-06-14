using GymManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagement.Configrations
{
    public class LeaveRequestConfigrations : IEntityTypeConfiguration<LeaveRequest>
    {
        public void Configure(EntityTypeBuilder<LeaveRequest> builder)
        {
            builder.HasOne(l => l.Employee)
                   .WithMany(e => e.LeaveRequests)
                   .HasForeignKey(l => l.EmployeeId);

            builder.HasOne(l => l.LeaveType)
                   .WithMany(e => e.LeaveRequests)
                   .HasForeignKey(l => l.LeaveTypeId);
        }
    }
}
