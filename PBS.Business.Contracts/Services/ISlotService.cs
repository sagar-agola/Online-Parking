using PBS.Business.Core.BusinessModels;
using System.Collections.Generic;

namespace PBS.Business.Contracts.Services
{
    public interface ISlotService
    {
        List<SlotViewModel> GetAll ();
        List<SlotViewModel> GetByParkingLot (int parkingLotId);
        SlotViewModel Get (int id);

        SlotViewModel Add (SlotViewModel model);
        bool Update (SlotViewModel model);
        bool Remove (int id);
        bool MakeAvailable (int id);
    }
}
