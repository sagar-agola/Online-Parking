using PBS.Business.Core.BusinessModels;

namespace PBS.Business.Contracts.Services
{
    public interface IAuthService
    {
        UserViewModel Register (UserViewModel model);
        UserViewModel Login (string email, string password);
    }
}
