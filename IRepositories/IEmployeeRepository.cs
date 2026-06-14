using GymManagement.Domain;

namespace GymManagement.IRepositories
{
    public interface IEmployeeRepository:IGeneralRepository<Employee> 
    {
        Task<List<Employee>> GetAllEmployeeWithRole();
    }
}
