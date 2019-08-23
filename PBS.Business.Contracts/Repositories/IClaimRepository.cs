using PBS.Business.Core.Models;
using PBS.Database.Models;
using System.Collections.Generic;

namespace PBS.Business.Contracts.Repositories
{
    public interface IClaimRepository
    {
        List<UserClaim> GetAll ();
        List<UserClaim> GetUserClaims (int userId);

        UserClaim Add (UserClaim model);
        bool Remove (ClaimRemoveModel model);
        void RemoveAll (int userId);
    }
}
