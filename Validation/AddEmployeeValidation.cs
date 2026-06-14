using FluentValidation;
using GymManagement.Data;
using GymManagement.Dtos;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Validation
{
    public class AddEmployeeValidation : AbstractValidator<AddNewEmployeeDto>
    {
        private readonly ApplicationDbContext _context;

        public AddEmployeeValidation(ApplicationDbContext context)
        {
            _context = context;

            RuleFor(e => e.UserName)
                    .NotEmpty().WithMessage("First Name is required")
                    .Length(2, 50).WithMessage("First Name must be between 2 and 50 characters")
                    .Matches(@"^[a-zA-Z\s]+$").WithMessage("First Name can only contain letters and spaces");

            RuleFor(e => e.HireDate)
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
                .WithMessage("Hire date cannot be in the future")
                .GreaterThanOrEqualTo(new DateOnly(1990,1,1)).WithMessage("Hire date seems to be too old");


            RuleFor(e => e.RoleId)
                .MustAsync(RoleExistsAsync).WithMessage("Role does not exist");
        }
        private async Task<bool> RoleExistsAsync(int roleId, CancellationToken token)
        {
            return await _context.Roles.AnyAsync(e => e.Id == roleId, token);
        }
    }
}
