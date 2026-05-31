using AutoMapper;
using GymManagement.Domain;
using GymManagement.Dtos;
using GymManagement.IRepositories;
using GymManagement.IRepositories.Repositories;

namespace GymManagement.IServices.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;
        private readonly ISubscriptionTypeService _subscriptionTypeService;

        public MemberService(IMemberRepository memberRepository, IMapper mapper,
            IRoleService roleService, ISubscriptionTypeService subscriptionTypeService)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
            _roleService = roleService;
            _subscriptionTypeService = subscriptionTypeService;
        }

        public async Task<GeneralResponse<MemberDto>> RegisterMemberService(RegisterMemberDto registerMemberDto)
        {
            var getRoleResult = await _roleService.GetRoleById(registerMemberDto.RoleId);
            if (!getRoleResult.Success)
                return GeneralResponse<MemberDto>.ErrorResponse($"Role with {registerMemberDto.RoleId} not found ");

            var getSubscriptionType = await _subscriptionTypeService.GetByIdAsync(registerMemberDto.MemberPlanId);
            if (!getSubscriptionType.Success)
                return GeneralResponse<MemberDto>.ErrorResponse($"SubscriptionType with {registerMemberDto.MemberPlanId} not found ");

            var startDate = DateOnly.FromDateTime(DateTime.Today);

            var newMember = _mapper.Map<Member>(registerMemberDto);
            newMember.AvailableDays = getSubscriptionType.Data.NumberOfDaysPerPlans;
            newMember.subscriptionStartDate = startDate;
            newMember.subscriptionEndDate = startDate.AddDays(getSubscriptionType.Data.NumberOfDaysPerPlans);

            await _memberRepository.CreateNewAsync(newMember);

            var member = _mapper.Map<MemberDto>(newMember);
            return GeneralResponse<MemberDto>.Succsess(member, "Member created successfully");
        }
        public async Task<GeneralResponse<MemberDto>> RenewMemberService(int memberId, RenewMemberDto renewMemberDto)
        {
            var startDate = DateOnly.FromDateTime(DateTime.Today);
            var findMember = await _memberRepository.GetTById(memberId);
            if (findMember == null)
                return GeneralResponse<MemberDto>.ErrorResponse($"Member with {memberId} not found ");

            if (findMember.AvailableDays > 0 || findMember.subscriptionEndDate > startDate)
                return GeneralResponse<MemberDto>.ErrorResponse($"Sorry, your current subscription has not expired yet ");

            var getSubscriptionType = await _subscriptionTypeService.GetByIdAsync(renewMemberDto.MemberPlanId);
            if (!getSubscriptionType.Success)
                return GeneralResponse<MemberDto>.ErrorResponse($"SubscriptionType with {renewMemberDto.MemberPlanId} not found ");



            findMember.MemberPlanId = getSubscriptionType.Data.Id;
            findMember.AvailableDays = getSubscriptionType.Data.NumberOfDaysPerPlans;
            findMember.subscriptionStartDate = startDate;
            findMember.subscriptionEndDate = startDate.AddDays(getSubscriptionType.Data.NumberOfDaysPerPlans);

            await _memberRepository.UpdateAsync(findMember);

            var member = _mapper.Map<MemberDto>(findMember);
            return GeneralResponse<MemberDto>.Succsess(member, "Member update successfully");
        }
        public async Task<GeneralResponse<MemberDto>> GetMemberByIdService(int memberId)
        {

            var findMember = await _memberRepository.GetTById(memberId, m => m.MembershipPlans);
            if (findMember == null)
                return GeneralResponse<MemberDto>.ErrorResponse($"Member with {memberId} not found ");

            var member = _mapper.Map<MemberDto>(findMember);
            return GeneralResponse<MemberDto>.Succsess(member);
        }
        public async Task<GeneralResponse<List<MemberDto>>> GetAllMembersService()
        {

            var findMembers = await _memberRepository.GetAll(m => m.MembershipPlans);
            if (findMembers == null)
                return GeneralResponse<List<MemberDto>>.ErrorResponse("No members found");

            var member = _mapper.Map<List<MemberDto>>(findMembers);
            return GeneralResponse<List<MemberDto>>.Succsess(member);
        }
        public async Task<GeneralResponse<MemberDto>> DeactivatedMemberByIdService(int memberId)
        {

            var findMember = await _memberRepository.GetTById(memberId);
            if (findMember == null)
                return GeneralResponse<MemberDto>.ErrorResponse($"Member with {memberId} not found ");
            findMember.IsActive = false;
            await _memberRepository.UpdateAsync(findMember);

            var member = _mapper.Map<MemberDto>(findMember);
            return GeneralResponse<MemberDto>.Succsess(member);
        }
















    }
}
