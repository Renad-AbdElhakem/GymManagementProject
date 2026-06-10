using GymManagement.Domain;
using GymManagement.Dtos;

namespace GymManagement.IServices
{
    public interface IMemberService
    {
        Task<GeneralResponse<MemberDto>> RegisterMemberService(RegisterMemberDto registerMemberDto);
        Task<GeneralResponse<MemberDto>> RenewMemberService(int memberId , RenewMemberDto renewMemberDto);
        Task<GeneralResponse<MemberDto>> GetMemberByIdService(int memberId);
        Task<GeneralResponse<List<MemberDto>>> GetAllMembersService();
        Task<GeneralResponse<List<MemberDto>>> GetMembersExpiringSoon();
        Task<GeneralResponse<MemberDto>> DeactivatedMemberByIdService(int memberId);
        Task<GeneralResponse<MemberDto>> AssignPrivateTrainerAsync(int memberId, AssignTrainerDto privateMemberDto);
        Task UpdateAvailableDaysAsync(UpdateAvailableDaysForMemberDto dto);


    }
}
