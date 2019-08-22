using PBS.Business.Core.BusinessModels;
using System.Collections.Generic;

namespace PBS.Business.Contracts.Services
{
    public interface IUserService
    {
        List<UserViewModel> GetAll ();
        UserViewModel Get (int id);
        UserViewModel Update (UserViewModel model);
    }
}
