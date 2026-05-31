using AutoMapper;
using GymManagement.Domain;
using GymManagement.Dtos;

namespace GymManagement.AutoMapper
{
    public class MemberProfile : Profile
    {
        public MemberProfile()
        {
            CreateMap<RegisterMemberDto, Member>();
            CreateMap<Member, MemberDto>().ForMember(dest => dest.MemberPlanName,
                                                    opt => opt.MapFrom(src => src.MembershipPlans.PlanName));
        }

    }
}
