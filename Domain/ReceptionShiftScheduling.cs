namespace GymManagement.Domain
{
    public class ReceptionShiftScheduling : BaseEntity
    {
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }

        //Navigation

        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public int? WeekDaysId { get; set; }
        public WeekDays? WeekDays { get; set; }


    }
}
