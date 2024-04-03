using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace Utility
{
    public class EmailSender : IEmailSender
    {
        public string SendGridSecret { get; set; }

        public EmailSender(IConfiguration _config)
        {
            SendGridSecret = _config.GetValue<string>("SendGrid:SecretKey");
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        /// <summary>
        /// {
        /// </summary>
            //logic to send email

            var client = new SendGridClient(SendGridSecret);

            var from = new EmailAddress("willffdunn@tropicbaygolf.com", "Tropic Bay Golf Club");
            var to = new EmailAddress(email);
            var message = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);

            return client.SendEmailAsync(message);


        }
    }
}