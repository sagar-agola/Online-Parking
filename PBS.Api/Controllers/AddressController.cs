using Microsoft.AspNetCore.Mvc;
using PBS.Business.Contracts.Services;
using PBS.Business.Core.ApiRoute;
using PBS.Business.Core.BusinessModels;
using PBS.Business.Core.Models;

namespace PBS.Api.Controllers
{
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        private readonly IUserService _userService;

        public AddressController (IAddressService addressService, IUserService userService)
        {
            _addressService = addressService;
            _userService = userService;
        }

        [HttpPost (ApiRoutes.Address.Add)]
        public object Add (int id, AddressViewModel model)
        {
            model = _addressService.Add (model);
            
            UserViewModel userModel = _userService.Get (id);
            userModel.AddressId = model.Id;
            userModel = _userService.Update (userModel);

            if (userModel == null)
            {
                return new ResponseDetails (false, "Address not saved.");
            }

            return new ResponseDetails (true, model);
        }

        [HttpPost (ApiRoutes.Address.Update)]
        public object Update (AddressViewModel model)
        {
            int id = (int) model.Id;
            model = _addressService.Update (model);

            if (model == null)
            {
                return new ResponseDetails (false, $"Address with Id: { id } is not found.");
            }

            return new ResponseDetails (true, model);
        }
    }
}