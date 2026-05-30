using GymManagement.Domain;

namespace GymManagement.Dtos
{
    public class CreateReceptionShiftSchedulingDto
    {

        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }

        public int EmployeeId { get; set; }
        public string DayName { get; set; }

    }
}
