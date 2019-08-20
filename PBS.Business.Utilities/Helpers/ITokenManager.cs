using PBS.Business.Core.BusinessModels;

namespace PBS.Business.Utilities.Helpers
{
    public interface ITokenManager
    {
        string GetToken (UserViewModel model);
    }
}