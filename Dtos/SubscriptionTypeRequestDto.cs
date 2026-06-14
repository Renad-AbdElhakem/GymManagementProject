namespace GymManagement.Dtos
{
    public class SubscriptionTypeRequestDto
    {
        public string PlanName { get; set; }
        public string Description { get; set; }
        public decimal PlansFee { get; set; }
        public int NumberOfDaysPerPlans { get; set; }
    }
}
