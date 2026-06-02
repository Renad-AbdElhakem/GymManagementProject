using GymManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagement.Configrations
{
    public class AttendanceMemberConfigrtion : IEntityTypeConfiguration<MemberAttendance>
    {
        public void Configure(EntityTypeBuilder<MemberAttendance> builder)
        {
            builder.HasOne(a => a.Member)
                   .WithMany(p => p.MemberAttendancecs)
                   .HasForeignKey(a => a.MemberId);

            builder.HasOne(a => a.MembershipPlans)
                   .WithMany(p => p.MemberAttendances)
                   .HasForeignKey(a => a.MemberPlansId);
        }
    }
}
