using GymManagement.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GymManagement.IRepositories
{
    public interface ISchedulingRepository : IGeneralRepository<Scheduling>
    {

         Task<List<Scheduling?>> SchedulingByDayId(int dayId, params Expression<Func<Scheduling, object>>[] includes);
        Task<List<Scheduling?>> SchedulingByClassId(int classId, params Expression<Func<Scheduling, object>>[] includes);
    }
}
