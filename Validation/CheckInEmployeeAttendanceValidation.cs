using FluentValidation;
using GymManagement.Data;
using GymManagement.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagement.Validation
{
    public class CheckInEmployeeAttendanceValidation : AbstractValidator<AttendanceRequestDto>
    {
        private readonly ApplicationDbContext _dbContext;

        public CheckInEmployeeAttendanceValidation(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;


            RuleFor(a => a)
                .MustAsync(IsEmployeeIdValid)
                .WithMessage("Employee not found ")
                .MustAsync(IsEmployeeActive)
                .WithMessage("Employee not active ");

        }

        private async Task<bool> IsEmployeeActive(AttendanceRequestDto dto, CancellationToken token)
        {
            return await _dbContext.Employee.AnyAsync(e => e.Id == dto.EmployeeId && e.IsActive);
        }

        private async Task<bool> IsEmployeeIdValid(AttendanceRequestDto dto, CancellationToken token)
        {
            return await _dbContext.Employee.AnyAsync(e => e.Id == dto.EmployeeId);
        }
    }
}
