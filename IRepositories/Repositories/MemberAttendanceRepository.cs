using GymManagement.Data;
using GymManagement.Domain;
using GymManagement.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GymManagement.IRepositories.Repositories
{
    public class MemberAttendanceRepository : GeneralRepository<MemberAttendance>, IMemberAttendanceRepository
    {
        public MemberAttendanceRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }



        public int GetAllCountMemberPerDay()
        {
            return _dbSet.Count(a => a.Date == DateOnly.FromDateTime(DateTime.Now));
        }

        public List<AttendancePerDayDto> GetMemberCountPerDay()
        {
            return _dbSet
                .GroupBy(a => a.Date)
                .Select(g => new AttendancePerDayDto
                {
                    Date = g.Key,
                    Count = g.Count()
                })
                .OrderBy(x => x.Date)
                .ToList();
        }

        // R
        public async Task<decimal> GetTodayProfits()
        {
           return  await _dbSet
            .Where(a => a.Date == DateOnly.FromDateTime(DateTime.Now) && a.MemberPlansId == 3)
            .Include(a => a.MembershipPlans)
            .SumAsync(a => a.MembershipPlans.PlansFee);
     
        }
        //R
        public async Task<decimal> GetProfitsByDay(DateOnly date)
        {
            return await _dbSet
                .Where(a => a.Date == date)
                .Include(a => a.MembershipPlans)
                .SumAsync(a => a.MembershipPlans.PlansFee);
        }
    }
}
