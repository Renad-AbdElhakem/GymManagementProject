namespace GymManagement.Domain
{
    public class LeaveType : BaseEntity
    {
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public int MaxDaysPerYear { get; set; }

        public bool RequiresApproval { get; set; } = true;

        public bool IsActive { get; set; } = true;

        public ICollection<LeaveRequest>? LeaveRequests { get; set; } = new List<LeaveRequest>();
    }
}
