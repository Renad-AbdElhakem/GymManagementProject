namespace GymManagement.Dtos
{
    public class ReceptionShiftSchedulingDto
    {
        public int Id { get; set; }
        public string DayName { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        
    }
}
