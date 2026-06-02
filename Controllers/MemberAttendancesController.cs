using GymManagement.Dtos;
using GymManagement.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberAttendancesController : ControllerBase
    {
        private readonly IMemberAttendanceService _memberAttendanceService;

        public MemberAttendancesController(IMemberAttendanceService memberAttendanceService)
        {
            _memberAttendanceService = memberAttendanceService;
        }



        [HttpPost]
        public async Task<ActionResult<AttendanceDto>> CreateAttendanceMember(CreateMemberAttendanceDto createMemberDto)
        {
            var newAttendance = await _memberAttendanceService.CreateMemberAttendanceAsync(createMemberDto);
            return newAttendance.Success ? Ok(newAttendance) : BadRequest(newAttendance);
        }
        [HttpGet("count/today")]
        public ActionResult<int> GetMemberCountToday()
        {
            var count = _memberAttendanceService.GetMemberCountToday();
            return Ok(new { date = DateOnly.FromDateTime(DateTime.Now), count });
        }

        [HttpGet("count/per-day")]
        public ActionResult<List<AttendancePerDayDto>> GetMemberCountPerDay()
        {
            var result = _memberAttendanceService.GetMemberCountPerDay();
            return Ok(result);
        }

        [HttpGet("profits/today")]
        public async Task<ActionResult<decimal>> GetTodayProfits()
        {
            var profits = await _memberAttendanceService.GetTodayProfits();
            return Ok(new { date = DateOnly.FromDateTime(DateTime.Now), profits });
        }

        [HttpGet("profits/by-day")]
        public async Task<ActionResult<decimal>> GetProfitsByDay([FromQuery] DateOnly date)
        {
            var profits = await _memberAttendanceService.GetProfitsByDay(date);
            return Ok(new { date, profits });
        }





    }
}
