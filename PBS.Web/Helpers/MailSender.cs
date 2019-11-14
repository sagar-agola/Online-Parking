using PBS.Business.Utilities.MailClient;
using System;

namespace PBS.Web.Helpers
{
    public class MailSender
    {
        private readonly IMailClient _client;
        private readonly ITokenDecoder _tokenDecoder;
        private readonly DataProtector _dataProtector;

        public MailSender (IMailClient client,
            ITokenDecoder tokenDecoder,
            DataProtector dataProtector)
        {
            _client = client;
            _tokenDecoder = tokenDecoder;
            _dataProtector = dataProtector;
        }

        public string GanerateAndSendOTP (string email)
        {
            Random random = new Random (DateTime.Now.Second);

            int otp = random.Next (1111, 9999);

            string encryptedOtp = _dataProtector.Protect (otp);

            string body = "Dear " + _tokenDecoder.UserName + ", <br /><br />" + "Kindly enter below number as OTP for your account activities.<br />OTP: " + otp.ToString () + "<br /><br />Greetings,<br />Parking Booking System.";

            _client.SendEmail (email, _tokenDecoder.UserName, "OTP for change password", body);

            return encryptedOtp;
        }
    }
}
