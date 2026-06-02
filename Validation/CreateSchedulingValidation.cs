using FluentValidation;
using GymManagement.Data;
using GymManagement.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace GymManagement.Validation
{
    public class CreateSchedulingValidation : AbstractValidator<CreateSchedulingDto>
    {
        private readonly ApplicationDbContext _dbContext;

        public CreateSchedulingValidation(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(s => s.EndTime)
                .CustomAsync(IsTimeRangeValid);


            RuleFor(s => s)
                .MustAsync(OverLappingTrainerScheduling)
                .WithMessage("OverLapping Trainer Scheduling");

            RuleFor(s => s)
                .MustAsync(IsTrainerAssignedToClass)
                .WithMessage("the Trainer not assigned to this Class");
        }
        //R
        private async Task IsTimeRangeValid(TimeOnly only, ValidationContext<CreateSchedulingDto> context, CancellationToken token)
        {
            if (context.InstanceToValidate.StartTime > context.InstanceToValidate.EndTime)
                context.AddFailure("EndTime can not be before StartTime");
        }

        private async Task<bool> IsTrainerAssignedToClass(CreateSchedulingDto schedulingDto, CancellationToken token)
        {

            return await _dbContext.Employee.Include(c => c.Course)
                                               .AnyAsync(t => t.Id == schedulingDto.EmployeeId
                                                && t.Course.Any(c => c.CourseName.Contains(schedulingDto.ClassName)));
        }
        private async Task<bool> OverLappingTrainerScheduling(CreateSchedulingDto schedulingDto, CancellationToken token)
        {

            return await _dbContext.Schedulings.Where(s => s.EmployeeId == schedulingDto.EmployeeId)
                                                  .Include(d => d.WeekDays)
                                                  .AnyAsync(s => s.WeekDays.DayName == schedulingDto.DayName
                                             && !(s.EndTime > schedulingDto.StartTime && s.StartTime < schedulingDto.EndTime));

        }
    }
}
