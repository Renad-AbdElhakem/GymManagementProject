using AutoMapper;
using AutoMapper.Execution;
using GymManagement.Domain;
using GymManagement.Dtos;
using GymManagement.IRepositories;
using GymManagement.IRepositories.Repositories;
using System.Linq.Expressions;

namespace GymManagement.IServices.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;
        private readonly ISubscriptionTypeService _subscriptionTypeService;
        private readonly IEmployeeService _employeeService;

        public MemberService(IMemberRepository memberRepository, IMapper mapper,
            IRoleService roleService, ISubscriptionTypeService subscriptionTypeService, IEmployeeService employeeService)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
            _roleService = roleService;
            _subscriptionTypeService = subscriptionTypeService;
            _employeeService = employeeService;
        }

        public async Task<GeneralResponse<MemberDto>> RegisterMemberService(RegisterMemberDto registerMemberDto)
        {
            var getRoleResult = await _roleService.GetRoleById(registerMemberDto.RoleId);
            if (!getRoleResult.Success)
                return GeneralResponse<MemberDto>.ErrorResponse($"Role with {registerMemberDto.RoleId} not found ");

            var getSubscriptionType = await _subscriptionTypeService.GetByIdAsync(registerMemberDto.MemberPlanId);
            if (!getSubscriptionType.Success)
                return GeneralResponse<MemberDto>.ErrorResponse($"SubscriptionType with {registerMemberDto.MemberPlanId} not found ");

            if (registerMemberDto.PrivateTrainerId.HasValue)
            {
                var findTrainer = await _employeeService.GetEmployeeById(registerMemberDto.PrivateTrainerId.GetValueOrDefault());
                if (findTrainer == null || !findTrainer.IsActive)
                    return GeneralResponse<MemberDto>.ErrorResponse($"Trainer with ID {registerMemberDto.PrivateTrainerId} is not found or not active");

            }
            var startDate = DateOnly.FromDateTime(DateTime.Today);

            var newMember = _mapper.Map<Domain.Member>(registerMemberDto);
            //*************************
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

            var findMember = await _memberRepository.GetTById(memberId, x => x.MembershipPlans,
                                                                       x => x.Employee);
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

        public async Task<GeneralResponse<List<MemberDto>>> GetMembersExpiringSoon()
        {

            var findMembers = await _memberRepository.GetAllByCondition(m => m.subscriptionEndDate.HasValue &&
                                                     m.subscriptionEndDate.Value == DateOnly.FromDateTime(DateTime.Today.AddDays(7)) && m.AvailableDays > 0);
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



        public async Task<GeneralResponse<MemberDto>> AssignPrivateTrainerAsync(int memberId, AssignTrainerDto privateMemberDto)
        {

            var member = await _memberRepository.GetTById(memberId, x => x.MembershipPlans, x => x.Employee);

            if (member == null)
                return GeneralResponse<MemberDto>.ErrorResponse($"Member with ID {memberId} not found.");


            var trainer = await _employeeService.GetEmployeeById(privateMemberDto.TrainerId);

            if (trainer == null || !trainer.IsActive)
                return GeneralResponse<MemberDto>.ErrorResponse($"Trainer with ID {privateMemberDto.TrainerId} is not found or not active.");

            member.IsPrivateMember = true;
            member.PrivateTrainerId = privateMemberDto.TrainerId;

            await _memberRepository.UpdateAsync(member);

            var memberDto = _mapper.Map<MemberDto>(member);
            return GeneralResponse<MemberDto>.Succsess(memberDto);
        }


        // R   
        public async Task UpdateAvailableDaysAsync(UpdateAvailableDaysForMemberDto dto)
        {
            var member = await _memberRepository.GetTById(dto.MemberId);
            if (member is null) return;

            member.AvailableDays = dto.AvailableDays;
            await _memberRepository.UpdateAsync(member);

        }








    }
}
