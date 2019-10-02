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
using Rotativa.AspNetCore;

namespace PBS.Web.Controllers
{
    public class BookingController : Controller
    {
        private readonly IApiHelper _apiHelper;
        private readonly ITokenDecoder _tokenDecoder;

        public BookingController (IApiHelper apiHelper, ITokenDecoder tokenDecoder)
        {
            this._apiHelper = apiHelper;
            this._tokenDecoder = tokenDecoder;
        }

        [HttpGet]
        public IActionResult ParkingLot (int id)
        {
            ResponseDetails response = _apiHelper.SendApiRequest ("", "parkinglot/get/" + id, HttpMethod.Get);

            if (response.Success)
            {
                ParkingLotViewModel model = JsonConvert.DeserializeObject<ParkingLotViewModel> 
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

        [HttpGet]
        public IActionResult Confirm(int id)
        {
            ConfirmBookingModel model = new ConfirmBookingModel
            {
                SlotId = id,
                UserId = _tokenDecoder.UserId,
                StartDateTime = DateTime.Now
            };

            return View (model);
        }

        [HttpPost]
        public IActionResult Confirm(ConfirmBookingModel model)
        {
            if (ModelState.IsValid)
            {
                ResponseDetails response = _apiHelper.SendApiRequest ("", "slot/make-booked/" + model.SlotId, 
                    HttpMethod.Post);

                if (response.Success)
                {
                    string VehicalNumber = model.StateCode + "-" + model.DistrictCode
                        + "-" + model.SeriesCode + " " + model.Number;
                    DateTime EndDate = model.StartDateTime
                        .AddHours (model.DurationHour)
                        .AddMinutes (model.DurationMinute);

                    BookingViewModel bookingModel = new BookingViewModel ()
                    {
                        CustomerId = model.UserId,
                        SlotId = model.SlotId,
                        StartDateTime = model.StartDateTime,
                        EndDateTime = EndDate,
                        VehicleNumber = VehicalNumber,
                        IsActive = true
                    };

                    response = _apiHelper.SendApiRequest (bookingModel, "booking/add", HttpMethod.Post);

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
    }
}