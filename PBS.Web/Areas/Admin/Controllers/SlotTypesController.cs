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
    public class SlotTypesController : Controller
    {
        private readonly IApiHelper _apiHelper;
        private readonly DataProtector _dataProtector;

        public SlotTypesController (IApiHelper apiHelper, DataProtector dataProtector)
        {
            _apiHelper = apiHelper;
            _dataProtector = dataProtector;
        }

        public IActionResult Index ()
        {
            ResponseDetails response = _apiHelper.SendApiRequest ("", "slot-type/get-all", HttpMethod.Get);

            List<SlotTypeViewModel> model = JsonConvert.DeserializeObject<List<SlotTypeViewModel>> (response.Data.ToString ());

            _dataProtector.ProtectSlotTypeRouteValues (model);

            return View (model);
        }

        [HttpGet]
        public IActionResult Create ()
        {
            return View ();
        }

        [HttpPost]
        public IActionResult Create (SlotTypeViewModel model)
        {
            if (ModelState.IsValid)
            {
                ResponseDetails response = _apiHelper.SendApiRequest (model, "slot-type/add", HttpMethod.Post);

                if (response.Success)
                {
                    return RedirectToAction ("Index");
                }

                ErrorViewModel errorModel = new ErrorViewModel ()
                {
                    Message = response.Data.ToString ()
                };

                return View ("Error", errorModel);
            }

            ModelState.AddModelError ("", "Validation failed");
            return View (model);
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
        public IActionResult RemovePost (string id)
        {
            int newId = _dataProtector.Unprotect (id);

            ResponseDetails response = _apiHelper.SendApiRequest ("", $"slot-type/remove/{ newId }", HttpMethod.Delete);

            if (response.Success)
            {
                return RedirectToAction ("Index");
            }
            else
            {
                ErrorViewModel ErrorModel = new ErrorViewModel ()
                {
                    Message = response.Data.ToString ()
                };

                return View ("Error", ErrorModel);
            }
        }
    }
}