using FluentValidation;
using GymManagement.Dtos;
using GymManagement.IRepositories;
using GymManagement.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeAttendancesController : ControllerBase
    {
        private readonly IEmployeeAttendanceService _attendanceService;
        private readonly IValidator<AttendanceRequestDto> _validator;

        public EmployeeAttendancesController(IEmployeeAttendanceService attendanceService,
                                            IValidator<AttendanceRequestDto> validator)
        {
            _attendanceService = attendanceService;
            _validator = validator;
        }


        [HttpPost("Check-In")]
        public async Task<ActionResult> CreateNewEmployeeAttendance(AttendanceRequestDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(
                    new
                    {
                        Errors = validationResult.Errors.Select(e =>
                        new
                        {
                            Message = e.ErrorMessage,
                        })
                    });

            }
            var result = await _attendanceService.CreateNewEmployeeAttendanceAsync(dto);
            return Ok(new { message = $"{result}" });
        }

        [HttpPatch("Check-Out")]
        public async Task<ActionResult> CheckOutEmployeeAttendance(AttendanceRequestDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(
                    new
                    {
                        Errors = validationResult.Errors.Select(e =>
                        new
                        {
                            Message = e.ErrorMessage,
                        })
                    });

            }
            var result = await _attendanceService.CheckOutEmployeeAttendanceAsync(dto);
            return Ok(new { message = $"{result}" });
        }


        [HttpGet]
        public async Task<ActionResult<List<EmployeeAttendanceDto>>> GetAllEmployeeAttendance()
        {
            var result = await _attendanceService.GetAllEmployeeAttendanceAsync();
            return Ok(result);
        }

        [HttpGet("late-employees")]
        public async Task<ActionResult<List<EmployeeAttendanceDto>>> GetAllLateEmployees()
        {
            var result = await _attendanceService.GetAllLateEmployeesAsync();
            return Ok(result);
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<ActionResult<List<EmployeeAttendanceDto>>> GetEmployeeAttendanceByEmployeeId(int employeeId)
        {
            var result = await _attendanceService.GetEmployeeAttendanceByEmployeeIdAsync(employeeId);
            return Ok(result);
        }

        [HttpGet("day/{dayName}")]
        public async Task<ActionResult<List<EmployeeAttendanceDto>>> GetEmployeeAttendanceByDate(DateOnly dayName)
        {
            var result = await _attendanceService.GetEmployeeAttendanceByDateAsync(dayName);
            return Ok(result);
        }


    }
}
