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
            _dataProtector = dataProtectionProvider.CreateProtector (purposeStrings.MasterPurposeString);
        }

        #region Manage
        public IActionResult Manage (string id)
        {
            int newId = Convert.ToInt32 (_dataProtector.Unprotect (id));

            ResponseDetails response = _apiHelper.SendApiRequest ("", "slot/parkinglot/" + newId, HttpMethod.Get);

            if (response.Success)
            {
                List<SlotViewModel> model = JsonConvert.DeserializeObject<List<SlotViewModel>> (response.Data.ToString ());

                model = model.Select (x =>
                {
                    x = PopulateHelperProperties (x);
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
        #endregion

        #region Details
        public IActionResult Details (string id)
        {
            int newId = Convert.ToInt32 (_dataProtector.Unprotect (id));

            ResponseDetails response = _apiHelper.SendApiRequest ("", "slot/get/" + newId, HttpMethod.Get);

            if (response.Success)
            {
                SlotViewModel model = JsonConvert.DeserializeObject<SlotViewModel> (response.Data.ToString ());

                model = PopulateHelperProperties (model);

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
        #endregion

        #region Make Available
        public IActionResult MakeAvailable (string id, string parkingLotId)
        {
            int newId = Convert.ToInt32 (_dataProtector.Unprotect (id));

            ResponseDetails response = _apiHelper.SendApiRequest ("", "slot/make-available/" + newId, HttpMethod.Post);

            if (response.Success)
            {
                return RedirectToAction ("Manage", new { id = parkingLotId });
            }
            else
            {
                ErrorViewModel model = new ErrorViewModel ()
                {
                    Message = response.Data.ToString ()
                };

                return View ("Error", model);
            }
        }
        #endregion

        #region Enable-Disable Booking
        public IActionResult DisableBooking (string id, string parkingLotId)
        {
            int newId = Convert.ToInt32 (_dataProtector.Unprotect (id));

            ResponseDetails response = _apiHelper.SendApiRequest ("", "slot/make-booked/" + newId, HttpMethod.Post);

            if (response.Success)
            {
                return RedirectToAction ("Manage", new { id = parkingLotId });
            }
            else
            {
                ErrorViewModel model = new ErrorViewModel ()
                {
                    Message = response.Data.ToString ()
                };

                return View ("Error", model);
            }
        }

        public IActionResult EnableBooking (string id, string parkingLotId)
        {
            int newId = Convert.ToInt32 (_dataProtector.Unprotect (id));

            ResponseDetails response = _apiHelper.SendApiRequest ("", "slot/remove-booked/" + newId, HttpMethod.Post);

            if (response.Success)
            {
                return RedirectToAction ("Manage", new { id = parkingLotId });
            }
            else
            {
                ErrorViewModel errorModel = new ErrorViewModel ()
                {
                    Message = response.Data.ToString ()
                };

                return View ("Error", errorModel);
            }
        }
        #endregion

        #region Create
        [HttpPost]
        public IActionResult Create (CreateSlotModel model)
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
                    return RedirectToAction ("Manage", new { Id = _dataProtector.Protect (model.ParkingLotId.ToString ()) });
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
        #endregion

        #region Delete
        [HttpPost]
        public IActionResult Delete (int slotId, int parkingLotId)
        {
            ResponseDetails response = _apiHelper.SendApiRequest ("", "slot/remove/" + slotId, HttpMethod.Delete);

            if (response.Success)
            {
                return RedirectToAction ("Manage", new { Id = _dataProtector.Protect (parkingLotId.ToString ()) });
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
        #endregion

        #region Private Methods
        private SlotViewModel PopulateHelperProperties (SlotViewModel model)
        {
            model.EncryptedId = _dataProtector.Protect (model.Id.ToString ());
            model.EncryptedParkingLotId = _dataProtector.Protect (model.ParkingLotId.ToString ());
            model.EncryptedSlotTypeId = _dataProtector.Protect (model.SlotTypeId.ToString ());

            model.BookingViewModels = model.BookingViewModels.Select (x =>
            {
                x.EncryptedId = _dataProtector.Protect (x.Id.ToString ());
                x.EncryptedCustomerId = _dataProtector.Protect (x.CustomerId.ToString ());
                x.EncryptedSlotId = _dataProtector.Protect (x.SlotId.ToString ());

                return x;
            }).ToList ();

            if (model.IsBooked)
            {
                if (model.BookingViewModels.Any ())
                {
                    if (model.BookingViewModels.Any (b => b.IsActive))
                    {
                        model.CanMakeAvailable = true;
                        model.Status = "Booked";
                    }
                    else
                    {
                        model.CanDelete = true;
                        model.CanEnableBooking = true;
                        model.Status = "Booking Disabled";
                    }
                }
                else
                {
                    model.CanDelete = true;
                    model.CanEnableBooking = true;
                    model.Status = "Booking Disabled";
                }
            }
            else
            {
                model.Status = "Available";
                model.CanDelete = true;
                model.CanDisableBooking = true;
            }

            return model;
        }
        #endregion
    }
}