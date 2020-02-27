using PBS.Database.Models;
using System.Collections.Generic;

namespace PBS.Business.Contracts.Repositories
{
    public interface ISlotTypeRepository
    {
        List<SlotType> GetAll ();
        SlotType Get (int id);

        SlotType Add (SlotType model);
        void Remove (SlotType model);
        bool SlotTypeExists (int id);
    }
}
