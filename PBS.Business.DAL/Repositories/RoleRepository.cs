using Microsoft.EntityFrameworkCore;
using PBS.Business.Contracts.Repositories;
using PBS.Database.Context;
using PBS.Database.Models;
using System.Collections.Generic;
using System.Linq;

namespace PBS.Business.DAL.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly PbsDbContext _context;

        public RoleRepository (PbsDbContext context)
        {
            _context = context;
        }

        public Role Add (Role model)
        {
            _context.Roles.Add (model);

            return model;
        }

        public Role Get (int id)
        {
            Role model = _context.Roles
                .Include (role => role.Users)
                .First (role => role.Id == id);

            model.Users = model.Users.Where (u => u.IsActive).ToList ();

            return model;
        }

        public List<Role> GetAll ()
        {
            List<Role> model = _context.Roles
                .Include (role => role.Users)
                .ToList ();

            for(int i = 0; i < model.Count; i++)
            {
                model[i].Users = model[i].Users.Where (u => u.IsActive).ToList ();
            }

            return model;
        }

        public void Remove (Role model)
        {
            _context.Roles.Remove (model);
        }

        public void Update (Role model)
        {
            _context.Roles.Update (model);
        }

        public bool RoleExists (int id)
        {
            return _context.Roles.Any (role => role.Id == id);
        }
    }
}
