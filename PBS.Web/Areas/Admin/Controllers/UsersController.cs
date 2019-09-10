using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PBS.Business.Core.BusinessModels;
using PBS.Business.Core.Models;
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

        [HttpPost]
        public IActionResult Delete(int userId)
        {
            ResponseDetails response = _apiHelper.SendApiRequest ("", "user/remove/" + userId, HttpMethod.Delete);

            if (response.Success)
            {
                return RedirectToAction ("Index");
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