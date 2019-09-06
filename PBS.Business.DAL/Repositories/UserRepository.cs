using Microsoft.EntityFrameworkCore;
using PBS.Business.Contracts.Repositories;
using PBS.Business.Core.Models;
using PBS.Database.Context;
using PBS.Database.Models;
using System.Collections.Generic;
using System.Linq;
using Z.EntityFramework.Plus;

namespace PBS.Business.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PbsDbContext _context;

        public UserRepository (PbsDbContext context)
        {
            _context = context;
        }

        public List<User> GetAll ()
        {
            return _context.Users
                .AsNoTracking ()
                .Include (user => user.Role)
                .Include (user => user.Address)
                .Include (user => user.ParkingLots)
                .IncludeFilter (user => user.Bookings.Where (booking => booking.IsActive))
                .ToList ();
        }

        public User Get (int id)
        {
            if (UserExists (id))
            {
                return _context.Users
                    .AsNoTracking ()
                    .Include (user => user.Role)
                    .Include (user => user.Address)
                    .Include (user => user.ParkingLots)
                    .IncludeFilter (user => user.Bookings.Where (booking => booking.IsActive))
                    .First (user => user.Id == id);
            }

            return null;
        }

        public bool Update (User model)
        {
            if (UserExists (model.Id))
            {
                User user = _context.Users.AsNoTracking ().First (u => u.Id == model.Id);

                model.PasswordHash = user.PasswordHash;
                model.PasswordSalt = user.PasswordSalt;

                _context.Users.Update (model);

                return true;
            }

            return false;
        }

        public bool ChangePassword (ChangePasswordDbModel model)
        {
            if (UserExists (model.Id))
            {
                User user = _context.Users.First (u => u.Id == model.Id);

                user.PasswordHash = model.PasswordHash;
                user.PasswordSalt = model.PasswordSalt;

                _context.Users.Update (user);

                return true;
            }

            return false;
        }

        public bool MakeOwner(int userId)
        {
            if (UserExists (userId))
            {
                User model = _context.Users.First (user => user.Id == userId);
                int ownerRoleId = _context.Roles.First (role => role.Title == "Owner").Id;

                model.RoleId = ownerRoleId;

                _context.Users.Update (model);

                return true;
            }

            return false;
        }

        public bool UserExists (int id)
        {
            return _context.Users.Any (user => user.Id == id);
        }
    }
}
