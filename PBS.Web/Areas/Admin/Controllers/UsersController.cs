using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PBS.Business.Core.BusinessModels;
using PBS.Business.Core.Models;
using PBS.Web.Areas.Admin.Models;
using PBS.Web.Helpers;
using PBS.Web.Models;

namespace PBS.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly IApiHelper _apiHelper;

        public UsersController (IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public IActionResult Index()
        {
            ResponseDetails response = _apiHelper.SendApiRequest ("", "user/get-all", HttpMethod.Get);

            if (response.Success)
            {
                List<UserViewModel> model = JsonConvert.DeserializeObject<List<UserViewModel>> (response.Data.ToString ());

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

        public IActionResult Details (int id)
        {
            ResponseDetails response = _apiHelper.SendApiRequest ("", "user/get/" + id, HttpMethod.Get);

            if (response.Success)
            {
                UserViewModel model = JsonConvert.DeserializeObject<UserViewModel> (response.Data.ToString ());

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

        [HttpGet]
        [ActionName("Delete")]
        public IActionResult DeleteGet(int id)
        {
            ViewData["UserId"] = id;
            return View ();
        }

        [HttpPost]
        [ActionName ("Delete")]
        public IActionResult DeletePost(int userId)
        {
            ResponseDetails response = _apiHelper.SendApiRequest ("", "user/remove/" + userId, HttpMethod.Delete);

            if (response.Success)
            {
                return RedirectToAction ("Index");
            }
            else
            {
                ModelState.AddModelError ("", response.Data.ToString ());
                ViewData["UserId"] = userId;
                return View (userId);
            }
        }

        [HttpGet]
        public IActionResult UpdateRole(int userId, int roleId)
        {
            ResponseDetails response = _apiHelper.SendApiRequest ("", "role/get-all", HttpMethod.Get);

            List<RoleViewModel> roles = JsonConvert.DeserializeObject<List<RoleViewModel>> (response.Data.ToString ());

            UpdateRoleModel model = new UpdateRoleModel ();

            model.ApiModel.UserId = userId;
            model.ApiModel.RoleId = roleId;
            model.Roles = roles;

            return View (model);
        }

        [HttpPost]
        public IActionResult UpdateRole(UpdateRoleModel model)
        {
            ResponseDetails response = _apiHelper.SendApiRequest (model.ApiModel, "user/change-role", HttpMethod.Post);

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
    }
}