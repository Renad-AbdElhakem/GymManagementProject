using GymManagement.Dtos;
using GymManagement.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MembersController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpPost]
        public async Task<ActionResult<MemberDto>> RegisterMember(RegisterMemberDto registerMemberDto)
        {
            var newMemberResult = await _memberService.RegisterMemberService(registerMemberDto);

            return newMemberResult.Success ? Ok(newMemberResult) : BadRequest(newMemberResult.Message);
        }

        [HttpPatch("{memberId}/subscription")]
        public async Task<ActionResult<MemberDto>> RenewMember( int memberId, [FromBody] RenewMemberDto renewMemberDto)
        {
            var updateMemberResult = await _memberService.RenewMemberService(memberId,renewMemberDto);

            return updateMemberResult.Success ? Ok(updateMemberResult) : NotFound(updateMemberResult);
        }

        [HttpPatch("{memberId}/deactivate")]
        public async Task<ActionResult> DeactivatedMemberById(int memberId)
        {
            var member = await _memberService.DeactivatedMemberByIdService(memberId);
            return member.Success ? NoContent() : NotFound(member.Errors);
        }

        [HttpGet("{memberId}")]
        public async Task<ActionResult<MemberDto>> GetMemberByID(int memberId)
        { 
        
            var member = await _memberService.GetMemberByIdService(memberId);
            return member.Success ? Ok(member) : NotFound(member.Errors);
        }

        [HttpGet]
        public async Task<ActionResult<List<MemberDto>>> GetAllMembers()
        {
            var member = await _memberService.GetAllMembersService();
            return member.Success ? Ok(member) : NotFound(member.Errors);
        }

        [HttpPatch("{memberId}/assign-trainer")] 
        public async Task<ActionResult> AssignTrainer(int memberId, [FromBody] AssignTrainerDto privateMemberDto)
        {
            var response = await _memberService.AssignPrivateTrainerAsync(memberId, privateMemberDto);
            return response.Success ? Ok(response) : BadRequest(response.Message);
            
        }


        [HttpPatch("available-days")]
        public async Task<IActionResult> UpdateAvailableDays([FromBody] UpdateAvailableDaysForMemberDto dto)
        {
            await _memberService.UpdateAvailableDaysAsync(dto);
            return Ok(new { message = $"Available days updated for member {dto.MemberId}" });
        }
    }
}
