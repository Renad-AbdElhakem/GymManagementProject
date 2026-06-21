using GymManagement.Data;
using GymManagement.Domain;
using GymManagement.Dtos;
using GymManagement.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleDto createRoleDto)
        {
            var result = await _roleService.CreateRole(createRoleDto);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var result = await _roleService.GetRoleById(id);
            if (!result.Success)
                return NotFound(result);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var result = await _roleService.GetAllRoles();
            return Ok(result);
        }
    }


}
