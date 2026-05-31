namespace GymManagement.Dtos
{
    public class RegisterMemberDto
    {
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public bool? IsPrivateMember { get; set; } 
        public int MemberPlanId { get; set; }
        public int? PrivateTrainerId { get; set; }
        public int RoleId { get; set; }
    }
}
