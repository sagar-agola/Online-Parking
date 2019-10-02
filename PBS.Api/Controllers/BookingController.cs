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

        public BookingController (IBookingService bookingService)
        {
            _bookingService = bookingService;
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

        [HttpGet(ApiRoutes.Booking.GetAll)]
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
    }
}