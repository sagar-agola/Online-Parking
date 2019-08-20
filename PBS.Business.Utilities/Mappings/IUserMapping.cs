using PBS.Business.Core.BusinessModels;
using PBS.Database.Models;
using System.Collections.Generic;

namespace PBS.Business.Utilities.Mappings
{
    public interface IUserMapping
    {
        UserViewModel MapUser (User model);
        List<UserViewModel> MapUserList (List<User> model);
    }
}