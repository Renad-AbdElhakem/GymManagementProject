namespace GymManagement.Domain
{
    public class Course:BaseEntity
    {
      
        public string CourseName { get; set; }
        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public ICollection<Scheduling> ?Schedulings { get; set; } = new List<Scheduling>();


    }
}
