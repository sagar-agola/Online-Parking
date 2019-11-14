using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PBS.Business.Core.BusinessModels;
using PBS.Business.Core.Models;
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
        private readonly DataProtector _dataProtector;
        private readonly MailSender _mailSender;

        public UserController (IApiHelper apiHelper,
            ITokenDecoder tokenDecoder,
            DataProtector dataProtector,
            MailSender mailSender)
        {
            _apiHelper = apiHelper;
            _tokenDecoder = tokenDecoder;
            _dataProtector = dataProtector;
            _mailSender = mailSender;
        }

        #region Change Password Actions
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
                string encryptedOtp = _mailSender.GanerateAndSendOTP (model.Email);

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
                int plainOTP = _dataProtector.Unprotect (OTP);

                HttpContext.Session.Remove ("OTP");

                if (plainOTP != Convert.ToInt32 (model.OTP))
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
                    ErrorViewModel errorModel = new ErrorViewModel
                    {
                        Message = response.Data.ToString ()
                    };

                    return View ("Error", errorModel);
                }
            }
            else
            {
                ModelState.AddModelError ("", "Validation error.");
            }

            return View (model);
        }
        #endregion

        #region View Profile
        public IActionResult Profile ()
        {
            object model = GetUserDetails ();

            if (model.GetType () == typeof (UserViewModel))
            {
                return View (model as UserViewModel);
            }
            else
            {
                return View ("Error", model as ErrorViewModel);
            }
        }
        #endregion

        #region Update Profile
        [HttpGet]
        public IActionResult Update ()
        {
            object model = GetUserDetails ();

            if (model.GetType () == typeof (UserViewModel))
            {
                return View (model as UserViewModel);
            }
            else
            {
                return View ("Error", model as ErrorViewModel);
            }
        }

        [HttpPost]
        public IActionResult Update (UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                ResponseDetails response = _apiHelper.SendApiRequest (model, "user/update", HttpMethod.Post);

                if (response.Success)
                {
                    return RedirectToAction ("Profile");
                }
                else
                {
                    ErrorViewModel error = new ErrorViewModel
                    {
                        Message = response.Data.ToString ()
                    };

                    return View ("Error", error);
                }
            }
            else
            {
                ModelState.AddModelError ("", "Validation error.");
                return View (model);
            }
        }
        #endregion

        #region Update Address
        [HttpGet]
        public IActionResult UpdateAddress ()
        {
            object model = GetUserDetails ();

            if (model.GetType () == typeof (UserViewModel))
            {
                UserViewModel user = (UserViewModel) model;

                return View (user.AddressViewModel);
            }
            else
            {
                return View ("Error", model as ErrorViewModel);
            }
        }

        [HttpPost]
        public IActionResult UpdateAddress (AddressViewModel model)
        {
            if (ModelState.IsValid)
            {
                ResponseDetails response = _apiHelper.SendApiRequest (model, "address/update", HttpMethod.Post);

                if (response.Success)
                {
                    return RedirectToAction ("UpdateAddress");
                }
                else
                {
                    ErrorViewModel errorModel = new ErrorViewModel
                    {
                        Message = response.Data.ToString ()
                    };

                    return View ("Error", errorModel);
                }
            }
            else
            {
                ModelState.AddModelError ("", "Validation error.");
                return View (model);
            }
        }
        #endregion

        #region Private Methods
        private object GetUserDetails ()
        {
            ResponseDetails response = _apiHelper.SendApiRequest ("", "user/get/" + _tokenDecoder.UserId, HttpMethod.Get);

            if (response.Success)
            {
                return JsonConvert.DeserializeObject<UserViewModel> (response.Data.ToString ());
            }
            else
            {
                return new ErrorViewModel
                {
                    Message = response.Data.ToString ()
                };
            }
        }

        
        #endregion
    }
}