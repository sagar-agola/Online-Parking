using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PBS.Business.Core.AuthorizeNetApiModels.Request;
using PBS.Business.Core.AuthorizeNetApiModels.Response;
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
                    bookingModel = JsonConvert.DeserializeObject<BookingViewModel>
                        (response.Data.ToString ());

                    _dataProtector.ProtectBookingRouteValues (bookingModel);

                    return RedirectToAction ("Payment", new { id = _dataProtector.Protect (bookingModel.Id) });
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
            int newId = _dataProtector.Unprotect (id);

            ResponseDetails response = _apiHelper.SendApiRequest ("", "booking/get/" + newId, HttpMethod.Get);

            if (response.Success)
            {
                BookingViewModel bookingModel = JsonConvert.DeserializeObject<BookingViewModel> (response.Data.ToString ());

                _dataProtector.ProtectBookingRouteValues (bookingModel);

                PaymentModel model = new PaymentModel ()
                {
                    Booking = bookingModel
                };

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

        [HttpPost]
        public IActionResult Payment (PaymentModel model)
        {
            ModelState.Remove ("Booking");

            TryValidateModel (model);

            if (ModelState.IsValid)
            {
                ApiRequestBody reqestBody = BuildRequestBody (model);
                ErrorViewModel errorModel = new ErrorViewModel ();

                ResponseDetails response = _apiHelper.SendPaymentApiRequest (reqestBody);

                if (response.Success)
                {
                    ApiResponseBody responseBody = JsonConvert.DeserializeObject<ApiResponseBody> (response.Data.ToString ());

                    if (responseBody.Messages.ResultCode.ToLower () == "ok")
                    {
                        if (responseBody.TransactionResponse.ResponseCode == "1")
                        {
                            int newId = Convert.ToInt32 (responseBody.RefId);

                            response = _apiHelper.SendApiRequest ("", "booking/confirm-booking/" + newId, HttpMethod.Post);

                            if (response.Success)
                            {
                                return RedirectToAction ("Receipt", new { id = _dataProtector.Protect (newId) });
                            }
                            else
                            {
                                errorModel.Message = response.Data.ToString ();
                            }
                        }
                        else
                        {
                            string error = responseBody.TransactionResponse.Errors.First ().ErrorText;
                            if (string.IsNullOrEmpty (error))
                            {
                                errorModel.Message = "Transaction failed.";
                            }
                            else
                            {
                                errorModel.Message = error;
                            }
                        }
                    }
                    else
                    {
                        errorModel.Message = responseBody.Messages.Message.First ().Text;
                    }
                }
                else
                {
                    errorModel.Message = response.Data.ToString ();
                }

                return View ("Error", errorModel);
            }
            else
            {
                ModelState.AddModelError ("", "Validation Failed.");
                return RedirectToAction ("Payment", new { id = _dataProtector.Protect (model.Booking.Id) });
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

        private ApiRequestBody BuildRequestBody (PaymentModel model)
        {
            return new ApiRequestBody ()
            {
                CreateTransactionRequest = new CreateTransactionRequest ()
                {
                    TransactionRequest = new TransactionRequest ()
                    {
                        Amount = model.Amount + (model.Amount / 10),
                        Payment = new Payment ()
                        {
                            CreditCard = new CreditCard ()
                            {
                                CardNumber = model.CardNumber,
                                CardCode = model.CardCode,
                                ExpirationDate = $"{ model.ExpYear }-{ model.ExpMonth }"
                            }
                        },
                        Tax = new Tax ()
                        {
                            Amount = model.Amount / 10,
                            Name = "GST",
                            Description = "Gift from modiji"
                        },
                        Customer = new Customer ()
                        {
                            Id = _tokenDecoder.UserId.ToString (),
                        },
                        BillTo = model.BillTo
                    },
                    RefId = model.Booking.Id.ToString ()
                }
            };
        }
        #endregion
    }
}