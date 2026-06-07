using System.ComponentModel.DataAnnotations;

namespace GymManagement.Dtos
{
    public class AttendanceRequestDto
    {
        [Required]
        public int EmployeeId { get; set; }
      
    }
}
