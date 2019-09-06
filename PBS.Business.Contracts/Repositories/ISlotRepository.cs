using PBS.Database.Models;
using System.Collections.Generic;

namespace PBS.Business.Contracts.Repositories
{
    public interface ISlotRepository
    {
        List<Slot> GetAll ();
        List<Slot> GetByParkingLot (int parkingLotId);
        Slot Get (int id);

        Slot Add (Slot model);
        void Update (Slot model);
        void Remove (Slot model);
        bool SlotExists (int id);
    }
}
