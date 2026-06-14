using AutoMapper;
using GymManagement.Domain;
using GymManagement.Dtos;

namespace GymManagement.AutoMapper
{
    public class LeaveRequestProfile : Profile
    {
        public LeaveRequestProfile()
        {
            CreateMap<CreateLeaveRequestDto, LeaveRequest>().ForMember(dest => dest.TotalDays,
                otp => otp.MapFrom(src => src.EndDate.DayNumber - src.StartDate.DayNumber + 1));


            CreateMap<LeaveRequest, LeaveRequestDto>()
                         .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.UserName))
                         .ForMember(dest => dest.LeaveTypeName, opt => opt.MapFrom(src => src.LeaveType.Name));

        }
    }
}
