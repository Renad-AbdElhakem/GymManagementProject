namespace GymManagement.Dtos
{
    public class LeaveRequestDto
    {
        public int Id { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int TotalDays { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public string? RejectionReason { get; set; }
        public DateTime CreatedAt { get; set; }
        public string EmployeeName { get; set; }
        public string LeaveTypeName { get; set; }
    }
}
