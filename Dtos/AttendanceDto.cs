namespace GymManagement.Dtos
{
    public class AttendanceDto
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public int? MemberId { get; set; }
        public string MemberName { get; set; } = string.Empty;
        public int? MemberPlansId { get; set; }
   
    }
}
