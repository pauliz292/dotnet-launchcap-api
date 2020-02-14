namespace Application.Interfaces
{
    public interface IEmailSender
    {
        string SendEmail(string email, string subject, string htmlMessage);
    }
}