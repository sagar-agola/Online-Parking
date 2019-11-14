using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PBS.Business.Core.BusinessModels;
using PBS.Business.Core.Models;
using PBS.Web.Helpers;
using System.Net.Http;

namespace PBS.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IApiHelper _apiHelper;
        private readonly ITokenDecoder tokenDecoder;

        public AuthController (IApiHelper apiHelper,
            ITokenDecoder tokenDecoder)
        {
            _apiHelper = apiHelper;
            this.tokenDecoder = tokenDecoder;
        }

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
                model.RoleId = 4;

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

        [HttpPost]
        public IActionResult Logout ()
        {
            HttpContext.Session.Remove ("token");

            return RedirectToAction ("Login", "Auth");
        }

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