namespace GymManagement.Domain
{
    public class EmployeeAttendance
    {
        public int Id { get; set; }

        public DateOnly Date { get; set; }

        public TimeOnly? ClockIn { get; set; }
        public TimeOnly? ClockOut { get; set; }

        public TimeSpan? TotalHours => ClockOut - ClockIn;

        public string? Status { get; set; } = "Absent";

        public string? Notes { get; set; }

        public bool IsLate { get; set; }

        public TimeSpan? LateBy { get; set; }
        public TimeSpan? OvertimeHours { get; set; } = new TimeSpan(0, 0, 0);

        public decimal? LatePenalty { get; set; }
        public decimal? OvertimeBonus { get; set; }


        //Navigation

        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }

    }
}
