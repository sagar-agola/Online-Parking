﻿using Microsoft.AspNetCore.Mvc;
using PBS.Business.Contracts.Services;
using PBS.Business.Core.ApiRoute;
using PBS.Business.Core.BusinessModels;
using PBS.Business.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace PBS.Api.Controllers
{
    [ApiController]
    public class SlotController : ControllerBase
    {
        private readonly ISlotService _slotService;
        private readonly IBookingService _bookingService;

        public SlotController (ISlotService slotService, IBookingService bookingService)
        {
            _slotService = slotService;
            _bookingService = bookingService;
        }

        [HttpPost (ApiRoutes.Slot.Add)]
        public object Add (SlotViewModel model)
        {
            model = _slotService.Add (model);

            if (model == null)
            {
                return new ResponseDetails (false, "Could not add slot.");
            }

            return new ResponseDetails (true, model);
        }

        [HttpGet (ApiRoutes.Slot.GetAll)]
        public object GetAll ()
        {
            List<SlotViewModel> model = _slotService.GetAll ();

            if (model.Any ())
            {
                return new ResponseDetails (true, model);
            }

            return new ResponseDetails (false, "None at the moment.");
        }

        [HttpGet (ApiRoutes.Slot.Get)]
        public object Get (int id)
        {
            SlotViewModel model = _slotService.Get (id);

            if (model == null)
            {
                return new ResponseDetails (false, $"Parking Slot with Id : { id } does not exists.");
            }

            return new ResponseDetails (true, model);
        }

        [HttpGet (ApiRoutes.Slot.GetByParkingLot)]
        public object GetByParkingLot (int id)
        {
            List<SlotViewModel> model = _slotService.GetByParkingLot (id);

            if (model == null)
            {
                return new ResponseDetails (false, $"Parking loy with Id : { id } does not exists.");
            }

            return new ResponseDetails (true, model);
        }

        [HttpPost (ApiRoutes.Slot.Update)]
        public object Update (SlotViewModel model)
        {
            bool success = _slotService.Update (model);

            if (success)
            {
                return new ResponseDetails (true, "parking slot updated successfully.");
            }

            return new ResponseDetails (false, $"Parking Slot with Id : { model.Id } does not exists.");
        }

        [HttpPost (ApiRoutes.Slot.MakeBooked)]
        public object MakeBooked (int id)
        {
            SlotViewModel model = _slotService.Get (id);

            if (model == null)
            {
                return new ResponseDetails (false, $"Parking Slot with Id : { id.ToString () } does not exists.");
            }

            model.IsBooked = true;
            _slotService.Update (model);

            return new ResponseDetails (true, "parking slot is now marked as booked.");
        }

        [HttpPost (ApiRoutes.Slot.RemoveBooked)]
        public object RemoveBooked (int id)
        {
            SlotViewModel model = _slotService.Get (id);

            if (model == null)
            {
                return new ResponseDetails (false, $"Parking Slot with Id : { model.Id } does not exists.");
            }

            model.IsBooked = false;
            _slotService.Update (model);

            return new ResponseDetails (true, "parking slot is now marked as not booked.");
        }

        [HttpPost (ApiRoutes.Slot.MakeAvailable)]
        public object MakeAvailable (int id)
        {
            bool success = _slotService.MakeAvailable (id);

            if (success)
            {
                return new ResponseDetails (true, "Slot is now free.");
            }

            return new ResponseDetails (false, $"Parking Slot with Id : { id } does not exists.");
        }

        [HttpDelete (ApiRoutes.Slot.Remove)]
        public object Remove (int id)
        {
            bool success = _slotService.Remove (id);

            if (success)
            {
                return new ResponseDetails (true, "Parking slot removed successfully.");
            }

            return new ResponseDetails (false, $"Parking Slot with Id : { id } does not exists.");
        }
    }
}