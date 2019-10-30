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

        public RolesController (IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public IActionResult Index ()
        {
            ResponseDetails response = _apiHelper.SendApiRequest ("", "role/get-all", HttpMethod.Get);

            List<RoleViewModel> model = JsonConvert.DeserializeObject<List<RoleViewModel>> (response.Data.ToString ());

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
        [ActionName("Remove")]
        public IActionResult RemoveGet(int id)
        {
            ViewData["Id"] = id;
            return View ();
        }

        [HttpPost]
        [ActionName("Remove")]
        public IActionResult RemovePost (int roleId)
        {
            ResponseDetails response = _apiHelper.SendApiRequest ("", "role/remove/" + roleId, HttpMethod.Delete);

            if (response.Success)
            {
                return RedirectToAction ("Index");
            }
            else
            {
                ModelState.AddModelError ("", response.Data.ToString ());
                ViewData["Id"] = roleId;

                return View ();
            }
        }

        public IActionResult Details (int id)
        {
            ResponseDetails response = _apiHelper.SendApiRequest ("", "role/get/" + id, HttpMethod.Get);

            if (response.Success)
            {
                RoleViewModel model = JsonConvert.DeserializeObject<RoleViewModel> (response.Data.ToString ());

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