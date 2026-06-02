namespace GymManagement.Domain
{
    public class MemberAttendance:BaseEntity
    {

       
        public DateOnly Date { get; set; }


        //Navigation

        public int? MemberId { get; set; }
        public Member? Member { get; set; }

        public int? MemberPlansId { get; set; }
        public SubscriptionType? MembershipPlans { get; set; }

   
    }
}

