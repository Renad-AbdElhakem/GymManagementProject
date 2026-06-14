using GymManagement.Domain;
using GymManagement.Dtos;

namespace GymManagement.IServices
{
    public interface IMemberAttendanceService
    {
        Task<GeneralResponse<AttendanceDto>> CreateMemberAttendanceAsync(CreateMemberAttendanceDto memberAttendanceDto);
        int GetMemberCountToday();
        List<AttendancePerDayDto> GetMemberCountPerDay();
        Task<decimal> GetTodayProfits();
        Task<decimal> GetProfitsByDay(DateOnly date);
        


    }
}
