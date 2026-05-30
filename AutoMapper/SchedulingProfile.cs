using AutoMapper;
using GymManagement.Domain;
using GymManagement.Dtos;

namespace GymManagement.AutoMapper
{
    public class SchedulingProfile : Profile
    {

        public SchedulingProfile()
        {
            CreateMap<Scheduling, SchedulingDto>()
                    .ForMember(dest => dest.DayName,
                       opt => opt.MapFrom(src => src.WeekDays.DayName))

                   .ForMember(dest => dest.ClassName,
                      opt => opt.MapFrom(src => src.Course.CourseName));

            CreateMap<Scheduling, ReceptionShiftSchedulingDto>()
               .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.UserName))
               .ForMember(dest => dest.DayName, opt => opt.MapFrom(src => src.WeekDays.DayName));
        }
    }
}
