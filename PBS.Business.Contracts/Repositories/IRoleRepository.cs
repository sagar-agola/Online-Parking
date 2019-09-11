using PBS.Database.Models;
using System.Collections.Generic;

namespace PBS.Business.Contracts.Repositories
{
    public interface IRoleRepository
    {
        List<Role> GetAll ();
        Role Get (int id);

        Role Add (Role model);
        void Update (Role model);
        void Remove (Role model);
        bool RoleExists (int id);
    }
}
