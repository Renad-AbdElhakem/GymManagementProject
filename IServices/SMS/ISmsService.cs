using Twilio.TwiML.Messaging;

namespace GymManagement.IServices.SMS
{
    public interface ISmsService
    {
        Task SendAsync(string toNumber, string message);
    }
}
