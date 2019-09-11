using Microsoft.EntityFrameworkCore;
using PBS.Business.Contracts.Repositories;
using PBS.Database.Context;
using PBS.Database.Models;
using System.Collections.Generic;
using System.Linq;

namespace PBS.Business.DAL.Repositories
{
    public class ParkingLotRepository : IParkingLotRepository
    {
        private readonly PbsDbContext _context;

        public ParkingLotRepository (PbsDbContext context)
        {
            _context = context;
        }

        public ParkingLot Add (ParkingLot model)
        {
            _context.ParkingLots.Add (model);

            return model;
        }

        public ParkingLot Get (int id)
        {
            return _context.ParkingLots
                .AsNoTracking ()
                .Include (lot => lot.Address)
                .Include (lot => lot.Owner)
                    .ThenInclude(owner => owner.Role)
                .Include (lot => lot.Owner)
                    .ThenInclude (owner => owner.Address)
                .Include (lot => lot.ParkingLotImages)
                .Include (lot => lot.Slots)
                    .ThenInclude (Slot => Slot.SlotType)
                .Include (lot => lot.Slots)
                    .ThenInclude (slot => slot.Bookings)
                .First (lot => lot.IsActive && lot.Id == id);
        }

        public List<ParkingLot> GetAll ()
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
                .Where (lot => lot.IsActive)
                .ToList ();
        }

        public List<ParkingLot> GetRequested ()
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
                .Where (lot => lot.IsAproved == false && lot.IsActive)
                .ToList ();
        }

        public List<ParkingLot> GetByUser (int userId)
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
                .Where (lot => lot.IsActive && lot.OwnerId == userId)
                .ToList ();
        }

        public bool ParkingLotExists (int id)
        {
            return _context.ParkingLots.Any (lot => lot.IsActive && lot.Id == id);
        }

        public void Remove (ParkingLot model)
        {
            _context.ParkingLots.Remove (model);
        }

        public void Update (ParkingLot model)
        {
            _context.ParkingLots.Update (model);
        }

        public ParkingLotImage AddImage (ParkingLotImage model)
        {
            _context.ParkingLotImages.Add (model);

            return model;
        }

        public List<ParkingLotImage> GetImages(int parkingLotId)
        {
            return _context.ParkingLotImages.Where (img => img.ParkingLotId == parkingLotId).ToList ();
        }
    }
}
