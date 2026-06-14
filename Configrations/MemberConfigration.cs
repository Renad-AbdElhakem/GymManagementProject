using GymManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagement.Configrations
{
    public class MemberConfigration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.HasMany(m => m.MemberAttendancecs)
                   .WithOne(m => m.Member);

            builder.HasOne(p => p.MembershipPlans)
                   .WithMany(p => p.Members)
                   .HasForeignKey(p=>p.MemberPlanId);


            builder.HasOne(t => t.Employee)
                   .WithMany(e => e.PrivateMembers)
                   .HasForeignKey(m => m.PrivateTrainerId);
        }
    }
}
