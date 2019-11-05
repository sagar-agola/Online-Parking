using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PBS.Business.Core.BusinessModels;
using PBS.Business.Core.Models;
using PBS.Business.Utilities.Configuration;
using PBS.Business.Utilities.Helpers;
using PBS.Web.Helpers;
using PBS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace PBS.Web.Controllers
{
    public class ParkingLotController : Controller
    {
        private readonly IApiHelper _apiHelper;
        private readonly ITokenDecoder _tokenDecoder;
        private readonly IDataProtector _dataProtector;

        public ParkingLotController (IApiHelper apiHelper,
            ITokenDecoder tokenDecoder,
            IConfiguration configuration,
            IDataProtectionProvider dataProtectionProvider,
            DataProtectionPurposeStrings purposeStrings)
        {
            _apiHelper = apiHelper;
            _tokenDecoder = tokenDecoder;
            _dataProtector = dataProtectionProvider.CreateProtector (purposeStrings.MasterPurposeString);
        }

        #region Dashboard
        public IActionResult Dashboard ()
        {
            ResponseDetails response = _apiHelper.SendApiRequest ("", "parkinglot/get/user/" + _tokenDecoder.UserId, HttpMethod.Get);

            if (response.Success)
            {
                List<ParkingLotViewModel> model = JsonConvert.DeserializeObject<List<ParkingLotViewModel>> (response.Data.ToString ());

                model = model.Select (x =>
                 {
                     x.EncryptedId = _dataProtector.Protect (x.Id.ToString ());
                     x.EncryptedOwnerId = _dataProtector.Protect (x.OwnerId.ToString ());
                     x.EncryptedAddressId = _dataProtector.Protect (x.AddressId.ToString ());

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

        #region Add new parking lot
        [HttpGet]
        public IActionResult Add ()
        {
            return View ();
        }

        [HttpPost]
        public IActionResult Add (AddParkingLotModel model)
        {
            if (ModelState.IsValid)
            {
                ParkingLotViewModel lot = new ParkingLotViewModel ()
                {
                    Name = model.Name,
                    IsActive = true,
                    IsAproved = false,
                    OwnerId = _tokenDecoder.UserId,
                    AddressViewModel = model.AddressViewModel
                };

                for (int i = 0; i < model.NoOf2WheelSlot; i++)
                {
                    lot.SlotViewModels.Add (new SlotViewModel ()
                    {
                        IsBooked = false,
                        SlotTypeId = 1
                    });
                }

                for (int i = 0; i < model.NoOf4WheelSlot; i++)
                {
                    lot.SlotViewModels.Add (new SlotViewModel ()
                    {
                        IsBooked = false,
                        SlotTypeId = 2
                    });
                }

                ResponseDetails response = _apiHelper.SendApiRequest (lot, "parkinglot/add", HttpMethod.Post);

                if (response.Success)
                {
                    return RedirectToAction ("Dashboard");
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
                ModelState.AddModelError ("Error", "Validation Error");
                return View (model);
            }
        }
        #endregion

        #region Details
        public IActionResult Details (string id)
        {
            int newId = Convert.ToInt32 (_dataProtector.Unprotect (id));

            ResponseDetails response = _apiHelper.SendApiRequest ("", "parkinglot/get/" + newId, HttpMethod.Get);

            if (response.Success)
            {
                ParkingLotViewModel model = JsonConvert.DeserializeObject<ParkingLotViewModel> (response.Data.ToString ());
                model.EncryptedId = _dataProtector.Protect (model.Id.ToString ());
                model.EncryptedOwnerId = _dataProtector.Protect (model.OwnerId.ToString ());
                model.EncryptedAddressId = _dataProtector.Protect (model.AddressId.ToString ());

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

        #region Update

        [HttpPost]
        public IActionResult Update (ParkingLotViewModel model)
        {
            //model.Id = Convert.ToInt32 (_dataProtector.Unprotect (model.EncryptedId));

            //ModelState.Clear ();
            //TryValidateModel (model);

            if (ModelState.IsValid)
            {
                ResponseDetails response = _apiHelper.SendApiRequest (model, "parkinglot/update", HttpMethod.Post);

                if (response.Success)
                {
                    return RedirectToAction ("Details", new { id = _dataProtector.Protect (model.Id.ToString ()) });
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
                ModelState.AddModelError ("Error", "Validation Error");
                return View (model);
            }
        }
        #endregion

        #region Parking lot Images
        public IActionResult Images (string id)
        {
            int newId = Convert.ToInt32 (_dataProtector.Unprotect (id));

            ResponseDetails response = _apiHelper.SendApiRequest ("", "parkingLot/all-images/" + newId, HttpMethod.Get);

            if (response.Success)
            {
                List<string> model = JsonConvert.DeserializeObject<List<string>> (response.Data.ToString ());

                ParkingImageModel ImageModel = new ParkingImageModel ()
                {
                    ParkingLotId = newId,
                    Images = model
                };

                return View (ImageModel);
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
        public IActionResult UploadImage (ParkingImageModel model)
        {
            if (ModelState.IsValid)
            {
                string base64EncodedImage = ImageProcessing.ProcessIFormFile (model.Image);

                UploadLotImageModel dataModel = new UploadLotImageModel
                {
                    Image = base64EncodedImage,
                    ImageName = model.Image.FileName,
                    ParkingLotId = model.ParkingLotId
                };

                ResponseDetails response = _apiHelper.SendApiRequest (dataModel, "parkingLot/upload-image", HttpMethod.Post);

                if (response.Success)
                {
                    return RedirectToAction ("Images", new { id = _dataProtector.Protect (model.ParkingLotId.ToString ()) });
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

            ModelState.AddModelError ("", "Validation Error.");
            return View (model);
        }
        #endregion

        #region Parking lot list
        [HttpGet]
        public IActionResult List ()
        {
            ErrorViewModel model = new ErrorViewModel
            {
                Message = "Invalid attempt through url."
            };

            return View ("Error", model);
        }

        [HttpPost]
        public IActionResult List (string query)
        {
            ResponseDetails response = _apiHelper.SendApiRequest ("", "home/search/" + query, HttpMethod.Get);

            List<ParkingLotViewModel> model = JsonConvert.DeserializeObject<List<ParkingLotViewModel>> (response.Data.ToString ());

            return View (model);
        }
        #endregion
    }
}