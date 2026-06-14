namespace GymManagement.Dtos
{
    public class EmployeeAttendanceDto
    {
        public DateOnly Date { get; set; }
        public string EmployeeName { get; set; }

        public TimeOnly? ClockIn { get; set; }
        public TimeOnly? ClockOut { get; set; }

        public bool IsLate { get; set; }

        public TimeSpan? LateBy { get; set; }
        public TimeSpan? OvertimeHours { get; set; } = new TimeSpan(0, 0, 0);

    }
}
