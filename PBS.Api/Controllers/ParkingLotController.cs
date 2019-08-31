using Microsoft.AspNetCore.Mvc;
using PBS.Business.Contracts.Services;
using PBS.Business.Core.ApiRoute;
using PBS.Business.Core.BusinessModels;
using PBS.Business.Core.Models;
using System.Collections.Generic;

namespace PBS.Api.Controllers
{
    [ApiController]
    public class ParkingLotController : ControllerBase
    {
        private readonly IParkingLotService _parkingLotService;

        public ParkingLotController (IParkingLotService parkingLotService)
        {
            _parkingLotService = parkingLotService;
        }

        [HttpPost(ApiRoutes.parkingLot.Add)]
        public object Add(ParkingLotViewModel model)
        {
            model = _parkingLotService.Add (model);

            if (model == null)
            {
                return new ResponseDetails (false, "Could not add new parking lot request.");
            }

            return new ResponseDetails (true, model);
        }

        [HttpGet(ApiRoutes.parkingLot.GetAll)]
        public object GetAll ()
        {
            List<ParkingLotViewModel> model = _parkingLotService.GetAll ();

            return new ResponseDetails (true, model);
        }

        [HttpGet(ApiRoutes.parkingLot.GetByUser)]
        public object GetByUser (int userId)
        {
            List<ParkingLotViewModel> model = _parkingLotService.GetByUser (userId);

            if (model == null)
            {
                return new ResponseDetails (false, $"User with Id : { userId } not found.");
            }

            return new ResponseDetails (true, model);
        }

        [HttpGet(ApiRoutes.parkingLot.Get)]
        public object Get(int id)
        {
            ParkingLotViewModel model = _parkingLotService.Get (id);

            if (model == null)
            {
                return new ResponseDetails (false, $"Parking lot with Id : { id } not found.");
            }

            return new ResponseDetails (true, model);
        }

        [HttpPost(ApiRoutes.parkingLot.Update)]
        public object Update(ParkingLotViewModel model)
        {
            model = _parkingLotService.Update (model);

            if (model == null)
            {
                return new ResponseDetails (false, $"Parking lot with Id : { model.Id } not found.");
            }

            return new ResponseDetails (true, model);
        }

        [HttpDelete(ApiRoutes.parkingLot.Remove)]
        public object Remove(int id)
        {
            bool success = _parkingLotService.Remove (id);

            if (success)
            {
                return new ResponseDetails (true, $"Parking lot with Id : { id } Removed.");
            }

            return new ResponseDetails (false, $"Parking lot with Id : { id } not found.");
        }
    }
}