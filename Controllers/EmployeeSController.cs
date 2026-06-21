using FluentValidation;
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
    public class EmployeeSController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IValidator<AddNewEmployeeDto> _validator;

        public EmployeeSController(IEmployeeService employeeService, IValidator<AddNewEmployeeDto> validator)
        {
            _employeeService = employeeService;
            _validator = validator;
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> AddNewEmployee(AddNewEmployeeDto newEmployeeDto)
        {

            var validationResult = await _validator.ValidateAsync(newEmployeeDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(
                    new
                    {
                        Errors = validationResult.Errors.Select(e =>
                        new
                        {
                            PropertyField = e.PropertyName,
                            Message = e.ErrorMessage,
                        })
                    });

            }

            var employee = await _employeeService.AddNewEmployee(newEmployeeDto);

            return CreatedAtAction(
                  nameof(GetEmployeeById),
                  new { id = employee.Id },
                  employee);


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
