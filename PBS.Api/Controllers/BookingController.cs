using Microsoft.AspNetCore.Mvc;
using PBS.Business.Contracts.Services;
using PBS.Business.Core.ApiRoute;
using PBS.Business.Core.BusinessModels;
using PBS.Business.Core.Models;
using System.Collections.Generic;

namespace PBS.Api.Controllers
{
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly ISlotService _slotService;

        public BookingController (IBookingService bookingService, ISlotService slotService)
        {
            _bookingService = bookingService;
            _slotService = slotService;
        }

        [HttpPost (ApiRoutes.Booking.Add)]
        public object Add (BookingViewModel model)
        {
            model = _bookingService.Add (model);

            if (model == null)
            {
                return new ResponseDetails (false, "Could not add entry in bookings.");
            }

            return new ResponseDetails (true, model);
        }

        [HttpGet (ApiRoutes.Booking.Get)]
        public object Get (int id)
        {
            BookingViewModel model = _bookingService.Get (id);

            if (model == null)
            {
                return new ResponseDetails (false, $"Booking details with Id : { id } Not found.");
            }

            return new ResponseDetails (true, model);
        }

        [HttpGet (ApiRoutes.Booking.GetAll)]
        public object GetAll ()
        {
            List<BookingViewModel> model = _bookingService.GetAll ();

            return new ResponseDetails (true, model);
        }

        [HttpGet (ApiRoutes.Booking.GetByUser)]
        public object GetByUser (int id)
        {
            List<BookingViewModel> model = _bookingService.GetByUser (id);

            return new ResponseDetails (true, model);
        }

        [HttpPost (ApiRoutes.Booking.ConfirmBooking)]
        public object ConfirmBooking (int id)
        {
            BookingViewModel model = _bookingService.Get (id);

            if (model == null)
            {
                return new ResponseDetails (false, $"Booking with Id : { id.ToString () } does not exists.");
            }

            model.SlotViewModel.IsBooked = true;
            _slotService.Update (model.SlotViewModel);

            model.IsConfirmed = true;
            _bookingService.Update (model);

            return new ResponseDetails (true, "Booking is now confirmed.");
        }

    }
}