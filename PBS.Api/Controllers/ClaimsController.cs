using Microsoft.AspNetCore.Mvc;
using PBS.Business.Contracts.Services;
using PBS.Business.Core.BusinessModels;
using PBS.Business.Core.Models;
using System.Collections.Generic;

namespace PBS.Api.Controllers
{
    [Route ("api/claims")]
    [ApiController]
    public class ClaimsController : ControllerBase
    {
        private readonly IClaimService _claimService;

        public ClaimsController (IClaimService claimService)
        {
            _claimService = claimService;
        }

        [HttpPost ("add")]
        public object Add (UserClaimViewModel model)
        {
            model = _claimService.Add (model);

            return new ResponseDetails (true, "Claim added successfully.");
        }

        [HttpGet ("get-all")]
        public object GetAll ()
        {
            List<UserClaimViewModel> model = _claimService.GetAll ();

            if (model != null)
            {
                if (model.Count > 0)
                {
                    return new ResponseDetails (true, model);
                }
            }

            return new ResponseDetails (false, "None at the moment.");
        }

        [HttpGet ("get/{id}")]
        public object Get (int id)
        {
            List<UserClaimViewModel> model = _claimService.GetUserClaim (id);

            if (model == null)
            {
                return new ResponseDetails (false, $"User with Id: { id } is not found.");
            }

            if (model.Count == 0)
            {
                return new ResponseDetails (false, "None at the moment.");
            }

            return new ResponseDetails (true, model);
        }

        [HttpDelete ("remove")]
        public object Remove (ClaimRemoveModel model)
        {
            bool success = _claimService.Remove (model);

            if (success)
            {
                return new ResponseDetails (true, $"{ model.ClaimTitle } Claim removed from user.");
            }

            return new ResponseDetails (false, $"User with Id: { model.UserId } is not found or invalid Claim title.");
        }

        [HttpDelete ("remove-all/{id}")]
        public object RemoveAll (int id)
        {
            bool success = _claimService.RemoveAll (id);

            if (success)
            {
                return new ResponseDetails (true, "All claims are removed from user.");
            }

            return new ResponseDetails (false, $"User with Id: { id } is not found.");
        }
    }
}
