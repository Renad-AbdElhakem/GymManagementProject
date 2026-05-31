using GymManagement.Data;
using GymManagement.Domain;

namespace GymManagement.IRepositories.Repositories
{
    public class MemberRepository : GeneralRepository<Member>, IMemberRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public MemberRepository(ApplicationDbContext dbContext):base(dbContext) 
        {
           _dbContext = dbContext;
        }
    }
}
