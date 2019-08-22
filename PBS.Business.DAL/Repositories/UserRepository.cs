using Microsoft.EntityFrameworkCore;
using PBS.Business.Contracts.Repositories;
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
            else
            {
                return null;
            }
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

        #region Private methods
        private bool UserExists (int id)
        {
            return _context.Users.Any (user => user.Id == id);
        }
        #endregion
    }
}
