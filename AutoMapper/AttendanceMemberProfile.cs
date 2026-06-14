using AutoMapper;
using GymManagement.Domain;
using GymManagement.Dtos;

namespace GymManagement.AutoMapper
{
    public class AttendanceMemberProfile : Profile
    {
        public AttendanceMemberProfile()
        {
            CreateMap<MemberAttendance, AttendanceDto>()
                .ForMember(dest => dest.MemberName, opt => opt.MapFrom(src => src.Member.UserName));

        }
    }
}
