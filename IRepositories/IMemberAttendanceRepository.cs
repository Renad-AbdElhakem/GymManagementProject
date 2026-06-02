using GymManagement.Domain;
using GymManagement.Dtos;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.IRepositories
{
    public interface IMemberAttendanceRepository : IGeneralRepository<MemberAttendance>
    {
        public int GetAllCountMemberPerDay();
        public List<AttendancePerDayDto> GetMemberCountPerDay();
        Task<decimal> GetTodayProfits();
        Task<decimal> GetProfitsByDay(DateOnly date);

    }
}