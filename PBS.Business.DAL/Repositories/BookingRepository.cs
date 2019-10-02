using Microsoft.EntityFrameworkCore;
using PBS.Business.Contracts.Repositories;
using PBS.Database.Context;
using PBS.Database.Models;
using System.Collections.Generic;
using System.Linq;

namespace PBS.Business.DAL.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly PbsDbContext _context;

        public BookingRepository (PbsDbContext context)
        {
            _context = context;
        }

        public Booking Add (Booking model)
        {
            _context.Bookings.Add (model);

            return model;
        }

        public Booking Get (int id)
        {
            return RetriveEntities ().First (e => e.Id == id);
        }

        public List<Booking> GetAll ()
        {
            return RetriveEntities ().ToList ();
        }

        public List<Booking> GetByUser(int userId)
        {
            return RetriveEntities ().Where (e => e.CustomerId == userId).ToList ();
        }

        public bool BookingExists (int id)
        {
            return _context.Bookings.Any (Booking => Booking.Id == id);
        }

        private IQueryable<Booking> RetriveEntities ()
        {
            return _context.Bookings
                .AsNoTracking ()
                .Include (b => b.Customer)
                .Include (b => b.Slot)
                    .ThenInclude (s => s.SlotType)
                .Include (b => b.Slot)
                    .ThenInclude (s => s.ParkingLot);
        }
    }
}
