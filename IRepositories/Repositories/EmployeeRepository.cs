using GymManagement.Data;
using GymManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.IRepositories.Repositories
{
    public class EmployeeRepository : GeneralRepository<Employee> , IEmployeeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EmployeeRepository(ApplicationDbContext dbContext):base(dbContext)
        {
           _dbContext = dbContext;
        }

        public async Task<List<Employee>> GetAllEmployeeWithRole()
        {
            var query = _dbSet.AsQueryable();
            query = query.Include(e => e.Role);
            return await query.ToListAsync();
        }
    }
}
