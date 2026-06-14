using GymManagement.Data;
using GymManagement.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GymManagement.IRepositories.Repositories
{
    public class WeekDaysRepository : GeneralRepository<WeekDays>, IWeekDaysRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public WeekDaysRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<WeekDays?> SearchByName(string DayName, params Expression<Func<WeekDays, object>>[] includes)
        {

            var query = _dbSet.AsQueryable();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(c => c.DayName.Contains(DayName));


        }
        public async Task<List<WeekDays?>> SchedulingByDayName(string DayName, params Expression<Func<WeekDays, object>>[] includes)
        {

            var query = _dbSet.AsQueryable();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.Where(c => c.DayName.Contains(DayName)).ToListAsync();


        }
    }
}
