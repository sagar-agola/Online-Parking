using System.Collections.Generic;
using System.Security.Claims;

namespace PBS.Business.Utilities.Helpers
{
    public interface ITokenManager
    {
        string GetToken (List<Claim> claims);
    }
}