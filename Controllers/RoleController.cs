using GymManagement.Data;
using GymManagement.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public RoleController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }



        [HttpPost]
        public async Task<IActionResult> AddNewRole(string roleName) 
        {

            var newRole = new Role { RoleName = roleName };
            await _dbContext.Roles.AddAsync(newRole);
            await _dbContext.SaveChangesAsync();
            return Ok();
        
        }

    }
}
