using GymManagement.Dtos;

namespace GymManagement.IServices
{
    public interface IEmployeeService
    {
        public Task<EmployeeDto> AddNewEmployee(AddNewEmployeeDto newEmployeeDto);

        public Task<EmployeeDto> GetEmployeeById(int id);


        public Task<List<EmployeeDto>?> GetAllEmployee();
   
    }
}
