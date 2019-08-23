using System.Collections.Generic;
using PBS.Business.Core.BusinessModels;
using PBS.Database.Models;

namespace PBS.Business.Utilities.Mappings
{
    public interface IClaimMapping
    {
        UserClaimViewModel MapUserClaim (UserClaim model);
        List<UserClaimViewModel> MapUserClaimList (List<UserClaim> model);
    }
}