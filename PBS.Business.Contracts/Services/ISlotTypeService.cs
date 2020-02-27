using PBS.Business.Core.BusinessModels;
using System.Collections.Generic;

namespace PBS.Business.Contracts.Services
{
    public interface ISlotTypeService
    {
        List<SlotTypeViewModel> GetAll ();
        SlotTypeViewModel Get (int id);

        SlotTypeViewModel Add (SlotTypeViewModel model);
        bool Remove (int id);
    }
}
