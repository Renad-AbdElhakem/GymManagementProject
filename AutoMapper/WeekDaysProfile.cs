using AutoMapper;
using GymManagement.Domain;
using GymManagement.Dtos;

namespace GymManagement.AutoMapper
{
    public class WeekDaysProfile:Profile
    {
        public WeekDaysProfile()
        {

            CreateMap<CreateWeekDaysDto, WeekDays>();
            CreateMap<WeekDays, DaysDto>();
        }
    }
}
