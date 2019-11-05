using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PBS.Business.Core.BusinessModels;
using PBS.Business.Core.Models;
using PBS.Web.Areas.Admin.Models;
using PBS.Web.Helpers;
using PBS.Web.Models;
using System.Collections.Generic;
using System.Net.Http;

namespace PBS.Web.Areas.Admin.Controllers
{
    [Area ("Admin")]
    public class UsersController : Controller
    {
        private readonly IApiHelper _apiHelper;
        private readonly DataProtector _dataProtector;

        public UsersController (IApiHelper apiHelper,
            DataProtector dataProtector)
        {
            _apiHelper = apiHelper;
            _dataProtector = dataProtector;
        }

        public IActionResult Index ()
        {
            ResponseDetails response = _apiHelper.SendApiRequest ("", "user/get-all", HttpMethod.Get);

            if (response.Success)
            {
                List<UserViewModel> model = JsonConvert.DeserializeObject<List<UserViewModel>> (response.Data.ToString ());

                _dataProtector.ProtectUserRouteValues (model);

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

        public IActionResult Details (string id)
        {
            int newId = _dataProtector.Unprotect (id);

            ResponseDetails response = _apiHelper.SendApiRequest ("", "user/get/" + newId, HttpMethod.Get);

            if (response.Success)
            {
                UserViewModel model = JsonConvert.DeserializeObject<UserViewModel> (response.Data.ToString ());

                _dataProtector.ProtectUserRouteValues (model);

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
        [ActionName ("Delete")]
        public IActionResult DeleteGet (string id)
        {
            ViewData["UserId"] = id;
            return View ();
        }

        [HttpPost]
        [ActionName ("Delete")]
        public IActionResult DeletePost (string userId)
        {
            int newId = _dataProtector.Unprotect (userId);

            ResponseDetails response = _apiHelper.SendApiRequest ("", "user/remove/" + newId, HttpMethod.Delete);

            if (response.Success)
            {
                return RedirectToAction ("Index");
            }
            else
            {
                ModelState.AddModelError ("", response.Data.ToString ());
                ViewData["UserId"] = newId;

                return View (newId);
            }
        }

        [HttpGet]
        public IActionResult UpdateRole (string userId, string roleId)
        {
            int newUserId = _dataProtector.Unprotect (userId);
            int newRoleId = _dataProtector.Unprotect (roleId);

            ResponseDetails response = _apiHelper.SendApiRequest ("", "role/get-all", HttpMethod.Get);

            List<RoleViewModel> roles = JsonConvert.DeserializeObject<List<RoleViewModel>> (response.Data.ToString ());

            UpdateRoleModel model = new UpdateRoleModel ();

            model.ApiModel.UserId = newUserId;
            model.ApiModel.RoleId = newRoleId;
            model.Roles = roles;

            return View (model);
        }

        [HttpPost]
        public IActionResult UpdateRole (UpdateRoleModel model)
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