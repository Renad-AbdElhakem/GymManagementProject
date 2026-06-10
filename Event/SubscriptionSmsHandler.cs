
using GymManagement.IServices.SMS;

namespace GymManagement.Event
{
    public class SubscriptionSmsHandler : IEventHandler<SubscriptionAboutToExpireEvent>
    {
        private readonly ISmsService _smsService;

        public SubscriptionSmsHandler(ISmsService smsService)
        {
            _smsService = smsService;
        }
        public async Task HandleAsync(SubscriptionAboutToExpireEvent @event)
        {
            var message = $"Hi {@event.MemberName}, your gym subscription will expire on {@event.subscriptionEndDate:dd/MM/yyyy}. You have {@event.AvailableDays} days left. Make the most of it!";
            await _smsService.SendAsync(@event.MemberPhone, message);
        }
    
    }
}
