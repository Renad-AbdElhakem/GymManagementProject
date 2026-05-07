using AutoMapper;
using GymManagement.Domain;
using GymManagement.Dtos;
using GymManagement.IRepositories;

namespace GymManagement.IServices.Services
{
    public class EmployeesService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeesService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
       
        
        
        public async Task<EmployeeDto> AddNewEmployee(AddNewEmployeeDto newEmployeeDto)
        {

            var addNewEmployee = _mapper.Map<Employee>(newEmployeeDto);

            var createdEmployee = await _employeeRepository.CreateNewAsync(addNewEmployee);

            var employee = _mapper.Map<EmployeeDto>(createdEmployee);
       
            return employee;
       
        }

        public async Task<List<EmployeeDto>?> GetAllEmployee()
        {
            var employees = await _employeeRepository.GetAll();
           
            if (employees == null)
                return new List<EmployeeDto>();

            var emploueListAutoMapper=_mapper.Map<List<EmployeeDto>>(employees);

            return emploueListAutoMapper;
        }

        public async Task<EmployeeDto> GetEmployeeById(int id)
        {
            var getEmployee = await _employeeRepository.GetTById(id, e => e.Role);
           
            if (getEmployee == null) { return null; }
           
            var employee = _mapper.Map<EmployeeDto>(getEmployee);


            return employee;
        }
    }
}
