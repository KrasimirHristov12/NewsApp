using SendGrid.Helpers.Mail;
using SendGrid;

namespace NewsApp.Services.Emails
{
    public class EmailSenderService : IEmailSenderService
    {
        public async Task SendEmailAsync(string replyToMail, string replyToName)
        {

            var apiKey = "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";
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
