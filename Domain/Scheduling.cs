namespace GymManagement.Domain
{
    public class Scheduling
    {
        public int Id { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }

        //Navigation

        public int ?EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public int? WeekDaysId { get; set; }
        public WeekDays ?WeekDays { get; set; }

        public int? CourseId { get; set; }
        public Course? Course { get; set; }  
    }
}
