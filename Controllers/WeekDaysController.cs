using GymManagement.Dtos;
using GymManagement.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeekDaysController : ControllerBase
    {
        private readonly IWeekDaysService _weekDaysService;

        public WeekDaysController(IWeekDaysService weekDaysService)
        {
            _weekDaysService = weekDaysService;
        }


        [HttpPost]
        public async Task<ActionResult> CreateNewDay(CreateWeekDaysDto createDayDto)
        {
            var newDay = await _weekDaysService.CreateNewDay(createDayDto);
            return Ok(newDay);
        }




    }
}
