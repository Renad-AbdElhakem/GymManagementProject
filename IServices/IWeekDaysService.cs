using GymManagement.Domain;
using GymManagement.Dtos;

namespace GymManagement.IServices
{
    public interface IWeekDaysService
    {
        Task<GeneralResponse<DaysDto>> CreateNewDay(CreateWeekDaysDto createDayDto);
        Task<GeneralResponse<DaysDto>> FindDayByName(string dayName);
    }
}
