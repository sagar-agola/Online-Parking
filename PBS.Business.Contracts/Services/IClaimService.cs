using PBS.Business.Core.BusinessModels;
using PBS.Business.Core.Models;
using System.Collections.Generic;

namespace PBS.Business.Contracts.Services
{
    public interface IClaimService
    {
        List<UserClaimViewModel> GetAll ();
        List<UserClaimViewModel> GetUserClaim (int userId);

        UserClaimViewModel Add (UserClaimViewModel model);
        bool Remove (ClaimRemoveModel model);
        bool RemoveAll (int userId);
    }
}
