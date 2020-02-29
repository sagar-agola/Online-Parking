using PBS.Business.Core.BusinessModels;
using PBS.Database.Models;
using System.Collections.Generic;

namespace PBS.Business.Utilities.Mappings
{
    public interface ISlotTypeMapping
    {
        SlotTypeViewModel MapSlotType (SlotType model);
        List<SlotTypeViewModel> MapSlotTypes (List<SlotType> model);
    }
}