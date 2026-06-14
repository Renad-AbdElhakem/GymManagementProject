using FluentValidation;
using GymManagement.Data;
using GymManagement.Dtos;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Validation
{
    public class CreateMemberAttendanceValidation : AbstractValidator<CreateMemberAttendanceDto>
    {
        private readonly ApplicationDbContext _dbContext;

        public CreateMemberAttendanceValidation(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(s => s)
              .MustAsync(IsMemberAssignedToPlan)
              .WithMessage("The member is not assigned to this plan.");
        }

        private async Task<bool> IsMemberAssignedToPlan(CreateMemberAttendanceDto schedulingDto, CancellationToken token)
        {

            return await _dbContext.Members.AnyAsync(m => m.Id == schedulingDto.MemberId 
                                        && m.MemberPlanId == schedulingDto.MemberPlansId);
        }
    }
}
