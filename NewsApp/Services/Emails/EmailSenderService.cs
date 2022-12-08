using SendGrid.Helpers.Mail;
using SendGrid;

namespace NewsApp.Services.Emails
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly IConfiguration config;

        public EmailSenderService(IConfiguration config)
        {
            this.config = config;
        }
        public async Task SendEmailAsync(string replyToMail, string replyToName)
        {

            var apiKey = config["SendGridApiKey"];
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("hkrasimir53@gmail.com", "Krasimir Hristov"),
                Subject = "Your registration to our website was successful",
                PlainTextContent = "Thank you! :)"
            };
            msg.AddTo(new EmailAddress(replyToMail, replyToName));
            var response = await client.SendEmailAsync(msg);
        }
    }
}
