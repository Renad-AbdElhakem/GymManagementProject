using GymManagement.Domain;

namespace GymManagement.Dtos
{
    public class CreateLeaveRequestDto
    {
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string Reason { get; set; } 
        public int EmployeeId { get; set; }
        public int LeaveTypeId { get; set; }
    }
}
