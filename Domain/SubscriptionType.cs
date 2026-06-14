namespace GymManagement.Domain
{
    public class SubscriptionType : BaseEntity
    {

        public string PlanName { get; set; }

        public string Description { get; set; }
        public decimal PlansFee { get; set; }

        public int NumberOfDaysPerPlans { get; set; }

        //Navigation 
        public ICollection<Member>? Members { get; set; } = new List<Member>();
        public ICollection<MemberAttendance>? MemberAttendances { get; set; } = new List<MemberAttendance>();


    }
}
