using GymManagement.Domain;

namespace GymManagement.Dtos
{
    public class CreateSchedulingDto
    {
        public string DayName{ get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public int EmployeeId { get; set; }
        public string ?ClassName { get; set; }
       
    }
}
