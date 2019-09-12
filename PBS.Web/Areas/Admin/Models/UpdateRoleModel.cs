using PBS.Business.Core.BusinessModels;
using PBS.Business.Core.Models;
using System.Collections.Generic;

namespace PBS.Web.Areas.Admin.Models
{
    public class UpdateRoleModel
    {
        public ChangeUserRoleModel ApiModel { get; set; } = new ChangeUserRoleModel ();

        public List<RoleViewModel> Roles { get; set; } = new List<RoleViewModel> ();
    }
}
