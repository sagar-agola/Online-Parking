using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PBS.Business.Core.BusinessModels;
using PBS.Business.Core.Models;
using PBS.Web.Helpers;
using PBS.Web.Models;
using System.Collections.Generic;
using System.Net.Http;

namespace PBS.Web.Areas.Admin.Controllers
{
    [Area ("Admin")]
    public class BookingsController : Controller
    {
        private readonly IApiHelper _apiHelper;
        private readonly DataProtector _dataProtector;

        public BookingsController (IApiHelper apiHelper,
            DataProtector dataProtector)
        {
            _apiHelper = apiHelper;
            _dataProtector = dataProtector;
        }

        #region Index
        public IActionResult Index (string userId, string parkingLotId)
        {
            ErrorViewModel errorModel = new ErrorViewModel ();

            if (string.IsNullOrEmpty (userId) && string.IsNullOrEmpty (parkingLotId))
            {
                ResponseDetails response = _apiHelper.SendApiRequest ("", "booking/get-all", HttpMethod.Get);

                List<BookingViewModel> model = JsonConvert.DeserializeObject<List<BookingViewModel>> (response.Data.ToString ());

                _dataProtector.ProtectBookingRouteValues (model);

                return View (model);
            }

            if (!string.IsNullOrEmpty (parkingLotId))
            {
                int newParkingLotId = _dataProtector.Unprotect (parkingLotId);

                ResponseDetails response = _apiHelper.SendApiRequest ("", "booking/get/parkingLot/" + newParkingLotId, HttpMethod.Get);

                List<BookingViewModel> model = JsonConvert.DeserializeObject<List<BookingViewModel>> (response.Data.ToString ());

                _dataProtector.ProtectBookingRouteValues (model);

                return View (model);
            }
            else if (!string.IsNullOrEmpty (userId))
            {
                int newUserId = _dataProtector.Unprotect (userId);

                ResponseDetails response = _apiHelper.SendApiRequest ("", "booking/get/user/" + newUserId, HttpMethod.Get);

                List<BookingViewModel> model = JsonConvert.DeserializeObject<List<BookingViewModel>> (response.Data.ToString ());

                _dataProtector.ProtectBookingRouteValues (model);

                return View (model);
            }

            return View ("Error", errorModel);
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
    }
}