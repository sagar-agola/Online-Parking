using PBS.Business.Core.Models;
using PBS.Database.Models;
using System.Collections.Generic;

namespace PBS.Business.Contracts.Repositories
{
    public interface IUserRepository
    {
        List<User> GetAll ();
        User Get (int id);
        bool Update (User model);
        bool ChangePassword (ChangePasswordDbModel model);
    }
}
