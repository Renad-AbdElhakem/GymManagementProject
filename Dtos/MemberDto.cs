namespace GymManagement.Dtos
{
    public class MemberDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public bool IsActive{ get; set; }
        public int? MemberPlanName { get; set; }
        public int? AvailableDays { get; set; }
        public DateOnly? subscriptionStartDate { get; set; }
        public DateOnly? subscriptionEndDate { get; set; }


    }
}
