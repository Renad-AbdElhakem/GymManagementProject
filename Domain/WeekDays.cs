namespace GymManagement.Domain
{
    public class WeekDays:BaseEntity
    {
      
        public string DayName { get; set; }
      
        public ICollection<Scheduling> ?Schedulings { get; set; } = new List<Scheduling>();
       
    }
}
