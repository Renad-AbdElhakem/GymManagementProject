namespace GymManagement.Domain
{
    public class LeaveRequest : BaseEntity
    {

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public int TotalDays { get; set; }

        public string Reason { get; set; } = null!;

        public string Status { get; set; } = "Pending";

        public int? ApprovedByUserId { get; set; }

        public DateTime? ApprovedAt { get; set; }

        public string? RejectionReason { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }


        //Navigation 

        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        public int? LeaveTypeId { get; set; }
        public LeaveType? LeaveType { get; set; }

    }
}
