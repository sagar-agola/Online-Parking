using Microsoft.EntityFrameworkCore;
using PBS.Business.Contracts.Repositories;
using PBS.Database.Context;
using PBS.Database.Models;
using System.Collections.Generic;
using System.Linq;

namespace PBS.Business.DAL.Repositories
{
    public class SlotTypeRepository : ISlotTypeRepository
    {
        private readonly PbsDbContext _context;

        public SlotTypeRepository (PbsDbContext context)
        {
            _context = context;
        }

        public SlotType Add (SlotType model)
        {
            _context.SlotTypes.Add (model);

            return model;
        }

        public List<SlotType> GetAll ()
        {
            return _context.SlotTypes
                .Include(s => s.Slots)
                .ToList ();
        }

        public SlotType Get (int id)
        {
            return _context.SlotTypes
                .Include(s => s.Slots)
                .FirstOrDefault (s => s.Id == id);
        }

        public void Remove (SlotType model)
        {
            _context.SlotTypes.Remove (model);
        }

        public bool SlotTypeExists (int id)
        {
            return _context.SlotTypes.Any (s => s.Id == id);
        }
    }
}
