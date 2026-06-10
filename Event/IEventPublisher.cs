namespace GymManagement.Event
{
    public interface IEventPublisher
    {
        Task PublishAsync<TEvent>(TEvent @event);
    }
}
