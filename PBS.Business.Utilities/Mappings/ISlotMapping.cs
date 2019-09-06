using System.Collections.Generic;
using PBS.Business.Core.BusinessModels;
using PBS.Database.Models;

namespace PBS.Business.Utilities.Mappings
{
    public interface ISlotMapping
    {
        SlotViewModel MapSlot (Slot model);
        List<SlotViewModel> MapSlotList (List<Slot> model);
    }
}