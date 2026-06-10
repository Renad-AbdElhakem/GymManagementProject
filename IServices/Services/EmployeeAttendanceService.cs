using AutoMapper;
using GymManagement.Domain;
using GymManagement.Dtos;
using GymManagement.IRepositories;

namespace GymManagement.IServices.Services
{
    public class EmployeeAttendanceService : IEmployeeAttendanceService
    {
        private readonly IEmployeeAttendanceRepository _repo;
        private readonly ISchedulingService _schedulingService;
        private readonly IMapper _mapper;

        public EmployeeAttendanceService(IEmployeeAttendanceRepository repo, ISchedulingService schedulingService, IMapper mapper)
        {
            _repo = repo;
            _schedulingService = schedulingService;
            _mapper = mapper;
        }


        public async Task<string> CreateNewEmployeeAttendanceAsync(AttendanceRequestDto checkInDto)
        {
            var checkInTime = TimeOnly.FromDateTime(DateTime.Now);
            var date = DateOnly.FromDateTime(DateTime.Today);
            var day = date.DayOfWeek.ToString();

        
            var employeeSchedulling = await _schedulingService.SchedulingByEmployeeIdAndDayNameAsync(checkInDto.EmployeeId, day);
            if (!employeeSchedulling.Success)
                return employeeSchedulling.Message;

            var endOfDay = employeeSchedulling.Data.LastOrDefault();

            if (endOfDay.EndTime < checkInTime)
                return "the day already end ";

            var newEmployeeAttendance = new EmployeeAttendance
            {
                Date = date,
                ClockIn = checkInTime,
                IsLate = false,
                LateBy = TimeSpan.Zero,
                EmployeeId = checkInDto.EmployeeId,
            };

            var checkStartTime = employeeSchedulling.Data.FirstOrDefault();

            if (checkInTime > checkStartTime.StartTime.AddMinutes(10))
            {
                newEmployeeAttendance.Status = "Late";
                newEmployeeAttendance.IsLate = true;
                newEmployeeAttendance.LateBy = checkInTime - checkStartTime.StartTime;
            }

            await _repo.CreateNewAsync(newEmployeeAttendance);

            return "Attendance Created";

        }

        public async Task<string> CheckOutEmployeeAttendanceAsync(AttendanceRequestDto dto)
        {
            var checkOutTime = TimeOnly.FromDateTime(DateTime.Now);
            var date = DateOnly.FromDateTime(DateTime.Today);
            var day = date.DayOfWeek.ToString();

            var employeeAttendance = await _repo.EmployeeAttendanceByEmployeeIdAndDate(dto.EmployeeId, date, e => e.Employee);

            if (employeeAttendance == null)
                return $"No check-in record found for the employee with ID {dto.EmployeeId} today.";

            if (employeeAttendance.ClockOut != null)
                return $"Employee already clockout.!";

            employeeAttendance.ClockOut = checkOutTime;

            //1-
            var employeeSchedulling = await _schedulingService.SchedulingByEmployeeIdAndDayNameAsync(dto.EmployeeId, day);
            if (!employeeSchedulling.Success)
                return employeeSchedulling.Message;

            //2- 
            var lastEndTimeOntheDayScheduling = employeeSchedulling.Data.LastOrDefault();

            if (lastEndTimeOntheDayScheduling.EndTime.AddHours(1) < checkOutTime)
            {
                employeeAttendance.Status = "OverTime";
                var HourlyRate = employeeAttendance.Employee.Salary / 176;
                employeeAttendance.OvertimeHours = checkOutTime.ToTimeSpan() - lastEndTimeOntheDayScheduling.EndTime.ToTimeSpan();
                employeeAttendance.OvertimeBonus = (employeeAttendance.OvertimeHours.Value.Hours * 1.5m * HourlyRate);
            }
            employeeAttendance.Status = lastEndTimeOntheDayScheduling.EndTime > checkOutTime ? "HalfDay" : "Present";

            await _repo.UpdateAsync(employeeAttendance);

            return "Check-out completed successfully.";
        }


        public async Task<List<EmployeeAttendanceDto>> GetEmployeeAttendanceByDateAsync(DateOnly Date)
        {

            var attendanceByDayName = await _repo.GetAllByCondition(a => a.Date == Date, e => e.Employee);
            var attendanceByDayNameDto = _mapper.Map<List<EmployeeAttendanceDto>>(attendanceByDayName);
            return attendanceByDayNameDto;
        }
        public async Task<List<EmployeeAttendanceDto>> GetAllEmployeeAttendanceAsync()
        {
            var attendanceByDayName = await _repo.GetAll(e => e.Employee);
            var attendanceByDayNameDto = _mapper.Map<List<EmployeeAttendanceDto>>(attendanceByDayName);
            return attendanceByDayNameDto;
        }


        public async Task<List<EmployeeAttendanceDto>> GetEmployeeAttendanceByEmployeeIdAsync(int employeeId)
        {
            var attendanceByDayName = await _repo.GetAllByCondition(a => a.EmployeeId == employeeId, e => e.Employee);
            var attendanceByDayNameDto = _mapper.Map<List<EmployeeAttendanceDto>>(attendanceByDayName);
            return attendanceByDayNameDto;
        }

        public async Task<List<EmployeeAttendanceDto>> GetAllLateEmployeesAsync()
        {

            var attendanceByDayName = await _repo.GetAllByCondition(a => a.IsLate);
            var attendanceByDayNameDto = _mapper.Map<List<EmployeeAttendanceDto>>(attendanceByDayName);
            return attendanceByDayNameDto;

        }









    }
}
