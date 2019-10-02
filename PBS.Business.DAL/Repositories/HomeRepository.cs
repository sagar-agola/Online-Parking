using Microsoft.EntityFrameworkCore;
using PBS.Business.Contracts.Repositories;
using PBS.Database.Context;
using PBS.Database.Models;
using System.Collections.Generic;
using System.Linq;

namespace PBS.Business.DAL.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly PbsDbContext _context;

        public HomeRepository (PbsDbContext context)
        {
            _context = context;
        }

        public List<ParkingLot> Search (string query)
        {
            return _context.ParkingLots
                .AsNoTracking ()
                .Include (lot => lot.Address)
                .Include (lot => lot.Owner)
                .Include (lot => lot.ParkingLotImages)
                .Include (lot => lot.Slots)
                    .ThenInclude (Slot => Slot.SlotType)
                .Include (lot => lot.Slots)
                    .ThenInclude (slot => slot.Bookings)
                .Where (lot => lot.IsActive && lot.IsAproved && (
                    lot.Address.AddressLine1.Contains (query) ||
                    lot.Address.AddressLine2.Contains (query) ||
                    lot.Address.City.Contains (query) ||
                    lot.Address.LandMark.Contains (query) ||
                    lot.Name.Contains (query)
                )).ToList ();
        }
    }
}
