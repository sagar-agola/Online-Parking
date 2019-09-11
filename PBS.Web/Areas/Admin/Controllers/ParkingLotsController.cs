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

        public ParkingLotsController (IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public IActionResult Requests ()
        {
            ResponseDetails response = _apiHelper.SendApiRequest ("", "parkinglot/get-requested", HttpMethod.Get);

            List<ParkingLotViewModel> model = new List<ParkingLotViewModel> ();

            if (response.Success)
            {
                model = JsonConvert.DeserializeObject<List<ParkingLotViewModel>> (response.Data.ToString ());
            }

            return View (model);
        }

        public IActionResult Details (int id)
        {
            ResponseDetails response = _apiHelper.SendApiRequest ("", "parkinglot/get/" + id, HttpMethod.Get);

            if (response.Success)
            {
                ParkingLotRequestdetailsModel model = new ParkingLotRequestdetailsModel ()
                {
                    ParkingLot = JsonConvert.DeserializeObject<ParkingLotViewModel> (response.Data.ToString ())
                };

                if (model.ParkingLot.ParkingLotImageViewModels.Any ())
                {
                    response = _apiHelper.SendApiRequest ("", "parkinglot/all-images/" + id, HttpMethod.Get);

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
        public IActionResult Aprove(int id)
        {
            ResponseDetails response = _apiHelper.SendApiRequest ("", "parkinglot/aprove/" + id, HttpMethod.Post);

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