namespace GymManagement.Domain
{
    public class Member : BaseUser
    {

        public int? AvailableDays { get; set; }
        public DateOnly? subscriptionStartDate { get; set; }
        public DateOnly? subscriptionEndDate { get; set; }

        public bool? IsActive { get; set; } = true;
        public bool? IsPrivateMember { get; set; } = false;
        //Navigation 
        public int? MemberPlanId { get; set; }
        public SubscriptionType? MembershipPlans { get; set; }

        public ICollection<MemberAttendance>? MemberAttendancecs { get; set; } = new List<MemberAttendance>();

        public int? PrivateTrainerId { get; set; }
        public Employee? Employee { get; set; }


    }
}
