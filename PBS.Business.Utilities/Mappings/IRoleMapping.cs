using System.Collections.Generic;
using PBS.Business.Core.BusinessModels;
using PBS.Database.Models;

namespace PBS.Business.Utilities.Mappings
{
    public interface IRoleMapping
    {
        RoleViewModel MapRole (Role model);
        List<RoleViewModel> MapRoleList (List<Role> model);
    }
}