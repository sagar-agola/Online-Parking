using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PBS.Business.Contracts.Services;
using PBS.Business.Core.ApiRoute;
using PBS.Business.Core.BusinessModels;
using PBS.Business.Core.Models;
using System.Collections.Generic;
using System.IO;

namespace PBS.Api.Controllers
{
    [ApiController]
    public class ParkingLotController : ControllerBase
    {
        private readonly IParkingLotService _parkingLotService;
        private readonly IUserService _userService;
        private readonly IHostingEnvironment _hostingEnvironment;

        private readonly string _path;

        public ParkingLotController (IParkingLotService parkingLotService,
                                     IUserService userService,
                                     IHostingEnvironment hostingEnvironment)
        {
            _parkingLotService = parkingLotService;
            _userService = userService;
            _hostingEnvironment = hostingEnvironment;

            _path = Path.Combine (_hostingEnvironment.WebRootPath, "Images", "ParkingLot");
        }

        [HttpPost (ApiRoutes.ParkingLot.Add)]
        public object Add (ParkingLotViewModel model)
        {
            bool success = _userService.MakeOwner (model.OwnerId);

            if (success)
            {
                model = _parkingLotService.Add (model);

                if (model == null)
                {
                    return new ResponseDetails (false, "Could not add new parking lot request.");
                }

                return new ResponseDetails (true, model);
            }
            else
            {
                return new ResponseDetails (false, $"User with Id : { model.OwnerId } not found.");
            }
        }

        [HttpGet (ApiRoutes.ParkingLot.GetAll)]
        public object GetAll ()
        {
            List<ParkingLotViewModel> model = _parkingLotService.GetAll ();

            return new ResponseDetails (true, model);
        }

        [HttpGet (ApiRoutes.ParkingLot.GetByUser)]
        public object GetByUser (int userId)
        {
            List<ParkingLotViewModel> model = _parkingLotService.GetByUser (userId);

            if (model == null)
            {
                return new ResponseDetails (false, $"User with Id : { userId } not found.");
            }

            return new ResponseDetails (true, model);
        }

        [HttpGet (ApiRoutes.ParkingLot.Get)]
        public object Get (int id)
        {
            ParkingLotViewModel model = _parkingLotService.Get (id);

            if (model == null)
            {
                return new ResponseDetails (false, $"Parking lot with Id : { id } not found.");
            }

            return new ResponseDetails (true, model);
        }

        [HttpPost (ApiRoutes.ParkingLot.Update)]
        public object Update (ParkingLotViewModel model)
        {
            model = _parkingLotService.Update (model);

            if (model == null)
            {
                return new ResponseDetails (false, $"Parking lot with Id : { model.Id } not found.");
            }

            return new ResponseDetails (true, model);
        }

        [HttpDelete (ApiRoutes.ParkingLot.Remove)]
        public object Remove (int id)
        {
            bool success = _parkingLotService.Remove (id);

            if (success)
            {
                return new ResponseDetails (true, $"Parking lot with Id : { id } Removed.");
            }

            return new ResponseDetails (false, $"Parking lot with Id : { id } not found.");
        }

        [HttpPost (ApiRoutes.ParkingLot.UploadImage)]
        public object UploadImage ([FromForm] UploadLotImageModel model)
        {
            ParkingLotImageViewModel imageModel = _parkingLotService.UploadImage (model, _path);

            if (imageModel == null)
            {
                return new ResponseDetails (false, "Could not upload Image");
            }

            List<string> returnModel = _parkingLotService.GetImages (model.ParkingLotId, _path);

            return new ResponseDetails (true, returnModel);
        }

        [HttpGet(ApiRoutes.ParkingLot.GetImages)]
        public object GetImages(int id)
        {
            List<string> model = _parkingLotService.GetImages (id, _path);

            if (model == null)
            {
                return new ResponseDetails (false, $"Parking lot with Id : { id } not found.");
            }

            return new ResponseDetails (true, model);
        }
    }
}