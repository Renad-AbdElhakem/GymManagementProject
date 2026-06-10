
using GymManagement.Helper;
using Microsoft.Extensions.Options;
using System.Runtime;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace GymManagement.IServices.SMS
{
    public class SmsService : ISmsService
    {
        private readonly TwilioSettings _settings;

        public SmsService(IOptions<TwilioSettings> settings)
        {
            _settings = settings.Value;
        }
        public  async Task SendAsync(string toNumber, string message)
        {
            TwilioClient.Init(_settings.AccountSID, _settings.AuthToken);

            await MessageResource.CreateAsync(
                body: message,
                from: new PhoneNumber(_settings.TwilioPhoneNumber),
                to: new PhoneNumber(toNumber)
            );
        }
    }
}
