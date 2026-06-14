using System.ComponentModel.DataAnnotations;

namespace GymManagement.Dtos
{
    public class CreateMemberAttendanceDto
    {
        [Required]
        public int MemberId { get; set; }
        [Required]
        public int MemberPlansId { get; set; }
    
    }
}
