namespace GymManagement.Dtos
{
    public class SubscriptionTypeResponseDto
    {
        public int Id { get; set; }
        public string PlanName { get; set; }
        public string Description { get; set; }
        public decimal PlansFee { get; set; }
        public int NumberOfDaysPerPlans { get; set; }
    }
}
