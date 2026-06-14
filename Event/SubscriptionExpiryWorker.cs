using GymManagement.IServices;
using System.ComponentModel;

namespace GymManagement.Event
{
    public class SubscriptionExpiryWorker : BackgroundService
    {

        private readonly IServiceScopeFactory _serviceScope;
        private readonly IEventPublisher _eventPublisher;

        public SubscriptionExpiryWorker(IServiceScopeFactory serviceScope, IEventPublisher publishEvent)
        {
            _serviceScope = serviceScope;
            _eventPublisher = publishEvent;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceScope.CreateScope();

                var memberService = scope.ServiceProvider.GetRequiredService<IMemberService>();

                var soonToExpire = await memberService.GetMembersExpiringSoon();

                foreach (var member in soonToExpire.Data)
                {

                    var memberEvent = new SubscriptionAboutToExpireEvent
                    {
                        MemberId = member.Id,
                        MemberName = member.UserName,
                        MemberPhone=member.PhoneNumber,
                        AvailableDays = member.AvailableDays,
                        subscriptionEndDate = member.subscriptionEndDate

                    };

                  await  _eventPublisher.PublishAsync(memberEvent);
                }

                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            }
        }
    }
}
