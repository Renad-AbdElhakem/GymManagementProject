using GymManagement.Data;
using GymManagement.Domain;

namespace GymManagement.IRepositories.Repositories
{
    public class EmployeeRepository : GeneralRepository<Employee> , IEmployeeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EmployeeRepository(ApplicationDbContext dbContext):base(dbContext)
        {
           _dbContext = dbContext;
        }
    }
}
