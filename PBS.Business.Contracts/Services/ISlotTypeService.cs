using PBS.Business.Core.BusinessModels;
using System.Collections.Generic;

namespace PBS.Business.Contracts.Services
{
    public interface ISlotTypeService
    {
        List<SlotTypeViewModel> GetAll ();
    }
}
