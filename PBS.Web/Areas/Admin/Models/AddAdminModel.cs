using PBS.Business.Core.BusinessModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PBS.Web.Areas.Admin.Models
{
    public class AddAdminModel
    {
        [Required (ErrorMessage = "Email address is required field")]
        [RegularExpression (@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
            ErrorMessage = "Please Enter Correct Email Address")]
        public string Email { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Select Role")]
        public int Role { get; set; }

        public List<RoleViewModel> Roles { get; set; }
    }
}
