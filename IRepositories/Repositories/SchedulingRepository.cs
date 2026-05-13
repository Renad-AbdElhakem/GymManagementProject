using GymManagement.Data;
using GymManagement.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GymManagement.IRepositories.Repositories
{
    public class SchedulingRepository : GeneralRepository<Scheduling>, ISchedulingRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SchedulingRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Scheduling?>> SchedulingByDayId(int dayId,params Expression<Func<Scheduling, object>>[] includes)
                                                                             
        {

            var query = _dbSet.AsQueryable();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

           return await query.Where(s=>s.WeekDaysId==dayId).ToListAsync();
           


        }
        public async Task<List<Scheduling?>> SchedulingByClassId(int classId,params Expression<Func<Scheduling, object>>[] includes)
                                                                             
        {

            var query = _dbSet.AsQueryable();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

           return await query.Where(s=>s.CourseId==classId).ToListAsync();
           


        }
        
    }
}
