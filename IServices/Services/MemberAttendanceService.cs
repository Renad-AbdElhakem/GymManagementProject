using AutoMapper;
using GymManagement.Domain;
using GymManagement.Dtos;
using GymManagement.IRepositories;

namespace GymManagement.IServices.Services
{
    public class MemberAttendanceService : IMemberAttendanceService
    {
        private readonly IMemberAttendanceRepository _memberAttendanceRepo;
        private readonly IMemberService? _memberService;
        private readonly IMapper _mapper;

        public MemberAttendanceService(IMemberAttendanceRepository memberAttendanceRepo, IMemberService memberService
            , IMapper mapper)
        {
            _memberAttendanceRepo = memberAttendanceRepo;
            _memberService = memberService;
            _mapper = mapper;
        }




        public async Task<GeneralResponse<AttendanceDto>> CreateMemberAttendanceAsync(CreateMemberAttendanceDto memberAttendanceDto)
        {

            var date = DateOnly.FromDateTime(DateTime.Now);

            var member = await _memberService.GetMemberByIdService(memberAttendanceDto.MemberId);
            if (!member.Success)
                return GeneralResponse<AttendanceDto>.ErrorResponse($"Member with ID {memberAttendanceDto.MemberId} is not found");

            if (member.Data.AvailableDays <= 0)
                return GeneralResponse<AttendanceDto>.ErrorResponse("Member has no remaining sessions.");

            if (member.Data.subscriptionEndDate < date)
                return GeneralResponse<AttendanceDto>.ErrorResponse("Member subscription has expired.");

            if (!member.Data.IsActive)
                return GeneralResponse<AttendanceDto>.ErrorResponse("Member account is currently inactive.");
           
            
            --member.Data.AvailableDays;
            await _memberService.UpdateAvailableDaysAsync(new UpdateAvailableDaysForMemberDto
            {
                MemberId = memberAttendanceDto.MemberId,
                AvailableDays = member.Data.AvailableDays.GetValueOrDefault()
            });

            var newAttendance = new MemberAttendance
            {
                Date = date,
                MemberId = memberAttendanceDto.MemberId,
                MemberPlansId = memberAttendanceDto.MemberPlansId,
            };

            await _memberAttendanceRepo.CreateNewAsync(newAttendance);

            var attendanceDto = _mapper.Map<AttendanceDto>(newAttendance);
            return GeneralResponse<AttendanceDto>.Succsess(attendanceDto, "Attendance created");

        }


        public int GetMemberCountToday()
        {
            return _memberAttendanceRepo.GetAllCountMemberPerDay();
        }

        public List<AttendancePerDayDto> GetMemberCountPerDay()
        {
            return _memberAttendanceRepo.GetMemberCountPerDay();
        }

        public Task<decimal> GetTodayProfits()
        {
            return _memberAttendanceRepo.GetTodayProfits();
        }

        public Task<decimal> GetProfitsByDay(DateOnly date)
        {
            return _memberAttendanceRepo.GetProfitsByDay(date);
        }




    }
}
