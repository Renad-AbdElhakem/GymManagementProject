using GymManagement.Data;
using GymManagement.Dtos;
using GymManagement.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace GymManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        public async Task<ActionResult> AddNewEmployee(AddNewEmployeeDto newEmployeeDto)
        {

            var employee = await _employeeService.AddNewEmployee(newEmployeeDto);

            return CreatedAtAction(
                   nameof(GetEmployeeById),
                   new { id = employee.Id });


        }

      
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeById(int id)
        {
            var employee = await _employeeService.GetEmployeeById(id);
          
            if (employee == null)  
                return BadRequest($"Not found employee with Id: {id}"); 
           
            return Ok(employee);
        }

     
        [HttpGet]
        public async Task<ActionResult<List<EmployeeDto>>> GetAllEmployee()
        {

            var employeeList = await _employeeService.GetAllEmployee();
            return Ok(employeeList);

        }


    }
}
