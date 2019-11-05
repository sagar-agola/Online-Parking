using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PBS.Business.Core.BusinessModels;
using PBS.Business.Core.Models;
using PBS.Web.Helpers;
using PBS.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace PBS.Web.Controllers
{
    public class SlotController : Controller
    {
        private readonly IApiHelper _apiHelper;
        private readonly DataProtector _dataProtector;

        public SlotController (IApiHelper apiHelper,
            DataProtector dataProtector)
        {
            _apiHelper = apiHelper;
            _dataProtector = dataProtector;
        }

        #region Manage
        public IActionResult Manage (string id)
        {
            int newId = _dataProtector.Unprotect (id);

            ResponseDetails response = _apiHelper.SendApiRequest ("", "slot/parkinglot/" + newId, HttpMethod.Get);

            if (response.Success)
            {
                List<SlotViewModel> model = JsonConvert.DeserializeObject<List<SlotViewModel>> (response.Data.ToString ());

                PopulateHelperProperties (model);
                _dataProtector.ProtectSlotRouteValues (model);

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
            int newId = _dataProtector.Unprotect (id);

            ResponseDetails response = _apiHelper.SendApiRequest ("", "slot/get/" + newId, HttpMethod.Get);

            if (response.Success)
            {
                SlotViewModel model = JsonConvert.DeserializeObject<SlotViewModel> (response.Data.ToString ());

                PopulateHelperProperties (model);

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
            int newId = _dataProtector.Unprotect (id);

            ResponseDetails response = _apiHelper.SendApiRequest ("", "slot/make-available/" + newId, HttpMethod.Post);

            if (response.Success)
            {
                return RedirectToAction ("Manage", new { id = parkingLotId }); // parkingLotId is encrypted
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
            int newId = _dataProtector.Unprotect (id);

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
            int newId = _dataProtector.Unprotect (id);

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
                    return RedirectToAction ("Manage", new { Id = _dataProtector.Protect (model.ParkingLotId) });
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

            return RedirectToAction ("Manage", new { Id = _dataProtector.Protect (model.ParkingLotId) });
        }
        #endregion

        #region Delete
        [HttpPost]
        public IActionResult Delete (int slotId, int parkingLotId)
        {
            ResponseDetails response = _apiHelper.SendApiRequest ("", "slot/remove/" + slotId, HttpMethod.Delete);

            if (response.Success)
            {
                return RedirectToAction ("Manage", new { Id = _dataProtector.Protect (parkingLotId) });
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
        private void PopulateHelperProperties (List<SlotViewModel> model)
        {
            model.Select (x =>
            {
                PopulateHelperProperties (x);
                return x;
            }).ToList ();
        }

        private void PopulateHelperProperties (SlotViewModel model)
        {
            _dataProtector.ProtectSlotRouteValues (model);
            _dataProtector.ProtectBookingRouteValues (model.BookingViewModels);

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
        }
        #endregion
    }
}