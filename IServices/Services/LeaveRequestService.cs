using AutoMapper;
using GymManagement.Domain;
using GymManagement.Dtos;
using GymManagement.IRepositories;

namespace GymManagement.IServices.Services
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public LeaveRequestService(ILeaveRequestRepository leaveRequestRepository, IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
            _leaveTypeRepository = leaveTypeRepository;
        }


        public async Task<string> CreateLeaveRequest(CreateLeaveRequestDto requestDto)
        {
            var overlappingLeaveRequests = await _leaveRequestRepository.GetAllByCondition(l => l.EmployeeId == requestDto.EmployeeId
                                                                                           && l.StartDate <= requestDto.EndDate
                                                                                           && l.EndDate >= requestDto.StartDate
                                                                                           , l => l.LeaveType);

            if (overlappingLeaveRequests != null)
                return "You already have a leave request during the selected period.";

            var leaveType = await _leaveTypeRepository.GetTById(requestDto.LeaveTypeId);

            var employeeLeaveRequests = await _leaveRequestRepository.GetAllByCondition(l => l.EmployeeId == requestDto.EmployeeId
                                                                                     && l.LeaveTypeId == requestDto.LeaveTypeId
                                                                                     && l.StartDate.Year == DateTime.Now.Year);


            var newLeaveRequest = _mapper.Map<LeaveRequest>(requestDto);

            var totalDaysAfterRequest = employeeLeaveRequests.Sum(t => t.TotalDays) + newLeaveRequest.TotalDays;

            if (newLeaveRequest.TotalDays > leaveType.MaxDaysPerYear || totalDaysAfterRequest > leaveType.MaxDaysPerYear)
                return "You have exceeded the maximum allowed days for this leave type.";


            await _leaveRequestRepository.CreateNewAsync(newLeaveRequest);
            return "Leave request created successfully.";

        }


        public async Task<List<LeaveRequestDto>> GetMyLeaveRequests(int employeeId)
        {
            var leaveRequests = await _leaveRequestRepository.GetAllByCondition(
                l => l.EmployeeId == employeeId,
                l => l.LeaveType);

            return _mapper.Map<List<LeaveRequestDto>>(leaveRequests);
        }

        public async Task<List<LeaveRequestDto>> GetAllLeaveRequests()
        {
            var leaveRequests = await _leaveRequestRepository.GetAll();

            return _mapper.Map<List<LeaveRequestDto>>(leaveRequests);
        }
        public async Task<List<LeaveRequestDto>> GetPendingLeaveRequests()
        {
            var leaveRequests = await _leaveRequestRepository.GetAllByCondition(
                l => l.Status == "Pending",
                l => l.LeaveType,
                l => l.Employee);

            return _mapper.Map<List<LeaveRequestDto>>(leaveRequests);
        }

        public async Task<string> ApproveLeaveRequest(int requestId, int approvedByUserId)
        {
            var leaveRequest = await _leaveRequestRepository.GetTById(requestId);
            if (leaveRequest == null)
                return "Leave request not found.";

            if (leaveRequest.Status != "Pending")
                return "Only pending requests can be approved.";

            leaveRequest.Status = "Approved";
            leaveRequest.ApprovedByUserId = approvedByUserId;
            leaveRequest.ApprovedAt = DateTime.Now;
            leaveRequest.UpdatedAt = DateTime.Now;

            await _leaveRequestRepository.UpdateAsync(leaveRequest);
            return "Leave request approved successfully.";
        }

        public async Task<string> RejectLeaveRequest(int requestId, RejectLeaveRequestDto rejectLeaveRequest)
        {
            var leaveRequest = await _leaveRequestRepository.GetTById(requestId);
            if (leaveRequest == null)
                return "Leave request not found.";

            if (leaveRequest.Status != "Pending")
                return "Only pending requests can be rejected.";

            leaveRequest.Status = "Rejected";
            leaveRequest.ApprovedByUserId = rejectLeaveRequest.ApprovedByUserId;
            leaveRequest.RejectionReason = rejectLeaveRequest.RejectionReason ;
            leaveRequest.UpdatedAt = DateTime.Now;

            await _leaveRequestRepository.UpdateAsync(leaveRequest);
            return "Leave request rejected successfully.";
        }


    }
}
