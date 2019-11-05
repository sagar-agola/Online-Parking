using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PBS.Business.Core.BusinessModels;
using PBS.Business.Core.Models;
using PBS.Web.Helpers;
using PBS.Web.Models;
using System.Collections.Generic;
using System.Net.Http;

namespace PBS.Web.Areas.Admin.Controllers
{
    [Area ("Admin")]
    public class RolesController : Controller
    {
        private readonly IApiHelper _apiHelper;
        private readonly DataProtector _dataProtector;

        public RolesController (IApiHelper apiHelper,
            DataProtector dataProtector)
        {
            _apiHelper = apiHelper;
            _dataProtector = dataProtector;
        }

        public IActionResult Index ()
        {
            ResponseDetails response = _apiHelper.SendApiRequest ("", "role/get-all", HttpMethod.Get);

            List<RoleViewModel> model = JsonConvert.DeserializeObject<List<RoleViewModel>> (response.Data.ToString ());

            _dataProtector.ProtectRoleRouteValues (model);

            return View (model);
        }

        [HttpGet]
        public IActionResult Create ()
        {
            return View ();
        }

        [HttpPost]
        public IActionResult Create (RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                ResponseDetails response = _apiHelper.SendApiRequest (model, "role/add", HttpMethod.Post);

                if (response.Success)
                {
                    return RedirectToAction ("Index");
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
                return View (model);
            }
        }

        [HttpGet]
        [ActionName ("Remove")]
        public IActionResult RemoveGet (string id)
        {
            ViewData["Id"] = id;

            return View ();
        }

        [HttpPost]
        [ActionName ("Remove")]
        public IActionResult RemovePost (string roleId)
        {
            int newId = _dataProtector.Unprotect (roleId);

            ResponseDetails response = _apiHelper.SendApiRequest ("", "role/remove/" + newId, HttpMethod.Delete);

            if (response.Success)
            {
                return RedirectToAction ("Index");
            }
            else
            {
                ModelState.AddModelError ("", response.Data.ToString ());
                ViewData["Id"] = newId;

                return View ();
            }
        }

        public IActionResult Details (string id)
        {
            int newId = _dataProtector.Unprotect (id);

            ResponseDetails response = _apiHelper.SendApiRequest ("", "role/get/" + newId, HttpMethod.Get);

            if (response.Success)
            {
                RoleViewModel model = JsonConvert.DeserializeObject<RoleViewModel> (response.Data.ToString ());

                _dataProtector.ProtectRoleRouteValues (model);

                return View (model);
            }
            else
            {
                ErrorViewModel model = new ErrorViewModel
                {
                    Message = response.Data.ToString ()
                };

                return View ("Error", model);
            }
        }
    }
}