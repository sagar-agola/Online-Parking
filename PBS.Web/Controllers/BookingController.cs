using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PBS.Business.Core.BusinessModels;
using PBS.Business.Core.Models;
using PBS.Business.Utilities.Configuration;
using PBS.Web.Helpers;
using PBS.Web.Models;
using Rotativa.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace PBS.Web.Controllers
{
    public class BookingController : Controller
    {
        private readonly IApiHelper _apiHelper;
        private readonly ITokenDecoder _tokenDecoder;
        private readonly IDataProtector _dataProtector;

        public BookingController (IApiHelper apiHelper,
            ITokenDecoder tokenDecoder,
            IDataProtectionProvider dataProtectionProvider,
            DataProtectionPurposeStrings purposeStrings)
        {
            _apiHelper = apiHelper;
            _tokenDecoder = tokenDecoder;
            _dataProtector = dataProtectionProvider.CreateProtector (purposeStrings.MasterPurposeString);
        }

        [HttpGet]
        public IActionResult ParkingLot (int id)
        {
            ResponseDetails response = _apiHelper.SendApiRequest ("", "parkinglot/get/" + id, HttpMethod.Get);

            if (response.Success)
            {
                ParkingLotViewModel model = JsonConvert.DeserializeObject<ParkingLotViewModel>
                    (response.Data.ToString ());

                model.SlotViewModels = model.SlotViewModels.Select (x =>
                {
                    // populate can book property logic
                    x.CanBook = DetermineWhetherCanSlotBook (x);

                    return x;
                }).ToList ();

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
            int newId = Convert.ToInt32 (_dataProtector.Unprotect (id));

            ResponseDetails response = _apiHelper.SendApiRequest ("", "booking/get/" + newId, HttpMethod.Get);

            if (response.Success)
            {
                BookingViewModel model = JsonConvert.DeserializeObject<BookingViewModel> (response.Data.ToString ());

                model.EncryptedId = _dataProtector.Protect (model.Id.ToString ());
                model.EncryptedSlotId = _dataProtector.Protect (model.SlotId.ToString ());
                model.EncryptedCustomerId = _dataProtector.Protect (model.CustomerId.ToString ());

                return View (model);
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

        [HttpGet]
        public IActionResult Confirm (int id)
        {
            ConfirmBookingModel model = new ConfirmBookingModel
            {
                SlotId = id,
                UserId = _tokenDecoder.UserId
            };

            return View (model);
        }

        [HttpPost]
        public IActionResult Confirm (ConfirmBookingModel model)
        {
            if (ModelState.IsValid)
            {
                string VehicalNumber = model.StateCode + "-" + model.DistrictCode
                    + "-" + model.SeriesCode + " " + model.Number;

                model.StartDate = model.StartDate.AddHours (model.StartHour).AddMinutes (model.StartMinute);

                if ((DateTime.Now.Subtract (model.StartDate).TotalMinutes) > 0)
                {
                    ModelState.AddModelError ("", "Booking time must be after current moment.");

                    return View (model);
                }

                DateTime EndDate = model.StartDate.AddHours (model.DurationHour).AddMinutes (model.DurationMinute);

                BookingViewModel bookingModel = new BookingViewModel ()
                {
                    CustomerId = model.UserId,
                    SlotId = model.SlotId,
                    StartDateTime = model.StartDate,
                    EndDateTime = EndDate,
                    VehicleNumber = VehicalNumber,
                    IsActive = true
                };

                ResponseDetails response = _apiHelper.SendApiRequest (bookingModel, "booking/add", HttpMethod.Post);

                if (response.Success)
                {
                    BookingViewModel returnedModel = JsonConvert.DeserializeObject<BookingViewModel>
                        (response.Data.ToString ());

                    return RedirectToAction ("Receipt", new { id = returnedModel.Id });
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
                ModelState.AddModelError ("", "Validation error");
                return View (model);
            }
        }

        [HttpGet]
        public IActionResult Receipt (int id)
        {
            ResponseDetails response = _apiHelper.SendApiRequest ("", "booking/get/" + id, HttpMethod.Get);

            if (response.Success)
            {
                BookingViewModel model = JsonConvert.DeserializeObject<BookingViewModel>
                    (response.Data.ToString ());

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

        public IActionResult Print (int id)
        {
            ResponseDetails response = _apiHelper.SendApiRequest ("", "booking/get/" + id, HttpMethod.Get);

            if (response.Success)
            {
                BookingViewModel model = JsonConvert.DeserializeObject<BookingViewModel>
                    (response.Data.ToString ());

                return new ViewAsPdf ("_ReceiptPartial", model);
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

        public IActionResult All ()
        {
            ResponseDetails response = _apiHelper.SendApiRequest ("", "booking/get/user/" +
                _tokenDecoder.UserId, HttpMethod.Get);

            List<BookingViewModel> model = JsonConvert.DeserializeObject<List<BookingViewModel>>
                (response.Data.ToString ());

            return View (model);
        }

        #region Private Methods
        private bool DetermineWhetherCanSlotBook (SlotViewModel model)
        {
            if (model.IsBooked)
            {
                BookingViewModel bookingInfo = model.BookingViewModels.FirstOrDefault (x => x.IsActive);

                if (bookingInfo != null)
                {
                    if ((bookingInfo.StartDateTime - DateTime.Now).TotalMinutes <= 60)
                    {
                        return false;
                    }
                }

                return false;
            }

            return true;
        }
        #endregion
    }
}