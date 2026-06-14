using GymManagement.Data;
using GymManagement.Domain;

namespace GymManagement.IRepositories.Repositories
{
    public class LeaveTypeRepository:GeneralRepository<LeaveType>, ILeaveTypeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public LeaveTypeRepository(ApplicationDbContext dbContext):base(dbContext)
        {
           _dbContext = dbContext;
        }
    }
}
