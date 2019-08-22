using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using PBS.Business.Utilities.Configuration;

namespace PBS.Business.Utilities.MailClient
{
    public class MailClient : IMailClient
    {
        private readonly IWebConfiguration _configuration;

        public MailClient (IWebConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmail (string email, string title, string subject, string message)
        {
            string server = "smtp.gmail.com";
            int port = 587;

            MimeMessage mimeMessage = new MimeMessage ();

            mimeMessage.From.Add (new MailboxAddress (_configuration.SenderName, _configuration.Email));
            mimeMessage.To.Add (new MailboxAddress (title, email));
            mimeMessage.Subject = subject;
            mimeMessage.Body = new TextPart (TextFormat.Html)
            {
                Text = message
            };

            using (SmtpClient client = new SmtpClient ())
            {
                client.Connect (server, port, false);
                client.Authenticate (_configuration.Email, _configuration.Password);

                client.Send (mimeMessage);
                client.Disconnect (true);
            }
        }
    }
}
