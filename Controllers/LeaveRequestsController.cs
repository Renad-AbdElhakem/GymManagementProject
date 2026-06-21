using FluentValidation;
using GymManagement.Dtos;
using GymManagement.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestsController : ControllerBase
    {
        private readonly ILeaveRequestService _leaveRequestService;
        private readonly IValidator<CreateLeaveRequestDto> _validator;

        public LeaveRequestsController(ILeaveRequestService leaveRequestService, IValidator<CreateLeaveRequestDto> validator)
        {
            _leaveRequestService = leaveRequestService;
            _validator = validator;
        }


        [HttpPost]
        //[Authorize(Roles = "Employee")]
        public async Task<ActionResult<LeaveRequestDto>> CreateLeaveRequest([FromBody] CreateLeaveRequestDto requestDto)
        {
            var validationResult = await _validator.ValidateAsync(requestDto);
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
            var result = await _leaveRequestService.CreateLeaveRequest(requestDto);

            return result.Success ? Ok(result.Data) : BadRequest(result.Message);
        }

        [HttpGet("MyLeaveRequests")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> GetMyLeaveRequests(int employeeId)
        {
            var result = await _leaveRequestService.GetMyLeaveRequests(employeeId);
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllLeaveRequests()
        {
            var result = await _leaveRequestService.GetAllLeaveRequests();
            return Ok(result);
        }

        [HttpGet("pending")]
        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> GetPendingLeaveRequests()
        {
            var result = await _leaveRequestService.GetPendingLeaveRequests();
            return Ok(result);
        }

        [HttpPut("{id}/approve")]
        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> ApproveLeaveRequest(int id, [FromQuery] int approvedByUserId)
        {
            var result = await _leaveRequestService.ApproveLeaveRequest(id, approvedByUserId);
            return Ok(result);
        }

        [HttpPut("{id}/reject")]
        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> RejectLeaveRequest(int id, [FromBody] RejectLeaveRequestDto dto)
        {
            var result = await _leaveRequestService.RejectLeaveRequest(id, dto);
            return Ok(result);
        }
    }
}
