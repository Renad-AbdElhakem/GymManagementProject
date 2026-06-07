using GymManagement.Dtos;

namespace GymManagement.IServices
{
    public interface IEmployeeAttendanceService
    {
        Task<string> CreateNewEmployeeAttendanceAsync(AttendanceRequestDto checkInDto);
        Task<string> CheckOutEmployeeAttendanceAsync(AttendanceRequestDto dto);

        Task<List<EmployeeAttendanceDto>> GetEmployeeAttendanceByDateAsync(DateOnly date);
       
        Task<List<EmployeeAttendanceDto>> GetAllEmployeeAttendanceAsync();
        

        Task<List<EmployeeAttendanceDto>> GetAllLateEmployeesAsync();
        

        Task<List<EmployeeAttendanceDto>> GetEmployeeAttendanceByEmployeeIdAsync(int employeeId);
        
    }
}
