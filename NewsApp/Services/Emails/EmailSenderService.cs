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
        public async Task SendEmailAsync(string replyToMail, string replyToName, string userId)
        {

            var apiKey = config["SendGridApiKey"];
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("hkrasimir53@gmail.com", "Krasimir"),
                Subject = "Confirm account",
                HtmlContent = $@"<p>Hi {replyToName}</p>
                                 <p>Thanks for registering to our website!</p>
                                  <p>In order to verify you account, please click <a href=""https://localhost:7017/users/confirm/{userId}"">here</a></p>"
            };
            msg.AddTo(new EmailAddress(replyToMail, replyToName));
            var response = await client.SendEmailAsync(msg);
        }
    }
}
