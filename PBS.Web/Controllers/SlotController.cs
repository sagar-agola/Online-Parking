using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PBS.Business.Core.BusinessModels;
using PBS.Business.Core.Models;
using PBS.Web.Helpers;
using PBS.Web.Models;
using System.Collections.Generic;
using System.Net.Http;

namespace PBS.Web.Controllers
{
    public class SlotController : Controller
    {
        private readonly IApiHelper _apiHelper;

        public SlotController (IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public IActionResult Manage (int id)
        {
            ResponseDetails response = _apiHelper.SendApiRequest ("", "slot/parkinglot/" + id, HttpMethod.Get);

            if (response.Success)
            {
                List<SlotViewModel> model = JsonConvert.DeserializeObject<List<SlotViewModel>> (response.Data.ToString ());

                ResponseDetails slotTypeResponse = _apiHelper.SendApiRequest ("", "slot-type/get-all", HttpMethod.Get);

                ManageSlotModel slotModel = new ManageSlotModel
                {
                    Slots = model,
                    SlotTypes = JsonConvert.DeserializeObject<List<SlotTypeViewModel>> (slotTypeResponse.Data.ToString ())
                };

                return View (slotModel);
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

        public IActionResult Details(int id)
        {
            ResponseDetails response = _apiHelper.SendApiRequest ("", "slot/get/" + id, HttpMethod.Get);

            if (response.Success)
            {
                SlotViewModel model = JsonConvert.DeserializeObject<SlotViewModel> (response.Data.ToString ());

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
        public IActionResult Create(CreateSlotModel model)
        {
            // TODO : learn how to validate model in pop up
            if (ModelState.IsValid)
            {
                SlotViewModel slot = new SlotViewModel ()
                {
                    IsBooked = false,
                    ParkingLotId = model.ParkingLotId,
                    SlotTypeId = model.SlotTypeId
                };

                ResponseDetails response = _apiHelper.SendApiRequest (slot, "slot/add", HttpMethod.Post);

                if (response.Success)
                {
                    return RedirectToAction ("Manage", new { Id = model.ParkingLotId });
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

            return RedirectToAction ("Manage", new { Id = model.ParkingLotId });
        }

        [HttpPost]
        public IActionResult Delete(int slotId, int parkingLotId)
        {
            ResponseDetails response = _apiHelper.SendApiRequest ("", "slot/remove/" + slotId, HttpMethod.Delete);

            if (response.Success)
            {
                return RedirectToAction ("Manage", new { Id = parkingLotId });
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