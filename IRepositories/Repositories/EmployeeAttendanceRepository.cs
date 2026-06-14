using GymManagement.Data;
using GymManagement.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GymManagement.IRepositories.Repositories
{
    public class EmployeeAttendanceRepository : GeneralRepository<EmployeeAttendance>, IEmployeeAttendanceRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EmployeeAttendanceRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<EmployeeAttendance> EmployeeAttendanceByEmployeeIdAndDate(int employeeId, DateOnly date
                                                    , params Expression<Func<EmployeeAttendance, object>>[] includes)
        {

            var query = _dbSet.AsQueryable();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(s => s.EmployeeId == employeeId && s.Date == date);

        }
    }
}
