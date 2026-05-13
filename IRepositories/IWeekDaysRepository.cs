using GymManagement.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GymManagement.IRepositories
{
    public interface IWeekDaysRepository :IGeneralRepository<WeekDays>
    {
        public Task<WeekDays?> SearchByName(string DayName, params Expression<Func<WeekDays, object>>[] includes);
        Task<List<WeekDays?>> SchedulingByDayName(string DayName, params Expression<Func<WeekDays, object>>[] includes);


    }
}
