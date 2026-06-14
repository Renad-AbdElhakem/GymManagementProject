namespace GymManagement.Dtos
{
    public class UpdateSchedulingDto
    {
        public string? DayName { get; set; }
        public TimeOnly? StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
        public string? ClassName { get; set; }
    }
}
