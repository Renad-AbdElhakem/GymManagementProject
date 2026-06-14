using GymManagement.Data;
using GymManagement.Domain;

namespace GymManagement.IRepositories.Repositories
{
    public class LeaveRequestRepository : GeneralRepository<LeaveRequest>,ILeaveRequestRepository 
    {
        private readonly ApplicationDbContext _dbContext;

        public LeaveRequestRepository(ApplicationDbContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
