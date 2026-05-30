using GymManagement.Domain;
using GymManagement.Dtos;
using System.Linq.Expressions;

namespace GymManagement.IServices
{
    public interface ISchedulingService
    {
        Task<GeneralResponse<SchedulingDto>> CreateTrainerDayScheduling(CreateSchedulingDto createSchedulingDto);
        Task<GeneralResponse<List<SchedulingDto>>> GetSchedulingByRoleId(int roleId);
        Task<GeneralResponse<SchedulingDto>> SchedullingById(int id);
        Task<GeneralResponse<List<SchedulingDto>>>SchedullingList();
        Task<GeneralResponse<SchedulingDto>> DeleteSchedullingById(int id);
        Task<GeneralResponse<List<SchedulingDto>>> SchedulingByDayName(string dayName,
                                                   params Expression<Func<WeekDays, object>>[] includes);

        Task<GeneralResponse<List<SchedulingDto>>> SchedulingByClassId(int classId, 
                                                   params Expression<Func<WeekDays, object>>[] includes);
        Task<GeneralResponse<SchedulingDto>> UpdateSchedullingById(int schedulingId, UpdateSchedulingDto updateSchedulingDto);

    }
}
