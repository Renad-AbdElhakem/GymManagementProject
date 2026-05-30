namespace GymManagement.Domain
{
    public class Employee : BaseUser
    {
        public decimal Salary { get; set; }
        public bool IsActive { get; set; } = true;
        public DateOnly HireDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly ?ResignationDate { get; set; }
        
        //Navigation
       
        public ICollection<Course> ? Course { get; set; } = new List<Course>();
        public ICollection<EmployeeAttendance> ? EmployeeAttendancecs { get; set; } = new List<EmployeeAttendance>();
        public ICollection<Scheduling>? Schedulings { get; set; } = new List<Scheduling>();
       

    }
}
