namespace PBS.Business.Utilities.MailClient
{
    public interface IMailClient
    {
        void SendEmail (string email, string title, string subject, string message);
    }
}