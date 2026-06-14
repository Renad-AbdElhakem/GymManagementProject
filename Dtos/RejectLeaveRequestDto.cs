namespace GymManagement.Dtos
{
    public class RejectLeaveRequestDto
    {
        public int ApprovedByUserId { get; set; }
        public string RejectionReason { get; set; }
    }
}
