using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PBS.Business.Core.Models;
using PBS.Business.Utilities.Helpers;
using PBS.Business.Utilities.MailClient;
using PBS.Web.Helpers;
using PBS.Web.Models;
using System;
using System.Net.Http;

namespace PBS.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IApiHelper _apiHelper;
        private readonly ITokenDecoder _tokenDecoder;
        private readonly IEncryptionHelper _encryptionHelper;
        private readonly IMailClient _client;
        private const int KEY = 5;

        public UserController (IApiHelper apiHelper, ITokenDecoder tokenDecoder,
            IEncryptionHelper encryptionHelper, IMailClient client)
        {
            _apiHelper = apiHelper;
            _tokenDecoder = tokenDecoder;
            _encryptionHelper = encryptionHelper;
            _client = client;
        }

        [HttpGet]
        public IActionResult ChangePassword ()
        {
            ChangePasswordViewModel model = new ChangePasswordViewModel
            {
                Id = _tokenDecoder.UserId,
                Email = string.Empty
            };

            return View (model);
        }

        [HttpPost]
        public IActionResult ChangePassword (ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                string encryptedOtp = GanerateAndSendOTP (model);

                HttpContext.Session.SetString ("OTP", encryptedOtp);
                return RedirectToAction ("UpdatePassword");
            }

            ModelState.AddModelError ("", "Validation error.");
            return View (model);
        }

        [HttpGet]
        public IActionResult UpdatePassword ()
        {
            return View ();
        }

        [HttpPost]
        public IActionResult UpdatePassword (UpdatePasswordViewModel model)
        {
            model.Id = _tokenDecoder.UserId;

            TryValidateModel (model);

            if (ModelState.IsValid)
            {
                string OTP = HttpContext.Session.GetString ("OTP");

                OTP = _encryptionHelper.Decrypt (OTP, KEY);

                if (OTP.CompareTo (model.OTP) != 0)
                {
                    ModelState.AddModelError ("", "Invalid OTP.");
                    return View (model);
                }

                ChangePasswordModel changePasswordModel = new ChangePasswordModel
                {
                    Id = model.Id == null ? 0 : int.Parse (model.Id.ToString ()),
                    Password = model.Password
                };

                ResponseDetails response = _apiHelper.SendApiRequest (changePasswordModel, "user/change-password", HttpMethod.Post);

                if (response.Success)
                {
                    return RedirectToAction ("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError ("", response.Data.ToString ());
                }
            }
            else
            {
                ModelState.AddModelError ("", "Validation error.");
            }

            return View (model);
        }

        private string GanerateAndSendOTP (ChangePasswordViewModel model)
        {
            Random random = new Random (DateTime.Now.Second);

            int otp = random.Next (1111, 9999);

            string encryptedOtp = _encryptionHelper.Encrypt (otp.ToString (), KEY);

            string body = "Dear " + _tokenDecoder.UserName + ", <br /><br />" + "Kindly enter below number as OTP for changing" +
                " your account password.<br />OTP: " + otp.ToString () + "<br /><br />Greetings,<br />Parking Booking System.";

            _client.SendEmail (model.Email, _tokenDecoder.UserName, "OTP for change password", body);
            return encryptedOtp;
        }
    }
}