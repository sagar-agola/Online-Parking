using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PBS.Business.Core.BusinessModels;
using PBS.Business.Core.Models;
using PBS.Business.Utilities.Configuration;
using PBS.Web.Helpers;
using PBS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace PBS.Web.Controllers
{
    public class SlotController : Controller
    {
        private readonly IApiHelper _apiHelper;
        private readonly IDataProtector _dataProtector;

        public SlotController (IApiHelper apiHelper,
            IDataProtectionProvider dataProtectionProvider,
            DataProtectionPurposeStrings purposeStrings)
        {
            _apiHelper = apiHelper;
            _dataProtector = dataProtectionProvider.CreateProtector (purposeStrings.MasterpurposeString);
        }

        public IActionResult Manage (string id)
        {
            int newId = Convert.ToInt32 (_dataProtector.Unprotect (id));

            ResponseDetails response = _apiHelper.SendApiRequest ("", "slot/parkinglot/" + newId, HttpMethod.Get);

            if (response.Success)
            {
                List<SlotViewModel> model = JsonConvert.DeserializeObject<List<SlotViewModel>> (response.Data.ToString ());

                model = model.Select (x =>
                 {
                     x.EncryptedId = _dataProtector.Protect (x.Id.ToString ());
                     x.EncryptedParkingLotId = _dataProtector.Protect (x.ParkingLotId.ToString ());
                     x.EncryptedSlotTypeId = _dataProtector.Protect (x.SlotTypeId.ToString ());

                     return x;
                 }).ToList ();

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

        public IActionResult Details(string id)
        {
            int newId = Convert.ToInt32 (_dataProtector.Unprotect (id));

            ResponseDetails response = _apiHelper.SendApiRequest ("", "slot/get/" + newId, HttpMethod.Get);

            if (response.Success)
            {
                SlotViewModel model = JsonConvert.DeserializeObject<SlotViewModel> (response.Data.ToString ());
                model.EncryptedId = _dataProtector.Protect (model.Id.ToString ());
                model.EncryptedParkingLotId = _dataProtector.Protect (model.ParkingLotId.ToString ());
                model.EncryptedSlotTypeId = _dataProtector.Protect (model.SlotTypeId.ToString ());

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
                    return RedirectToAction ("Manage", new { Id = _dataProtector.Protect(model.ParkingLotId.ToString()) });
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

            return RedirectToAction ("Manage", new { Id = _dataProtector.Protect (model.ParkingLotId.ToString ()) });
        }

        [HttpPost]
        public IActionResult Delete(int slotId, int parkingLotId)
        {
            ResponseDetails response = _apiHelper.SendApiRequest ("", "slot/remove/" + slotId, HttpMethod.Delete);

            if (response.Success)
            {
                return RedirectToAction ("Manage", new { Id = _dataProtector.Protect(parkingLotId.ToString()) });
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