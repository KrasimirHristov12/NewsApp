namespace NewsApp.Services.Emails
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(string replyToMail, string replyToName);
    }
}
