using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PBS.Business.Core.BusinessModels;
using PBS.Business.Core.Models;
using PBS.Web.Areas.Admin.Models;
using PBS.Web.Helpers;
using PBS.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace PBS.Web.Areas.Admin.Controllers
{
    [Area ("Admin")]
    public class ParkingLotsController : Controller
    {
        private readonly IApiHelper _apiHelper;
        private readonly DataProtector _dataProtector;

        public ParkingLotsController (IApiHelper apiHelper,
            DataProtector dataProtector)
        {
            _apiHelper = apiHelper;
            _dataProtector = dataProtector;
        }

        public IActionResult Index ()
        {
            ResponseDetails response = _apiHelper.SendApiRequest ("", "parkinglot/get-all", HttpMethod.Get);

            List<ParkingLotViewModel> lots = JsonConvert.DeserializeObject<List<ParkingLotViewModel>> (response.Data.ToString ());
            List<ParkingLotViewModel> model = new List<ParkingLotViewModel> ();

            for (int i = 0; i < lots.Count; i++)
            {
                if (lots[i].IsAproved)
                {
                    model.Add (lots[i]);
                }
            }

            _dataProtector.ProtectParkingLotRouteValues (model);

            return View (model);
        }

        public IActionResult Requests ()
        {
            ResponseDetails response = _apiHelper.SendApiRequest ("", "parkinglot/get-requested", HttpMethod.Get);

            List<ParkingLotViewModel> model = new List<ParkingLotViewModel> ();

            if (response.Success)
            {
                model = JsonConvert.DeserializeObject<List<ParkingLotViewModel>> (response.Data.ToString ());
            }

            _dataProtector.ProtectParkingLotRouteValues (model);

            return View (model);
        }

        public IActionResult Details (string id)
        {
            int newId = _dataProtector.Unprotect (id);

            ResponseDetails response = _apiHelper.SendApiRequest ("", "parkinglot/get/" + newId, HttpMethod.Get);

            if (response.Success)
            {
                ParkingLotViewModel parkingLotViewModel = JsonConvert.DeserializeObject<ParkingLotViewModel>
                    (response.Data.ToString ());

                _dataProtector.ProtectParkingLotRouteValues (parkingLotViewModel);

                ParkingLotRequestdetailsModel model = new ParkingLotRequestdetailsModel ()
                {
                    ParkingLot = parkingLotViewModel
                };

                if (model.ParkingLot.ParkingLotImageViewModels.Any ())
                {
                    response = _apiHelper.SendApiRequest ("", "parkinglot/all-images/" + newId, HttpMethod.Get);

                    if (response.Success)
                    {
                        model.Images = JsonConvert.DeserializeObject<List<string>> (response.Data.ToString ());

                        return View (model);
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
                    return View (model);
                }
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

        [HttpPost]
        public IActionResult Aprove (string id)
        {
            int newId = _dataProtector.Unprotect (id);

            ResponseDetails response = _apiHelper.SendApiRequest ("", "parkinglot/aprove/" + newId, HttpMethod.Post);

            if (response.Success)
            {
                return RedirectToAction ("Requests");
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
    }
}