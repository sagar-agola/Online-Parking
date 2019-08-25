using Microsoft.AspNetCore.Mvc;
using PBS.Business.Contracts.Services;
using PBS.Business.Core.BusinessModels;
using PBS.Business.Core.Models;

namespace PBS.Api.Controllers
{
    [Route ("api/address")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController (IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpPost ("add")]
        public object Add (AddressViewModel model)
        {
            model = _addressService.Add (model);

            return new ResponseDetails (true, model);
        }

        [HttpPost("update")]
        public object Update(AddressViewModel model)
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