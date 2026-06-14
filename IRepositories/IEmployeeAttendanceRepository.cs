using GymManagement.Domain;
using System.Linq.Expressions;

namespace GymManagement.IRepositories
{
    public interface IEmployeeAttendanceRepository : IGeneralRepository<EmployeeAttendance>
    {
        Task<EmployeeAttendance> EmployeeAttendanceByEmployeeIdAndDate(int employeeId, DateOnly date
                                                    , params Expression<Func<EmployeeAttendance, object>>[] includes);
    }
}
