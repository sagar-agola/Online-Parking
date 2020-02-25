using Microsoft.EntityFrameworkCore;
using PBS.Business.Contracts.Repositories;
using PBS.Business.Core.Models;
using PBS.Database.Context;
using PBS.Database.Models;
using System;
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
            List<User> users = _context.Users
                .AsNoTracking ()
                .Include (user => user.ParkingLots)
                .Include (user => user.Role)
                .Include (user => user.Address)
                .Include (user => user.Bookings)
                .ToList ();

            if (users == null)
            {
                return null;
            }

            for (int i = 0; i < users.Count; i++)
            {
                users[i].Bookings = users[i].Bookings.Where (b => b.IsActive).ToList ();
            }

            return users;
        }

        public User Get (int id)
        {
            if (UserExists (id))
            {
                User user = _context.Users
                    .AsNoTracking ()
                    .Include (u => u.Role)
                    .Include (u => u.Address)
                    .Include (u => u.ParkingLots)
                    .Include (u => u.Bookings)
                    .First (u => u.Id == id);

                user.Bookings = user.Bookings.Where (b => b.IsActive).ToList ();

                return user;
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

        public void ChangePassword (ChangePasswordDbModel model)
        {
            User user = _context.Users.First (u => u.Email == model.Email);

            user.PasswordHash = model.PasswordHash;
            user.PasswordSalt = model.PasswordSalt;

            _context.Users.Update (user);
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

        public void Remove (int id)
        {
            User model = _context.Users.First (user => user.Id == id);

            model.IsActive = false;

            _context.Users.Update (model);
        }

        public bool UserExists (int id)
        {
            return _context.Users.Any (user => user.Id == id);
        }
    }
}
