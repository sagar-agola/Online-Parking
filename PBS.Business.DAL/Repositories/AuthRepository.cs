using Microsoft.EntityFrameworkCore;
using PBS.Business.Contracts.Repositories;
using PBS.Database.Context;
using PBS.Database.Models;
using System.Linq;

namespace PBS.Business.DAL.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly PbsDbContext _context;

        public AuthRepository (PbsDbContext context)
        {
            _context = context;
        }

        public bool EmailExists (string email)
        {
            return _context.Users.Any (user => user.Email == email);
        }

        public User Login (string email)
        {
            return _context.Users
                .Include (user => user.Role)
                .Include (user => user.Address)
                .Include (user => user.Bookings)
                .Include (user => user.ParkingLots)
                .FirstOrDefault (user => user.Email == email);
        }

        public User Register (User model)
        {
            _context.Users.Add (model);

            return model;
        }
    }
}
