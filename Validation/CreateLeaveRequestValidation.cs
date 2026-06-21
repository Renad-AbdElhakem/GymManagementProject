using FluentValidation;
using GymManagement.Data;
using GymManagement.Dtos;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Validation
{
    public class CreateLeaveRequestValidation : AbstractValidator<CreateLeaveRequestDto>
    {
        private readonly ApplicationDbContext _dbContext;

        public CreateLeaveRequestValidation(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(l => l.StartDate)
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now))
               .WithMessage("Start date must be today or a future date.");

            RuleFor(l => l.EndDate)
                   .GreaterThanOrEqualTo(l => l.StartDate)
                   .WithMessage("EndDate must be greater than StartDate");

            RuleFor(l => l.Reason).Length(30, 100);

            RuleFor(a => a)
                .MustAsync(IsEmployeeIdValid)
                .WithMessage("Employee not found ")
                .MustAsync(IsEmployeeActive)
                .WithMessage("Employee not active ");

            RuleFor(a => a)
                .MustAsync(IsLeaveTypeActive)
                .WithMessage("LeaveType not active ");


        }

        private async Task<bool> IsEmployeeActive(CreateLeaveRequestDto dto, CancellationToken token)
        {
            return await _dbContext.Employee.AnyAsync(e => e.Id == dto.EmployeeId &&  e.IsActive);
        }

        private async Task<bool> IsEmployeeIdValid(CreateLeaveRequestDto dto, CancellationToken token)
        {
            return await _dbContext.Employee.AnyAsync(e => e.Id == dto.EmployeeId);
        }

        private async Task<bool> IsLeaveTypeActive(CreateLeaveRequestDto dto, CancellationToken token)
        {
            return await _dbContext.LeaveTypes.AnyAsync(l => l.Id == dto.LeaveTypeId && l.IsActive);
        }


    }
}
