using PBS.Business.Core.BusinessModels;
using PBS.Business.Core.Models;
using System.Collections.Generic;

namespace PBS.Business.Contracts.Services
{
    public interface IUserService
    {
        List<UserViewModel> GetAll ();
        UserViewModel Get (int id);
        UserViewModel Update (UserViewModel model);
        bool ChangePassword (ChangePasswordModel model);
        bool MakeOwner (int userId);
    }
}
