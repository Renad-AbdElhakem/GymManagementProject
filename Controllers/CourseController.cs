using GymManagement.Domain;
using GymManagement.Dtos;
using GymManagement.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }


        [HttpPost]
        public async Task<ActionResult> CreateNewCourse(CreateCourseDto createCourseDto)
        {
            var result = await _courseService.CreateCourse(createCourseDto);
            if (!result.Success)
            {
                return BadRequest(new { message = result.Message });
            }

           return CreatedAtAction(
                   nameof(GetCourseById),
                   new { id = result.Data.Id });
        }


        [HttpPatch]
        public async Task<ActionResult> UpdateCourseWithAssignCoach(AssignCoachCourseDto assignCoach)
        {

            var updateCourse = await _courseService.UpdateCourseWithAssignCoah(assignCoach);

            if (!updateCourse.Success)
                return BadRequest(new { message = updateCourse.Message });

            return Ok(updateCourse);

        }


        [HttpGet]
        public async Task<ActionResult<List<CourseDto>>> GetAllCourse()
        {
            var course = await _courseService.GetAllCourse();

            if (!course.Success)
                return Ok(course.Message);

            return Ok(course.Data);

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> GetCourseById(int id)
        {
            var findCourse = await _courseService.GetCourseById(id);

            if (!findCourse.Success)
                return NotFound(new { Message = findCourse.Message });
            
            return Ok(findCourse.Data);

        }



    }
}
