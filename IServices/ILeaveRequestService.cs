using GymManagement.Domain;
using GymManagement.Dtos;

namespace GymManagement.IServices
{
    public interface ILeaveRequestService
    {
        Task<string> CreateLeaveRequest(CreateLeaveRequestDto requestDto);

        Task<List<LeaveRequestDto>> GetMyLeaveRequests(int employeeId);

        Task<List<LeaveRequestDto>> GetAllLeaveRequests();

        Task<List<LeaveRequestDto>> GetPendingLeaveRequests();

        Task<string> ApproveLeaveRequest(int requestId, int approvedByUserId);

        Task<string> RejectLeaveRequest(int requestId, RejectLeaveRequestDto rejectLeaveRequest);


    }
}
