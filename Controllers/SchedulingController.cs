using GymManagement.Dtos;
using GymManagement.IServices;
using GymManagement.IServices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulingController : ControllerBase
    {
        private readonly ISchedulingService _schedulingService;

        public SchedulingController(ISchedulingService schedulingService)
        {
            _schedulingService = schedulingService;
        }

        [HttpPost("TrainerSchedulling")]
        public async Task<ActionResult> CreateTrainerSchedulling(CreateSchedulingDto createSchedulingDto)
        {

            var schaduling = await _schedulingService.CreateTrainerDayScheduling(createSchedulingDto);
            return Ok(schaduling.Data);
        }

       

        [HttpGet]
        public async Task<ActionResult<List<SchedulingDto>>> GetAllSchedulingList()
        {
            var schedullingList = await _schedulingService.SchedullingList();

            if (!schedullingList.Success)
                return Ok(schedullingList.Message);

            return Ok(schedullingList.Data);

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<SchedulingDto>> GetSchedullingById(int id)
        {
            var findScheduling = await _schedulingService.SchedullingById(id);

            if (!findScheduling.Success)
                return NotFound(new { Message = findScheduling.Message });

            return Ok(findScheduling.Data);

        }
        [HttpGet("WeekDay/{dayName}")]
        public async Task<ActionResult<List<SchedulingDto>>> GetSchedullingByDayName(string dayName)
        {
            var findSchedulingList = await _schedulingService.SchedulingByDayName(dayName);

            if (!findSchedulingList.Success)
                return NotFound(new { Message = findSchedulingList.Message });

            return Ok(findSchedulingList.Data);

        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateSchedulingById( int id, [FromBody] UpdateSchedulingDto updateSchedulingDto)
        {
            var scheduling = await _schedulingService.UpdateSchedullingById(id, updateSchedulingDto);
           
            if (!scheduling.Success)
                return NotFound(new { Message = scheduling.Message });
           
            return Ok(scheduling.Data);
        }
       
        [HttpGet("scheduling/{roleId}")]
        public async Task<IActionResult> GetSchedulingByRoleId(int roleId)
        {
            var result = await _schedulingService.GetSchedulingByRoleId(roleId);
            if (!result.Success)
                return NotFound(result);
            return Ok(result);
        }

    }
}
