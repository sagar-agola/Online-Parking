using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PBS.Business.Core.BusinessModels;
using PBS.Business.Core.Models;
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
        private readonly DataProtector _dataProtector;

        public BookingController (IApiHelper apiHelper,
            ITokenDecoder tokenDecoder,
            DataProtector dataProtector)
        {
            _apiHelper = apiHelper;
            _tokenDecoder = tokenDecoder;
            _dataProtector = dataProtector;
        }

        #region Parking Lot (search results)
        [HttpGet]
        public IActionResult ParkingLot (string id)
        {
            int newId = _dataProtector.Unprotect (id);

            ResponseDetails response = _apiHelper.SendApiRequest ("", "parkinglot/get/" + newId, HttpMethod.Get);

            if (response.Success)
            {
                ParkingLotViewModel model = JsonConvert.DeserializeObject<ParkingLotViewModel>
                    (response.Data.ToString ());

                _dataProtector.ProtectParkingLotRouteValues (model);

                model.SlotViewModels = model.SlotViewModels.Select (x =>
                {
                    // populate can book property logic
                    x.CanBook = DetermineWhetherCanSlotBook (x);

                    _dataProtector.ProtectSlotRouteValues (x);

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
        #endregion

        #region Details
        public IActionResult Details (string id)
        {
            int newId = _dataProtector.Unprotect (id);

            ResponseDetails response = _apiHelper.SendApiRequest ("", "booking/get/" + newId, HttpMethod.Get);

            if (response.Success)
            {
                BookingViewModel model = JsonConvert.DeserializeObject<BookingViewModel> (response.Data.ToString ());

                _dataProtector.ProtectBookingRouteValues (model);

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
        #endregion

        #region Confirm
        [HttpGet]
        public IActionResult Confirm (string id)
        {
            int newId = _dataProtector.Unprotect (id);

            ResponseDetails response = _apiHelper.SendApiRequest ("", "slot/get/" + newId, HttpMethod.Get);

            ConfirmBookingModel model = new ConfirmBookingModel
            {
                SlotId = newId,
                UserId = _tokenDecoder.UserId,
                HourlyRate = JsonConvert.DeserializeObject<SlotViewModel> (response.Data.ToString ()).HourlyRate
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
                    IsActive = true,
                    IsConfirmed = false,
                    Amount = Convert.ToInt32 ((EndDate - model.StartDate).TotalHours * model.HourlyRate)
                };

                ResponseDetails response = _apiHelper.SendApiRequest (bookingModel, "booking/add", HttpMethod.Post);

                if (response.Success)
                {
                    BookingViewModel returnedModel = JsonConvert.DeserializeObject<BookingViewModel>
                        (response.Data.ToString ());

                    return RedirectToAction ("Payment", new { id = _dataProtector.Protect (returnedModel.Id) });
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
        #endregion

        #region Booking Payment
        [HttpGet]
        public IActionResult Payment (string id)
        {
            ViewData["Id"] = id;
            return View ();
        }

        [HttpPost]
        [ActionName("Payment")]
        public IActionResult PaymentPost (string id)
        {
            int newId = _dataProtector.Unprotect (id);

            ResponseDetails response = _apiHelper.SendApiRequest ("", "booking/confirm-booking/" + newId, HttpMethod.Post);

            if (response.Success)
            {
                return RedirectToAction ("Receipt", new { id });
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

        #region Booking Receipt
        [HttpGet]
        public IActionResult Receipt (string id)
        {
            int newId = _dataProtector.Unprotect (id);

            ResponseDetails response = _apiHelper.SendApiRequest ("", "booking/get/" + newId, HttpMethod.Get);

            if (response.Success)
            {
                BookingViewModel model = JsonConvert.DeserializeObject<BookingViewModel>
                    (response.Data.ToString ());

                _dataProtector.ProtectBookingRouteValues (model);

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

        public IActionResult Print (string id)
        {
            int newId = _dataProtector.Unprotect (id);

            ResponseDetails response = _apiHelper.SendApiRequest ("", "booking/get/" + newId, HttpMethod.Get);

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
        #endregion

        #region User All Bookings
        public IActionResult All ()
        {
            ResponseDetails response = _apiHelper.SendApiRequest ("", "booking/get/user/" +
                _tokenDecoder.UserId, HttpMethod.Get);

            List<BookingViewModel> model = JsonConvert.DeserializeObject<List<BookingViewModel>>
                (response.Data.ToString ());

            _dataProtector.ProtectBookingRouteValues (model);

            return View (model);
        }
        #endregion

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