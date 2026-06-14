namespace GymManagement.Event
{
    public class SubscriptionAboutToExpireEvent
    {
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public string MemberPhone { get; set; }
        public DateOnly? subscriptionEndDate { get; set; }
        public int? AvailableDays { get; set; }
    }
}
