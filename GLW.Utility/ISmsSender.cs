using Microsoft.Extensions.Configuration;
using Twilio.Types;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Utility;

public interface ISmsSender
{
    Task SendSmsAsync(string number, string message);
}

public class SmsSender : ISmsSender
{
    private readonly IConfiguration _configuration;

    public SmsSender(IConfiguration configuration)
    {
        _configuration = configuration;
        var accountSid = _configuration["Twilio:AccountSID"];
        var authToken = _configuration["Twilio:AuthToken"];
        TwilioClient.Init(accountSid, authToken);
    }

    public async Task SendSmsAsync(string number, string message)
    {
        try { 
            var twilioNumber = _configuration["Twilio:PhoneNumber"];
            var messageOptions = new CreateMessageOptions(new PhoneNumber(number))
            {
                From = new PhoneNumber(twilioNumber),
                Body = message
            };

        await MessageResource.CreateAsync(messageOptions);
        }
        catch (Exception ex)
        {

            Console.WriteLine($"Failed to send message: {ex.Message}");

        }
    }
}
