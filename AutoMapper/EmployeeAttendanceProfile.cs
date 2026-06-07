using AutoMapper;
using GymManagement.Domain;
using GymManagement.Dtos;

namespace GymManagement.AutoMapper
{
    public class EmployeeAttendanceProfile : Profile
    {

        public EmployeeAttendanceProfile()
        {
            CreateMap<EmployeeAttendance, EmployeeAttendanceDto>()
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.UserName));
        }
    }
}
