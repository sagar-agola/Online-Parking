using PBS.Business.Core.BusinessModels;
using System.Collections.Generic;

namespace PBS.Business.Contracts.Services
{
    public interface IRoleService
    {
        List<RoleViewModel> GetAll ();
        RoleViewModel Get (int id);

        RoleViewModel Add (RoleViewModel model);
        bool Update (RoleViewModel model);
        bool Remove (int id);
    }
}
