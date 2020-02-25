using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PBS.Business.Core.BusinessModels;
using PBS.Business.Core.Models;
using PBS.Web.Helpers;
using PBS.Web.Models;
using PBS.Web.Security;
using System;
using System.Net.Http;

namespace PBS.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IApiHelper _apiHelper;
        private readonly ITokenDecoder _tokenDecoder;
        private readonly MailSender _mailSender;
        private readonly DataProtector _dataProtector;

        public AuthController (IApiHelper apiHelper,
            ITokenDecoder tokenDecoder,
            MailSender mailSender,
            DataProtector dataProtector)
        {
            _apiHelper = apiHelper;
            _tokenDecoder = tokenDecoder;
            _mailSender = mailSender;
            _dataProtector = dataProtector;
        }

        #region Login
        [HttpGet]
        public IActionResult Login ()
        {
            return View ();
        }

        [HttpPost]
        public IActionResult Login (LoginModel model)
        {
            if (ModelState.IsValid)
            {
                ResponseDetails response = _apiHelper.SendApiRequest (model, "auth/login", HttpMethod.Post);

                if (response.Success)
                {
                    HttpContext.Session.SetString ("token", response.Data.ToString ());
                    return RedirectToAction ("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError ("", "Email or Password is incurrect.");
                }
            }
            else
            {
                ModelState.AddModelError ("", "Validation error.");
            }

            return View (model);
        }
        #endregion

        #region Register
        [HttpGet]
        public IActionResult Register ()
        {
            return View ();
        }

        [HttpPost]
        public IActionResult Register (UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Remove hardcoded role Id in future
                model.RoleId = 4;

                // Make address optional in future
                model.AddressViewModel = new AddressViewModel ()
                {
                    AddressLine1 = "test",
                    AddressLine2 = "test",
                    City = "test",
                    LandMark = "test",
                    PinCode = "111111",
                    District = "test",
                    SubDistrict = "test",
                    State = "test"
                };

                ResponseDetails response = _apiHelper.SendApiRequest (model, "auth/register", HttpMethod.Post);

                if (response.Success)
                {
                    HttpContext.Session.SetString ("token", response.Data.ToString ());
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
        #endregion

        #region Logout
        [HttpPost]
        public IActionResult Logout ()
        {
            HttpContext.Session.Remove ("token");

            return RedirectToAction ("Login", "Auth");
        }
        #endregion

        #region Confirm Email
        [HttpGet]
        public IActionResult ConfirmEmail ()
        {
            ResponseDetails response = _apiHelper.SendApiRequest ("", "user/get/" + _tokenDecoder.UserId, HttpMethod.Get);

            if (response.Success)
            {
                UserViewModel userModel = JsonConvert.DeserializeObject<UserViewModel> (response.Data.ToString ());

                string encryptedOTP = _mailSender.GanerateAndSendOTP (userModel.Email);

                HttpContext.Session.SetString ("OTP", encryptedOTP);

                ConfirmEmailModel model = new ConfirmEmailModel ()
                {
                    Id = userModel.Id,
                    Email = userModel.Email
                };

                return View (model);
            }
            else
            {
                ErrorViewModel errorModel = new ErrorViewModel ()
                {
                    Message = response.Data.ToString ()
                };

                return View ("Error", errorModel);
            }
        }

        [HttpPost]
        public IActionResult ConfirmEmail (ConfirmEmailModel model)
        {
            ErrorViewModel errorModel = new ErrorViewModel ();

            if (model.Id == _tokenDecoder.UserId)
            {
                string OTP = HttpContext.Session.GetString ("OTP");
                int plainOTP = _dataProtector.Unprotect (OTP);

                HttpContext.Session.Remove ("OTP");

                if (plainOTP == Convert.ToInt32 (model.OTP))
                {
                    ResponseDetails response = _apiHelper.SendApiRequest ("", "user/confirm-email/" + _tokenDecoder.UserId, HttpMethod.Post);

                    if (response.Success)
                    {
                        return RedirectToAction ("EmailConfirmed");
                    }
                    else
                    {
                        errorModel.Message = response.Data.ToString ();
                    }
                }
                else
                {
                    errorModel.Message = "Invalid OTP.";
                }
            }
            else
            {
                errorModel.Message = "It seems like someone is trying to bypass the security";
            }

            return View ("Error", model);
        }

        public IActionResult EmailConfirmed ()
        {
            return View ();
        }

        public IActionResult EmailConfirmationRequired ()
        {
            return View ();
        }
        #endregion

        public new IActionResult Unauthorized ()
        {
            return View ();
        }

        public IActionResult LoginRequired ()
        {
            return View ();
        }
    }
}