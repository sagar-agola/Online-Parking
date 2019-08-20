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

        public AuthController (IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
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
                ResponseDetails response = _apiHelper.SendApiRequest (model, "auth/register", HttpMethod.Post);

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
    }
}