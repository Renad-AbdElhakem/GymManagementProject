namespace GymManagement.Domain
{
    public class WeekDays
    {
        public int Id { get; set; }
        public string DayName { get; set; }
        public DateOnly DateOnly { get; set; }

        public ICollection<Scheduling> ?Schedulings { get; set; } = new List<Scheduling>();

    }
}
