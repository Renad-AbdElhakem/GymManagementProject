using GymManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagement.Configrations
{
    public class RoleConfigrations : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x => x.Id);
           
            builder.HasMany(r => r.BaseUsers)
                   .WithOne(r=>r.Role);
            
            builder.HasData(
                new Role
                {
                    Id = 1,
                    RoleName = "Manager",
                });
       
        
        }
    }
}
