using Microsoft.EntityFrameworkCore;
using PBS.Business.Contracts.Repositories;
using PBS.Database.Context;
using PBS.Database.Models;
using System.Collections.Generic;
using System.Linq;
using Z.EntityFramework.Plus;

namespace PBS.Business.DAL.Repositories
{
    public class SlotRepository : ISlotRepository
    {
        private readonly PbsDbContext _context;

        public SlotRepository (PbsDbContext context)
        {
            _context = context;
        }

        public Slot Add (Slot model)
        {
            _context.Slots.Add (model);

            return model;
        }

        public Slot Get (int id)
        {
            return RetriveEntity ().FirstOrDefault (slot => slot.Id == id);
        }

        public List<Slot> GetAll ()
        {
            return RetriveEntity ().ToList ();
        }

        public List<Slot> GetByParkingLot (int parkingLotId)
        {
            return RetriveEntity().Where (slot => slot.ParkingLotId == parkingLotId).ToList ();
        }

        public void Remove (Slot model)
        {
            _context.Slots.Remove (model);
        }

        public void Update (Slot model)
        {
            _context.Slots.Update (model);
        }

        public bool SlotExists (int id)
        {
            return _context.Slots.Any (slot => slot.Id == id);
        }

        private IQueryable<Slot> RetriveEntity ()
        {
            return _context.Slots
                .Include (slot => slot.SlotType)
                .Include (slot => slot.ParkingLot)
                .IncludeFilter (slot => slot.Bookings.Where (booking => booking.IsActive));
        }
    }
}
