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

        public List<SlotType> GetAll ()
        {
            return _context.SlotTypes.ToList ();
        }
    }
}
